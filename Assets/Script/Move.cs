using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    const float SPEED = 5;
    delegate void Action();
    Action action;
    Action busyAction, idleAction;
    public MoveState moveState;

    //public Box targetBox;
    public Box box; // currentBox

    protected void Start()
    {
        moveState = new MoveState(MoveStart, MoveEnd, SPEED);
        busyAction = new Action(Busy);
        idleAction = new Action(Idle);
        action = idleAction;
    }
    void LateUpdate()
    {
        action();
    }
    bool CanMove()
    {
        Box result = GetTarget();
        if (result != null)
        {
            moveState.from = transform.position;
            moveState.to = result.position;
            box.Leave();
            result.Enter(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 移动交换两个animal对象的位置.
    /// </summary>
    /// <param name="target">与该对象交换</param>
    public void Swap(Move target)
    {

        if (action == idleAction && target.action == target.idleAction)
        {
            moveState.from = transform.position;
            moveState.to = target.box.position;

            target.moveState.from = moveState.to;
            target.moveState.to = moveState.from;

            Box thisBox = box;
            target.box.Enter(this);
            thisBox.Enter(target);

            moveState.isMoving = true;
            target.moveState.isMoving = true;

        }
    }
    //开始移动
    void MoveStart()
    {
        //print("MoveStart");
        action = busyAction;
    }
    //移动结束
    void MoveEnd()
    {
        //print("MoveEnd");
        //targetBox = null;
        action = idleAction;
    }
    //移动中的处理
    void Busy()
    {
        if (moveState.percentage < .95f)
        {
            moveState.UpdatePercentage();
            transform.position = moveState.GetValue();
        }
        else
        {
            transform.position = moveState.to;
            moveState.isMoving = false;
        }
        
    }
    //检测是否可以移动
    void Idle()
    {
        if (CanMove())
        {
            moveState.isMoving = true;
        }
    }

    //找要移动的目标,如果有可移动目标,反回要移动的目标,否则返回null
    Box GetTarget()
    {
        //if (currentBox.fresh == this)  //用来判断是不是顶部的等待进入的球
        //{
        //    if (currentBox.IsEmpty())
        //    {
        //        currentBox.Spawn();
        //        return targetBox;
        //    }
        //}
        //else
        //{
            if (box.lower[0] != null && box.lower[0].IsEmpty())
            {
                Box result = box.lower[0];
                //while (result.lower[0] != null && result.lower[0].IsEmpty())
                //{
                //    result = result.lower[0];
                //}
                return result;
            }
            if (box.lower[1] != null && box.lower[1].isNeck && box.lower[1].IsEmpty())
            {
                return box.lower[1];
            }
            if (box.lower[2] != null && box.lower[2].isNeck && box.lower[2].IsEmpty())
            {
                return box.lower[2];
            }
        //}
        return null;
    }
}

/// <summary>
/// 保存移动相关的信息，计算移动的位置。移动的开始和结束在外部调用，该类中只保存和计算 对应数据。
/// </summary>
public class MoveState
{
    public Vector3 from;
    public Vector3 to;
    public float percentage = 0;
    float runningTime = 0;
    float time;
    public delegate void MoveCallback();
    public event MoveCallback event_move_start;
    public event MoveCallback event_move_end;
    float speed;
    bool _isMoving = false;
    public bool isMoving
    {
        get { return _isMoving; }
        set
        {
            if (value != _isMoving)
            {
                if (_isMoving)
                {
                    if (event_move_end != null)
                    {
                        event_move_end();
                        percentage = 0;
                    }
                }
                else
                {
                    if (event_move_start != null)
                    {
                        event_move_start();
                        time = Vector3.Distance(from, to) / speed;
                        runningTime = 0;
                    }
                }
                _isMoving = value;
            }
        }
    }

    public MoveState(MoveCallback start, MoveCallback end, float speed)
    {
        event_move_start += start;
        event_move_end += end;
        this.speed = speed;
    }
    public void UpdatePercentage()
    {
        runningTime += Time.deltaTime;
        percentage = runningTime / time;
        //Debug.Log(runningTime +" / "+ time + "    " + percentage);
    }

    public Vector3 GetValue()
    {
        return Vector3.Lerp(from, to, percentage);
    }
}
