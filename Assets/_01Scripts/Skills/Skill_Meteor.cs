using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Meteor : MonoBehaviour
{
    public static Skill_Meteor instance;

    private void Awake()
    {
        if (Skill_Meteor.instance == null)
            Skill_Meteor.instance = this;
    }

    [SerializeField]
    Skill_ScriptableObject meteor;
    float meteorSpeed = 3.0f;

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�

    float meteorTime = 0;
    float meteorDelay = 1.5f;
    void Start()
    {
        skillimage.sprite = meteor.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = meteor.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;
    }

    public void meteorShoot()
    {

        if (isCoolTime) // ��Ÿ�����϶��� ����X
        {
            return;
        }
        SkillManager.instance.OnMeteorAttack();
        CoolTimeStart();
        


    }

    

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = meteor.coolTime;

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
