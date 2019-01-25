using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    CrabController CrabControllerRef = null;

    ///////////
    // Setup //
    ///////////

    void Start()
    {

        // Grab useful references
        CrabControllerRef = GameObject.FindWithTag("crab").GetComponent<CrabController>();

    }

    void Update()
    {

        // Detect player turn
        if (Input.GetKey(KeyCode.A))
        {
            CrabControllerRef.turnCrab(false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CrabControllerRef.turnCrab(true);
        }

        // Detect player move
        if (Input.GetKey(KeyCode.W))
        {
            CrabControllerRef.moveCrab(true);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            CrabControllerRef.moveCrab(false);
        }

    }


}
