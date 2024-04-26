using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform playerTr; // �÷��̾� ��ġ
    public Transform pos; // ���� �ν��� ��ġ
    public Transform DownPos;
    Animator anim;
    Rigidbody2D rb;
    Transform minDisMon;

    public LayerMask layermask;
    float range = 1.0f;
    int monsterLayer;
    

    float attackDistance = 3.0f;
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
        //monsterTr = GameObject.FindWithTag("monster").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //pos = GameObject.Find("pos").GetComponentInChildren<Transform>();
        anim = GetComponent<Animator>();

        monsterLayer = LayerMask.NameToLayer("monster");
        layermask = 1 << monsterLayer;

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

            minDisMon = null;
            distance = 10000;
            
           


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
