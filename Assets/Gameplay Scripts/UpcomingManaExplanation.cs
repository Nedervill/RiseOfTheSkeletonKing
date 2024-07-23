using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpcomingManaExplanation : MonoBehaviour
{
    TextMeshProUGUI theText;
    [SerializeField] RoundStatus roundStatus;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
        theText.text = roundStatus.manaBonus + " third of mana bonus\n";
        theText.text += roundStatus.manaStepAdd + " mana bonus of round " + (GameStatus.RoundNum);
    }
        void Update()
    {
        theText.text = roundStatus.manaBonus + " third of mana bonus\n";
        theText.text += roundStatus.manaStepAdd + " mana bonus of round " + (GameStatus.RoundNum);
      


    }
}
