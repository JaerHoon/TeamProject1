using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;//�Ͻ����� �г� ������
    private bool isPause = false;

    public static GameManager instance;//���� �Ŵ��� �̱��� ����

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


        pausePanel.SetActive(false);//�Ͻ����� �г� ��Ȱ��ȭ
    }

    public PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        //SoundManager.instance.PlaySound(SoundId.BGM);
    }

    public void OnSetPausePanel()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
