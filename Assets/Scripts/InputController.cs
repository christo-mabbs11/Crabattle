using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    CrabController[] CrabControllerRef;

    ///////////
    // Setup //
    ///////////

    public void SetupCrabs(CrabController[] Crabs)
    {

        CrabControllerRef = Crabs;

        Debug.Log(CrabControllerRef[0]);

    }

    void Update()
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
