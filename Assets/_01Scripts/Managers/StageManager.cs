using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class StageManager : MonoBehaviour,IQuestChecker

{

    public static StageManager instance;//���� �Ŵ��� �̱��� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //=============����=================
    public Button stageStartButton;
    public TextMeshProUGUI stageText;

    public GameObject stageRestartBt;
    public GameObject gameOverPanel;

    [SerializeField]
    StageScData StageData;

    [SerializeField]
    int buttonDamage;

    Sequence textSequence;

    public Action<StageScData, int> StartWave;


    bool isStartStage = false;


    public Quest_ScriptableObject.QuestType questType { get; set; }

    private void Start()
    {
        stageText.text = "";
        gameOverPanel.SetActive(false);
        GOPalpha = gameOverPanel.GetComponent<Image>().color;
        Invoke("OnStartWave", 1.5f);
    }


    public void OnStartWave()//��ư���� �������� ���� �̺�Ʈ ���� 
    {
        
        SpawnManager.instance.RestoreNum = 0;//�������� �ʱ�ȭ �� ���� ���� �� �ʱ�ȭ
        SpawnManager.instance.allSpawnNum = 0;//�������� �ʱ�ȭ �� ���� ���� �� �ʱ�ȭ
        stageStartButton.interactable = false;
        ChangeStageText();//�������� �ؽ�Ʈ �̵� �� ����
        StartCoroutine(DelayStageStart());//�ؽ�Ʈ �̵� �ð� Ȯ��

    }

    void ChangeStageText()
    {
        stageText.enabled = true;
        string stageTx = "STAGE 1-" + (StageData.currentStageNum + 1).ToString();
        stageText.text = stageTx;
        textSequence = DOTween.Sequence();
        stageText.transform.position = Vector3.zero;
        Color color = stageText.color;
        color.a = 0;
        stageText.color = color;
        StartCoroutine(DelayStageAlhpa());
        textSequence.Append(stageText.transform.DOScale(new Vector3(2, 2, 1), 1.5f));
        textSequence.Append(stageText.transform.DOMove(new Vector3(-0.2f, 3.7f, 0), 0.7f).SetEase(Ease.OutCubic));
        textSequence.Join(stageText.transform.DOScale(new Vector3(0.68f, 0.68f, 1), 0.7f));

    }

    public void OnPlayerDie()
    {
        gameOverPanel.SetActive(true);
        stageRestartBt.SetActive(false);
        StartCoroutine(FadeOutGameOverPanel());
    }

    Color GOPalpha;

    IEnumerator FadeOutGameOverPanel()
    {
        while (GOPalpha.a <= 0.98)
        {
            GOPalpha.a = Mathf.Lerp(GOPalpha.a, 1, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
            gameOverPanel.GetComponent<Image>().color = GOPalpha;
            if (GOPalpha.a > 0.9f && GOPalpha.a < 0.95f)
            {
                stageRestartBt.SetActive(true);
                yield break;
            }
        }
    }

    IEnumerator FadeInGameOverPanel()
    {
        while (GOPalpha.a >= 0.02)
        {
            GOPalpha.a = Mathf.Lerp(GOPalpha.a, 0, Time.deltaTime * 2);
            yield return new WaitForFixedUpdate();
            gameOverPanel.GetComponent<Image>().color = GOPalpha;
            if (GOPalpha.a <= 0.1f)
            {
                gameOverPanel.SetActive(false);
                yield break;
            }
        }
       
    }


    IEnumerator DelayStageAlhpa()
    {
        Color color = stageText.color;
        float i = 0;
        while (i < 1)
        {
            i += 0.01f;
            color.a = i;
            stageText.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator DelayStageStart()
    {
        yield return new WaitForSeconds(2.5f);
        StartWave?.Invoke(StageData, StageData.currentStageNum);//�������� ���� �̺�Ʈ �߻�
    }

    public void OnStageWin()//�������� Ŭ���� �̺�Ʈ ���� �Լ�
    {

        StageData.currentStageNum++;//���� �������� ���
        if(StageData.currentStageNum > 5)
        {
            UpdateQuestInfo();
        }
        if (StageData.currentStageNum >= StageData.Stages.Length)//������ �������� Ŭ���� �� ó�� ���������� ���ƿ�
            StageData.currentStageNum = 0;
        OnStartWave();

    }

    public void OnStageMonsterClear()//�������� Ŭ���� �̺�Ʈ ���� �Լ�
    {
        print("AAA");
        SpawnManager.instance.OnDestroyAllMonster();
        StartCoroutine(FadeInGameOverPanel());
        Invoke("OnStartWave", 3.0f);
    }

    public void OnStageMonsterDamaged()//�������� �� ��� ���� �ǰ� �̺�Ʈ ���� �Լ�
    {
        SpawnManager.instance.OnDamagedAllMonster(buttonDamage);
    }

    public void OnStageMonsterAllDie()//�������� �� ��� ���� �ǰ� �̺�Ʈ ���� �Լ�
    {
        SpawnManager.instance.OnDamagedAllMonster(9999);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuestInfo()
    {
        QuestManager.instance.UpDateQuest(Quest_ScriptableObject.QuestType.StageClear);
    }
}
