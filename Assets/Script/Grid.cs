/* ======
 * =字典=
 * ======
 * 管口neck：没有正上方接口，只能从两侧获得资源。
 */

/* ======
 * =问题=
 * ======
 * -XA01:允许手动设置spawn点.
 * -XA02:影响管口变化的因素
 * -XA111302  创建特效
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : Only<Grid> {
    public bool debug = false;

    public const int CRITICAL_POINT = 3;   //针对数据定义的分界点,用来气氛数据中那些是可以通过的.

    //public static Grid Instance;
    public GameObject gridPrefab;    //生成背景方格的prefab
    public const int GRID_X_COUNT = 9;      //Grid数据的最宽值
    public const int GRID_Y_COUNT = 9;      //Grid数据的最高值
    public const int GRID_XY_COUNT = GRID_X_COUNT * GRID_Y_COUNT; //Grid格数

    public Box[] boxs = new Box[GRID_XY_COUNT];
    public List<int> spawnList = new List<int>();
    public List<int> necks = new List<int>(); //管口数据,Grid变化时,更改数据
    public bool[] moveList;// = new bool[GRID_XY_COUNT];
    bool stopCalled = false;
    bool lastStopflag = false;
    int[] data = 
    {
        0,0,1,0,0,0,1,0,0,          //1
        0,1,1,1,0,1,1,1,0,          //2
        1,1,1,1,1,1,1,1,1,          //3
        1,1,1,1,1,1,1,1,1,          //4
        1,1,1,1,1,1,1,1,1,          //5
        1,1,1,1,1,1,1,1,1,          //6
        0,1,1,1,1,1,1,1,0,          //7
        0,0,1,1,1,1,1,0,0,          //8
        0,0,0,1,1,1,0,0,0           //9
    };
    //void Awake()
    //{
    //    //Instance = this;
    //}

	void Start () {
        moveList = new bool[GRID_XY_COUNT];
        CreateGrid();
        Camera.main.transform.position = CameraPosition();
        CalculateSpawn();
	}
	
	void LateUpdate () 
    {
        if (moveList.FirstIndexof<bool>(true) == -1)
        {
            if (lastStopflag)
            {
                if (!stopCalled)
                {
                    stopCalled = true;
                    Stop();
                }
            }
            else
                lastStopflag = true;
        }
        else
        {
            lastStopflag = false;
            stopCalled = false;
        }
	}
    void Stop()
    {
        Clear();
    }
    void Clear()
    {
        List<Animal> tempList = new List<Animal>();
        for (int i = 0; i < GRID_XY_COUNT; i++)
        {
            if (boxs[i] != null && boxs[i].content != null && !tempList.Contains(boxs[i].content.animal))
            {
                if(boxs[i].checker.Check(boxs[i].content.animal.color)) // -XA111302  创建特效
                {
                    foreach (Animal animal in boxs[i].checker.GetEliminateList())
                    {
                       tempList.Add(animal);
                    }
                }
            }
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].EliminateSelf();
        }
    }

    void CreateGrid()
    {
        CreateGrid(data);
    }
    /// <summary>
    /// 根据数据创建背景格子
    /// </summary>
    /// <param name="data">数据内容</param>
    void CreateGrid(int[] data)
    {
        Transform parent = new GameObject("Grid").transform;
        int i = 0;
        while (i < GRID_XY_COUNT && i < data.Length)
        {
            int x = i % GRID_X_COUNT;
            int y = i / GRID_X_COUNT;
            if (data[i] == 1)
            {
                GameObject go = Instantiate(gridPrefab, new Vector3(x, -y, 0), Quaternion.identity) as GameObject;
                go.name = i.ToString();
                go.transform.parent = parent;
                go.GetComponentInChildren<TextMesh>().text = go.name;
                boxs[i] = go.GetComponent<Box>();
                boxs[i].index = i;
                if (IsNeck(i))
                {
                    necks.Add(i);
                    boxs[i].GetComponentInChildren<TextMesh>().color = Color.blue;
                    Debug.Log("Set boxs[" + i + "].text.color = blue");
                }
                if (!debug)
                    go.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
            }
            i++;
        }
    }
    /// <summary>
    /// 可以计算出哪些是出生点，目前自动计算，之后可以写在数据里
    /// </summary>
    void CalculateSpawn()
    {
        spawnList.Clear();
        int head = data.FirstIndexof<int>(1);
        int i = head;
        while (i < (head / GRID_X_COUNT + 1) * GRID_X_COUNT)  //i<当前行，当前行是最高有数据行
        {
            if (data[i] == 1)
            {
                //boxs[i].spawner = true;
                if(debug)
                    boxs[i].GetComponentInChildren<TextMesh>().color = Color.green;
                Spawn spawn = boxs[i].gameObject.AddComponent<Spawn>();
                spawn.box = boxs[i];
                Debug.Log("Set boxs[" + i + "].text.color = green");
                spawnList.Add(i);
                //添加开发点
                GameObject go = new GameObject("Spawn" + i);
                Box box = go.AddComponent<Box>();
                go.transform.position = new Vector3(i % GRID_X_COUNT, i / GRID_X_COUNT + 1, 0);
                box.index = -1;
                box.lower[0] = boxs[i];
                box.spawner = true;
            }
            i++;
        }
    }

    /// <summary>
    /// 用来计算当前格子是否是管口
    /// </summary>
    bool IsNeck(int index)
    {
        int up = index - GRID_X_COUNT;
        if (up >= 0)
        {
            if (data[up] > 0 && data[up] < CRITICAL_POINT)
                return false;
        }
        //如果是spawn也反回false,因为没有手动设置spawn,这里暂时没有代码.添加手动设置spawn的时候添加 -XA01
        //其他地形变化影响管口计算   -XA02
        return true;
    }

    Vector3 CameraPosition()
    {
        return new Vector3(4, -4, -10);
    }
}
