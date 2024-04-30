using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Wind : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject Wind;
    float WindSpeed = 3.0f;

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�
    public float rotateSpeed = 30.0f;

    Vector2[] dir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // �� �밢�� �������� ��ų�� ������.

    
    void Start()
    {
        skillimage.sprite = Wind.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = Wind.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;

        
    }

    public void WindShoot()
    {
        if (isCoolTime) // ��Ÿ�����϶��� ����X
        {
            return;
        }

        foreach (Vector2 direction in dir)
        {
            GameObject wind = Instantiate(Wind.skillPrefab, transform.position, Quaternion.identity);
            CoolTimeStart(); // ��ų �ߵ��ǰ� ���� ��Ÿ�� ����
            Rigidbody2D rb = wind.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * WindSpeed;
            

        }

        


    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = Wind.coolTime;

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

