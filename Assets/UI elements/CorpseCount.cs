using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseCount : MonoBehaviour
{
    public animationButton aniButt;
    TMPro.TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = ""+ aniButt.getCurrentCorpseNum();
        
    }
}
