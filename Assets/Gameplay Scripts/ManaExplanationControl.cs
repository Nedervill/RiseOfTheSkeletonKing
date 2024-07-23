using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaExplanationControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject manaExplanation;
    void Start()
    {
        manaExplanation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showExplanation()
    {
        if (RoundStatus.currentgameStatus!=RoundStatus.CurrrentGameStatus.Aftermath)
         manaExplanation.SetActive(true);
    }

    public void hideExplanation()
    {
        manaExplanation.SetActive(false);
    }
}
