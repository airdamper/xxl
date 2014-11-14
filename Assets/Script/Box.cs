/* ======
 * =问题=
 * ======
 * -XA03:是否是空格的计算.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour {
    public bool spawner = false;
    public int index;
    public Box[] upper = new Box[3];
    public Box[] lower = new Box[3];
    public Move content;
    public Vector3 position;

    AnimalChecker _checker;
    public AnimalChecker checker
    {
        get
        {
            if (_checker == null)
            {
                _checker = new AnimalChecker(this);
            }
            return _checker;
        }
    }
    //该块是否是管口
    public bool isNeck
    {
        get
        {
            return Grid.Instance.necks.Contains(index);
        }
    }
	void Start () 
    {
        position = transform.position;
        CalculateUpper(); //因为在计算lower的时候要用的upper，所以要先计算upper后计算lower
        CalculateLower();
        if (spawner)
            Spawn();
	}

	void Update () 
    {
	}

    void CalculateUpper()
    {
        int up = index - Grid.GRID_X_COUNT;
        if (up >= 0)
        {
            upper[0] = Grid.Instance.boxs[up];
            if(up % Grid.GRID_X_COUNT != 0)
                upper[1] = Grid.Instance.boxs[up - 1];
            if(up % Grid.GRID_X_COUNT !=  Grid.GRID_X_COUNT - 1)
                upper[2] = Grid.Instance.boxs[up + 1];
        }
    }
    //计算当前box是哪些头的lower
    void CalculateLower()
    {
        if (upper[0])
            upper[0].lower[0] = this;
        if (upper[1])
            upper[1].lower[2] = this;
        if (upper[2])
            upper[2].lower[1] = this;
    }

    //球移走
    public void Leave() 
    {
        content = null;
        if (spawner)
        {
            Spawn();
        }
    }
    //球进入
    public void Enter(Move move)
    {
        content = move;
        content.box = this;
    }

    //是空的就可以让球进入,条件根据状态改变,之后可能添加不同的状态影响这里的判断  -XA03
    public bool IsEmpty()
    {
        return content == null;
    }

    //新建球体的位置
    public void Spawn()
    {
        CreateObj().transform.position = position;// +Vector3.up;
    }
    Move CreateObj()
    {
        GameObject go = Instantiate(Level.Instance.animalPrefab) as GameObject;//GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Move move = go.GetComponent<Move>();
        //go.renderer.material.color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1), 1);
        move.box = this;
        //move.targetBox = this;
        return move;
    }
    public bool IsNeighbor(Box other)
    {
        return (position - other.position).sqrMagnitude < 2;
    }
}
