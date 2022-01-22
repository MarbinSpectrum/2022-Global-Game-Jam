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
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        GameManager.eventRunning = true;
        eraseObj.SetActive(true);
    }
}
