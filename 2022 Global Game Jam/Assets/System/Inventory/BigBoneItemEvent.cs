using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoneItemEvent : InventoryItemEvent
{
    [SerializeField] private GameObject bone;
    [SerializeField] private string []boneSE = new string[4];
    [SerializeField] private string se_Error;
    [SerializeField] private string answer;
    private bool solve = false;
    public override void RunEvent()
    {
        if (bone.activeSelf == false)
        {
            bone.SetActive(true);
        }
        else
        {
            passList.Clear();
            bone.SetActive(false);
            GameManager.eventRunning = false;
        }
    }

    private List<char> passList = new List<char>();
    private bool CheckAnswer()
    {
        if (passList.Count != answer.Length)
            return false;
        string s = "";
        for (int i = 0; i < answer.Length; i++)
            s += passList[i];

        for (int i = 0; i < answer.Length; i++)
            if (answer[i] != passList[i])
                return false;
        return true;
    }

    public void InputPass(int n)
    {
        if(DayOnOffSystem.nowState == DayState.MORNING)
        {
            SoundManager.PlaySE(se_Error);
            return;
        }

        passList.Add((char)('A' + n));
        if(passList.Count > answer.Length)
        {
            passList.RemoveAt(0);
        }
        SoundManager.PlaySE(boneSE[n]);

        if(CheckAnswer() && solve == false)
        {
            solve = true;
            bone.SetActive(false);
            TalkGhost02.instance.boneSolveEvent();
            GameManager.eventRunning = true;
            Inventory.RemoveItem("bonebig",true);
        }
    }
}
