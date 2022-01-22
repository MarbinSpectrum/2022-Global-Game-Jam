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
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                //NULL값인 경우 데이터를 매니저를 가져온다.
                GameObject InventoryObj = Instantiate(Resources.Load("Inventory") as GameObject);
                instance = InventoryObj.GetComponent<Inventory>();
                DontDestroyOnLoad(instance.gameObject);

                instance.Init();
            }
            return instance;
        }
    }

    private List<ItemData> itemList = new List<ItemData>();
    public Dictionary<string, InventoryItemEvent> itemEvent = new Dictionary<string, InventoryItemEvent>();

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Init()
    {
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            itemList.Add(new ItemData("None", transform.GetChild(0).GetChild(i).GetComponent<Image>()));
        }
        UpdateImage();
    }

    public static void AddItem(string itemId)
    {
        Instance.addItem(itemId);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 아이템 정렬
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
    /// : 아이템 이미지 갱신
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
    /// : 아이템 추가 메소드
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void addItem(string itemId)
    {
        SortITemList();

        //실질적으로 추가하는 부분
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemId == "None")
            {
                itemList[i].itemId = itemId;
                break;
            }
        }

        ItemCombination();

        SortITemList();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 아이템 보유 확인
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool HasItem(string itemId)
    {
        return Instance.hasItem(itemId);
    }
    private bool hasItem(string itemId)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            //아이템을 보유중인지 검사하는 부분
            if (itemList[i].itemId == itemId)
            {
                return true;
            }
        }
        return false;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 아이템 제거
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void RemoveItem(string itemId, bool all = false)
    {
        Instance.removeItem(itemId, all);
    }
    private void removeItem(string itemId,bool all = false)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            //아이템을 보유중인지 검사하는 부분
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
    /// : 아이템 사용
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void UseItem(string itemId, bool all = false)
    {
        Instance.useItem(itemId, all);
    }
    private void useItem(string itemId, bool all = false)
    {
        RemoveItem(itemId, all);
        UpdateImage();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 아이템 조합
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool ItemCombination()
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
                List<int> idx = combinationData[i].HasRequireItemIdx(itemIds);
                for(int j = 0; j < idx.Count; j++)
                {
                    itemIds[idx[j]] = "None";
                }
                itemIds[idx[0]] = combinationData[i].returnItemId;
                for (int j = 0; j < idx.Count; j++)
                {
                    itemList[idx[j]].itemId = itemIds[idx[j]];
                }
                return true;
            }
        }

        return false;
    }

    public void InventoryItemClick(int idx)
    {
        if (GameManager.eventRunning)
            return;
        if(itemEvent.ContainsKey(itemList[idx].itemId))
        {
            InventoryItemEvent inventoryItemEvent = itemEvent[itemList[idx].itemId];
            inventoryItemEvent.RunEvent();
        }
    }
}
