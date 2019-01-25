using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    CrabController CrabControllerRef;

    ///////////
    // Setup //
    ///////////

    void Awake()
    {

        // Grab useful references
        // CrabControllerRef = this.GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        // Detect player controls here (basic)
        // if (Input.GetKey(KeyCode.A))
        //     rb.AddForce(Vector3.left);
        // if (Input.GetKey(KeyCode.D))
        //     rb.AddForce(Vector3.right);
        // if (Input.GetKey(KeyCode.W))
        //     rb.AddForce(Vector3.up);
        // if (Input.GetKey(KeyCode.S))
        //     rb.AddForce(Vector3.down);

    }


}
