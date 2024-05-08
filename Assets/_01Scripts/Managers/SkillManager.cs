using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (SkillManager.instance == null)
            SkillManager.instance = this;

        
    }


    [SerializeField]
    Skill_ScriptableObject earth;


    [SerializeField]
    Skill_ScriptableObject tornado;


    [SerializeField]
    Skill_ScriptableObject wind;


    [SerializeField]
    Skill_ScriptableObject meteor;


    [SerializeField]
    GameObject EarthPrefab;

    [SerializeField]
    GameObject WindPrefab;

    [SerializeField]
    GameObject TornadoPrefab;

    [SerializeField]
    GameObject MeteorPrefab;

    [SerializeField]
    GameObject BigMeteorPrefab;

    [SerializeField]
    GameObject BuffForthPrefab;

    [SerializeField]
    GameObject BuffBackPrefab;

    [SerializeField]
    Transform playerTr;

    [SerializeField]
    Image skillimage;

    [SerializeField]
    Image skillCoolTimeGauge; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    [SerializeField]
    Image skillCoolTimeGauge2; // ��ų ��Ÿ�� ������ ǥ�� �̹���

    SkillFactory skillfactory;
    SkillFactory earthFactory;
    SkillFactory windFactory;
    SkillFactory tornadoFactory;
    SkillFactory meteorFactory;
    SkillFactory BigmeteorFactory;
    //SkillFactory buffForthFactory;
    //SkillFactory buffBackFactory;
    public Transform earthPos;
    



    float tornadoSpeed = 5.0f;
    
    Vector2[] Tornadodir = { Vector2.up, Vector2.down, Vector2.right, Vector2.left, Vector2.down+Vector2.left,
                      Vector2.up+Vector2.right, Vector2.up+Vector2.left, Vector2.down+Vector2.right};

    Vector2[] Winddir = { Vector2.up + Vector2.right, Vector2.down+Vector2.left,
                      Vector2.up + Vector2.left, Vector2.down + Vector2.right }; // �� �밢�� �������� ��ų�� ������.
    float[] angles; // Wind�� ȸ������ �迭�� �Ҵ�


    public GameObject[] Slot = new GameObject[3];
    public Image[] cooltimeObject = new Image[3];
    public Button[] button = new Button[3];

    int[] skillSlot = new int[3];//��ų ���Կ� ������ ��ų ��ȣ�� ǥ��
    public Skill_ScriptableObject equipskills;
    public GameObject skillslot1;

    bool isEarthCoolTime = true;
    
    [SerializeField]
    GameObject[] slot0skillimages;
    [SerializeField]
    GameObject[] slot1skillimages;
    /*********************��Ÿ�� ���� ����****************************/
    bool isCoolTime = false; // ��Ÿ�� �÷���
    float skillCoolTime = 0f; // �ʱ� ��Ÿ�Ӱ�
    float maxskillCool; // �ִ� ��Ÿ�Ӱ�
    void Start()
    {
        earthFactory = new SkillFactory(EarthPrefab, 1); // Earth���� ���丮 ��������
        windFactory = new SkillFactory(WindPrefab, 4); // wind ���� ���丮 ��������
        tornadoFactory = new SkillFactory(TornadoPrefab, 8); // tornado ���� ���丮 ��������
        meteorFactory = new SkillFactory(MeteorPrefab, 1); 
        BigmeteorFactory = new SkillFactory(BigMeteorPrefab, 1); // meteor���� ���丮 ��������
        //buffForthFactory = new SkillFactory(BuffForthPrefab, 1);
        //buffBackFactory = new SkillFactory(BuffBackPrefab, 1);
        angleWind();

        

        BuffForthPrefab.SetActive(false); // ����Ȱ��ȭ ������ �����Ҷ� ��Ȱ��ȭ
        BuffBackPrefab.SetActive(false); // ����Ȱ��ȭ ������ �����Ҷ� ��Ȱ��ȭ
        skillSlot[0] = 1; // 0�� ��ų ���Կ� 1�� ��ų �ߵ�
        skillSlot[1] = 3; // 1�� ��ų ���Կ� 3�� ��ų �ߵ�
        skillSlot[2] = 5; // 2�� ��ų ���Կ� 5�� ��ų �ߵ�
        
        skillCoolTimeGauge.fillAmount = 0f;
        skillCoolTimeGauge2.fillAmount = 0f;
        skillslot1 = GameObject.Find("skillslot1");

        //skillslot1.transform.GetChild(0).gameObject.SetActive(false);
        //skillslot1.transform.GetChild(1).gameObject.SetActive(true);
        //skillslot1.transform.GetChild(2).gameObject.SetActive(false);
        //skillslot1.transform.GetChild(3).gameObject.SetActive(false);

        

        //if (skillSlot[0] == 1)
        {
            //skillimage.sprite = earth.icon;
        }


    }

    void Oncooltime(int slotnum)
    {
        //Slot[slotnum]�ȿ� ����ִ� ��ų �Լ� ����
        // ��ŸƮ �ڷ�ƾ(Slot[sslotnum].��ų��Ÿ�����Ű�����1, cooltimeOBJ[slotnum])
        // button �� �� 
    }

    IEnumerator Startcooltimeg(float cooltime, int  slotnum)
    {
        yield return new WaitForSeconds(0.1f);
        //cooltimeObject[slotnum]

        button[slotnum].enabled = true;
        //��Ÿ�� ������ �ٽ� ��ư�� ��.
    }

    public void OnclickSkill(int slotNumber)
    {
        if (slotNumber == 0)
        {
            print("0�� ���� ��ų �Դϴ�");
            SkillUse(skillSlot[0]); // 0�� �����϶� 1�� � ��ų �ߵ�
            CoolTimeState(skillCoolTimeGauge, skillSlot[0], button[0]);
        }
        else if (slotNumber == 1)
        {
            print("1�� ���� ��ų �Դϴ�");
            SkillUse(skillSlot[1]); // 1�� �����϶� 3�� ���� ��ų �ߵ�
            CoolTimeState(skillCoolTimeGauge2, skillSlot[1], button[1]);
        }
        else
        {
            print("2�� ���� ��ų �Դϴ�");
            SkillUse(skillSlot[2]); // 2�� �����϶� 5�� ���� ��ų �ߵ�
        }
    }

    void SkillUse(int skilNum)
    {
        switch (skilNum)
        {
            case 0:
                print("��ų�� �������� �ʽ��ϴ�!!");
                break;
            case 1:
                print("� �ߵ�!!");
                OnEarthAttack();
                break;
            case 2:
                print("����̵� �ߵ�!!");
                OnTornadoAttack();
                break;
            case 3:
                print("���� �ߵ�!!");
                OnWindAttack();
                break;
            case 4:
                print("���׿� �ߵ�!!");
                OnBigMeteorAttack();
                break;
            case 5:
                print("���� �ߵ�!!");
                break;
            default:
                break;



        }
    }

    public void CoolTimeState(Image skillcoolTime, int skillNum, Button button)
    {
       
        float skillCool=0;
        button.enabled = false;
        if (skillNum == 1) skillCool = earth.coolTime;
        else if (skillNum == 2) skillCool = tornado.coolTime;
        else if (skillNum == 3) skillCool = wind.coolTime;
        else if (skillNum == 4) skillCool = meteor.coolTime;

        StartCoroutine(startSkillCoolTime(skillcoolTime, skillCool, button));
    }

    IEnumerator startSkillCoolTime(Image coolTimeimage, float coolTime, Button Getbutton)
    {   
        float subCoolTime = 1 / coolTime;
        
        int i = 0;
        coolTimeimage.fillAmount = 1;
        while(coolTimeimage.fillAmount>=0.001f)
        {

            coolTimeimage.fillAmount -= subCoolTime * Time.deltaTime;

            yield return new WaitForFixedUpdate();
            i++;
            if (i > 1000)
                break;
        }
        Getbutton.enabled = true;
       
    }

    public void OnClickchangedskill()
    {
        for (int i = 1; i < slot0skillimages.Length; i++)
        {
            slot0skillimages[i].SetActive(false);
        }
        slot0skillimages[skillSlot[0]].SetActive(true);


        for (int i = 1; i < slot1skillimages.Length; i++)
        {
            slot1skillimages[i].SetActive(false);
        }
        slot1skillimages[skillSlot[1]].SetActive(true);
        
    }

    void angleWind() // �� �迭�� Rotation���� �Ҵ��� ���Ҵ�.
    {
        angles = new float[4];


        angles[0] = 30f;
        angles[1] = 150f;
        angles[2] = -150f;
        angles[3] = -30f;
    }


    public void OnEarthAttack()
    {

       
        GameObject earth = earthFactory.GetSkill();
        earth.transform.position = earthPos.position;

        //isCoolTime = true;
       // skillCoolTime = this.earth.coolTime;
        //maxskillCool = this.earth.coolTime;

    

    }


    public void OnTornadoAttack()
    {
        if (isCoolTime) // ��Ÿ���϶� ��ų��ư ������ ���� �ȳ���
        {
            return;
        }


        foreach (Vector2 direction in Tornadodir)
        {
            GameObject tornado = tornadoFactory.GetSkill();
            tornado.transform.position = playerTr.position;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * tornadoSpeed;

        }

        isCoolTime = true;
        skillCoolTime = tornado.coolTime;
        maxskillCool = tornado.coolTime;

    }

    public void OnWindAttack()
    {
        

        for (int i = 0; i < Winddir.Length; i++)
        {
            GameObject wind = windFactory.GetSkill();
            wind.transform.position = playerTr.position;
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
        }

        //isCoolTime = true;
        //skillCoolTime = wind.coolTime;
        //maxskillCool = wind.coolTime;
    }

    public void OnMeteorAttack()
    {
        if (isCoolTime) // ��Ÿ���϶� ��ų��ư ������ ���� �ȳ���
        {
            return;
        }

        GameObject meteor = meteorFactory.GetSkill();
        float posX = Random.Range(-2, 13);
        meteor.transform.position = new Vector2(posX, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);

        isCoolTime = true;
        skillCoolTime = this.meteor.coolTime;
        maxskillCool = this.meteor.coolTime;
    }

    public void OnBigMeteorAttack()
    {
        if (isCoolTime) // ��Ÿ���϶� ��ų��ư ������ ���� �ȳ���
        {
            return;
        }

        GameObject meteor = BigmeteorFactory.GetSkill();
        meteor.transform.position = new Vector2(7, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);

        isCoolTime = true;
        skillCoolTime = this.meteor.coolTime;
        maxskillCool = this.meteor.coolTime;
    }

    public void OnActivatedBuff()
    {
        BuffForthPrefab.SetActive(true);
        BuffBackPrefab.SetActive(true);
        StartCoroutine(DestroyBuff());
    }

    IEnumerator DestroyBuff()
    {
        yield return new WaitForSeconds(20.0f);
        BuffForthPrefab.SetActive(false);
        BuffBackPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
