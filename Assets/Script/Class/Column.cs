using UnityEngine;
using System.Collections;

public class Column : Stunt
{
    public Column(Animal animal)
        : base(animal)
    {
        animal.Play(AnimatEnum.column);
    }
    public override void Action()
    {
        animal.DestroySelf();
    }
}
