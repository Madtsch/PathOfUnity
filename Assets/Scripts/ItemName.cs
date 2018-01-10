using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemName : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public GameObject target;
    public int itemID;
    private static GameObject canvas;

    // Use this for initialization
    void Start () {

        // find the canvas in this scene
        canvas = GameObject.Find("ItemNames");

    }

    // Update is called once per frame
    void Update () {

        // move item name to the correct position on the UI panel
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        gameObject.transform.position = screenPosition;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("IN.OPE.this: " + this.transform.parent.parent.name);
        // mark mouse over with color change
        this.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.75f);
        // get actual item name in front of all item names
        this.transform.SetParent(this.transform.parent.parent.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // mark mouse over with color change back to normal
        this.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.50f);
        // get actual item name back to the other item names
        this.transform.SetParent(canvas.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("IN.OPE.itemID: " + itemID);
        // add item to inventory
        GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(itemID);
        // destroy item and item name
        Destroy(target);
        Destroy(gameObject);
    }
}