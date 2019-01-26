using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabShellCollection : MonoBehaviour
{

    private void Awake()
    {
        //gameObject.AddComponent<BoxCollider2D>();
        //Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        //rb.bodyType = RigidbodyType2D.Dynamic;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag.Equals("Shells"))
        {
            Debug.Log("Touched a shell!");
            col.gameObject.tag = "Untagged";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<ShellController>().shellPickedUp(col.gameObject);
        }
    }
}
