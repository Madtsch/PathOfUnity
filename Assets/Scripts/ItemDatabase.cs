using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<inventoryItem> database = new List<inventoryItem>();
    private JsonData itemData;
    public bool itemDatabaseReady;
    public static int itemCount;

    private void Start()
    {
        itemDatabaseReady = false;
        //itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));
        StartCoroutine(LoadFileFromURL());
    }

    private IEnumerator LoadFileFromURL()
    {
        Debug.Log("ItemDatabase - LoadFileFromURL!");
        WWW w = new WWW("https://apex.oracle.com/pls/apex/f?p=58614:31::FLOW_EXCEL_OUTPUT_R7764851736493763310_de&tz=2:00");
        yield return w;
        if (w.error != null)
        {
            Debug.Log("Error: " + w.error);
        }
        else
        {
            Debug.Log("Found ... ==>" + w.text.Replace("\"\"", "\""));
            ConstructItemDatabase(w.text.Replace("\"\"", "\""));
            itemDatabaseReady = true;
        }
    }

    public inventoryItem FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase(string urlText)
    {
        // generate item data out of the json file
        itemData = JsonMapper.ToObject(urlText);
        // set the item count for other scripts
        itemCount = itemData["items"].Count;
        for (int i = 0; i < itemData["items"].Count; i++)
        {
            database.Add(new inventoryItem((int)itemData["items"][i]["id"]
                                        ,       itemData["items"][i]["title"].ToString()
                                        ,  (int)itemData["items"][i]["value"]
                                        ,       itemData["items"][i]["description"].ToString()
                                        , (bool)itemData["items"][i]["stackable"]
                                        ,  (int)itemData["items"][i]["rarity"]
                                        ,       itemData["items"][i]["slug"].ToString()
                                        ));
        }
    }
}

public class inventoryItem
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public inventoryItem(int id, string title, int value, string description, bool stackable, int rarity, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public inventoryItem()
    {
        this.ID = -1;
    }

}