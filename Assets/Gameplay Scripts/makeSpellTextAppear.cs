using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class makeSpellTextAppear : MonoBehaviour
{
    [SerializeField] GameObject spellText;
    // Start is called before the first frame update
    void Start()
    {
        spellText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void showSpellText()
    {

        spellText.SetActive(true);
    }
    public void hideSpellText()
    {

        spellText.SetActive(false);
    }

}
