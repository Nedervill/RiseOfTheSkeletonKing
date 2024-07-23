using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MarkOfDeathActivation : MonoBehaviour
{
    static public int MarkOfDeathCost = 50;
    public int normalCost = 50;
    public int enhansedlCost = 0;
    public static bool MarkOfDeathSelectionMode = false;
    [SerializeField] AudioClip markOfDeathSelect;
  
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SpellsAviability.EnhancedMode)  //check if the Skeleton king got full energy and switch for propper cost
            MarkOfDeathCost = enhansedlCost;
        else
            MarkOfDeathCost = normalCost;

        
         if ((MarkOfDeathSelectionMode) && (RoundStatus.currentgameStatus == RoundStatus.CurrrentGameStatus.Battle))//case of getting into end of battle while skill selection  is still activateed
        {
          
            MarkOfDeathSelectionMode = false;
         
        }
       
    }

    public void activateSpell()
    {
        if (GameStatus.mana >= MarkOfDeathCost)
        {
            Time.timeScale = 0.2f;  //slow-mo for easier target selection
            //GameStatus.mana -= MarkOfDeathCost;
            MarkOfDeathSelectionMode = true;
            SoundFXManager.Instance.playSFXClip(markOfDeathSelect, transform, 1.3f);
          

        }

    }

    


}
