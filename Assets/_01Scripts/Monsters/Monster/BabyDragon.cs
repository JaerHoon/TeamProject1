using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyDragon : RecyclableMonster
{
    // Start is called before the first frame update

    public Transform playerPositionTest;
    [SerializeField]
    MonsterData drangonData;
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
       
    }

    public void OnMonDamaged(int PlayerDamage)//�÷��̾��� ���� �̺�Ʈ�� ���� �Լ�
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if(hp <= 0)
        {
            MonDeath();//���� ���� �̺�Ʈ
            isDead = true;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        LookPlayer(playerPositionTest.position);
        MonsterState(playerPositionTest.position, drangonData.attackDistance ,drangonData.attackSpeed, drangonData.attackMotionSpeed);
        UpdateState(playerPositionTest.position, drangonData.moveSpeed);
    }
}
