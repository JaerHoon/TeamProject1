using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SmallQuestUI : MonoBehaviour
{
    
    [SerializeField]
    TextMeshProUGUI questName;//����Ʈ ���� ������Ʈ
    [SerializeField]
    TextMeshProUGUI questCountText;//����Ʈ ���� Ƚ�� ������Ʈ
    [SerializeField]
    GameObject complete;
    [SerializeField]
    Button button;

    public void Setting(Quest quest)
    {
        complete.gameObject.SetActive(false);
        button.enabled = false;

        if (quest != null)
        {
           

            if(quest.questStat == Quest.QuestStat.Complete)
            {
                questName.text = quest.questdata.questName;
                questCountText.text = String.Format("(����ȹ��)");
                complete.gameObject.SetActive(true);
                button.enabled = true;

            }
            else
            {
                questName.text = quest.questdata.questName;
                questCountText.text = String.Format("({0}/{1})", quest.quest_Count, quest.questdata.numberOfGoal);
            }
         
        }
        else
        {
            return;
        }
        
    }

    public void OnClick()
    {
        print("Ŭ��");
    }
}
