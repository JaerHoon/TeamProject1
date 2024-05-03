using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StageManager : MonoBehaviour
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
    public Button _1StartButton;
    public Button _2StartButton;
    public Button _3StartButton;
    public Button _4StartButton;

    [SerializeField]
    StageData currentStageData;
    [SerializeField]
    int buttonDamage;

    public StageData StageData1;
    public StageData StageData2;
    public StageData StageData3;
    public StageData StageData4;
    public StageData StageData5;
    public Action<StageData> StartWave;

    
    public int StageNum = 1;
    bool isStartStage = false;

    public void OnStartWave()//��ư���� �������� ���� �̺�Ʈ ���� 
    {

        stageStartButton.interactable = false;
        ButtonActivate(false);//��� ��ü���� ��ư ��Ȱ��ȭ
        if (StageNum == 1)
            StartWave?.Invoke(StageData1);//�������� ���� �̺�Ʈ �߻�
        else if(StageNum == 2)
            StartWave?.Invoke(StageData2);
        else if (StageNum == 3)
            StartWave?.Invoke(StageData3);
        else if (StageNum == 4)
            StartWave?.Invoke(StageData4);
        else
            StartWave?.Invoke(StageData5);
    
    }

    public void OnStageWin()//�������� Ŭ���� �̺�Ʈ ���� �Լ�
    {
        
        isStartStage = false;//��ư Ȱ��ȭ
        StageNum++;//���� �������� ���
        currentStageData.currentStageNum = StageNum;//�������� ���� ����
       
    }

    public void OnStageMonsterClear()//�������� Ŭ���� �̺�Ʈ ���� �Լ�
    {
        SpawnManager.instance.OnDestroyAllMonster();
        stageStartButton.interactable = true;
        ButtonActivate(true);
    }

    public void OnStageMonsterDamaged()//�������� �� ��� ���� �ǰ� �̺�Ʈ ���� �Լ�
    {
        SpawnManager.instance.OnDamagedAllMonster(buttonDamage);
    }

    public void OnStageMonsterAllDie()//�������� �� ��� ���� �ǰ� �̺�Ʈ ���� �Լ�
    {
        SpawnManager.instance.OnDamagedAllMonster(9999);
    }

    public void OnChangeStage(int num)
    {
        if(num == 1)
        {
            StageNum = 1;
            ButtonActivate(true);
            _1StartButton.interactable = false;
        }
        else if(num == 2)
        {
            StageNum = 2;
            ButtonActivate(true);
            _2StartButton.interactable = false;
        }
        else if (num == 3)
        {
            StageNum = 3;
            ButtonActivate(true);
            _3StartButton.interactable = false;
        }
        else if (num == 4)
        {
            StageNum = 4;
            ButtonActivate(true);
            _4StartButton.interactable = false;
        }
    }
    void ButtonActivate(bool TF)
    {
        _1StartButton.interactable = TF;
        _2StartButton.interactable = TF;
        _3StartButton.interactable = TF;
        _4StartButton.interactable = TF;
    }
    // Start is called before the first frame update
    void Start()
    {
        StageNum = currentStageData.currentStageNum;//���� �������� ������ �ҷ�����
        _1StartButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
