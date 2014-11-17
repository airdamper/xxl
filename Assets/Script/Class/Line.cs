using UnityEngine;
using System.Collections;

public class Line : Stunt 
{
    public Line(Animal animal)
        : base(animal)
    {
        animal.Play(AnimatEnum.line);
    }
    public override void Action()
    {
        animal.DestroySelf();
    }
}
