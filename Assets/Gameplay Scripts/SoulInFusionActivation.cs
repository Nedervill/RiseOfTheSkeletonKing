using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInFusionActivation : MonoBehaviour
{
    public static int SoulInFusionActivationCost = 50;
    public int normalCost = 50;
    public int enhansedlCost = 0;
    public static bool SoulInFusionSelectionMode = false;
    [SerializeField] AudioClip soulInFusionSelect;
   
    // Start is called before the first frame update
    void Update()
    {
        if (SpellsAviability.EnhancedMode) //check if the Skeleton king got full energy and switch for propper cost
            SoulInFusionActivationCost = enhansedlCost;
        else
            SoulInFusionActivationCost = normalCost;


       
        if ((SoulInFusionSelectionMode)  && (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Battle))//case of getting into end of battle while skill selection  is still activateed
        {
           
           
            SoulInFusionSelectionMode = false;
           
        }
      
    }

    // Update is called once per frame
    public void activateSpell()
    {
        if (GameStatus.mana >= SoulInFusionActivationCost)
        {
            Time.timeScale = 0.2f;  //slow-mo for easier target selection
            
            SoulInFusionSelectionMode = true;
            SoundFXManager.Instance.playSFXClip(soulInFusionSelect, transform, 1.3f);
          
        }

    }

  
}
