using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    [SerializeField] private List<Animation> aniObjects;
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
        aniObjects.ForEach(x => x.Play());

        if(destroyThis)
        {
            Destroy(this);
        }
    }
}
