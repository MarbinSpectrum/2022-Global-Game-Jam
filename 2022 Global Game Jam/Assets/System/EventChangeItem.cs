using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChangeItem : MonoBehaviour
{
    [SerializeField] private string requireItem;
    [SerializeField] private string returnItem;
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
        if(Inventory.HasItem(requireItem))
        {
            Inventory.RemoveItem(requireItem);
            Inventory.AddItem(returnItem);
        }
    }
}
