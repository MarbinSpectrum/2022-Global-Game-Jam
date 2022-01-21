using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemCombinationData
{
    [HorizontalGroup("Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Split/�ʿ����")]
    [HideLabel]
    public List<string> requireItem;
    [BoxGroup("Split/���")]
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
                //NULL���� ��� �����͸� �Ŵ����� �����´�.
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
        //������Id�� ���� Sprite ��ȯ
        return Instance.itemImage[itemId];
    }
}
