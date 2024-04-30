using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    List<RecyclableMonster> monsterStore = new List<RecyclableMonster>();
    public Transform[] stageSpawnPoint;//�������� ���� ����Ʈ

    public Action<RecyclableMonster> something;
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

        StageManager.instance.StartWave += OnStartSpawn;//���̺� ���۰� �������� �̺�Ʈ ����
    }
    
    void OnMonsterDestroyed(RecyclableMonster usedMonster)
    {
        
        usedMonster.Destroyed -= OnMonsterDestroyed;
        usedMonster.ClearDestroyed -= OnMonsterDestroyed;
        int monsterIndex = monsterStore.IndexOf(usedMonster);//����Ʈ�� �ε��� �� ����
        monsterStore.RemoveAt(monsterIndex);
        //monsterFactory.MonsterRestore(usedMonster);
        if (usedMonster.name == "slime(Clone)")
        {
            slimeFactory.MonsterRestore(usedMonster);
        }
        else if (usedMonster.name == "bat(Clone)") batFactory.MonsterRestore(usedMonster);
        else if (usedMonster.name == "spider(Clone)") spiderFactory.MonsterRestore(usedMonster);
        else if (usedMonster.name == "baby_dragon(Clone)") babyDragonFactory.MonsterRestore(usedMonster);
    }
    //void OnSlimeDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnSlimeDestroyed;
    //}
    //void OnBatDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnBatDestroyed;
    //}
    //void OnSpiderDestroyed(RecyclableMonster usedBabyDragon)
    //{
    //    usedBabyDragon.Destroyed -= OnSpiderDestroyed;
    //}

    public void OnStartSpawn(StageData stageData)
    {
        if (stageData.slimeNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.slimeNum,  slimeFactory));
        }
        if (stageData.batNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.batNum, batFactory));
        }
        if (stageData.spiderNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.spiderNum, spiderFactory));
        }
        if (stageData.babyDragonNum > 0)
        {
            StartCoroutine(SpawnDelay(stageData.babyDragonNum, babyDragonFactory));
        }




    }

    IEnumerator SpawnDelay(int MaxMonsterTypeNum, MonsterFactory monsterFactory)
    {
        int spawnNum = MaxMonsterTypeNum;
        int A = 0;
       
        while (spawnNum > 0)
        {
            int randomInt = UnityEngine.Random.Range(0, 8);
     
            yield return new WaitForSeconds(60/MaxMonsterTypeNum);//��ȯ�� ������ �� �п� 30 �� ���� ��ȯ/60->1�ʿ� �Ѹ��� 20->3�ʾ� �Ѹ���
        
            
            RecyclableMonster monster = monsterFactory.GetMonster();//���� Ȱ��ȭ
            spawnNum--;
            monster.Activate(stageSpawnPoint[randomInt].position);//���� ��ġ ����
            monster.Destroyed += OnMonsterDestroyed;
            monster.ClearDestroyed += OnMonsterDestroyed;
            monsterStore.Add(monster);//���� �����ϱ� ���� ��Ƶδ� ����Ʈ


            A++;
            if(MaxMonsterTypeNum == A)
                Debug.Log($"{monster.name} {A} ���� ��� ��ȯ��");
            if (A > 1000)
                break;
        }

    }

    public void OnDestroyAllMonster()
    {
        //foreach (var monster in monsterStore)
        //{
        //    monster.OnStageClearMonDestroy();
        //}
        StopAllCoroutines();
        for (int i = monsterStore.Count -1; i >= 0; i--)
        {
            monsterStore[i].OnStageClearMonDestroy();
        }
    }

    public void OnDamagedAllMonster()
    {
        //foreach (var monster in monsterStore)
        //{
        //    monster.OnMonDamaged(3);
        //}
        for (int i = monsterStore.Count - 1; i >= 0; i--)
        {
            monsterStore[i].OnMonDamaged(3);
        }
    }

    void Update()
    {



        //=======================�׽�Ʈ�� ��ȯ======================================
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    RecyclableMonster babyDragon = babyDragonFactory.GetMonster();
        //    babyDragon.Activate(spawnPoint[0].position);
        //    babyDragon.Destroyed += OnBabyDragonDestroyed;
        //    //GameManager.instance.playerAttack.monAttack += babyDragon.MonDamaged;//���� �ǰ� �̺�Ʈ ����

        //}
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    RecyclableMonster slime = slimeFactory.GetMonster();
        //    slime.Activate(spawnPoint[1].position);
        //    slime.Destroyed += OnSlimeDestroyed;

        //}
        //if (Input.GetKeyDown(KeyCode.RightAlt))
        //{
        //    RecyclableMonster bat = batFactory.GetMonster();
        //    bat.Activate(spawnPoint[2].position);
        //    bat.Destroyed += OnBatDestroyed;

        //}
        //if (Input.GetKeyDown(KeyCode.RightControl))
        //{
        //    RecyclableMonster spider = spiderFactory.GetMonster();
        //    spider.Activate(spawnPoint[3].position);
        //    spider.Destroyed += OnSpiderDestroyed;

           

        //}

    }
}
