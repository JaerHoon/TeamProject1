using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Resource : MonoBehaviour
{
    public static Resource instance;//���� �Ŵ��� �̱��� ����

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

    [SerializeField]
    Resource_ScriptableObject ResourceData;
    [SerializeField]
    AudioStorage soundStorage;//��������

    [SerializeField]
    TextMeshProUGUI coinText;
    [SerializeField]
    TextMeshProUGUI jemText;

    AudioSource myAudioSource;

    public int coinNum;
    public int jemNum;

    // Start is called before the first frame update

    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        coinNum = ResourceData.coin;//���ҽ� ���� �ҷ�����
        jemNum = ResourceData.jem;
        if (coinNum != 0)
            coinText.text = string.Format("{0:#,###}", coinNum);
        else
            coinText.text = "0";
        if (jemNum != 0)
            jemText.text = string.Format("{0:#,###}", jemNum);
        else
            jemText.text = "0";
    }

    private void OnDisable()
    {
        ResourceData.coin = coinNum;//���� ���� �� ���� ���� ����
        ResourceData.jem = jemNum;

    }

    public void GetResource(int coinNum, int jemNum)
    {

        int randInt = Random.Range(0, 2);
        myAudioSource.PlayOneShot(soundStorage.SoundSrc[randInt].SoundFile);
        this.coinNum += coinNum;
        this.jemNum += jemNum;

        coinText.text = string.Format("{0:#,###}", this.coinNum);
        jemText.text = string.Format("{0:#,###}", this.jemNum);
    }

    public void SubResource(int coinNum, int jemNum)
    {
        this.coinNum -= coinNum;
        this.jemNum -= jemNum;

        coinText.text = string.Format("{0:#,###}", this.coinNum);
        jemText.text = string.Format("{0:#,###}", this.jemNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
