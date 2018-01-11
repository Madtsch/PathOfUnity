using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    NavMeshAgent playerAgent;
    
    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        Debug.Log("playerAgent is good!");
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) 
        {
            Debug.Log("Clicked somewhere!");
            GetInteraction();
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            playerAgent.speed = 0;
        }
        else
        {
            playerAgent.speed = 8;
        }
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Interactable Object")
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else if (interactedObject.tag == "Enemy")
            {
                if (Time.time > nextFire)
                {
                    LookAndFire(interactedObject);
                    nextFire = Time.time + fireRate;
                }
            }
            else
            {
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Time.time > nextFire)
                    {
                        LookAndFireToMouse(interactionInfo.point);
                        nextFire = Time.time + fireRate;
                    }
                    playerAgent.destination = playerAgent.transform.position;
                }
            }

        }
    }

    void LookAndFire(GameObject interactedObject)
    {
        Vector3 direction = (interactedObject.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        playerAgent.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);
        playerAgent.stoppingDistance = 15f;
        GetComponent<PlayerWeaponController>().PerformWeaponAttack();
    }

    void LookAndFireToMouse(Vector3 mousePos)
    {
        Vector3 direction = (mousePos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        playerAgent.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);
        playerAgent.stoppingDistance = 15f;
        GetComponent<PlayerWeaponController>().PerformWeaponAttack();
    }

}
