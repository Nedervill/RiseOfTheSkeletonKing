
using UnityEngine;
using UnityEngine.UI;

public class animationButton : MonoBehaviour
{
    public Stats stats;
    public GameObject units;
    public Animator cameraAnim;
    public GameStatus status;
    public bool LockedByTutorial = false; // boolean locking the ability to buy skeleton warriors by mistake in a tutorial
    [SerializeField] GameObject buyFillData;
    [SerializeField] Animator crystalAnim;

    int mana;

    // Start is called before the first frame update
    private void Awake()
    {
        
        GetComponent<SpriteRenderer>().sprite = stats.deadSprite;
        buyFillData.GetComponent<BuyFillData>().stats = stats;
        LayoutRebuilder.ForceRebuildLayoutImmediate(buyFillData.GetComponent<RectTransform>()); // allows to reload the info popup prefab in appropriate sizes and position
        buyFillData.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        LayoutRebuilder.ForceRebuildLayoutImmediate(buyFillData.GetComponent<RectTransform>()); // allows to reload the info popup prefab in appropriate sizes and position
    }

    private void OnMouseDown()

    {
        // the buying of new unit peocess;
        mana = GameStatus.mana;
        if (( mana >= stats.cost) && (getCurrentCorpseNum()>0) && (!LockedByTutorial))
        {
            RoundStatus.SKprepareToCast = true;  //makes the SkeletonKing casting animation
            SoundFXManager.Instance.playSFXClip(stats.spawnSound, transform, 1);
            addToGameStatus();
            animate(); 
            status.manaUpd(stats.cost * -1);
            reduceCorpse();

        }
        // Debug.Log("i am pressed");

    }

    private void animate()
    {
        //creating the unit in planning mode and making it position not to override other units
        GameObject newUnit = Instantiate(stats.prefub, transform.position, Quaternion.identity, units.transform);
        //  newUnit.transform.parent = units.transform;
        newUnit.layer = 3;
        newUnit.GetComponent<PlanningMode>().reanimatePosition();
        newUnit.GetComponent<StatusUpd>().animating();  //the "ressuraecting" animation
        // newUnit.GetComponent<PlanningMode>().isDragged = true;

    }

    private void OnMouseEnter()
    {
        cameraAnim.SetBool("Reanimation", true);  // keep the camera and the crystal UI in "reanimation pan" mode
        crystalAnim.SetBool("Reanimation", true);
        buyFillData.SetActive(true); //show the popup info
        LayoutRebuilder.ForceRebuildLayoutImmediate(buyFillData.GetComponent<RectTransform>());  // allows to reload the info popup prefab in appropriate sizes and position

    }
    private void OnMouseExit()
    {
       // cameraAnim.SetBool("Reanimation", false);
        buyFillData.SetActive(false);
    }

  

    void addToGameStatus()
    {
        // update the number of active units for proper load in next levels
        switch (stats.theName)
        {
            case "Skeleton Warrior": GameStatus.SkeletonsWarriorsAnimated++; break;
            case "Skeleton Archer": GameStatus.SkeletonsArchersAnimated++; break;
            case "Skeleton Mage": GameStatus.SkeletonMagesAnimated++; break;
            case "Ghost": GameStatus.GhostsAnimated++; break;
            case "Skeleton Captain": GameStatus.SkeletonCaptainAnimated++; break;
            default: break;
        }
    }

   

    void reduceCorpse()
    {
        // reduce the nuumber of avilible corpses to use
        switch (stats.theName)
        {
            case "Skeleton Warrior": GameStatus.SoldierCorpse--; break;
            case "Skeleton Archer": GameStatus.ArcherCorpse--; break;
            case "Skeleton Mage": GameStatus.MageCorpse--; break;
            case "Ghost": GameStatus.AssasinCorpse--; break;
            case "Skeleton Captain": GameStatus.HeroCorpse--; break;

            default: break;
        }

    }

    public int getCurrentCorpseNum()
    {
      //  returens current availble corpses
        switch (stats.theName)
        {
            case "Skeleton Warrior": return (GameStatus.SoldierCorpse);
            case "Skeleton Archer": return (GameStatus.ArcherCorpse); 
            case "Skeleton Mage": return (GameStatus.MageCorpse); 
            case "Ghost": return (GameStatus.AssasinCorpse); 
            case "Skeleton Captain": return (GameStatus.HeroCorpse); 

            default: return( 0);
        }
    }
}