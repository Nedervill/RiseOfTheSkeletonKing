using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculateCost : MonoBehaviour
{
    public TextMeshProUGUI Costtext;
    public TheLists theLists;
  
    [SerializeField] MarkOfDeathActivation markOfDeathActivation;
    int CurrentCost;

    // Start is called before the first frame update
    void Start()
    {
       
       /// text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCost = theLists.reanimationCost;

        if (CurrentCost > GameStatus.mana)
        {

              GetComponent<UnityEngine.UI.Image>().color = new Color(0.8962264f, 0.03804733f, 0.101879f, 1);
            Costtext.color = new Color(0.8962264f, 0.03804733f, 0.101879f, 1);
        }

        else
        {
           GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            Costtext.color = new Color(0, 180, 134);
        }



        if (CurrentCost > 0)
            Costtext.text = CurrentCost +"";
        else
            Costtext.text = "FREE";


    }
}