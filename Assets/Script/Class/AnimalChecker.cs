/*
 * 保存结果,计算特技.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalChecker
{
    /// <summary>
    /// 是否有可以消除的对象
    /// </summary>
    //public bool canDo { get;private set; }
    int _checkColor = -1;
    int checkColor
    {
        get { return _checkColor; }
        set
        {
            if (_checkColor != value)
            {
                _checkColor = value;
                Check(value);
            }
        }
    }
    Box box;
    List<Box> x;
    List<Box> y;
    List<Box> up;
    List<Box> down;
    List<Box> left;
    List<Box> right;
    public AnimalChecker(Animal animal)
    {
        box = animal.move.box;
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
    public bool Check(int color)
    {
        return false;
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
