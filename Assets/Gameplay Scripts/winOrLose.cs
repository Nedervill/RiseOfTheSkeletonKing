using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winOrLose : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool victory = false;
    public static bool defeat = false;
    public TheLists unitsLists;
    public GloryKill gk;
    bool enemiesInitiatedd = false;
    bool alliesInitiatedd = false;
    void Start()
    {
        defeat = false;
        victory = false;
    }

    // Update is called once per frame
    void Update()
    {
        //protect the win or lose conditions trigger before the units spawn
        if ((unitsLists.enemies.Count > 0) && !enemiesInitiatedd)
            enemiesInitiatedd = true;
        if ((unitsLists.allies.Count > 0) && !alliesInitiatedd)
            alliesInitiatedd = true;

        if (!gk.GKHappening)
        {

        
        if ((alliesInitiatedd) && (unitsLists.allies.Count == 0) && (victory == false)) //check if its not the gllorykill happenaing and if the game is not won yet
            {
                // Debug.Log("You Lose!");
                defeat = true;
            }

          if ((enemiesInitiatedd) && (unitsLists.enemies.Count == 0) && (defeat == false)) //check if its not the gllorykill happenaing and if the game is not lost yet
            {
               
                victory = true;
              
            }
        }
    }
}






















