using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Item 
{
    public Item_ScriptableObject itemData;
    [SerializeField]
    int itemLV;
    int MaxLv=10;

    public int ItemLv
    {
        get
        {
            return itemLV;
        }
        set
        {
            if (value > MaxLv)
            {
                itemLV = MaxLv;
            }
            else
            {
                itemLV = value;
            }
        }
    }


    public Sprite backGround;
    public Sprite slot;
    public string statUPType;
    public enum ItemGrade { Nomal, Rare, Unique, Epic, Legend }
    public ItemGrade itemGrade;
    

   

    public void Setting(Sprite background, Sprite slot)
    {
        this.backGround = background;
        this.slot = slot;
        if (itemData == null) return;
        switch (itemData.statUPType)
        {
            case Item_ScriptableObject.StatUPType.Health:
                statUPType = "�����";
                break;
            case Item_ScriptableObject.StatUPType.Attack:
                statUPType = "���ݷ�";
                break;
            case Item_ScriptableObject.StatUPType.Defence:
                statUPType = "����";
                break;
            case Item_ScriptableObject.StatUPType.CrtRate:
                statUPType = "ġ��ŸȮ��";
                break;

        }
    }


    int CalItempow()
    {
        int num= (int)itemGrade+1;
        int itempow = itemData.itemPow * num;

        return itempow;
    }

    
    public int Cal_LevelupPow(int Lv)// ���� ���� �ö󰡴� ������ �� ���
    {
        int pow;
        if(Lv <= 1)//������ 1�϶� ���
        {
            pow = CalItempow() *  Lv;
        }
        else
        {
            pow = CalItempow() * itemData.LvUP_Rate * Lv;
            //������ 2�̻��϶� ���
        }
       
        return pow;
    }

    public string Cal_Text(int Lv)
    {
        string st;

        if(itemData.itemType == Item_ScriptableObject.ItemType.Accessories)
        {
            st = String.Format("{0}%", Cal_LevelupPow(Lv)/10);
        }
        else
        {
            st = Cal_LevelupPow(Lv).ToString();
        }


        return st;
    }


}
