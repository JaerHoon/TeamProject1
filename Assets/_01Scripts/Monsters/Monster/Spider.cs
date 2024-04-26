using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : RecyclableMonster
{
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
        monName = spiderData.monsterName;
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
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, spiderData.attackDistance, spiderData.attackSpeed, spiderData.attackMotionSpeed);
        UpdateState(targetPosition.position, spiderData.moveSpeed);
    }
}
