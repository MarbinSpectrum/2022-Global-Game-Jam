using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookItemEvent : InventoryItemEvent
{
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject []page = new GameObject[2];

    public override void RunEvent()
    {
        if(book.activeSelf == false)
        {
            book.SetActive(true);
        }
        else
        {
            book.SetActive(false);
            GameManager.eventRunning = false;
        }
    }

    public void ChangePageEvent()
    {
        page[0].SetActive(false);
        page[1].SetActive(true);
    }
}
