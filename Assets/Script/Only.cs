using UnityEngine;
using System.Collections;

public class Only<T> : MonoBehaviour where T : Only<T>
{

    public static T Instance;
	// Use this for initialization
	void Awake () 
    {
        Instance = (T)this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
