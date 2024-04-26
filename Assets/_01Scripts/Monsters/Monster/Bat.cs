using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : RecyclableMonster
{
    public Transform playerPosition;
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
        monName = batData.monsterName;
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
        playerPosition = GameObject.FindWithTag("Player").transform;
        gameObject.tag = "monster";
    }

    public void OnMonDamaged(int PlayerDamage)//�÷��̾��� ���� �̺�Ʈ�� ���� �Լ�
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if (hp <= 0)
        {
            Destroyed?.Invoke(this);//���� ���� �̺�Ʈ
            isDead = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPosition.position);
        MonsterState(playerPosition.position, batData.attackDistance, batData.attackSpeed, batData.attackMotionSpeed);
        UpdateState(playerPosition.position, batData.moveSpeed);
    }
}
