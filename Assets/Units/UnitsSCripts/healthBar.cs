using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    public Defence defense;
     float health;
     float maxHealth;
    public GameObject theBar;
    public GameObject theBarBack;
    public GameObject gloryKillCam;
    bool visible = false;
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        health = defense.health;
        maxHealth = defense.maxHealth;
        

        if ((health != maxHealth) && (health >0) && (Camera.main.name != "GloryKillCam") && (RoundStatus.currentgameStatus!=RoundStatus.CurrrentGameStatus.Aftermath))
            visible = true;
        else
            visible = false;

        if (visible)
        {
            theBar.SetActive(true);
            theBarBack.SetActive(true);
            scale =   health/ maxHealth;
            if (scale > 0.25)
                theBar.GetComponent<SpriteRenderer>().color = Color.green;
            else
                theBar.GetComponent<SpriteRenderer>().color= Color.red;

            transform.localScale = new Vector3 (scale, 1, 1);
        }
        else
        {
            theBarBack.SetActive(false);
            theBar.SetActive(false);
        }

    }
}
