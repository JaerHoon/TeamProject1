using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField]
    List<Item_ScriptableObject> item_ScriptableObject = new List<Item_ScriptableObject>(); // item������ ����Ʈ
    [SerializeField]
    List<Sprite> backgrounds = new List<Sprite>();// ��޺� ������ ���� ��׶��� ��������Ʈ ����Ʈ
    [SerializeField]
    List<Sprite> slots = new List<Sprite>(); // ��޺� ������ ������ ���� ��������Ʈ ����Ʈ

   
    public Sprite defaultBackGround; // �󽽷� ��׶���
    public Sprite defaultSlot;

    public List<Sprite> equipDefaultItem = new List<Sprite>();//������ ���� ����Ʈ ������
   

    public List<Item> items = new List<Item>(); // ������ ������ ��ü�� ���� ����Ʈ
    public List<Item> gainedItems = new List<Item>(); // �÷��̾ ���� ������ ��ü�� ���� ����Ʈ

    
    private void Awake()
    {
        if(instance == null)//�̱���
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        CreatItem();
    }

    void CreatItem()
    {
        int eVaues = Enum.GetValues(typeof(Item.ItemGrade)).Length;

        List<Item> itemdatas = new List<Item>();

        for (int i=0; i < item_ScriptableObject.Count; i++)
        {
           
            for (int a = 0; a < eVaues; a++)
            {
                Item item = new Item()
                {
                    itemData = item_ScriptableObject[i],
                    itemGrade = (Item.ItemGrade)a,
                    backGround = backgrounds[a],
                    slot = slots[a],
                    ItemLv = 1

                };

                itemdatas.Add(item);
            }
        }

        items = itemdatas.OrderBy(data => data.itemData.itemType)
                .ThenBy(data => data.itemData.itemPow)
                .ThenBy(data =>data.itemGrade)
                .ToList();
    
    }

    
    public void GetItem(int itemnum)//�÷��̾ �������� ������� �����ϴ� �Լ�
    {
        gainedItems.Add(items[itemnum]);// �������� gainedItems����Ʈ�� ����
        items.RemoveAt(itemnum); // items ����Ʈ���� ����
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < items.Count; i++)
            {
                print(i +"/"+items[i].itemData.koreanName+"/"+ items[i].itemGrade);
            }
            
        }
    }*/


}
