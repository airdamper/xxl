/*
 * 计算加分: -XA111301f
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animal : MonoBehaviour 
{
    public int color;
    SpriteRenderer sprite;
    public Move move;
    Animat agent;

    public Animal other; // 上一次交换的对象
	// Use this for initialization
	void Start () 
    {
        agent = new Animat(GetComponent<Animator>());
        sprite = GetComponent<SpriteRenderer>();
        move = GetComponent<Move>();
        move.animal = this;
        RandomColor();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void RandomColor()
    {
        color = Random.Range(0, Level.Instance.animalColor.Length);
        agent.agent.runtimeAnimatorController = Level.Instance.animalColor[color];
    }

    /// <summary>
    /// 交换位置
    /// </summary>
    /// <param name="animal"></param>
    public void Swap(Animal animal)
    {
        Swap(animal, true);
    }
    public void Swap(Animal animal, bool callback)
    {
        
        
        if (callback)
        {
            other = animal;
            other.other = this; //记录上一次交换的对象
            int temp = color;
            color = other.color;
            other.color = temp;   //交换颜色,模拟交换后的数据.
            if (other.move.box.checker.Check(other.color) || move.box.checker.Check(color))
            {
                move.event_move_complete += CallBack_MoveEnd_Eliminate;
            }
            else
            {
                move.event_move_complete += CallBack_MoveEnd_MoveBack;
            }
            //换回颜色
            other.color = color;
            color = temp;
        }
        move.Swap(animal.move);
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="name"></param>
    public void Play(AnimatEnum name)
    {
        agent.Play(name);
    }
    //void Explode()
    //{
    //    agent.agent.runtimeAnimatorController = Level.Instance.destroy_effect;
    //    Destroy(gameObject,0.6f);
    //}
    public bool IsNeighbor(Animal other)
    {
        return move.box.IsNeighbor(other.move.box);
    }

    /// <summary>
    /// 消除当前
    /// </summary>
    public void EliminateSelf()
    {
        //move.box.Leave();
        agent.agent.runtimeAnimatorController = Level.Instance.destroy_effect;
        Destroy(gameObject, 0.6f);
        //计算加分     -XA111301
    }
    public void EliminateAll()
    {
        if (move.box.checker.Check(color))
        {
            foreach (Animal animal in move.box.checker.GetEliminateList())
            {
                animal.EliminateSelf();
            }
        }
    }
    /// <summary>
    /// 检测当前Animal位置填入对应颜色返回的消除列 
    /// </summary>
    /// <param name="color">当前位置要替换掉的颜色</param>
    public bool Check(int color)
    {
        return false;
    }

    void CallBack_MoveEnd_MoveBack()
    {
        move.event_move_complete -= CallBack_MoveEnd_MoveBack;
        other.move.Stop();
        Swap(other, false);
    }
    void CallBack_MoveEnd_Eliminate()
    {
        EliminateAll();
        other.EliminateAll();
        move.event_move_complete -= CallBack_MoveEnd_Eliminate;
    }
}
