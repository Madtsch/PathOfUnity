using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour
{

    public GameObject Player;
    public float OffsetX = 0;
    public float OffsetZ = -10;
    public float OffsetY = 10;
    public float MaximumDistance = 2;
    public float PlayerVelocity = 10;
    private float _movmentX;
    private float _movmentY;
    private float _movmentZ;

    // Update is called once per frame
    void Update()
    {
        _movmentX = ((Player.transform.position.x + OffsetX - this.transform.position.x)) / MaximumDistance;
        _movmentY = ((Player.transform.position.y + OffsetY - this.transform.position.y)) / MaximumDistance;
        _movmentZ = ((Player.transform.position.z + OffsetZ - this.transform.position.z)) / MaximumDistance;
        this.transform.position += new Vector3((_movmentX * PlayerVelocity * Time.deltaTime), (_movmentY * PlayerVelocity * Time.deltaTime), (_movmentZ * PlayerVelocity * Time.deltaTime));
    }
}
