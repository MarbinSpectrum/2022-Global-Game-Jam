using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayBGM : MonoBehaviour
{
    [SerializeField] bool destroyThis = false;
    [SerializeField] int slot = 0;
    [SerializeField] SoundEvent eventType;
    private void OnEnable()
    {
        if (eventType != SoundEvent.OnEnable)
            return;
        RunEvent();
    }

    private void Awake()
    {
        if (eventType != SoundEvent.Awake)
            return;
        RunEvent();
    }

    private void Start()
    {
        if (eventType != SoundEvent.Start)
            return;
        RunEvent();
    }

    private void RunEvent()
    {
        SoundManager.PlayBGM(slot);

        if (destroyThis)
        {
            Destroy(this);
        }
    }
}
