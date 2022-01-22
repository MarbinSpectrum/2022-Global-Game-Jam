using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour
{
    [SerializeField] private float pointDistance = 0.5f;
    [SerializeField] private GameObject removePoint;
    [SerializeField] private string itemId;
    [SerializeField] private GameObject onoffObj;
    private List<GameObject> pointList = new List<GameObject>();
    public bool CanSet(Vector2 pos)
    {
        for(int i = 0; i < pointList.Count; i++)
            if (Vector2.Distance(pos, pointList[i].transform.position) < pointDistance)
                return false;
        return true;
    }

    public void OnMouseDrag()
    {
        RemoveArea();
    }

    public void OnDisable()
    {
        for(int i = 0; i < pointList.Count; i++)
        {
            Destroy(pointList[i]);
        }
        pointList.Clear();
    }

    public void RemoveArea()
    {
        if (pointList.Count < 240)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (CanSet(pos))
            {
                GameObject obj = Instantiate(removePoint, pos, Quaternion.identity);
                obj.transform.parent = transform;
                pointList.Add(obj);
            }
        }    
        else if (pointList.Count == 220)
        {
            onoffObj.SetActive(true);
            Inventory.AddItem(itemId);
        }
    }
}
