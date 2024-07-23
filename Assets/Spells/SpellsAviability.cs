
using UnityEngine;
using UnityEngine.UI;
using static RoundStatus;

public class SpellsAviability : MonoBehaviour
{
    [SerializeField] GameObject[] spells;
    [SerializeField] GameObject enchasedEffects;
    [SerializeField] Defence SKDefence;
    public bool active = false;
    static public bool SwitchOff = false;
    static public bool EnhancedMode = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i].GetComponent<Button>().interactable = false;
            enchasedEffects.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()

    {
        if (!SKDefence.alive)  //disablle spells if Skeleton King is dead
            makeSpellsUnActive();

        if (EnhancedMode)  //on fulll mana make the buttons shy!
        {
            enchasedEffects.SetActive(true);
        }
        else
            enchasedEffects.SetActive(false);


        if (SwitchOff)
        {
            SwitchOff = false;
            makeSpellsUnActive();
        }

    }

    public void makeSpellsActive()
    {

        for (int i = 0; i < spells.Length; i++)
        {
            active = true;
            spells[i].GetComponent<Button>().interactable = true;

        }
    }

    
     public void makeSpellsUnActive()
    {
        if (currentgameStatus != CurrrentGameStatus.Aftermath)
            for (int i = 0; i < spells.Length; i++)
            {
                active = false;
                spells[i].GetComponent<Button>().interactable = false;

            }



    }

    public void afterMathMode()
    {
        for (int i = 0; i < spells.Length; i++)
      
          
            spells[i].GetComponent<Button>().interactable = false;
      

    }


  
}
