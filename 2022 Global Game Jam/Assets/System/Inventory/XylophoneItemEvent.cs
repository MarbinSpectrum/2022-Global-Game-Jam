using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneItemEvent : InventoryItemEvent
{
    [SerializeField] private GameObject xylophone;
    [SerializeField] private string []xylophoneSE = new string[4];
    private int answer = 0;
    public override void RunEvent()
    {
        xylophone.SetActive(true);
    }

    public void InputEvent(int n)
    {
        SoundManager.PlaySE(xylophoneSE[n]);
        answer++;
        Ending.Instance.EndEvent();
    }
}
