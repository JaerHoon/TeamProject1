using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSetting : MonoBehaviour
{
    [SerializeField]
    List<Skill_ScriptableObject> skillDatas = new List<Skill_ScriptableObject>();
    [SerializeField]
    List<Image> skillSets = new List<Image>();
    [SerializeField]
    List<Image> skillSlots = new List<Image>();

    SkillManager skillManager;

    int[] skillSetSlotNum = new int[3];
    [SerializeField]
    Sprite defaultSprite;

    [SerializeField]
    GameObject skillinfi_Panel;
    [SerializeField]
    Image info_icon;
    [SerializeField]
    TextMeshProUGUI info_skillName;
    [SerializeField]
    TextMeshProUGUI info_Explain;
    [SerializeField]
    TextMeshProUGUI info_CoolTime;
    [SerializeField]
    TextMeshProUGUI info_buttonText;

    [SerializeField]
    TextMeshProUGUI NoticText;

    [SerializeField]
    GameObject confirmbutton;

    int infoSkillnum;
    bool IsCanEquip;

    private void OnEnable()
    {
        Setting();
        skillinfi_Panel.SetActive(false);
        if(SkillManager.instance !=null) skillManager = SkillManager.instance;

    }

    public void Setting()
    {
       skillManager?.ChangeSlot(skillSetSlotNum);

        for (int i = 0; i < skillSetSlotNum.Length; i++)
        {
            if (skillSetSlotNum[i] != 0)
            {
                skillSets[i].sprite = skillDatas[skillSetSlotNum[i]].icon;
            }
            else
            {
                skillSets[i].sprite = defaultSprite;
            }

        }

        for (int i = 0; i < skillDatas.Count - 1; i++)
        {
            skillSlots[i].sprite = skillDatas[i + 1].icon;
        }

        NoticText.text = "���Կ� ������ ��ų�� ��������";
    }

    public void OnClickSkillSlot(int num)
    {
        NoticText.text = "������ ������ �����ϼ���.";
        infoSkillnum = num + 1;
        IsCanEquip = true;
        info_skillName.text = String.Format("��ų�̸� : {0}", skillDatas[infoSkillnum].skillname);
        info_icon.sprite = skillDatas[infoSkillnum].icon;
        info_Explain.text = skillDatas[infoSkillnum].skillinfo;
        info_CoolTime.text = String.Format("��Ÿ�� : {0}��", skillDatas[infoSkillnum].coolTime);
        confirmbutton.SetActive(false);
        skillinfi_Panel.SetActive(true);
    }

    public void OnClickSkillSet(int num)
    {
        if (IsCanEquip == true)
        {
            for (int i = 0; i < skillSetSlotNum.Length; i++)
            {
                if (skillSetSlotNum[i] == infoSkillnum)
                {
                    skillSetSlotNum[i] = 0;
                }
            }
            
            if(num == 2)
            {
               if(infoSkillnum == 5)
                {
                    UISound.instance.PlayerSound(UISound.pressEquipSkill);
                    skillSetSlotNum[num] = infoSkillnum;
                    Setting();
                    IsCanEquip = false;
                    Cancel();
                }
                else
                {
                    NoticText.text = "�������Կ��� ������ �� �����ϴ�.";
                }
            }
            else
            {
                if(infoSkillnum == 5)
                {
                    NoticText.text = "��Ƽ�� ���Կ��� ���� �� �� �����ϴ�.";
                }
                else
                {
                    UISound.instance.PlayerSound(UISound.pressEquipSkill);
                    skillSetSlotNum[num] = infoSkillnum;
                    Setting();
                    IsCanEquip = false;
                    Cancel();
                }
            }

            

            
          
        }
        else
        {

            if (skillSetSlotNum[num] != 0)
            {
                NoticText.text = "��ų�� �����Ͻðڽ��ϱ�?";
                infoSkillnum = skillSetSlotNum[num];
                info_skillName.text = String.Format("��ų�̸� : {0}", skillDatas[skillSetSlotNum[num]].skillname);
                info_icon.sprite = skillDatas[skillSetSlotNum[num]].icon;
                info_Explain.text = skillDatas[skillSetSlotNum[num]].skillinfo;
                info_CoolTime.text = String.Format("��Ÿ�� : {0}��", skillDatas[skillSetSlotNum[num]].coolTime);
                confirmbutton.SetActive(true);
                info_buttonText.text = "��ų ����";
                skillinfi_Panel.SetActive(true);
            }
        }
    }

    public void EquipSkill()
    {

        for (int i = 0; i < skillSetSlotNum.Length; i++)
        {
            if (skillSetSlotNum[i] == infoSkillnum)
            {
                skillSetSlotNum[i] = 0;
            }
        }

        Setting();
        Cancel();

    }

    public void Cancel()
    {
        UISound.instance.PlayerSound(UISound.pressButton);
        skillinfi_Panel.SetActive(false);
    }
}