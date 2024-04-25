using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecyclableMonster : MonoBehaviour
{
    protected enum STATE { IDEL, TRACE, ATTACK,DAMAGED, DIE }
    //Ʈ���� 0 : idle, 1 : walk, 2 : attack, 3 : hurt, 4 : dead, 5 : dragonfall
    //================����=============================
    protected Vector3 targetPosition;//�÷��̾��� ��ġ
    protected bool isDead = false;
    protected bool isActivated = false;
    protected bool isCanAttack = true;
    protected bool isAttacking = false;
    protected bool isDamaged = false;
    protected float lastAttackTime = 0;
    protected float DamagedTime = 0.3f;
    protected STATE state { get; set; }
    


    //================�̺�Ʈ==========================
    public Action MonDeath;


    //================================================



    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosition = Vector3.zero;//�÷��̾� ��ġ �ʱ�ȭ
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //���� ���� ��ġ ����
    public virtual void Activate(Vector3 spawnPos)
    {
        isActivated = true;//Ȱ�� �÷��� ��
        transform.position = spawnPos;//���� ����Ʈ�� ��ġ ��ȯ
    }

    //�÷��̾��� �������� y�� ȸ��
    public virtual void LookPlayer(Vector3 playerPos)
    {

        if (playerPos.x >= transform.position.x)//�÷��̾ ���ͺ��� �����ʿ� ������
        {
            if (transform.rotation.y == 180)//y���� 180�϶� �������� ���� ������ ���
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180.0f, transform.rotation.z);
            else//y���� 0�϶� �������� ���� ������ ���
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else//�÷��̾ ���ͺ��� ���ʿ� ������
        {
            if (transform.rotation.y == 180)
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180.0f, transform.rotation.z);
        }



    }


    //���� �����Ÿ����� �̵� �Լ�
    public virtual void MonsterState(Vector3 playerPos, float attackDistance, float attackSpeed, float motionSpeed)
    {
        
        float distance = Vector3.Distance(playerPos, transform.position);

        

        if (!isDead)//���� �ʾҴٸ�
        {
            if (!isDamaged)//������ ���� ���°� �ƴ϶��
            {
                if (!isAttacking)//�������� �ƴϸ�
                {
                    if (distance <= attackDistance)//�����Ÿ����� �����ٸ�
                    {
                        if (isCanAttack)//���� ���� ���¶��
                        {
                            state = STATE.ATTACK;//���� ����
                            StartCoroutine(DelayAttack(attackSpeed));//���ݼӵ� �ð� �� ���ݰ���
                            StartCoroutine(DelayAttackMotion(motionSpeed));//���ݼӵ� �ð� �� ���ݰ���
                            isAttacking = true;//������
                            isCanAttack = false;//�̹� ���� ���̹Ƿ� ���� �Ұ�
                        }
                        else//���� ���� ���°� �ƴ϶��
                        {
                            state = STATE.IDEL;
                        }
                    }
                    else if (distance > attackDistance)
                    {
                        state = STATE.TRACE;
                    }
                }
                else
                    state = STATE.ATTACK;
            }
            else
            {
                state = STATE.DAMAGED;
            }
        }
    }

    


    public virtual int MonDamaged(int MonHp,int MonDef,int PlayerDamage)//���� �ǰ� �Լ� ��� �� Hp ����
    {
        isDamaged = true;
        return MonHp - (PlayerDamage >= MonDef ? PlayerDamage - MonDef : 0);
    }

    IEnumerator DelayDamaged(float DamagedTime)
    {
        yield return new WaitForSeconds(DamagedTime);
        isDamaged = false;
    }

    IEnumerator DelayAttack(float attackSpeed)//���� ������ �ڷ�ƾ
    {
        yield return new WaitForSeconds(attackSpeed);
        isCanAttack = true;
        
    }

    IEnumerator DelayAttackMotion(float Motion)//���� ������ �ڷ�ƾ
    {
        yield return new WaitForSeconds(Motion);
        
        isAttacking = false;
    }

    public virtual void UpdateState(Vector3 playerPos, float moveSpeed)
    {
        switch (state)
        {
            case STATE.IDEL:
                IdleState();
                break;
            case STATE.TRACE:
                TraceState(playerPos, moveSpeed);
                break;
            case STATE.ATTACK:
                AttackState();
                break;
            case STATE.DAMAGED:
                DamagedState();
                break;
            case STATE.DIE:
                DieState();
                break;
            default:
                break;
        }

    }

    public virtual void IdleState()
    {

    }
    public virtual void TraceState(Vector3 playerPos, float moveSpeed)
    {
        MonsterMovement(playerPos, moveSpeed);
    }
    public virtual void AttackState()
    {

    }
    public virtual void DamagedState()
    {

    }
    public virtual void DieState()
    {

    }

    public virtual void MonsterMovement(Vector3 playerPos, float moveSpeed)//���� �̵�
    {
        Vector3 dir = (playerPos - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }



}