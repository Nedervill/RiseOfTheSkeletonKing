using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyFillData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Cost;
    [SerializeField] TextMeshProUGUI Data;
    [SerializeField] TextMeshProUGUI DataTitles;
    [SerializeField] TextMeshProUGUI SpecialAbility;
    [SerializeField] TextMeshProUGUI SpecialPassive;
    [SerializeField] GameObject SPPanel;
    public Stats stats;
    [SerializeField] GameObject missle;
    string dataString;
    // Start is called before the first frame update
    void Start()
    {
       
        Name.text = stats.theName;
        
        if (GameStatus.mana<stats.cost)
            Cost.color =  new Color(0.8962264f, 0.03804733f, 0.101879f, 1);
        else
            Cost.color = new Color(0, 180, 134);

        Cost.text = stats.cost + "";

        if (stats.ranged)
            fillRangedInfo();
        else
            fillMeeleInfo();
        if (!stats.ranged)
            rescalePanelsForMeele();
        SpecialAbility.text = stats.specialAbilityText;
        if (stats.specialPassiveText.Length == 0)
            SPPanel.SetActive(false);
        else
            SpecialPassive.text = stats.specialPassiveText;


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void rescalePanelsForMeele()
    {
        DataTitles.text = "HEALTH:\r\nENERGY\r\nATK SPEED\r\nMEELE DMG";
        //  StatsPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0,11);
    }
    private void fillMeeleInfo()
    {
        dataString = stats.health + "\n";
        dataString += stats.attackSpeed + "\n";
        if (stats.meeleAoe)
            dataString += "areal ";
        dataString += stats.meeleDamage;
        dataString += "\n";
      


        Data.text = dataString;
    }

    private void fillRangedInfo()
    {
        dataString = stats.health + "\n";
        dataString += stats.attackSpeed + "\n";
        if (stats.meeleAoe)
            dataString += "areal ";
        dataString += stats.meeleDamage;
        dataString += "\n";
        dataString += stats.range + "\n";
        if ((stats.ranged) && (missle.GetComponent<missileDmg>().explosionRad > 0))
            dataString += "areal ";
        dataString += stats.rangedDamage;


        Data.text = dataString;
    }
}
