using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleAnimationScript : MonoBehaviour
{

    private SpriteRenderer battleSpriteRender;

    private void Awake()
    {
        battleSpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableSprite()
    {
        battleSpriteRender.enabled = false;
    }

    public void enableSprite()
    {
        battleSpriteRender.enabled = true;
    }

}
