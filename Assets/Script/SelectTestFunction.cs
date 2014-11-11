using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectTestFunction : MonoBehaviour
{

    List<Move> list = new List<Move>();
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
                hit.transform.renderer.material.color = Color.black;
                list.Add(hit.transform.GetComponent<Move>());
            }
        }
        if (Input.GetKeyDown("a"))
        {
            foreach (Move move in list)
            {
                //move.box.content = null;
                Destroy(move.gameObject);
            }
            list.Clear();
        }
	}
}
