using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlaySE : ClickObject
{
    [SerializeField] private string seName;
    [SerializeField] private bool destroyThis;

    public override void Click()
    {
        if (GameManager.eventRunning)
            return;

        SoundManager.PlaySE(seName);

        if (destroyThis)
        {
            Destroy(this);
        }
    }
}
