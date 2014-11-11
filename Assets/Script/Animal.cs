using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
    int color;
    SpriteRenderer sprite;
    Move move;
    Animat agent;
	// Use this for initialization
	void Start () 
    {
        sprite = GetComponent<SpriteRenderer>();
        move = GetComponent<Move>();
        RandomColor();
        agent = new Animat(GetComponent<Animator>());
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void RandomColor()
    {
        color = Random.Range(0, Level.Instance.animalColor.Length);
        sprite.color = Level.Instance.animalColor[color];
    }

    /// <summary>
    /// 交换位置
    /// </summary>
    /// <param name="animal"></param>
    public void Swap(Animal animal)
    {
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
    void Explode()
    {
        Destroy(gameObject);
    }
    public bool IsNeighbor(Animal other)
    {
        return move.box.IsNeighbor(other.move.box);
    }
}
