using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerAttack : MonoBehaviour
{
    public Transform playerTr; // �÷��̾� ��ġ
    public Transform pos; // ���� �ν��� ��ġ
    Animator anim;
    Transform minDisMon;

    public LayerMask layermask;
    float range = 1.0f;
    int monsterLayer;
    int flymonsterLayer;

    public Action<int> monattack;

    int attackDamage = 5;
    public bool isAttack = false;
    public bool isMove;

    float scaleX;
    float scaleY;
    float scaleZ;

    [SerializeField]
    Vector2 boxSize;

    
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();

        monsterLayer = LayerMask.NameToLayer("monster");
        flymonsterLayer = LayerMask.NameToLayer("flymonster");

        layermask = 1 << monsterLayer | 1 << flymonsterLayer;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

    }


    public void IsMonsterAttack()
    {
        //Vector2 plusPos = new Vector2(pos.position.x + 1.3f, pos.position.y + 1.3f);
        //Vector2 size = new Vector2(1.2f, 2.0f);
        Collider2D[] AttackArea = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D colls in AttackArea)
        {
            if(colls.CompareTag("monster"))
            {
                isAttack = true;
                attackAnim();
                monattack?.Invoke(attackDamage);
            }
            
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, range);

        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

 
    public void IsMonsterRecognition()
    {
        Collider2D[] RecognitionArea = Physics2D.OverlapCircleAll(playerTr.position, range, layermask, -100.0f,100.0f);
        

        if (RecognitionArea.Length > 0)
        {

            float distance = float.MaxValue; // float�� �ִ�
            for (int i = 0; i < RecognitionArea.Length; i++)
            {
                if (distance > Vector2.Distance(playerTr.position,RecognitionArea[i].transform.position))
                {
                    distance = Vector2.Distance(playerTr.position,RecognitionArea[i].transform.position);
                    minDisMon = RecognitionArea[i].transform;
                    
                }
            }
           
            
            if(isMove==false)
            {
                if (minDisMon.position.x > playerTr.position.x)
                    transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                else
                    transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);


                
            }

            minDisMon = null; // minDisMon�� null�� ���� ����� ���͸� ��� �ν��ϵ��� �ϱ� ����.
            //distance = float.MaxValue;




        }
       
    }

   
    void attackAnim()
    {
        anim.SetInteger("attack", 1); // ���� �ִϸ��̼� ����
    }

    void stopAttackAnim()
    {
        anim.SetInteger("attack", 0); // ���� �ִϸ��̼� ����
    }
   
    void Update()
    {
       
        IsMonsterRecognition();

        

    }

    private void FixedUpdate() // ������ �ð� �������� ȣ��� => Physics2D ���� �Լ� ����� �� ������.
    {
        if (isMove == true) // ������ �� ���ݸ�� ���߰� run �ִϸ��̼����� ���ư���
        {
            stopAttackAnim();
            return;
        }
        IsMonsterAttack();
    }

}
