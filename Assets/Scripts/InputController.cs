using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Air controller
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class InputController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    CrabController[] CrabControllerRef;

    ///////////////////////////////
    // Control related variables //
    ///////////////////////////////

    [HideInInspector]
    public bool PlayerCanControlCrabs = false;

    ///////////
    // Setup //
    ///////////

    void Start()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    public void SetupCrabs(CrabController[] Crabs)
    {

        CrabControllerRef = Crabs;

    }

    void Update()
    {

        // If the player can control the crabs
        if (PlayerCanControlCrabs)
        {
            //Movement for player 1
            // W
            //A D
            // S
            // Detect player turn
            if (Input.GetKey(KeyCode.A))
            {
                CrabControllerRef[0].turnCrab(false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                CrabControllerRef[0].turnCrab(true);
            }
            // Detect player move
            if (Input.GetKey(KeyCode.W))
            {
                CrabControllerRef[0].moveCrab(true);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                CrabControllerRef[0].moveCrab(false);
            }



            //Movement for player 2
            // I
            //J L
            // K
            // Detect player turn
            if (Input.GetKey(KeyCode.J))
            {
                CrabControllerRef[1].turnCrab(false);
            }
            else if (Input.GetKey(KeyCode.L))
            {
                CrabControllerRef[1].turnCrab(true);
            }
            // Detect player move
            if (Input.GetKey(KeyCode.I))
            {
                CrabControllerRef[1].moveCrab(true);

            }
            else if (Input.GetKey(KeyCode.K))
            {
                CrabControllerRef[1].moveCrab(false);
            }


            //Movement for player 3
            //    Up
            //Left  Right
            //   Down
            // Detect player turn
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                CrabControllerRef[2].turnCrab(false);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CrabControllerRef[2].turnCrab(true);
            }

            // Detect player move
            if (Input.GetKey(KeyCode.UpArrow))
            {
                CrabControllerRef[2].moveCrab(true);

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                CrabControllerRef[2].moveCrab(false);
            }


            //Movement for player 4
            // 8
            //4 6
            // 5
            // Detect player turn
            if (Input.GetKey(KeyCode.Keypad4))
            {
                CrabControllerRef[3].turnCrab(false);
            }
            else if (Input.GetKey(KeyCode.Keypad6))
            {
                CrabControllerRef[3].turnCrab(true);
            }

            // Detect player move
            if (Input.GetKey(KeyCode.Keypad8))
            {
                CrabControllerRef[3].moveCrab(true);

            }
            else if (Input.GetKey(KeyCode.Keypad5))
            {
                CrabControllerRef[3].moveCrab(false);
            }
        }

    }

    ///////////////////////////////////
    // Air control related funcitons //
    ///////////////////////////////////

    void OnMessage(int from, JToken data)
    {
        AirConsole.instance.Message(from, "Full of pixels!");
    }

}
