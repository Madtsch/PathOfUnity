using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItems;

    private bool addItemDone;

    private int slotAmount;
    public List<inventoryItem> items = new List<inventoryItem>();
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        database = GetComponent<ItemDatabase>();
        addItemDone = false;
        slotAmount = 30;
    }

    private void Update()
    {
        if (database.itemDatabaseReady == true && addItemDone == false)
        {
            for (int i = 0; i < slotAmount; i++)
            {
                items.Add(new inventoryItem());
                slots.Add(Instantiate(inventorySlot));
                slots[i].GetComponent<Slot>().id = i;
                slots[i].transform.SetParent(slotPanel.transform);
            }
            AddItem(0);
            /*
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(2);
            AddItem(3);
            AddItem(4);
            AddItem(5);
            */
            addItemDone = true;
        }
    }

    public void AddItem(int id)
    {
        inventoryItem itemToAdd = database.FetchItemByID(id);
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            Debug.Log("I.AddItem.stackable and in inv: " + items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log("I.AddItem.else: " + items.Count);
                if (items[i].ID == -1)
                {
                    Debug.Log("I.AddItem.else: " + i);
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItems);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Slug;
                    break;
                }
            }
        }
    }

    public void RemoveItem(int id)
    {
        inventoryItem itemToRemove = database.FetchItemByID(id);

        if (itemToRemove.Stackable && CheckIfItemIsInInventory(itemToRemove))
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ID == id)
                {
                    ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0)
                    {
                        Destroy(slots[j].transform.GetChild(0).gameObject);
                        items[j] = new inventoryItem();
                        break;
                    }
                    if (data.amount == 1)
                    {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    items[i] = new inventoryItem();
                    break;
                }
            }
        }
    }
    
    bool CheckIfItemIsInInventory(inventoryItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
}
