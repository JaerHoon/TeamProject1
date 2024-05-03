using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    //���� ��Ȱ�� ��ũ��Ʈ

    List<GameObject> pool = new List<GameObject>();//���� Ǯ ����Ʈ ����
    GameObject skillPrefab;//���� ������
    int defaultCoinNumber;//ó�� pool�� ���� ���� ��

    //������ ���� ���� ����
    public SkillFactory(GameObject skillPrefab, int defaultCoinNumber = 4)
    {
        this.skillPrefab = skillPrefab;//�ܺο��� ���� ������ ���� 
        Debug.Log(skillPrefab.name);
        this.defaultCoinNumber = defaultCoinNumber;//�ܺο��� ���� ���� �� ����
        Debug.Assert(this.skillPrefab != null, "���� ���丮�� ���� ������ ����");
    }


    //������Ʈ ����
    void CreatePool()
    {
        for (int i = 0; i < defaultCoinNumber; i++)
        {//��Ȱ������ ������ ���� ����
            GameObject obj = GameObject.Instantiate(skillPrefab) as GameObject;
            obj.gameObject.SetActive(false);//���� �� �ٷ� ��Ȱ��ȭ
            pool.Add(obj);//pool ����Ʈ�� ����
        }
    }

    //���� �ҷ�����
    public GameObject GetSkill()
    {
        if (pool.Count == 0) CreatePool();//pool�� �����ִ� ���Ͱ� ���ٸ� ���� ����
        int lastIndex = pool.Count - 1;// pool����Ʈ�� ������ �ε���
        GameObject obj = pool[lastIndex];//pool����Ʈ ������ ���͸� obj�� ����
        pool.RemoveAt(lastIndex);//��Ȱ�� ������ ����Ʈ ������ ���� ����Ʈ���� ����(��� ������ ����Ʈ���� �����ϱ� ����)
        obj.gameObject.SetActive(true);//����� ���� ���� Ȱ��ȭ
        return obj;//���� 



    }

    //�ݳ��Լ� ��Ȱ���ϱ� ����
    public void CoinRestore(GameObject obj)
    {
        Debug.Assert(obj != null, "�ƹ��͵� ���� ������Ʈ�� ��ȯ�Ǿ�� �մϴ�");
        obj.gameObject.SetActive(false);//���� ���� ��Ȱ��ȭ
        pool.Add(obj);//��Ȱ���ϱ� ���� �ٽ� pool�� �߰�
    }
}
