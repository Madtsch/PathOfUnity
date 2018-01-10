using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public PlayerWeaponController playerWeaponController;
    public Item sword;

    public GameObject inventoryPanel;

    private void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power", "Your power level."));
        sword = new Item(swordStats, "staff");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerWeaponController.EquipWeapon(sword);
        }
        // switch the inventory on and off
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
            }
            else
            {
                inventoryPanel.SetActive(true);
            }
        }
    }
}
