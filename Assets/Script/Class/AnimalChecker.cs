/*
 * 保存结果,计算特技.
 * 
 * 
 * ===========
 * 
 * -XA111303 特效处理
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalChecker
{
    /// <summary>
    /// 是否有可以消除的对象
    /// </summary>
    public bool canDo { get;private set; }
    //int checkColor;
    Box box;
    List<Box> x = new List<Box>();
    List<Box> y = new List<Box>();
    List<Box> up;
    List<Box> down;
    List<Box> left;
    List<Box> right;
    public AnimalChecker(Box box)
    {
        this.box = box;
        up = GetNeighbor(-Grid.GRID_X_COUNT);
        down = GetNeighbor(Grid.GRID_X_COUNT);
        left = GetNeighbor(-1);
        right = GetNeighbor(1);
    }
    //获得相邻的两格内容,step:相邻的距离
    List<Box> GetNeighbor(int step)
    {
        List<Box> result = new List<Box>();
        for (int i = 1; i <= 2; i++)
        {
            int checkIndex = box.index + step * i;
            if (checkIndex >= 0 && checkIndex < Grid.GRID_XY_COUNT)
            {
                if ((step == 1 && checkIndex % Grid.GRID_X_COUNT == 0) ||
                    (step == -1 && checkIndex % Grid.GRID_X_COUNT == Grid.GRID_X_COUNT - 1) ||
                    Grid.Instance.boxs[checkIndex] == null)
                {
                    break;
                }
                else
                {
                    result.Add(Grid.Instance.boxs[checkIndex]);
                }
            }
        }
        return result;
    }
    /// <summary>
    /// 在执行其他操作前都需要执行此方法
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool Check(int color)
    {
        x.Clear();
        y.Clear();
        AddToList(up, color, false);
        AddToList(down, color, false);
        AddToList(left, color, true);
        AddToList(right, color, true);
        canDo = x.Count >= 2 || y.Count >= 2;
        return canDo;
    }
    //boo用来区分横纵,true:x,false:y
    void AddToList(List<Box> list,int color, bool boo)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].content != null && !list[i].content.moveState.isMoving && list[i].content.animal.color == color)
            {
                if (boo)
                {
                    x.Add(list[i]);
                }
                else
                {
                    y.Add(list[i]);
                }
            }
            else
                break;
        }
    }
    /// <summary>
    /// 获得消除列表,需要先执行Check方法
    /// </summary>
    /// <returns></returns>
    public List<Animal> GetEliminateList()
    {
        if (canDo)
        {
            List<Animal> result = new List<Animal>();
            if (x.Count >= 2)
            {
                for (int i = 0; i < x.Count; i++)
                {
                    result.Add(x[i].content.animal);
                }
            }
            if (y.Count >= 2)
            {
                for (int i = 0; i < y.Count; i++)
                {
                    result.Add(y[i].content.animal);
                }
            }
            result.Add(box.content.animal);   //-XA111303 特效处理
            return result;
        }
        else
        {
            return null;
        }
    }
    #region 测试
    public string GetTestString()
    {
        return "up: " + PrintTest(up) + "down: " + PrintTest(down) + "left: " + PrintTest(left) + "right: " + PrintTest(right);
    }
    string PrintTest(List<Box> list)
    {
        string result = "  ";
        for (int i = 0; i < list.Count; i++)
        {
            result += list[i].index + "  ";
        }
        return result;
    }
    #endregion
}
