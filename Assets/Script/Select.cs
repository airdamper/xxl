using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select : MonoBehaviour 
{
    Animal current;
    SpriteRenderer tile;
	// Use this for initialization
	void Start () {
        tile = Level.Instance.tileSelect;
        tile.enabled = false;
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
                    if (other != current && other.IsNeighbor(current))// && current.color != other.color)
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
        Sound.Instance.Click();
        if(current.stuntState == StuntEnum.none || current.stuntState == StuntEnum.bird)
            current.Play(AnimatEnum.idle);
        current = null;
        tile.enabled = false;
    }
    void DoSelect(Animal animal)
    {
        Sound.Instance.Click();
        current = animal;
        if(animal.stuntState == StuntEnum.none || animal.stuntState == StuntEnum.bird)
            current.Play(AnimatEnum.click);
        tile.transform.position = animal.move.box.position;
        tile.enabled = true;
        //测试AnimalChecker的邻居方法
        //AnimalChecker checker = new AnimalChecker(current);
        //print(checker.GetTestString());
        //print(animal.move.box.checker.Check(animal.color));
    }

    
}
