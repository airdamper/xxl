using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select : MonoBehaviour 
{
    Animal current;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Animal selectOne = hit.transform.GetComponent<Animal>();
                if (current == null)
                {
                    //选中
                    DoSelect(selectOne);
                }
                else
                {
                    if (selectOne == current)
                    {
                        //取消选中
                        CancelSelect();
                    }
                    else if (!selectOne.IsNeighbor(current))
                    {
                        CancelSelect();
                        DoSelect(selectOne);
                    }
                    //else if(相邻)
                    //{ 交换}
                }
            }
            if (current != null)
            {
                if (Input.GetMouseButton(0))
                {
                    Animal other = hit.transform.GetComponent<Animal>();
                    if (other != current && other.IsNeighbor(current) && current.color != other.color)
                    {
                        current.Swap(other);
                        //取消选中
                        CancelSelect();
                    }
                }
            }
        }
	}
    void CancelSelect()
    {
        current.Play(AnimatEnum.idle);
        current = null;
    }
    void DoSelect(Animal animal)
    {
        current = animal;
        current.Play(AnimatEnum.click);
        //测试AnimalChecker的邻居方法
        //AnimalChecker checker = new AnimalChecker(current);
        //print(checker.GetTestString());
        //print(animal.move.box.checker.Check(animal.color));
    }

    
}
