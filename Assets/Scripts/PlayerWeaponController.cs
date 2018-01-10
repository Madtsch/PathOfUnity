using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    Transform spawnProjectile;

    IWeapon equippedWeapon;
    CharacterStats characterStats;

    private void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<CharacterStats>();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug)
            , playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        if (EquippedWeapon.GetComponent<IWeapon>() != null)
        {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        }
        equippedWeapon.Stats = itemToEquip.Stats;
        EquippedWeapon.transform.SetParent(playerHand.transform);
        characterStats.AddStatBonus(itemToEquip.Stats);
        //Debug.Log(equippedWeapon.Stats[0].GetCalculatedStatValue());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformWeaponAttack();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PerformSpecialWeaponAttack();
        }
    }

    public void PerformWeaponAttack()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.PerformAttack();
        }
    }

    public void PerformSpecialWeaponAttack()
    {
        equippedWeapon.PerformSpecialAttack();
    }

}
