using UnityEngine;
using System.Collections;

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
                    current = hit.transform.GetComponent<Animal>();
                }
                else
                {
                    if (hit.transform.GetComponent<Animal>() == current)
                    {
                        //取消选中
                        current = null;
                    }
                    //else if(相邻)
                    //{ 交换}
                }
            }
            if (current != null)
            {
                if (Input.GetMouseButton(0))
                {
                    if (hit.transform.GetComponent<Animal>() != current)
                    {
                        current.Swap(hit.transform.GetComponent<Animal>());
                        //取消选中
                        current = null;
                    }
                }
            }
        }
	}
}
