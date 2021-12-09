using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    BaseScript bs;

    public Transform[] waypoints;

    public float moveSpeed = 1f;

    public int waypointIndex = 0;

    public static bool moveAllowed = false;

    public int curindex = 0;

    // Start is called before the first frame update
    void Start()
    {
        bs = FindObjectOfType<BaseScript>();
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAllowed)
        {
            move();
        }
    }

    public void move()
    {
        if (waypointIndex <= DiceCheckZoneScript.diceSum + curindex)
        {
            transform.position =
                Vector3
                    .MoveTowards(transform.position,
                    waypoints[waypointIndex % waypoints.Length]
                        .transform
                        .position,
                    moveSpeed * Time.deltaTime);

            if (
                transform.position ==
                waypoints[waypointIndex % waypoints.Length].transform.position
            )
            {   if(transform.position == waypoints[0].transform.position){bs.p2money+=200;}// if the player passes by the start tile, +200 to p2's money
                waypointIndex += 1;
            }
        }
        else
        {
            curindex = waypointIndex - 1;
            bs.arrivedOnCity(2, curindex % 32);
            // call purchase or not function here..
            // if (
            //     curindex != 0 &&
            //     curindex != 6 &&
            //     curindex != 8 &&
            //     curindex != 13 &&
            //     curindex != 16 &&
            //     curindex != 18 &&
            //     curindex != 24 &&
            //     curindex != 27
            // )
            // {
            //     bs.arrivedOnCity(1, curindex % 32);
            // }
            //else call other function for special cards
            moveAllowed = false;

            //reable the dice button
            print(curindex % 32);
        }
    }

    public void goToJail()
    {
        transform.position = waypoints[24].transform.position;
        waypointIndex = 25;
        curindex = 24;
        moveAllowed = false;
    }
}
