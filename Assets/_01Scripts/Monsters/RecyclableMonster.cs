using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecyclableMonster : MonoBehaviour
{
    protected enum STATE { IDEL, TRACE, ATTACK, DIE }
    //================����=============================
    protected Vector3 targetPosition;//�÷��̾��� ��ġ
    protected bool isDead = false;
    protected bool isActivated = false;
    protected bool isCanAttack = false;
    protected bool isAttacking = false;
    protected float lastAttackTime = 0;
    protected STATE state { get; set; }


    //================�̺�Ʈ==========================
    public Action<RecyclableMonster> MonsDeath;
    public event Action MonAttack;


    //================================================



    // Start is called before the first frame update
    void Start()
    {
        isCanAttack = true;
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
    public virtual void MonsterState(Vector3 playerPos, float attackDistance, float attackSpeed)
    {
        
        float distance = Vector3.Distance(playerPos, transform.position);

        

        if (!isDead)//���� �ʾҴٸ�
        {

            if (!isAttacking)
            {
                if (distance <= attackDistance && isCanAttack)//�����Ÿ����� �����ٸ�, ���ݰ��� ���¶��
                {
                    isCanAttack = false;
                    isAttacking = true;
                    StartCoroutine(DelayAttack(attackSpeed));//���ݼӵ� �ð� �� ���ݰ���
                    state = STATE.ATTACK;
                }
                else if (distance <= attackDistance && !isCanAttack) //�����Ÿ����� �����ٸ�, ���ݺҰ� ���¶��
                {
                    state = STATE.IDEL;
                }
                else if (distance > attackDistance && !isCanAttack)
                {
                    state = STATE.TRACE;
                }
            }
            else
                state = STATE.ATTACK;
        }
    }

    IEnumerator DelayAttack(float attackSpeed)//���� ������ �ڷ�ƾ
    {
        yield return new WaitForSeconds(attackSpeed);
        isCanAttack = true;
        isAttacking = false;
    }

    public virtual void UpdateState(Vector3 playerPos, int moveSpeed)
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
    public virtual void TraceState(Vector3 playerPos, int moveSpeed)
    {
        MonsterMovement(playerPos, moveSpeed);
    }
    public virtual void AttackState()
    {

    }
    public virtual void DieState()
    {

    }

    public virtual void MonsterMovement(Vector3 playerPos, int moveSpeed)//���� �̵�
    {
        Vector3 dir = (playerPos - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }



}