using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TheLists : MonoBehaviour
{
    /* the component hollds and overviews the current llists of allies, enemies and unded that can be reanimated
     

    */

    public List<GameObject> allies = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> undeadToReamimate = new List<GameObject>();
    public GameObject noGoZone;
    public GloryKill cameras;
    public int GloryKillChance = 0;
    [SerializeField] float GKcooldown = 5;
    float lastgloryKilltime;
    public int reanimationCost = 0;

    float anglePerAlly;
    float anglePerEnemy;
    // Start is called before the first frame update
    void Start()
    {
        lastgloryKilltime = Time.timeSinceLevelLoad;
        anglePerAlly =1.5f / allies.Count;
        anglePerEnemy = 1.5f / enemies.Count;

    }

    // Update is called once per frame
    void Update()
    {
        calculateReanimateUndeadCost();
       
    }

    public void GKME(GameObject obj, bool elite)  //sends the glory kill camera if it is needed
    {
       int KillCamEnabled = PlayerPrefs.GetInt("killCamOn");
        if (obj.tag == "Ally")
        {
            cameras.newDeath(obj.tag, anglePerAlly);
        }
        else
            cameras.newDeath(obj.tag, anglePerEnemy);

        if ((KillCamEnabled != 0) && (!MarkOfDeathActivation.MarkOfDeathSelectionMode) && (!SoulInFusionActivation.SoulInFusionSelectionMode))
            if ((Random.Range(0, 100) < GloryKillChance) || ((enemies.Contains(obj)) && (enemies.Count == 1)) || ((allies.Contains(obj)) && (allies.Count == 1)) || (elite == true))
            {
                if (Time.timeSinceLevelLoad >= GKcooldown + lastgloryKilltime)
                {
                    lastgloryKilltime = Time.timeSinceLevelLoad;
                    cameras.GloryKillShotStart(obj);
                }
           
            }
    }

    public void stopPlanning()  // transfer the units from pllanning mode to the default plane so the battle starts
    {

        noGoZone.SetActive(false);
        for (int i = 0; i < allies.Count; i++)
            allies[i].gameObject.layer = 0;
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].gameObject.layer = 0;

    }

    public void reanimateUndead()  //function called upon activation of the relevant spell and after the undead have won the battle
    {

        calculateReanimateUndeadCost();
        if (RoundStatus.currentgameStatus == RoundStatus.CurrrentGameStatus.Aftermath)  //will not cost the user mana upon win
            reanimationCost = 0;
        if ((GameStatus.mana >= reanimationCost) && (undeadToReamimate.Count > 0))
        {
            if (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Aftermath)
                RoundStatus.SKprepareToCast = true;
           // specialAttackEffect.Stop();
            SpellsAviability.EnhancedMode = false;
            GameStatus.mana -= reanimationCost;
            SoundFXManager.Instance.playMassReanimationCast(transform, 1.3f);
            int unitsToRessurect = undeadToReamimate.Count;
            for (int i = 0; i < unitsToRessurect; i++)
            {
                undeadToReamimate[0].GetComponent<StatusUpd>().reanimate();

            }
        }
        calculateReanimateUndeadCost();
    }

    public void calculateReanimateUndeadCost()
    {
       if (SpellsAviability.EnhancedMode)
           reanimationCost = 250;
       else
          reanimationCost = 500;

      
        
    }

}