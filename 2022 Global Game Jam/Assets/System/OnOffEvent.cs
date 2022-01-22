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
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }

        if (Inventory.HasItem(requireItem) == false && requireItem != "None")
        {
            return;     
        }

        Inventory.UseItem(requireItem);

        //�̺�Ʈ ����
        onObjects.ForEach(x => x.SetActive(true));
        offObjects.ForEach(x => x.SetActive(false));
    }
}
