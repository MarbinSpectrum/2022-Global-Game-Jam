using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventNextStage : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] SoundEvent eventType;
    private void OnEnable()
    {
        if (eventType != SoundEvent.OnEnable)
            return;
        GameManager.eventRunning = true;
        Invoke("RunEvent", delay);
    }

    private void Awake()
    {
        if (eventType != SoundEvent.Awake)
            return;
        GameManager.eventRunning = true;
        Invoke("RunEvent", delay);
    }

    private void Start()
    {
        if (eventType != SoundEvent.Start)
            return;
        GameManager.eventRunning = true;
        Invoke("RunEvent", delay);
    }

    private void RunEvent()
    {
        DayOnOffSystem.nowState = DayState.NIGHT;
        GameManager.eventRunning = false;
        GameManager.nowStage++;
        SceneManager.LoadScene("MAP0" + GameManager.nowStage.ToString());
    }
}
