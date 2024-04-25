using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyDragon : RecyclableMonster
{

    public Transform playerPosition;
    [SerializeField]
    MonsterData drangonData;
    //==================선언=========================
    [SerializeField]
    string monName;
    [SerializeField]
    int hp;
    [SerializeField]
    int damage;
    [SerializeField]
    int defense;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackMotionSpeed;

    private void OnEnable()//활성화 시 초기화
    {
        monName = drangonData.name;
        hp = drangonData.hp;
        damage = drangonData.damage;
        defense = drangonData.defense;
        moveSpeed = drangonData.moveSpeed;
        attackDistance = drangonData.attackDistance;
        attackSpeed = drangonData.attackSpeed;
        attackMotionSpeed = drangonData.attackMotionSpeed;
    }

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        gameObject.tag = "monster";
    }

    public void OnMonDamaged(int PlayerDamage)//플레이어의 공격 이벤트를 받을 함수
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if(hp <= 0)
        {
            MonDeath();//몬스터 죽음 이벤트
            isDead = true;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPosition.position);
        MonsterState(playerPosition.position, drangonData.attackDistance ,drangonData.attackSpeed, drangonData.attackMotionSpeed);
        UpdateState(playerPosition.position, drangonData.moveSpeed);
    }
}
