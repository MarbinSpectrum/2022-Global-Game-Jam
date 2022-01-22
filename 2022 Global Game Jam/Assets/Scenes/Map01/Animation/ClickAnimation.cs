using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimation : ClickObject
{
    [SerializeField] private Animation animation;
    [SerializeField] private bool destroyThis;

    public override void Click()
    {
        if (GameManager.eventRunning)
            return;

        animation.Play();

        if (destroyThis)
        {
            Destroy(this);
        }
    }
}
