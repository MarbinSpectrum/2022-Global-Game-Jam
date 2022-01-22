using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemCombinationData
{
    [HorizontalGroup("Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Split/필요재료")]
    [HideLabel]
    public List<string> requireItem;
    [BoxGroup("Split/결과")]
    [HideLabel]
    public string returnItemId;
    public bool HasRequireItems(List<string> items)
    {
        foreach(string itemId in requireItem)
        {
            if(items.Contains(itemId) == false)
            {
                return false;
            }
        }
        return true;
    }
    public List<int> HasRequireItemIdx(List<string> items)
    {
        List<int> temp = new List<int>();

        for(int i = 0; i < items.Count; i++)
        {
            if (requireItem.Contains(items[i]))
            {
                temp.Add(i);
            }
        }
        return temp;
    }
}
public class ItemManager : SerializedMonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                //NULL값인 경우 데이터를 매니저를 가져온다.
                GameObject itemManagerObj = Instantiate(Resources.Load("Manager/ItemManager") as GameObject);
                instance = itemManagerObj.GetComponent<ItemManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    [SerializeField] private Dictionary<string, Sprite> itemImage;

    public List<ItemCombinationData> itemCombinationDatas;

    public static Sprite GetSprite(string itemId)
    {
        //아이템Id에 따른 Sprite 반환
        return Instance.itemImage[itemId];
    }
}
