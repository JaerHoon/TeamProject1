using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Buff : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject buffForth;

    [SerializeField]
    Skill_ScriptableObject buffBack;

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    [SerializeField]
    Image buffState;

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�
    bool isBuffRun = false;

    SpriteRenderer player; // ��������Ʈ������ ����
    float changeColorTime = 20.0f; // �÷��̾� ������ 20�ʰ� �����Ѵ�.
    float buffTime = 10.0f;
    public Color skillColor = new Color(1.0f, 0.5f, 0.5f, 1.0f); // ���ʴ�� R,G,B,���İ��̸� red�������� �����Ѵ�.
    Color originColor; 
    void Start()
    {
        skillimage.sprite = buffForth.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = buffForth.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;

        skillimage.sprite = buffBack.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = buffBack.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;

        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        originColor = player.color; // �÷��̾��� ���� ������ originColor�� ��´�.

        buffState.enabled = false;
    }

    public void ActivatedBuff()
    {
        if (isCoolTime) // ��Ÿ�����϶��� ����X
        {
            return;
        }

        SkillManager.instance.OnActivatedBuff();
        CoolTimeStart();
        StartCoroutine(changeColor(skillColor, changeColorTime));
        
        buffState.enabled = true;
        StartCoroutine(currentBuff());


    }

    IEnumerator changeColor(Color redColorChange, float changeTime)
    {
        player.color = redColorChange; // ���� ������ �� �÷��̾� ������ ������ ������ skillColor�� �����Ѵ�.
        yield return new WaitForSeconds(changeTime); // changeColorTime��ŭ ����Ѵ�.
        player.color = originColor; // changeColorTime ���Ŀ��� �÷��̾��� ������ ���� �������� �ǵ�����.

        
    }


    IEnumerator currentBuff()
    {
        yield return new WaitForSeconds(0.5f);
        buffState.enabled = false;
        yield return new WaitForSeconds(0.5f);
        buffState.enabled = true;
        StartCoroutine(currentBuff());

    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = buffForth.coolTime;
        skillCoolTime = buffBack.coolTime;

    }

    public void CoolTimeState()
    {
        float distance = skillCoolTime / maxskillCool; // ��ų ��Ÿ�� / �ִ���Ÿ��
        skillCoolTimeGauge.fillAmount = distance;
        //print(buffCoolTimeState);
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
