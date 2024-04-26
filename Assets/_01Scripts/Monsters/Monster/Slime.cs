using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecyclableMonster
{

    [SerializeField]
    MonsterData slimeData;
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
        monName = slimeData.monsterName;
        hp = slimeData.hp;
        damage = slimeData.damage;
        defense = slimeData.defense;
        moveSpeed = slimeData.moveSpeed;
        attackDistance = slimeData.attackDistance;
        attackSpeed = slimeData.attackSpeed;
        attackMotionSpeed = slimeData.attackMotionSpeed;
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
        MonsterState(targetPosition.position, slimeData.attackDistance, slimeData.attackSpeed, slimeData.attackMotionSpeed);
        UpdateState(targetPosition.position, slimeData.moveSpeed);
    }
}
