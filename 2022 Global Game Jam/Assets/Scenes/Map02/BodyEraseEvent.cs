using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyEraseEvent : ClickObject
{
    public GameObject eraseObj;
    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }

        GameManager.eventRunning = true;
        eraseObj.SetActive(true);
    }
}
