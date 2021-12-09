using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    string[]
        landNameArr =
        {
            "USA",
            "Korea",
            "Canada",
            "Japan",
            "China",
            "",
            "India",
            "",
            "Russia",
            "UK",
            "Poland",
            "Germany",
            "",
            "Turkey",
            "France",
            "",
            "Italy",
            "",
            "Greece",
            "Spain",
            "Romania",
            "Serbia",
            "Croatia",
            "",
            "Bulgaria",
            "Sweden",
            "",
            "Chile",
            "Egypt",
            "Iceland",
            "Israel",
            ""
        };

    int[]
        landPriceArr =
        {
            0,
            60,
            60,
            100,
            100,
            0,
            120,
            140,
            0,
            140,
            160,
            180,
            180,
            200,
            0,
            220,
            0,
            220,
            240,
            0,
            260,
            260,
            280,
            300,
            0,
            300,
            0,
            320,
            350,
            400,
            500,
            500
        };

    int[] landStatus = new int[32];

    Land[] lands = new Land[32];

    public int p1money;
    public int p2money;

    public string whatUserCanDo = "";

    public int playernum = 0;

    public int curIndex = 0;

    void Awake()
    {
        p1money = 1300;
        p2money = 1300;
        Array.Reverse (landNameArr);
        for (int i = 0; i < lands.Length; i++)
        {
            lands[i] = new Land(landNameArr[i], landPriceArr[i], 0);
        }
        landStatus[14] = 3; // get
        landStatus[26] = 3;
        landStatus[5] = 4; // pay
        landStatus[19] = 4;
        landStatus[8] = 5; // go to jail
        landStatus[16] = 6; // free parking
        landStatus[0] = 7; // start,no effect, will give 200 inside players script.
        landStatus[24] = 7; // jail, but no effect
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void purchase()
    {
        TextMeshProUGUI txt;
        if (playernum == 1)
        {
            p1money -= lands[curIndex].price;
            landStatus[curIndex] = 1;
            txt =
                GameObject
                    .Find("Player1LandsList")
                    .GetComponent<TextMeshProUGUI>();
            txt.text += (landNameArr[curIndex] + "\n");
        }
        else if (playernum == 2)
        {
            p2money -= lands[curIndex].price;
            landStatus[curIndex] = 2;
            txt =
                GameObject
                    .Find("Player2LandsList")
                    .GetComponent<TextMeshProUGUI>();
            txt.text += (landNameArr[curIndex] + "\n");
        }
    }

    public void build()
    {
        if (playernum == 1)
        {
            p1money -= 2 * landPriceArr[curIndex];
        }
        else
        {
            p2money -= 2 * landPriceArr[curIndex];
        }
        lands[curIndex].numOfBuildings++;
        lands[curIndex].price += 2 * landPriceArr[curIndex];

        TextMesh txt;
        string landName = lands[curIndex].landName;
        txt = GameObject.Find(landName + "_price").GetComponent<TextMesh>();
        txt.text = (lands[curIndex].price).ToString();

        print(lands[curIndex].numOfBuildings);
        print(lands[curIndex].price);
    }

    public void arrivedOnCity(int playerNum, int curIndex)
    {
        TextMesh txt;
        this.playernum = playerNum;
        this.curIndex = curIndex;

        //whatusercando: = based on landstatus,
        if (landStatus[curIndex] == 0)
        {
            // if the land is not owned by anyone
            if (playerNum == 1)
            {
                // if p1 lands on it
                if (p1money >= lands[curIndex].price)
                {
                    // if p1 has enough money to buy a land
                    //print("this land is not owned by anyone, would you like to purchase this land? y - yes, n - no");
                    whatUserCanDo = "purchase";
                }
                else
                {
                    whatUserCanDo = "";
                } // if p1 does not have have money, nothing p1 can do
            }
            else
            {
                // if p2 lands on the land
                if (p2money >= lands[curIndex].price)
                {
                    // if p2 have enough money to buy the land
                    //print("this land is not owned by anyone, would you like to purchase this land? y - yes, n - no");
                    whatUserCanDo = "purchase";
                }
                else
                {
                    whatUserCanDo = "";
                } // if p2 does not have enough money to buy the land
            }
        }
        else if (landStatus[curIndex] == 1)
        {
            // if the land is owned by player 1
            if (playerNum == 1)
            {
                // if player1 lands on p1's property
                if (
                    p1money < 1.5 * lands[curIndex].price ||
                    lands[curIndex].numOfBuildings == 3
                )
                {
                    // if p1's money is not enough to add a building
                    // print not enough money to build or the land has already 3 buildings
                    whatUserCanDo = "";
                }
                else
                {
                    whatUserCanDo = "build";
                } // if p1 has enough money to build
            }
            else
            {
                // if p2 ladns on p1's property
                if (p2money < lands[curIndex].price)
                {
                    whatUserCanDo = "sell";
                } // if p2 does not have enough money to pay
                else
                {
                    // if p2 have enough money to pay for the rent
                    p2money -= lands[curIndex].price;
                    whatUserCanDo = "";
                }
            }
        }
        else if (landStatus[curIndex] == 2)
        {
            // it the lands is owned by p2
            if (playerNum == 2)
            {
                // if p2 lands on p2's property
                if (
                    p2money < 1.5 * lands[curIndex].price ||
                    lands[curIndex].numOfBuildings == 3
                )
                {
                    // if p2 does not have enough money to build
                    // print not enough money to build or the land has already 3 building
                    whatUserCanDo = "";
                }
                else
                {
                    whatUserCanDo = "build";
                }
            }
            else
            {
                // if p1 lands on p2's property
                if (p1money < lands[curIndex].price)
                {
                    whatUserCanDo = "sell";
                } // if p1 does not have any money to pay the rent
                else
                {
                    whatUserCanDo = "";
                    p1money -= lands[curIndex].price; // if p1 have enough money, just pay the rent
                }
            }
        }
        else if (landStatus[curIndex] == 3)
        {
            // user lands on get 100
            if (playerNum == 1)
            {
                p1money += 100;
                whatUserCanDo = "";
            }
            else
            {
                p2money += 100;
                whatUserCanDo = "";
            }
        }
        else if (landStatus[curIndex] == 4)
        {
            // user lands on pay 100
            if (playerNum == 1)
            {
                p1money -= 100;
                whatUserCanDo = "";
            }
            else
            {
                p2money -= 100;
                whatUserCanDo = "";
            }
        }

        //button on, purchase,build,sell
        if (
            whatUserCanDo == "purchase" ||
            whatUserCanDo == "build" ||
            whatUserCanDo == "sell"
        )
        {
            //turn the button on
        }
    }

    //button on, purchase,build,sell
    public void onyesclick()
    {
        TextMeshProUGUI txt;
        txt = GameObject.Find("Player1Money").GetComponent<TextMeshProUGUI>();
        txt.text = ("$" + p1money);
        txt = GameObject.Find("Player2Money").GetComponent<TextMeshProUGUI>();
        txt.text = ("$" + p2money);

        if (whatUserCanDo == "purchase")
        {
            purchase();
        }
        else if (whatUserCanDo == "build")
        {
            build();
        }
        else if (whatUserCanDo == "sell")
        {
            //sell();
        }

        // close the button tab
    }

    public void onnoclick()
    {
        //close the button tab
    }
}
