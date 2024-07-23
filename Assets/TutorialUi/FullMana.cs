using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMana : MonoBehaviour
{
    [SerializeField] GameObject EnergyFull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpellsAviability.EnhancedMode)
        {
            Time.timeScale = 0;
            EnergyFull.SetActive(true);
        }
        if (SoulInFusionActivation.SoulInFusionSelectionMode)
            EnergyFull.SetActive(false);

    }
}
