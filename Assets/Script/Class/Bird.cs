using UnityEngine;
using System.Collections;

public class Bird : Stunt
{
    public Bird(Animal animal)
        : base(animal)
    {
        animal.agent.agent.runtimeAnimatorController = Level.Instance.bird;
        animal.color = -1;
    }
    public override void Action()
    {
        animal.DestroySelf();
    }
}
