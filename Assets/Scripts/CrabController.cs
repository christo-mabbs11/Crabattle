using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    Rigidbody2D Rigidbody2DRef;

    ///////////
    // Setup //
    ///////////

    void Awake()
    {

        // Grab useful references
        Rigidbody2DRef = this.GetComponent<Rigidbody2D>();

    }

    ///////////////////
    // Crab movement //
    ///////////////////

    public void turnCrab(bool turnDirection)
    {



    }

    public void moveCrab(bool moveDirection)
    {



    }

}
