using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRandomizer : MonoBehaviour
{
   
    public bool corruptble;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
     public List<Sprite> Corruptedsprites = new List<Sprite>();
    public int decoVariant;
    [SerializeField] SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    { //Makes the decoaration sprite one random out of the sipplied list
        decoVariant = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[decoVariant];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorruptTheDeco()  //switches the sprite to its "corrupted" counterpart
    {
        spriteRenderer.sprite = Corruptedsprites[decoVariant];
    }
}
