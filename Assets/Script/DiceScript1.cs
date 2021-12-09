using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript1 : MonoBehaviour
{
    static Rigidbody rb;

    public static Vector3 diceVelocity1;

    public static int test = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        diceVelocity1 = rb.velocity;
    }

    public void RollDie()
    {
        rb.useGravity = true;
        DiceCheckZoneScript.diceNumCollected = 0;
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.position = new Vector3(3, 4, 4);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque (dirX, dirY, dirZ);
    }
}
