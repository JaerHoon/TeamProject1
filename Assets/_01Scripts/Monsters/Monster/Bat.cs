using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : RecyclableMonster
{
    public Transform playerPositionTest;
    [SerializeField]
    MonsterData batData;
    //==================����=========================
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

    private void OnEnable()//Ȱ��ȭ �� �ʱ�ȭ
    {
        monName = batData.name;
        hp = batData.hp;
        damage = batData.damage;
        defense = batData.defense;
        moveSpeed = batData.moveSpeed;
        attackDistance = batData.attackDistance;
        attackSpeed = batData.attackSpeed;
        attackMotionSpeed = batData.attackMotionSpeed;
    }

    void Start()
    {
        playerPositionTest = GameObject.Find("Player").transform;
    }

    public void OnMonDamaged(int PlayerDamage)//�÷��̾��� ���� �̺�Ʈ�� ���� �Լ�
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if (hp <= 0)
        {
            MonDeath();//���� ���� �̺�Ʈ
            isDead = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPositionTest.position);
        MonsterState(playerPositionTest.position, batData.attackDistance, batData.attackSpeed, batData.attackMotionSpeed);
        UpdateState(playerPositionTest.position, batData.moveSpeed);
    }
}
