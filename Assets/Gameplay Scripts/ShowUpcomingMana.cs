using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowUpcomingMana : MonoBehaviour
{
    TextMeshProUGUI theText;
    [SerializeField]  RoundStatus roundStatus;
    int ManaToCome;
    int CurrentShownNumber;
    // Start is called before the first frame update
    void Start()
    {
        CurrentShownNumber = roundStatus.manaStepAdd + roundStatus.manaBonus;
        theText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ManaToCome = roundStatus.manaStepAdd + roundStatus.manaBonus;

        if (CurrentShownNumber > ManaToCome)
            CurrentShownNumber--;
        else if (CurrentShownNumber < ManaToCome)
            CurrentShownNumber++;

        if (CurrentShownNumber == 0)
            theText.text = "";
        else
        {
            
            theText.text = "+" + CurrentShownNumber + " on victory";
        }
       
    }
}
