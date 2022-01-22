using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlaySE : MonoBehaviour
{
    [SerializeField] private string seName;
    [SerializeField] private bool destroyThis;
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
        SoundManager.PlaySE(seName);

        if (destroyThis)
        {
            Destroy(this);
        }
    }
}
