using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RecyclableMonster
{
    public Transform playerPosition;
    float angle;
    bool isShoot = false;
    [SerializeField]
    float moveSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        isShoot = false;

    }
    private void OnDisable()
    {
        isShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroyed?.Invoke(this);
        }
    }

    private void OnBecameInvisible()//ȭ�� ������ ���� ��
    {
        isActivated = false;
        Destroyed?.Invoke(this);//��Ȱ��ȭ �̺�Ʈ �߻�
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShoot)//���� �ʾ�����
        {
            angle = Mathf.Atan2(playerPosition.position.y - transform.position.y, playerPosition.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);//���̿� ���� �÷��̾� �ٶ󺸱�
            isShoot = true;
        }
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);//���̾� �� ���ư���
    }
}
