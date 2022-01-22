using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> onObjects;
    [SerializeField] private List<GameObject> offObjects;
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

    public void RunEvent()
    {
        //이벤트 실행
        onObjects.ForEach(x => x.SetActive(true));
        offObjects.ForEach(x => x.SetActive(false));

        if(destroyThis)
        {
            Destroy(this);
        }
    }
}
