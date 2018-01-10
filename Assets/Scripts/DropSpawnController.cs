using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropSpawnController : MonoBehaviour {

    public int spawnItemMin, spawnItemMax;
    public float itemDistanceMin, itemDistanceMax;
    private static GameObject canvas;
    public static int itemNameCount;
    ItemDatabase database;

    // Use this for initialization
    void Start () {

        // find the canvas in this scene
        canvas = GameObject.Find("ItemNames");
        // get item database
        database = GameObject.Find("Inventory").GetComponent<ItemDatabase>();

    }
	
	// Update is called once per frame
	void Update () {

        if (database.itemDatabaseReady == true)
        {
            //inventoryItem itemInfo = database.FetchItemByID(2);
            //Debug.Log("DSC.Start.itemInfo: " + itemInfo.Title);
        }

    }

    public void DropItems(Vector3 enemyPosition)
    {
        int spawnItemCount = Random.Range(spawnItemMin, spawnItemMax);
        for (int i = 0; i < spawnItemCount; i++)
        {
            // get a random position for the spawn item
            float posX = Random.Range(enemyPosition.x - Random.Range(itemDistanceMin, itemDistanceMax), enemyPosition.x + Random.Range(itemDistanceMin, itemDistanceMax));
            float posZ = Random.Range(enemyPosition.z - Random.Range(itemDistanceMin, itemDistanceMax), enemyPosition.z + Random.Range(itemDistanceMin, itemDistanceMax));
            // get a random item 
            int randomItem = Random.Range(0, ItemDatabase.itemCount);
            inventoryItem itemInfo = database.FetchItemByID(randomItem);
            //Debug.Log("DSC.DropItems.randomItem: " + randomItem);
            // spawn the random item
            GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/Items/" + itemInfo.Slug, typeof(GameObject)) as GameObject, new Vector3(posX, enemyPosition.y + 2, posZ), Random.rotation);
            // spawn the itemName panel
            GameObject itemNameInstance = (GameObject)Instantiate(Resources.Load("Prefabs/ItemName", typeof(GameObject)) as GameObject, instance.transform.position, Quaternion.identity);
            // set the item name of the random item
            itemNameInstance.transform.GetChild(0).GetComponent<Text>().text = itemInfo.Title;
            //Debug.Log("DSC.DropItems: " + itemNameInstance.transform.GetChild(0).GetComponent<Text>().text);
            // set the id of the item in the ItemName script
            itemNameInstance.GetComponent<ItemName>().itemID = randomItem;
            // move the item name panel to the canvas in the directory ItemNames
            itemNameInstance.transform.SetParent(canvas.transform);
            // the item name script needs the object of itself
            itemNameInstance.GetComponent<ItemName>().target = instance;
            // add some force to the dropped item
            instance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-359, 359), Random.Range(5, 20), Random.Range(-359, 359)) * 1.01f);
            // add count to itemNameCount
            itemNameCount++;
            //Debug.Log("DSC - DropItems - itemNameCount: " + itemNameCount);
        }

    }
}
