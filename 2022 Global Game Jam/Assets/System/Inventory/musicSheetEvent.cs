using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicSheetEvent : InventoryItemEvent
{
    [SerializeField] private GameObject sheet;

    public override void RunEvent()
    {
        if(sheet.activeSelf == false)
        {
            sheet.SetActive(true);
        }
        else
        {
            sheet.SetActive(false);
            GameManager.eventRunning = false;
        }
    }
}
