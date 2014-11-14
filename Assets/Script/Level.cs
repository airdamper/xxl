using UnityEngine;
using System.Collections;

public class Level : Only<Level>
{
    public GameObject animalPrefab;
    public RuntimeAnimatorController[] animalColor = new RuntimeAnimatorController[5];
    public RuntimeAnimatorController destroy_effect;
    public SpriteRenderer tileSelect;
    public GameObject mask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
