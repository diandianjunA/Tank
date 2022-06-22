using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] elements;
    public GameObject[] bonus;
    private List<Vector3> itemPositionList=new List<Vector3>();
    // Start is called before the first frame update
    public void Awake()
    {
        InitMap();
    }
    private void InitMap()
    {
        CreateItem(elements[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(elements[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(elements[1], new Vector3(-1, -8, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++)
        {
            CreateItem(elements[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        for (float i = -14.5f; i <= 14.5; i++)
        {
            CreateItem(elements[6], new Vector3(i, 9f, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++)
        {
            CreateItem(elements[6], new Vector3(-14f, i, 0), Quaternion.identity);
        }
        for (float i = -14.5f; i <= 14.5; i++)
        {
            CreateItem(elements[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++)
        {
            CreateItem(elements[6], new Vector3(12f, i, 0), Quaternion.identity);
        }
        InvokeRepeating("CreateEnemy", 3, 3);
        InvokeRepeating("BonusDeliver", 5, 5);
        //实例化地图
        for (int i = 0; i < 20; i++)
        {
            CreateItem(elements[1], CreateRandomPosition(), Quaternion.identity);
            CreateItem(elements[2], CreateRandomPosition(), Quaternion.identity);
            CreateItem(elements[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(elements[5], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 80; i++)
        {
            CreateItem(elements[1], CreateRandomPosition(), Quaternion.identity);
        }
        //初始化玩家
        GameObject go = Instantiate(elements[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
        //产生敌人
        CreateItem(elements[3], new Vector3(-13, 8, 0), Quaternion.identity);
        CreateItem(elements[3], new Vector3(11, 8, 0), Quaternion.identity);
        CreateItem(elements[3], new Vector3(0, 8, 0), Quaternion.identity);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetGameObject()
    {
        return gameObject;
    }

    private void CreateItem(GameObject createGameObject,Vector3 createPosition, Quaternion createRotation)
    {
        GameObject item = Instantiate(createGameObject,createPosition,createRotation);
        item.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    //产生随机位置
    private Vector3 CreateRandomPosition()
    {
        //不生成x=14，-14的两列，y=8,-8的两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-12, 11), Random.Range(-7, 8), 0);
            if (itemPositionList.Contains(createPosition))
            {
                continue;
            }
            return createPosition;
        }
    }
    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        switch (num)
        {
            case 0: EnemyPos = new Vector3(0, 8, 0); break;
            case 1: EnemyPos = new Vector3(-13, 8, 0); break;
            case 2: EnemyPos= new Vector3(11, 8, 0); break;
        }
        CreateItem(elements[3], EnemyPos, Quaternion.identity);
    }
    //产生道具
    private void BonusDeliver()
    {
        int num = Random.Range(0, 14);
        switch (num)
        {
            case 0: case 1:case 2:
                CreateItem(bonus[0], CreateRandomPosition(), Quaternion.identity);
                break;
            case 3: case 4:case 5:
                CreateItem(bonus[1], CreateRandomPosition(), Quaternion.identity);
                break;
            case 6: case 7:case 8:
                CreateItem(bonus[3], CreateRandomPosition(), Quaternion.identity);
                break;
            case 9: case 10:case 11:
                CreateItem(bonus[4], CreateRandomPosition(), Quaternion.identity);
                break;
            case 12: case 13:
                CreateItem(bonus[2], CreateRandomPosition(), Quaternion.identity);
                break;
        }

    }
}
