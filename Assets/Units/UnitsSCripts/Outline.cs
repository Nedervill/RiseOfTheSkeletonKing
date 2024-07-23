using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
{
    [SerializeField] GameObject theUnit;
    public GameObject MainSprite;
    private SpriteRenderer mySprite;
    [SerializeField] GameObject info;
    float infoAppearDelay = 0.5f;
    float lastTimeEnabled;
    bool infoEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
        mySprite = GetComponent<SpriteRenderer>();
        // spriteRenderer.material.color = Color.red;
       
       
    }

    // Update is called once per frame
    void Update()
    {
        Color customColor = new Color(0, 255f, 50f, 0.1f);
        if (((SoulInFusionActivation.SoulInFusionSelectionMode) && ((theUnit.tag == "Enemy") || theUnit.name == "Skeleton King")) || ((MarkOfDeathActivation.MarkOfDeathSelectionMode) && (theUnit.tag != "Enemy")))
            customColor = new Color(230, 0, 0, 1f);

        mySprite.material.SetColor("_Color", customColor);

        if (infoEnabled == false)
        {
            info.SetActive(true);
            infoEnabled = false;
        }

        lastTimeEnabled += Time.unscaledDeltaTime;
        if (lastTimeEnabled >= infoAppearDelay)
        {

            info.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
        }
        else
            info.SetActive(false);


        mySprite.sprite = MainSprite.GetComponent<SpriteRenderer>().sprite;
        mySprite.flipX = MainSprite.GetComponent<SpriteRenderer>().flipX;



    }

    private void OnEnable()
    {
        lastTimeEnabled = 0;
    }
}
