using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkills : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject skills; // ��ų ��ũ���ͺ� ������Ʈ



    [SerializeField]
    Transform skilsPos; // ��ų ���� ��ġ

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�
    void Start()
    {

        skillimage.sprite = skills.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = skills.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;
    }

    public void SkillAttack()
    {
        if (isCoolTime) // ��Ÿ���϶� ��ų��ư ������ ���� �ȳ���
        {
            return;
        }

        SkillManager.instance.OnEarthAttack();
        CoolTimeStart();


    }

    
    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = skills.coolTime;
        
    }

   
    public void CoolTimeState()
    {
        float cooltime = skillCoolTime / maxskillCool; // ��ų ��Ÿ�� / �ִ���Ÿ��
        skillCoolTimeGauge.fillAmount = cooltime;
    }

    


    // Update is called once per frame
    void Update()
    {
        if(isCoolTime)
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
