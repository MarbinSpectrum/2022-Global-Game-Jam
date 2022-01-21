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
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }

        Inventory.AddItem(itemName);
        gameObject.SetActive(false);
    }
}
