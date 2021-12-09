using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DiceCheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;

    Vector3 diceVelocity1;

    Player1Script player1;

    Player2Script player2;

    public static int diceNumCollected = 0;

    public static int diceNum1 = 0;

    public static int diceNum2 = 0;

    public static int diceSum = 0;

    public static int whosTurn = 0;

    public static int player1NumOfDouble = 0;

    public static int player2NumOfDouble = 0;

    public static int p1penalty = 0; // indicates number of turns that p1 has to be withheld in jail

    public static int p2penalty = 0; // indicates number of turns that p2 has to be withheld in jail

    public Text txt;

    // public int [] p1sample = {1,2,3,4,5,6,6,5,4,3,2,1};
    // public int [] p2sample = {1,2,3,4,5,6,6,5,4,3,2,1};
    // Update is called once per frame
    void Start()
    {
        player1 = FindObjectOfType<Player1Script>(); // when the game start assign player1script to player1 var
        player2 = FindObjectOfType<Player2Script>();
    }

    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
        //diceVelocity1 = DiceScript1.diceVelocity1;
    }

    void OnTriggerStay(Collider col)
    {
        // execute when a one side of the dice collide with the dice check zone
        if (
            diceVelocity.x == 0f &&
            diceVelocity.y == 0f &&
            diceVelocity.z == 0f &&
            diceNumCollected < 2
        )
        {
            // if the dice velocity =0, means that the dice has stopped
            switch (col.gameObject.name // every side of the dice has sphere collider, if this switch check the name of the collider to find the value of rolled dice
            )
            {
                case "Side1": // if it is side 1, means that the dice you rolled is 6.
                    diceNumCollected += 1; // dice num collected is to count how many dices has been rolled, since we are rolling two dices, this if statement will not run

                    // once we collect value of both dice1 and dice2
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 6;
                    }
                    else
                    {
                        diceNum2 = 6;
                    } // dicecoollect = 1, means its the first dice, dicecollect =2, means its the second dice

                    //DiceNumberTextScript.diceNumber = 6; later pass in to class that needs this
                    break;
                case "Side2":
                    diceNumCollected += 1;
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 5;
                    }
                    else
                    {
                        diceNum2 = 5;
                    }

                    //DiceNumberTextScript.diceNumber = 5;
                    break;
                case "Side3":
                    diceNumCollected += 1;
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 4;
                    }
                    else
                    {
                        diceNum2 = 4;
                    }

                    //DiceNumberTextScript.diceNumber = 4;
                    break;
                case "Side4":
                    diceNumCollected += 1;
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 3;
                    }
                    else
                    {
                        diceNum2 = 3;
                    }

                    //DiceNumberTextScript.diceNumber = 3;
                    break;
                case "Side5":
                    diceNumCollected += 1;
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 2;
                    }
                    else
                    {
                        diceNum2 = 2;
                    }

                    //DiceNumberTextScript.diceNumber = 2;
                    break;
                case "Side6":
                    diceNumCollected += 1;
                    if (diceNumCollected == 1)
                    {
                        diceNum1 = 1;
                    }
                    else
                    {
                        diceNum2 = 1;
                    }

                    //DiceNumberTextScript.diceNumber = 1;
                    break;
            }

            if (diceNumCollected == 2)
            {
                TextMeshProUGUI txt;
                GameObject obj;
                print("dice1=" + diceNum1);
                print("dice2=" + diceNum2);
                obj = GameObject.Find("SystemMsgTxt");

                txt = obj.GetComponent<TextMeshProUGUI>();
                txt.text = ("Result: " + (diceNum1 + diceNum2).ToString());
                print(diceNum1 + diceNum2);
            }

            if (diceNumCollected == 2)
            {
                // if we have collected the value of both dices
                diceSum = diceNum1 + diceNum2; // add dicenum1 and dicenum2
                if (whosTurn == 0)
                {
                    // if it is player 1's turn and if p1 is not withheld in jail
                    if (p1penalty != 0)
                    {
                        if (diceNum1 == diceNum2)
                        {
                            p1penalty = 0;
                        }
                        whosTurn = 1;
                    }
                    else
                    {
                        if (diceNum1 == diceNum2)
                        {
                            // if player rolls double
                            player1NumOfDouble++; // +1 to player1numofdouble
                            if (player1NumOfDouble == 3)
                            {
                                if (p2penalty == 0)
                                {
                                    p1penalty = 2;
                                }
                                else
                                {
                                    p1penalty = 2 - p2penalty;
                                    p2penalty = 0;
                                }
                                p1penalty = 2;
                                player1NumOfDouble = 0;
                                whosTurn = 1;
                                player1.goToJail();
                            } // if player roles double 3 times in a row,player will stay in jail for 3 turn,
                            else
                            {
                                Player1Script.moveAllowed = true;
                            } // else they will get another chance to roll dice
                        }
                        else
                        {
                            // else which is the case player didnt roll double
                            player1NumOfDouble = 0; // reset double count
                            Player1Script.moveAllowed = true; // allow p1 to move
                            if (p2penalty != 0)
                            {
                                p2penalty--;
                            }
                            whosTurn = 1;
                        }
                    }
                }
                else if (whosTurn == 1)
                {
                    if (p2penalty != 0)
                    {
                        if (diceNum1 == diceNum2)
                        {
                            p2penalty = 0;
                        }
                        whosTurn = 0;
                    }
                    else
                    {
                        if (diceNum1 == diceNum2)
                        {
                            player2NumOfDouble++;
                            if (player2NumOfDouble == 3)
                            {
                                if (p1penalty == 0)
                                {
                                    p2penalty = 2;
                                }
                                else
                                {
                                    p2penalty = 2 - p1penalty;
                                    p1penalty = 0;
                                }
                                player2NumOfDouble = 0;
                                whosTurn = 0;
                                player2.goToJail();
                            }
                            else
                            {
                                Player2Script.moveAllowed = true;
                                print("my turn to move");
                            }
                        }
                        else
                        {
                            player2NumOfDouble = 0;
                            Player2Script.moveAllowed = true;
                            if (p1penalty != 0)
                            {
                                p1penalty--;
                            }
                            whosTurn = 0;
                        }
                    }
                }
            }
        }
    }
}
// ifp1penalty!=0 and p2penalty!=0{p1penalty >= p2penalty : p1penalty -= p2penalty, p2penalty = 0 : p2penlaty -= p1penalty, p1 penalty =0 }
// else:
// if p1sturn and p1penalty == 0
//   if rolled double, numofdoublep1++,
//     if numofdoublep1==3,then penaltyp1+=3, numofdouble = 0;p2 turn
//     else moveallow=true
//   else moveallow = true, if p2 penalty = 0, p2 turns,else p2penalty --
// else if p2turn and p2penalty ==0
//     if rolled double,numofdoublep2++
//     if numofdoublep2==3,then penaltyp2+=3,numofdoublep2 = 0;p1turn
//     else move allow = true
//   else move allow true, if p1 penalty = 0, p2 turns,else p1penalty --
