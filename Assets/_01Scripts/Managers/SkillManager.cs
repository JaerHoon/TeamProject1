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

    
    void Start()
    {
        earthFactory = new SkillFactory(EarthPrefab, 1);
        windFactory = new SkillFactory(WindPrefab, 4);
        tornadoFactory = new SkillFactory(TornadoPrefab, 8);
        meteorFactory = new SkillFactory(MeteorPrefab, 1);
        BigmeteorFactory = new SkillFactory(BigMeteorPrefab, 1);
        //buffForthFactory = new SkillFactory(BuffForthPrefab, 1);
        //buffBackFactory = new SkillFactory(BuffBackPrefab, 1);
        angleWind();

        BuffForthPrefab.SetActive(false); // ����Ȱ��ȭ ������ �����Ҷ� ��Ȱ��ȭ
        BuffBackPrefab.SetActive(false); // ����Ȱ��ȭ ������ �����Ҷ� ��Ȱ��ȭ


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
    }

    public void OnTornadoAttack()
    {
   
        foreach (Vector2 direction in Tornadodir)
        {
            GameObject tornado = tornadoFactory.GetSkill();
            tornado.transform.position = playerTr.position;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * tornadoSpeed;

        }
        
        
    }

    public void OnWindAttack()
    {
        for (int i = 0; i < Winddir.Length; i++)
        {
            GameObject wind = windFactory.GetSkill();
            wind.transform.position = playerTr.position;
            wind.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
        }
        
    }

    public void OnMeteorAttack()
    {
        GameObject meteor = meteorFactory.GetSkill();
        float posX = Random.Range(-2, 13);
        meteor.transform.position = new Vector2(posX, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);
        

    }

    public void OnBigMeteorAttack()
    {
        GameObject meteor = BigmeteorFactory.GetSkill();
        meteor.transform.position = new Vector2(7, 7);
        meteor.transform.rotation = Quaternion.Euler(0, 0, -130.0f);
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
