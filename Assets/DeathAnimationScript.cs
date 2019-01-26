using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationScript : MonoBehaviour
{
    private SpriteRenderer deathSpriteRender;

    public Sprite deathSprite;
    public Sprite aliveSprite;

    private void Awake()
    {
        deathSpriteRender = gameObject.GetComponent<SpriteRenderer>();
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
        deathSpriteRender.sprite = aliveSprite;
        //deathSpriteRender.enabled = false;
    }

    public void enableSprite()
    {
        deathSpriteRender.sprite = deathSprite;
        //deathSpriteRender.enabled = true;
    }
}
