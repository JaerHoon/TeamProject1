using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : RecyclableMonster
{
    public Transform playerPosition;
    [SerializeField]
    MonsterData spiderData;
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
        monName = spiderData.name;
        hp = spiderData.hp;
        damage = spiderData.damage;
        defense = spiderData.defense;
        moveSpeed = spiderData.moveSpeed;
        attackDistance = spiderData.attackDistance;
        attackSpeed = spiderData.attackSpeed;
        attackMotionSpeed = spiderData.attackMotionSpeed;
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
            MonDeath();//���� ���� �̺�Ʈ
            isDead = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPosition.position);
        MonsterState(playerPosition.position, spiderData.attackDistance, spiderData.attackSpeed, spiderData.attackMotionSpeed);
        UpdateState(playerPosition.position, spiderData.moveSpeed);
    }
}
