using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data")]
public class Item_ScriptableObject : ScriptableObject
{
    public enum ItemType { Weapon, Shield, Helmet, Armor, Shoes, Accessories }
    public ItemType itemType;
    public enum StatUPType{ Health, Attack, Defence, CrtRate }
    public StatUPType statUPType;

    public string itemname;
    public string koreanName;
    public Sprite icon;
    public int itemPow;//�⺻���� ������ ���� �� ȿ��
    public int LvUP_Rate;
    
}
