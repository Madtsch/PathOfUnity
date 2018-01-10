using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private static GameObject canvas;
    private float healthbarWidth;

    public GameObject target;

    private void Start()
    {
        // find the canvas in this scene
        canvas = GameObject.Find("Healthbars");
        // move the instanciated healthbar to the canvas
        gameObject.transform.SetParent(canvas.transform, false);
        // find out the width of the healthbar
        healthbarWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update () {
        // get the position of the target and translate to ui screen position
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        // move the healthbar in the middle of the target
        screenPosition.x = screenPosition.x - (healthbarWidth / 2);
        // move the healthbar a little bit over the target
        screenPosition.y = screenPosition.y + 10f;
        gameObject.transform.position = screenPosition;
    }

    public void updateHealthbar(float currentHealth)
    {
        GetComponent<Slider>().value = currentHealth;
        //Debug.Log("Healthbar - updateHealthbar: " + currentHealth);
        if (currentHealth <= 0)
        {
            destroyHealthbar();
        }
    }

    void destroyHealthbar()
    {
        Destroy(gameObject);
        //Debug.Log("Healthbar - Destroy slime healthbar!");
    }

}
