using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterManager : Interactable
{

    public override void Interact()
    {
        Debug.Log("Interacting with Transporter!");
        // generate new world
        BaseScene.CreateScene();
          //var sceneInstance = new BaseScene();
          //sceneInstance.CreateScene();
          // move player in new world
    }

}
