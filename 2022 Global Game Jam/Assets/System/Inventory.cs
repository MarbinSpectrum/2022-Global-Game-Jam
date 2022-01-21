using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ItemData
{
    public string itemId;
    public Image itemImage;
    public ItemData(string id,Image image)
    {
        itemId = id;
        itemImage = image;
    }
}

public class Inventory : SerializedMonoBehaviour
{
    public static Inventory instance;
    private List<ItemData> itemList = new List<ItemData>();

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        instance = this;
        for(int i = 0; i < transform.childCount; i++)
        {
            itemList.Add(new ItemData("None", transform.GetChild(i).GetComponent<Image>()));
        }
    }

    private void Start()
    {
        UpdateImage(); 
    }

    public static void AddItem(string itemId)
    {
        instance.addItem(itemId);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ ����
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SortITemList()
    {
        List<string> tempItemList = new List<string>();
        itemList.ForEach(x =>
        {
            if(x.itemId != "None")
            {
                tempItemList.Add(x.itemId);
            }
        });
        itemList.ForEach(x =>
        {
            x.itemId = "None";
        });
        for(int i = 0; i < tempItemList.Count; i++)
        {
            itemList[i].itemId = tempItemList[i];
        }
        UpdateImage();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ �̹��� ����
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateImage()
    {
        itemList.ForEach(x =>
        {
            if (x.itemId != "None")
            {
                x.itemImage.color = new Color(1, 1, 1, 1);
                x.itemImage.sprite = ItemManager.GetSprite(x.itemId);
            }
            else
            {
                x.itemImage.color = new Color(1, 1, 1, 0);
            }
        });
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ �߰� �޼ҵ�
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void addItem(string itemId)
    {
        SortITemList();

        //���������� �߰��ϴ� �κ�
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemId == "None")
            {
                itemList[i].itemId = itemId;
                break;
            }
        }

        ItemCombination();

        UpdateImage();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ ���� Ȯ��
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool HasItem(string itemId)
    {
        return instance.hasItem(itemId);
    }
    private bool hasItem(string itemId)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            //�������� ���������� �˻��ϴ� �κ�
            if (itemList[i].itemId == itemId)
            {
                return true;
            }
        }
        return false;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ ����
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void RemoveItem(string itemId,bool all = false)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            //�������� ���������� �˻��ϴ� �κ�
            if (itemList[i].itemId == itemId)
            {
                itemList[i].itemId = "None";
                if (all == false)
                    return;
            }
        }
        UpdateImage();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ ���
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void UseItem(string itemId, bool all = false)
    {
        instance.useItem(itemId, all);
    }
    private void useItem(string itemId, bool all = false)
    {
        RemoveItem(itemId, all);
        UpdateImage();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ������ ����
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ItemCombination()
    {
        List<ItemCombinationData> combinationData = ItemManager.Instance.itemCombinationDatas;
        List<string> itemIds = new List<string>();

        foreach (ItemData itemData in itemList)
        {
            itemIds.Add(itemData.itemId);
        }

        for(int i = 0; i < combinationData.Count; i++)
        {
            if(combinationData[i].HasRequireItems(itemIds))
            { 
                foreach(string removeItemId in combinationData[i].requireItem)
                {
                    RemoveItem(removeItemId);
                }
                addItem(combinationData[i].returnItemId);
                return;
            }
        }
    }
}
