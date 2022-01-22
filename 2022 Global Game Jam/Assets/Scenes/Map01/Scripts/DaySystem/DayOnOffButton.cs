using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOnOffButton : ClickObject
{
    [SerializeField] private string onSeName;
    [SerializeField] private string offSeName;

    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        if (DayOnOffSystem.nowState == DayState.MORNING)
        {
            DayOnOffSystem.nowState = DayState.NIGHT;
            SoundManager.PlaySE(offSeName);
        }
        else
        {
            DayOnOffSystem.nowState = DayState.MORNING;
            SoundManager.PlaySE(onSeName);
        }
    }
}
