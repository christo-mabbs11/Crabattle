using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        ((Animator)gameObject.GetComponent<Animator>()).GetCurrentAnimatorStateInfo(0).ToString();
    }
}
