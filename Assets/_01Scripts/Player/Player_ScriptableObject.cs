using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class Player_ScriptableObject : ScriptableObject
{
    public int playerDamage; // 10���� ����
    public int playerHP; // 100���� ����
    public int playerDefence; // 10���� ����
    public int playerCritical; // 5�� ����
    public int playerCriticalPower;
}
