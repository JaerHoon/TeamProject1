using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerMoving : MonoBehaviour
{
    public VariableJoystick joy;
    Animator anim;
    

    float scaleX;
    float scaleY;
    float scaleZ;
    public int speed;

    private void Start()
    {
        
        anim = GetComponent<Animator>();

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void FixedUpdate()
    {
        float x = joy.Horizontal; // �¿� ������
        float y = joy.Vertical; // ���� ������
        Vector3 pos = new Vector3(x, y, 0); // ���� ����
        pos.Normalize(); // ���� ũ�� ����ȭ�ؼ� ������ �ӵ��� ��Ÿ�� �� ����

        transform.position += pos * speed * Time.deltaTime;

        if (joy.Horizontal >0)
        {
            anim.SetTrigger("run"); // ĳ���� �̵� �ִϸ��̼�
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        else if(joy.Horizontal < 0)
        {
            anim.SetTrigger("run");
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ); // �ݴ���� �ٶ󺸰� �ϱ� ���ؼ� ����
        }
        
       
    }
}
