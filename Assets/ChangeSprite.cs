using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    private GameObject playa;
    private PlayerController playaController;

    void Start()
    {
        playa = GameObject.Find("Playa");
        playaController = playa.GetComponent<PlayerController>();
        string type = playaController.GetDotType();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (type.Contains("Zaza"))
            spriteRenderer.sprite = sprites[2];
        else if (type.Contains("Husky"))
            spriteRenderer.sprite = sprites[1];
        else if (type.Contains("Doggo"))
            spriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
