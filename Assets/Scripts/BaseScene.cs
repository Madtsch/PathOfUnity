using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BaseScene : MonoBehaviour {

    public static void CreateScene()
    {
        Debug.Log("New base scene will be created!");
        SceneManager.LoadScene("BaseScene");
    }
}
