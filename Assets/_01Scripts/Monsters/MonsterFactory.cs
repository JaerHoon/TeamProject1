using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{//���� ��Ȱ�� ��ũ��Ʈ

    List<MonsterRecycle> pool = new List<MonsterRecycle>();//���� Ǯ ����Ʈ ����
    MonsterRecycle monsterPrefab;//���� ������
    int defaultMonsterNumber;//ó�� pool�� ���� ���� ��

    //������ ���� ���� ����
    public MonsterFactory(MonsterRecycle monsterPrefab, int defaultMonsterNumber = 5)
    {
        this.monsterPrefab = monsterPrefab;//�ܺο��� ���� ������ ���� 
        this.defaultMonsterNumber = defaultMonsterNumber;//�ܺο��� ���� ���� �� ����
        Debug.Assert(this.monsterPrefab != null, "���� ���丮�� ���� ������ ����");
    }


    //������Ʈ ����
    void CreatePool()
    {
        for (int i = 0; i < defaultMonsterNumber; i++)
        {//��Ȱ������ ������ ���� ����
            MonsterRecycle obj = GameObject.Instantiate(monsterPrefab) as MonsterRecycle;
            obj.gameObject.SetActive(false);//���� �� �ٷ� ��Ȱ��ȭ
            pool.Add(obj);//pool ����Ʈ�� ����
        }
    }

    //���� �ҷ�����
    public MonsterRecycle GetMonster()
    {
        if(pool.Count == 0) CreatePool();//pool�� �����ִ� ���Ͱ� ���ٸ� ���� ����
        int lastIndex = pool.Count - 1;// pool����Ʈ�� ������ �ε���
        MonsterRecycle obj = pool[lastIndex];//pool����Ʈ ������ ���͸� obj�� ����
        pool.RemoveAt(lastIndex);//��Ȱ�� ������ ����Ʈ ������ ���͸� ����Ʈ���� ����(��� ������ ����Ʈ���� �����ϱ� ����)
        obj.gameObject.SetActive(true);//����� ���� ���� Ȱ��ȭ
        return obj;//���� 
 

       
    }

    //�ݳ��Լ� ��Ȱ���ϱ� ����
    public void MonsterRestore(MonsterRecycle obj)
    {
        Debug.Assert(obj != null, "�ƹ��͵� ���� ������Ʈ�� ��ȯ�Ǿ�� �մϴ�");
        obj.gameObject.SetActive(false);//���� ���� ��Ȱ��ȭ
        pool.Add(obj);//��Ȱ���ϱ� ���� �ٽ� pool�� �߰�
    }
}
