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

    public void enableIdle()
    {
        Debug.Log("FALSE!");
        deathSpriteRender.sprite = aliveSprite;        
        (gameObject.transform.Find("crab-base_01_animate-left")).gameObject.SetActive(false);
        deathSpriteRender.enabled = true;
    }

    public void enableDeath()
    {
        Debug.Log("FALSE 2!");
        (gameObject.transform.Find("crab-base_01_animate-left")).gameObject.SetActive(false);
        deathSpriteRender.sprite = deathSprite;        
        deathSpriteRender.enabled = true;
    }

    public void enableMovement()
    {
        if ((gameObject.transform.Find("crab-base_01_animate-left")).gameObject.activeInHierarchy == false)
        {
            (gameObject.transform.Find("crab-base_01_animate-left")).gameObject.SetActive(true);
            deathSpriteRender.sprite = deathSprite;
            deathSpriteRender.enabled = false;
        }
    }
}
