using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Meteor : MonoBehaviour
{
    
    [SerializeField]
    Skill_ScriptableObject meteor; 

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�

    int skillNumber = 4;
    float nextSkillTime = 0.5f;
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
        SkillManager.instance.OnBigMeteorAttack(); // ��ų�Ŵ������� ���� �Լ��� �ҷ��´�.
        CoolTimeStart();



    }

    IEnumerator MeteorCopy()
    {
        int usedMeteor = 0;

        while (usedMeteor < skillNumber)
        {
            usedMeteor++;
            yield return new WaitForSeconds(nextSkillTime);
        }

        CoolTimeStart();
        for (int i = 0; i < skillNumber; i++)
        {
            SkillManager.instance.OnMeteorAttack();
            yield return new WaitForSeconds(nextSkillTime);


        }
        yield return new WaitForSeconds(0.6f);
        SkillManager.instance.OnBigMeteorAttack();


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
