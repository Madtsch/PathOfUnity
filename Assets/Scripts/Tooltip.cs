using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private inventoryItem item;
    private string data;
    private GameObject tooltip;

    private void Start()
    {
        //Debug.Log("Tooltip - Start!");
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(inventoryItem item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#0473F0><b>" + item.Title + "</b></color>\n\n";
        data += "<color=#0473F0>" + item.Description + "</color>\n\n";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

}