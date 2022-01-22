using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffEvent : ClickObject
{
    [SerializeField] private string requireItem;
    [SerializeField] private List<GameObject> onObjects;
    [SerializeField] private List<GameObject> offObjects;

    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        if (Inventory.HasItem(requireItem) == false && requireItem != "None")
        {
            return;     
        }

        Inventory.UseItem(requireItem);

        //이벤트 실행
        onObjects.ForEach(x => x.SetActive(true));
        offObjects.ForEach(x => x.SetActive(false));
    }
}
