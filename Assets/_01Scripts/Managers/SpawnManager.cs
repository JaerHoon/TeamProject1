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
    [SerializeField]
    Bat batPrefab;
    [SerializeField]
    Spider spiderPrefab;
    



    //===============���丮 ����=======================
    MonsterFactory babyDragonFactory; 
    MonsterFactory slimeFactory; 
    MonsterFactory batFactory; 
    MonsterFactory spiderFactory; 
    
    void Start()
    {
        babyDragonFactory = new MonsterFactory(babyDragonPrefab,5);//���� ���丮�� �巡�� �ν��Ͻ� ����
        slimeFactory = new MonsterFactory(slimePrefab, 5);//���� ���丮�� ������ �ν��Ͻ� ����
        batFactory = new MonsterFactory(batPrefab, 5);//���� ���丮�� �� �ν��Ͻ� ����
        spiderFactory = new MonsterFactory(spiderPrefab, 5);//���� ���丮�� �����̴� �ν��Ͻ� ����
        
    }
    
    void OnBabyDragonDestroyed(RecyclableMonster usedBabyDragon)
    {
        usedBabyDragon.Destroyed -= OnBabyDragonDestroyed;
    }
    void OnSlimeDestroyed(RecyclableMonster usedBabyDragon)
    {
        usedBabyDragon.Destroyed -= OnSlimeDestroyed;
    }
    void OnBatDestroyed(RecyclableMonster usedBabyDragon)
    {
        usedBabyDragon.Destroyed -= OnBatDestroyed;
    }
    void OnSpiderDestroyed(RecyclableMonster usedBabyDragon)
    {
        usedBabyDragon.Destroyed -= OnSpiderDestroyed;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
            babyDragon.Activate(spawnPoint[0].position);
            babyDragon.Destroyed += OnBabyDragonDestroyed;

        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            RecyclableMonster slime = slimeFactory.GetMonster();
            slime.Activate(spawnPoint[1].position);
            slime.Destroyed += OnSlimeDestroyed;

        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            RecyclableMonster bat = batFactory.GetMonster();
            bat.Activate(spawnPoint[2].position);
            bat.Destroyed += OnBatDestroyed;

        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            RecyclableMonster spider = spiderFactory.GetMonster();
            spider.Activate(spawnPoint[3].position);
            spider.Destroyed += OnSpiderDestroyed;

           

        }

    }
}
