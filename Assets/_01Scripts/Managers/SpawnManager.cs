using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;//���� �Ŵ��� �̱��� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform[] spawnPoint;
    //===============���� ������ ����===================
    [SerializeField]
    BabyDragon babyDragonPrefab;




    //===============���丮 ����=======================
    MonsterFactory babyDragonFactory; 
    
    void Start()
    {
        babyDragonFactory = new MonsterFactory(babyDragonPrefab,5);//���� ���丮�� �巡�� �ν��Ͻ� ����
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
            babyDragon.Activate(spawnPoint[0].position);
            
        }
    }
}
