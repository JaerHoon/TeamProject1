using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data")]

public class Quest_ScriptableObject : ScriptableObject
{
    public string questName;
    public int numberOfGoal;//��ǥ�޼�Ƚ��
    public int currentNumber;//���� Ƚ��
    public int rewordAmount;//������ ��
}
