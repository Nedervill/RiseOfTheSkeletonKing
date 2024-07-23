using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMana : MonoBehaviour
{
    TextMeshProUGUI theText;
    // public GameStatus gameStatus;
    int mana;
    int currentShownMana;  //the distinxtion between the mana cand current shown mana is made for the increasing/decreasing animation
    void Start()
    {
        currentShownMana = GameStatus.mana;
        theText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        mana = GameStatus.mana;
        if (currentShownMana > mana)
           currentShownMana--;
        
        else if (currentShownMana < mana)
            currentShownMana++;

        theText.text = currentShownMana.ToString();
    }
}
