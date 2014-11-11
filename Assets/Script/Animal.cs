using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour 
{
    int color;
    SpriteRenderer sprite;
    Move move;
	// Use this for initialization
	void Start () 
    {
        sprite = GetComponent<SpriteRenderer>();
        move = GetComponent<Move>();
        RandomColor();
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

    public void Swap(Animal animal)
    {
        move.Swap(animal.move);
    }
}
