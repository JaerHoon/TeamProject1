using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterRecycle : MonoBehaviour
{
    //================����=============================
    protected Vector3 targetPosition;//�÷��̾��� ��ġ
    protected bool isActivated = false;
    

    //================�̺�Ʈ==========================
    public Action<MonsterRecycle> MonsterDeath;


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
                transform.rotation = Quaternion.Euler(transform.rotation.x,180.0f,transform.rotation.z);
            else//y���� 0�϶� �������� ���� ������ ���
                transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
        }
        else//�÷��̾ ���ͺ��� ���ʿ� ������
        {
            if (transform.rotation.y == 180)
                transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
            else 
                transform.rotation = Quaternion.Euler(transform.rotation.x,180.0f,transform.rotation.z);
        }


       
    }

    public virtual void MoveToPlayer(Vector3 playerPos)
    {
        Vector3 dir = (playerPos - transform.position).normalized;

    }
   
}
