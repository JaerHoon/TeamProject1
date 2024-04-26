using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BabyDragon : RecyclableMonster
{

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

    Animator anim;

    [SerializeField]
    FireBall fireBallPrefab;//���̾� �� ������

    MonsterFactory fireBallFactory;//���̾� �� ���丮

    private void OnEnable()//Ȱ��ȭ �� �ʱ�ȭ
    {
        monName = drangonData.monsterName;
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
        anim = GetComponent<Animator>();
        gameObject.tag = "monster";
        fireBallFactory = new MonsterFactory(fireBallPrefab, 2);//���� ���丮�� �����̴� �ν��Ͻ� ����
    }

    public void OnMonDamaged(int PlayerDamage)//�÷��̾��� ���� �̺�Ʈ�� ���� �Լ�
    {
        hp = MonDamaged(hp, defense, PlayerDamage);
        if(hp <= 0)
        {
            Destroyed?.Invoke(this);//���� ���� �̺�Ʈ
            isDead = true;

        }
    }

    //===============���� ���¿� ���� �ִϸ����� �Ķ���� �� ����==============
    public override void AttackState()
    {
        base.AttackState();
        anim.SetInteger("STATE", 2);
    }
    public override void IdleState()
    {
        base.IdleState();
        anim.SetInteger("STATE", 0);
    }
    public override void TraceState(Vector3 playerPos, float moveSpeed)
    {
        base.TraceState(playerPos, moveSpeed);
        anim.SetInteger("STATE", 0);
    }
    public override void DamagedState()
    {
        base.DamagedState();
        anim.SetInteger("STATE", 3);
    }
    public override void DieState()
    {
        base.DieState();
        anim.SetInteger("STATE", 4);
    }

    public void OnFireBallLaunched()
    {
        RecyclableMonster fireBall = fireBallFactory.GetMonster();
        fireBall.Activate(GetComponentsInChildren<Transform>()[2].position);
        fireBall.Destroyed += OnFireBallDestoryed;
    }

    void OnFireBallDestoryed(RecyclableMonster usedFireBall)
    {
        usedFireBall.Destroyed -= OnFireBallDestoryed;
        fireBallFactory.MonsterRestore(usedFireBall);
    }

   
    //=====================================
    // Update is called once per frame
    void Update()
    {
        
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, drangonData.attackDistance ,drangonData.attackSpeed, drangonData.attackMotionSpeed);
        UpdateState(targetPosition.position, drangonData.moveSpeed);
    }
}
