using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOnOffButton : ClickObject
{
    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }

        if (DayOnOffSystem.nowState == DayState.MORNING)
        {
            DayOnOffSystem.nowState = DayState.NIGHT;
        }
        else
        {
            DayOnOffSystem.nowState = DayState.MORNING;
        }
    }
}
