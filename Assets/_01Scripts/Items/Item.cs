using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public Item_ScriptableObject itemData;
    int itemLV;
    int MaxLv=20;

    public Sprite backGround;
    public Sprite slot;


    public int ItemLv
    {
        get
        {
            return itemLV;
        }
        set
        {
            if(value > MaxLv)
            {
                itemLV = MaxLv;
            }
            else
            {
                itemLV = value;
            }
        }
    }
   
    public enum ItemGrade { Nomal, Rare, Unique, Epic, Legend }
    public ItemGrade itemGrade;

    public bool isGained = false;


    public void Setting(Sprite background, Sprite slot)
    {
        this.backGround = background;
        this.slot = slot;
    }


    int CalItempow()
    {
        int num=0;
        int itempow;

        switch (itemGrade)
        {
            case ItemGrade.Nomal: num = 1;break;
            case ItemGrade.Rare: num = 2; break;
            case ItemGrade.Unique: num = 3; break;
            case ItemGrade.Epic: num = 4; break;
            case ItemGrade.Legend: num = 5; break;
        }

        itempow = itemData.itemPow * num;

        return itempow;
    }


    public int Cal_LevelupPow()// ���� ���� �ö󰡴� ������ �� ���
    {
        int pow;
        if(itemLV <= 1)//������ 1�϶� ���
        {
            pow = CalItempow() *  itemLV;
        }
        else
        {
            pow = CalItempow() * itemData.LvUP_Rate * itemLV;
            //������ 2�̻��϶� ���
        }
       

        return pow;
    }


}
