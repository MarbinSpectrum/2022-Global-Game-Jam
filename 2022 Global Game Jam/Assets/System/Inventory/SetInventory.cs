using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInventory : MonoBehaviour
{
    [SerializeField] private List<string> items = new List<string>();
    public void OnEnable()
    {
        Inventory.RemoveItems();
        for(int i = 0; i < items.Count; i++)
        {
            Inventory.AddItem(items[i]);
        }
    }
}
