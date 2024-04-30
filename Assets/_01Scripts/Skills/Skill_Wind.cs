using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill_Wind : MonoBehaviour
{
    [SerializeField]
    Skill_ScriptableObject Wind; // ��ũ���ͺ� ������Ʈ Wind ����
    

    [SerializeField]
    Image skillimage; // ��ų ������ �̹���

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�
  

    Vector2[] dir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // �� �밢�� �������� ��ų�� ������.

    
    float[] angles; // Wind�� ȸ������ �迭�� �Ҵ�
    void Start()
    {
        skillimage.sprite = Wind.icon; // ��ų�̹��� ��������Ʈ�� ��ũ���ͺ� ������Ʈ�� ���� ������ ��������Ʈ�� ǥ��
        maxskillCool = Wind.coolTime; // �ִ� ��Ÿ�� = ��ũ���ͺ� ������Ʈ���� �ۼ��� ��Ÿ��
        skillCoolTimeGauge.fillAmount = 0f;

        
        angleWind();
    }

    

    public void angleWind() // �� �迭�� Rotation���� �Ҵ��� ���Ҵ�.
    {
        angles = new float[4];

        
        angles[0] = 30f;
        angles[1] = 150f;
        angles[2] = -150f;
        angles[3] = -30f;
    }

    public void WindShoot()
    {
        if (isCoolTime) // ��Ÿ�����϶��� ����X
        {
            return;
        }

        
        for (int i = 0; i < dir.Length; i++)
        {
            Vector2 pos = transform.position; // ��ų �߻�Ǵ� ��ġ => �÷��̾� ������� �ϱ� ����
            GameObject wind = Instantiate(Wind.skillPrefab, pos, Quaternion.identity);
            CoolTimeStart(); // ��ų �ߵ����� �� ��Ÿ���� ���ư���.

            // �ڿ������� �밢������ �߻�Ǵ� ��ų�� ����� ���� Vector2 �迭���� ������ ������ angles �迭�� �Ҵ��Ѵ�.
            // �آ�
            // �ע�  <= �̷�������� �߻�ǰ� ����� ���ؼ� ������ ����.
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]); 
                                                                         
            
            
        }

        


    }

    public void CoolTimeStart()
    {
        isCoolTime = true;
        skillCoolTime = Wind.coolTime; // ��ų ��Ÿ�� ���� ��ũ���ͺ� ������Ʈ���� ������ Wind ��Ÿ�� ������ �����.

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

