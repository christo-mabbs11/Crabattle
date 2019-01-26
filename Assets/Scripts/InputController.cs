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
