using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBar : MonoBehaviour
{
    public Attack attack;
    public Defence defence;
    float energy;
    float maxHMaxEnergy = 100;
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
        energy = attack.currentEnergy;
       // maxHMaxEnergy = defense.maxHealth;


        if ((energy > 0) && (Camera.main.name != "GloryKillCam") && (defence.health>0)&& (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Aftermath))
            visible = true;
        else
            visible = false;

        if (visible)
        {
            theBar.SetActive(true);
            theBarBack.SetActive(true);
            scale = energy / maxHMaxEnergy;
           

            transform.localScale = new Vector3(scale, 1, 1);
        }
        else
        {
            theBarBack.SetActive(false);
            theBar.SetActive(false);
        }

    }
}
