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
    [SerializeField]
    Slime slimePrefab;




    //===============���丮 ����=======================
    MonsterFactory babyDragonFactory; 
    MonsterFactory slimeFactory; 
    
    void Start()
    {
        babyDragonFactory = new MonsterFactory(babyDragonPrefab,5);//���� ���丮�� �巡�� �ν��Ͻ� ����
        slimeFactory = new MonsterFactory(slimePrefab, 5);//���� ���丮�� �巡�� �ν��Ͻ� ����
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
            babyDragon.Activate(spawnPoint[0].position);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            RecyclableMonster slime = slimeFactory.GetMonster();
            slime.Activate(spawnPoint[1].position);

        }
    }
}
