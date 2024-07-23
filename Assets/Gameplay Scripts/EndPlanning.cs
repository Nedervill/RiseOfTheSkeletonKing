using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlanning : MonoBehaviour
{
    public  bool imUp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //disables itslef once the planning mode is over
    {
        if (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Planning)
        {
        
            imUp = false;
            gameObject.SetActive(false);
          
        }
    }
}
