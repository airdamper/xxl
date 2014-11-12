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
                if (current == null)
                {
                    //选中
                    DoSelect(hit.transform.GetComponent<Animal>());
                }
                else
                {
                    if (hit.transform.GetComponent<Animal>() == current)
                    {
                        //取消选中
                        CancelSelect();
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
                    if (other != current && other.IsNeighbor(current))
                    {
                        current.Swap(hit.transform.GetComponent<Animal>());
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
    }

    
}
