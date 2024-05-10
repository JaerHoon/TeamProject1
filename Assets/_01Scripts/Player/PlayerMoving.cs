using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoving : MonoBehaviour
{
    public VariableJoystick joy;
    Animator anim;
    

    float scaleX;
    float scaleY;
    float scaleZ;
    public int speed;

    PlayerAttack playerattack;

    [SerializeField]
    LayerMask layermask;

    [SerializeField]
    Image Autoimage;
    [SerializeField]
    float rotSpeed = 50.0f;

    int monsterLayer;
    int flymonsterLayer;
    public float range = 5.0f;
    Rigidbody2D rb;
    Transform monsterPos;
    public bool isButtonPressed = false;

    float x;
    float y;
    private void Start()
    {
        monsterLayer = LayerMask.NameToLayer("monster");
        flymonsterLayer = LayerMask.NameToLayer("flymonster");

        layermask = 1 << monsterLayer | 1 << flymonsterLayer;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerattack = GetComponent<PlayerAttack>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnButtonClick()
    {
        isButtonPressed = !isButtonPressed;

        if(isButtonPressed)
        {
            monsterTrace();
        }
    }

    public void monsterTrace()
    {
    
        if(!isButtonPressed)
        {
            return;
        }
        
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, range, layermask);

        float closeMonster = Mathf.Infinity; // �ִ밪
        Vector3 closeMonsterPos = Vector3.zero; // ���� ��ġ�� ó���� (0,0)���� ����� 
        
        for (int i = 0; i < monsters.Length; i++)
        {
            float monsterDistance = Vector3.Distance(transform.position, monsters[i].transform.position);

            if(monsterDistance < closeMonster)
            {
                closeMonster = monsterDistance;
                closeMonsterPos = monsters[i].transform.position;
            }
        }

        
        if(Vector3.Distance(closeMonsterPos,transform.position) <4 && Vector3.Distance(closeMonsterPos, transform.position) >1 && monsters.Length != 0)
        {
            transform.Translate((closeMonsterPos-transform.position).normalized * speed * Time.deltaTime);
            print(transform.position);
            playerattack.isMove = true;
            anim.SetTrigger("run");
        }
        else if(Vector3.Distance(closeMonsterPos, transform.position) < 1)
        {
            
            playerattack.isMove = false;
        }

        if(monsters.Length == 0)
        {
            anim.SetTrigger("idle");
        }

        if (monsters.Length != 0)
        {


            if (closeMonsterPos.x > transform.position.x)
                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            else
                transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        }
    }

    void PlayerControll()
    {

        x = joy.Horizontal; // �¿� ������
        y = joy.Vertical; // ���� ������
        Vector3 pos = new Vector3(x, y, 0); // ���� ����
        pos.Normalize(); // ���� ũ�� ����ȭ�ؼ� ������ �ӵ��� ��Ÿ�� �� ����

        transform.position += pos * speed * Time.deltaTime;

        if (joy.Horizontal > 0)
        {
            playerattack.isMove = true;
            anim.SetTrigger("run"); // ĳ���� �̵� �ִϸ��̼�
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        else if (joy.Horizontal < 0)
        {
            playerattack.isMove = true;
            anim.SetTrigger("run");
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ); // �ݴ���� �ٶ󺸰� �ϱ� ����
        }
        else
        {
            playerattack.isMove = false;
            anim.SetTrigger("idle");

        }
    }


    void FixedUpdate()
    {
        if (isButtonPressed && (joy.Horizontal == 0 && joy.Vertical == 0)) //�ڵ������� ���� �ִµ� ���̽�ƽ�� �ȿ����̴� ���
        {
            monsterTrace();
        }
        else if(isButtonPressed && (joy.Horizontal != 0 || joy.Vertical != 0)) //�ڵ������� ���� �ִµ� ���̽�ƽ�� �����̴� ���
        {
            PlayerControll();
        }
        else //�ڵ������� ���� �ִµ� ���̽�ƽ�� �����̴� ���
        {

            PlayerControll();



        }




    }
}
