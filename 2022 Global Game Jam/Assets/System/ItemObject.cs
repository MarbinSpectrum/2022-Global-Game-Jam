using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ClickObject
{
    [SerializeField] private string itemName;

    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        Inventory.AddItem(itemName);
        gameObject.SetActive(false);
    }
}
