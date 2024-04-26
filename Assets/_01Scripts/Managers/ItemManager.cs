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
        ListItem();
    }

    void ListItem() // �����۵����͸� ž�� �߽����� �ٽ� �����ؼ� ����Ʈȭ �ϴ� �Լ�
    {
        List<Item_ScriptableObject> itemdatas = new List<Item_ScriptableObject>(); //�������� �޾Ƽ� ���Ӱ� �����ϱ� ���� ������ �Լ�

        int eVaues = Enum.GetValues(typeof(Item_ScriptableObject.ItemType)).Length;//������ Ÿ���� ������ ���� ����
        
        for (int i=0; i<eVaues; i++)
        {
            itemdatas = item_ScriptableObject.Where(n => n.itemType == (Item_ScriptableObject.ItemType)i).ToList();
            CreatItem(itemdatas);
            itemdatas.Clear();
        }
       
    } 
    
    void CreatItem(List<Item_ScriptableObject> itemdatas)// ������ ��ü ���� �� �� �����۰�ü�� �⺻ ������ ���ִ� �Լ� 
    {
        for (int i = 0; i < itemdatas.Count; i++)
        {
            Item item = new Item(); //������ ��ü ����
            item.itemData = itemdatas[i]; // CreatItem() �Լ����� �޾ƿ� itemdatas �����͸� �ֽ��ϴ�. 
            item.itemGrade = Item.ItemGrade.Nomal; // ������ ��� �Է�
            item.Setting(backgrounds[0], slots[0]); // ������ ���� ��׶���� ������ ��������Ʈ �Է�
            item.ItemLv = 1; // ������ ���� �ʱ�ȭ
            items.Add(item);// ������ ������ ��ü�� items����Ʈ�� ����
            for (int a = 0; a < 4; a++)// ������ ������ item�� 4���� ����� �� �������ֱ� ���� �ݺ���
            {
                Item item2 = new Item();
                item2.itemData = itemdatas[i];
                item2.itemGrade = (Item.ItemGrade)a + 1;
                item2.Setting(backgrounds[a + 1], slots[a + 1]);
                item2.ItemLv = 1;
                items.Add(item2);
            }
        } 
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
                print(i +"/"+items[i].itemGrade);
            }
            
        }
    }*/


}
