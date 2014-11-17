using UnityEngine;
using System.Collections;

public abstract class Stunt 
{
    protected Animal animal;
    public Stunt(Animal animal)
    {
        this.animal = animal;
    }
    public abstract void Action();
}
