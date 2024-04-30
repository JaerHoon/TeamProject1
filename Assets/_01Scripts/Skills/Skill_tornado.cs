using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_tornado : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject tornado;
    float tornadoSpeed = 3.0f;

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�

    Vector2[] dir = { Vector2.up, Vector2.down, Vector2.right, Vector2.left, Vector2.down+Vector2.left,
                      Vector2.up+Vector2.right, Vector2.up+Vector2.left, Vector2.down+Vector2.right};
    
    void Start()
    {
        skillimage.sprite = tornado.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = tornado.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;
    }

    public void tornadoShoot()
    {
        if(isCoolTime)
        {
            return;
        }

        foreach (Vector2 direction in dir)
        {
            GameObject Tornado = Instantiate(tornado.skillPrefab, transform.position, Quaternion.identity);
            CoolTimeStart();
            Rigidbody2D rb = Tornado.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized* tornadoSpeed;
            
        }
        
        
    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = tornado.coolTime;

    }

    public void CoolTimeState()
    {
        float cooltime = skillCoolTime / maxskillCool; // ��ų ��Ÿ�� / �ִ���Ÿ��
        skillCoolTimeGauge.fillAmount = cooltime;
    }


    

    // Update is called once per frame
    void Update()
    {
        if (isCoolTime)
        {
            skillCoolTime -= Time.deltaTime; // Time�� ��ŭ ��Ÿ�� ���ݾ� �����Ѵ�.
            CoolTimeState();
            if (skillCoolTime <= 0f)
            {
                isCoolTime = false;

            }
        }
    }
}
