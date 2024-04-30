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

    bool istargetDetected = true;
    

   
    public float spiderAttackMovementSpeed = 5.0f;

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
        Init();//�θ𿡼� �ʱ�ȭ
    }

    void Start()
    {
        gameObject.tag = "monster";
        anim = GetComponent<Animator>();
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

    //===============���� ���¿� ���� �ִϸ����� �Ķ���� �� ����==============
   
    public override void AttackState()
    {
        base.AttackState();
        if(istargetDetected)
        {
            istargetDetected = false;
            ReDetected();
            StartCoroutine(ReDetectedDelay(spiderData.attackMotionSpeed));
        }
       
        anim.SetInteger("STATE", 2);
        transform.position = Vector2.Lerp(transform.position, Ppos + dir* 1.5f, Time.deltaTime * spiderAttackMovementSpeed);
        //transform.position = Utility.EaseInQuint(transform.position, Ppos + dir * 1.5f, Time.deltaTime* spiderAttackMovementSpeed);//�÷��̾� �������� ����
         
    }
    Vector3 Ppos = Vector3.zero;//�Ͻ��� �÷��̾� ��ġ ����
    Vector3 dir = Vector3.zero;//�Ͻ��� �÷��̾� ���� ����
    void ReDetected()
    {
        Ppos = targetPosition.position;//��ġ ���
        dir = (targetPosition.position - transform.position).normalized;//���� ���
    }

    IEnumerator ReDetectedDelay(float time)//���� �� ������ �Լ�
    {
        yield return new WaitForSeconds(time);
        istargetDetected = true;
    }
    public override void IdleState()
    {
        base.IdleState();
        anim.SetInteger("STATE", 0);
    }
    public override void TraceState(Vector3 playerPos, float moveSpeed)
    {
        base.TraceState(playerPos, moveSpeed);
        anim.SetInteger("STATE", 1);
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


    // Update is called once per frame
    void Update()
    {
        LookPlayer(targetPosition.position);
        MonsterState(targetPosition.position, spiderData.attackDistance, spiderData.attackSpeed, spiderData.attackMotionSpeed);
        UpdateState(targetPosition.position, spiderData.moveSpeed);
    }
}
