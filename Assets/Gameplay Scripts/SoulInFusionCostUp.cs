using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulInFusionCostUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Costtext;
  //  [SerializeField] SoulInFusionActivation soulInFusionActivation;
    int CurrentCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() // show the current cost of the spell in a proper color 
    {
        CurrentCost = SoulInFusionActivation.SoulInFusionActivationCost;

        if (CurrentCost > GameStatus.mana)
        {
             GetComponent<UnityEngine.UI.Image>().color = new Color(0.8962264f, 0.03804733f, 0.101879f, 1);  // not enough mana - will be in red
            Costtext.color = new Color(0.8962264f, 0.03804733f, 0.101879f, 1);
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            Costtext.color = new Color(0, 180, 134);
        }


        if (CurrentCost > 0)
            Costtext.text = CurrentCost + "";
        else
            Costtext.text = "FREE";
      
    }
}
