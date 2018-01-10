/*
  @author: Madtsch
  @date: 2017-04-16
  @function: Enemy Script
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    // some properties for this type of enemy
    public float currentHealth, power, toughness;
    // max health of this type of enemy
    public float maxHealth;
    // is this instance allive?
    public bool allive;
    // the special healthbar for the slime
    private GameObject slimeHealthbar { get; set; }
    //The target player
    public Transform player;
    //At what distance will the enemy walk towards the player?
    public float walkingDistance = 10.0f;
    //In what time will the enemy complete the journey between its position and the players position
    public float smoothTime = 10.0f;
    //Vector3 used to store the velocity of the enemy
    private Vector3 smoothVelocity = Vector3.zero;

    private void Start()
    {
        //Debug.Log("Slime - Start - transform.position: " + transform.position);
        // at start the enemy has full health
        currentHealth = maxHealth;
        // this instance is allive
        allive = true;
        // get players transform
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (allive == true)
        {
            //Look at the player
            transform.LookAt(player);
            //Calculate distance between player
            float distance = Vector3.Distance(transform.position, player.position);
            //If the distance is smaller than the walkingDistance
            if (distance < walkingDistance)
            {
                //Move the enemy towards the player with smoothdamp
                transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
            }
        }

    }

    public void PerformAttack()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        // if the healthbar is not yet spawned
        if (slimeHealthbar == null)
        {
            // spawn the healthbar
            slimeHealthbar = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Healthbar"), gameObject.transform.position, Quaternion.identity);
            // set the enemy object as target in the spawned healthbar
            slimeHealthbar.GetComponent<Healthbar>().target = gameObject;
        }
        // remove the hitted amount from the health of this enemy
        currentHealth -= amount;
        // show the change of the health on the spawned healthbar
        slimeHealthbar.GetComponent<Healthbar>().updateHealthbar(currentHealth / maxHealth);
        // when health is 0 then the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.AddComponent<TriangleExplosion>();
        StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
        // make slim a little less high
        //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z);
        // disable the box collider
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        allive = false;
        GetComponent<DropSpawnController>().DropItems(gameObject.transform.position);
    }

}
