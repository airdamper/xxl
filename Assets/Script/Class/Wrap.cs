using UnityEngine;
using System.Collections;

public class Wrap : Stunt
{
    public Wrap(Animal animal)
        : base(animal)
    {
        animal.Play(AnimatEnum.wrap);
    }
    public override void Action()
    {
        animal.DestroySelf();
    }
}
