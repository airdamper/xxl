using UnityEngine;
using System.Collections;

public enum AnimatEnum
{
    idle,
    line,
    column,
    wrap,
    click,
    destroy
}
public class Animat 
{
    public Animator agent;
    //int idle;
    //int line;
    //int column;
    //int wrap;
    //int click;
    public Animat(Animator agent)
    {
        this.agent = agent;
        //idle = Animator.StringToHash("idle");
        //line = Animator.StringToHash("line");
        //column = Animator.StringToHash("column");
        //wrap = Animator.StringToHash("wrap");
        //click = Animator.StringToHash("click");
    }
    public void Play(AnimatEnum name)
    {
        agent.SetTrigger(name.ToString());
    }
}

