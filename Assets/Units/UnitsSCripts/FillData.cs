
using TMPro;
using UnityEngine;

public class FillData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Data;
    [SerializeField] TextMeshProUGUI DataTitles;
    [SerializeField] TextMeshProUGUI SpecialAbility;
    [SerializeField] TextMeshProUGUI SpecialPassive;
    [SerializeField] Stats stats;
    [SerializeField] Attack attack;
    [SerializeField] Defence defence;
    [SerializeField] GameObject missle;
    [SerializeField] GameObject SPPanel;

    string dataString;
    // Start is called before the first frame update
    void Awake()
    {
        Name.text = stats.theName;
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
        if (stats.ranged)
            fillRangedInfo();
        else
            fillMeeleInfo();


    }

    private void fillRangedInfo()
    {

        dataString = defence.health + "/" + stats.health + "\n";
        dataString += attack.currentEnergy + "/100" + "\n";
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
    private void fillMeeleInfo()
    {

        dataString = defence.health + "/" + stats.health + "\n";
        dataString += attack.currentEnergy + "/100" + "\n";
        dataString += stats.attackSpeed + "\n";
        if (stats.meeleAoe)
            dataString += "areal ";
        dataString += stats.meeleDamage;

        Data.text = dataString;
    }

    private void rescalePanelsForMeele()
    {
        DataTitles.text = "HEALTH:\r\nENERGY\r\nATK SPEED\r\nMEELE DMG";
      //  StatsPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0,11);
    }
}
