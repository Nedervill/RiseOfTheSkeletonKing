
using UnityEngine;
using UnityEngine.EventSystems;


public class StatusUpd : MonoBehaviour
{
    /*
      this is unit's maintancnce component. It initates the unit, changes its status and allows most of the players interaction with the unit

    */

    public GameObject cameras;
    public Stats stats;
    public bool controlLost = false;
    public int controlsCounter = 0;
    public GameObject SpriteHolder;
    public GameObject outline;

    private GameObject units;
    private TheLists unitsLists;
    private TargetSelector targetSelector;
    private Defence defence;
    private Attack attack;
    private MoveToTarget moveToTarget;
    private Animator animator;
    private Rigidbody rb;
    private SpecialAttack specialAttack;
    Collider myCollider;
    private Vector3 pushedOldPos;
    [SerializeField] GameObject markOfDeathEffect;
    [SerializeField] GameObject reanimateEffect;
    [SerializeField] GameObject soulInfusionEffect;

    private bool pushedBool = false;
    public bool justGloryKilled = false;
    bool planningMode = true;
    public bool elite;
    bool disableOutlineonFirstUpd = true;
    float outlineTime;
    float outlineTimeLimit = 0.05f;

    public bool tutorialPlanningModeOverride = false;
    private void Awake()
    {


        myCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        animator = SpriteHolder.GetComponent<Animator>();

        initiateTargets();
        initiateDefence();
        initiateAttack();
        initiateMovement();
        initiateSpecialAttack();

        //   planiingModeRoutine();

    }

    private void Start()
    {
        elite = checkIfIAmElite();
        outline.SetActive(true);
    }

    void Update()
    {
        if (outline.activeInHierarchy)  //if outline is active, but mouse is not on top of the unit, the timer will make the outline dissapear proved for a better solution then onmosExit since during game proceess units can teleport or jump in another way without regestering it
        {
            outlineTime += Time.unscaledDeltaTime;
            if (outlineTime > outlineTimeLimit)
                outline.SetActive(false);
        }


        if (disableOutlineonFirstUpd)  // allows some of outlines paramethers reset befeore the game starts
        {
            outline.SetActive(true);
            outline.SetActive(false);
            disableOutlineonFirstUpd = false;
        }
        if (RoundStatus.draggingIsHappening) //if any unit is dragged, no outlines will be triggered
            outline.SetActive(false);

        if (planningMode)
            planiingModeRoutine();





        if (controlsCounter < 0)
            controlsCounter = 0;

        if (animator != null)
        {


            if (SpriteHolder.GetComponent<AnimationFeedback>().recovered) //recovery from push
            {
                pushedBool = false;
               
                controlsCounter--;
                SpriteHolder.GetComponent<AnimationFeedback>().recovered = false;
            }

            if (SpriteHolder.GetComponent<AnimationFeedback>().reanimated) //recovery from reanimation
            {
               
                controlsCounter--;
                SpriteHolder.GetComponent<AnimationFeedback>().reanimated = false;
            }
        }
        if (controlLost && (controlsCounter == 0) && (defence.alive == true)) //restoring the unit's lost conntol over itself
        {
            controlLost = false;
            gameObject.GetComponent<MoveToTarget>().enabled = true;
            gameObject.GetComponent<Attack>().enabled = true;
            gameObject.GetComponent<TargetSelector>().enabled = true;
            gameObject.GetComponent<TargetSelector>().target = null;
        }

        if (winOrLose.victory) // allowing units to victory march with no collsions by moving them to no-collision layer
        {
            gameObject.layer = 8;
        }


    }

   
    void planiingModeRoutine()  //once planning mode will be over, the units will move to default layer. meanwhile, the function will maintain their static behaviour
    {
        if (gameObject.layer == 3)
        {
            if (!tutorialPlanningModeOverride)  // after unit is placed on the requested circle, it's Planning mode component will be switched off preventing from player to drag theem off. hence the override
                 gameObject.GetComponent<PlanningMode>().enabled = true;
            planningMode = true;
            loseControl();
            controlsCounter = 1;
            animator.SetBool("Move", false);
            //    rb.constraints = RigidbodyConstraints.FreezePositionY;
        }


        else if (gameObject.layer == 0)
        {
            //  rb.constraints = RigidbodyConstraints.None;
            controlsCounter = 0;
            gameObject.GetComponent<PlanningMode>().enabled = false;
            planningMode = false;
        }

    }

    void initiateTargets() //initiating the <TartgetSelector> component with relevant stats data
    {
        targetSelector = gameObject.GetComponent<TargetSelector>();

        units = gameObject.transform.parent.gameObject;
        unitsLists = units.GetComponent<TheLists>();

        if (gameObject.tag == "Ally")
        {
            targetSelector.allies = unitsLists.allies;
            targetSelector.enemies = unitsLists.enemies;
            targetSelector.allies.Add(gameObject);
        }
        else if (gameObject.tag == "Enemy")
        {
            targetSelector.enemies = unitsLists.allies;
            targetSelector.allies = unitsLists.enemies;
            targetSelector.allies.Add(gameObject);
        }
    }

    void initiateDefence()   //initiating the <Defence> component with relevant stats data
    {
        defence = gameObject.GetComponent<Defence>();
        defence.health = stats.health;
        defence.maxHealth = stats.health;
        defence.alive = true;
        defence.targetPriority = stats.targetPriority;
        defence.hitSound = stats.hitSound;
        defence.deathSound = stats.deathSound;
        defence.ArmourMat = stats.ArmourMat;

    }

    void initiateAttack()  //initiating the <Attack> component with relevant stats data
    {
        attack = gameObject.GetComponent<Attack>();

        attack.rangedDamage = stats.rangedDamage;
        attack.attackSpeed = stats.attackSpeed;
        attack.meeleDamage = stats.meeleDamage;
        attack.energyPerAttack = stats.energyPerAttack;
        attack.meeleAoe = stats.meeleAoe;
        attack.meeleAoeRange = stats.meeleAoeRange;
        attack.meelePushForce = stats.meelePushForce;
        attack.attackMat = stats.meeleAttackMat;
        attack.specialAttackMat = stats.specialAttackMat;

    }

    void initiateSpecialAttack()  //initiating the <SpecialAttack> component with relevant stats data
    {
        specialAttack = gameObject.GetComponent<SpecialAttack>();
        specialAttack.theName = stats.theName;

        attack.specialDamage = stats.specialDamage;
        attack.specialAoe = stats.specialAoe;
        attack.specialAoeRange = stats.specialAoeRange;
        attack.specialPushForce = stats.specialPushForce;

        specialAttack.specialAbilitySound1 = stats.specialAbilitySound1;
        specialAttack.specialAbilitySound2 = stats.specialAbilitySound2;
        specialAttack.specialAttackMat = stats.specialAttackMat;


    }
    void initiateMovement()  //initiating the <MoveToTarget> component with relevant stats data
    {
        moveToTarget = gameObject.GetComponent<MoveToTarget>();
        moveToTarget.ranged = stats.ranged;
        moveToTarget.range = stats.range;
        moveToTarget.meeleRange = stats.meeleRange;
        moveToTarget.moveSpeed = stats.moveSpeed;
    }

    public void initiateDeath() // making needed death-relateed animations and re-calculetions 
    {


     
        units = gameObject.transform.parent.gameObject;
        removeOneFromGameStatus();  //was made for previous game interractions, when reanimated the fallen units was a choise. still might be needed

        unitsLists = units.GetComponent<TheLists>();
        unitsLists.GKME(gameObject, elite);  //Sending the unit for a possible Glory Kill cam


        //remove the units from possible target lists and add or removem them from other conters 
        if (gameObject.tag == "Ally")
        {
            unitsLists.allies.Remove(gameObject);
            unitsLists.undeadToReamimate.Add(gameObject);
        }
        else if (gameObject.tag == "Enemy")
        {
            if (defence.targetPriority == 4)
                RoundStatus.markedTargets--;
            unitsLists.enemies.Remove(gameObject);
            addCorpse();
        }

        gameObject.GetComponent<MeshCollider>().isTrigger = true;
        gameObject.GetComponent<MoveToTarget>().enabled = false;
        gameObject.GetComponent<Attack>().enabled = false;
        //  gameObject.GetComponent<SpecialAttack>().enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<TargetSelector>().enabled = false;



    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // making sure its not a menu mode
        { 

            if ((tag == "Enemy") && (MarkOfDeathActivation.MarkOfDeathSelectionMode == true) && (defence.alive)) //getting MarkOfDeath spell
            {
                SoundFXManager.Instance.playMarkOfDeathCast(transform, 1.3f);
                GameStatus.mana -= MarkOfDeathActivation.MarkOfDeathCost;  //reducing the mana cost from mana upon activation
                Time.timeScale = 1.0f; //back from slo-mo
                defence.targetPriority = 4; //making the unit top-priority target
                RoundStatus.markedTargets++; //increasing the markedTargets counter;
              
                applyEffect(markOfDeathEffect); //adding visual effect
                SpellsAviability.EnhancedMode = false;    //disablling the Full Energy Mode of thee Skeleton King
                MarkOfDeathActivation.MarkOfDeathSelectionMode = false;
              
            }

            if ((tag == "Ally") && (stats.theName != "Skeleton King") && (SoulInFusionActivation.SoulInFusionSelectionMode == true) && (defence.alive)) //Getting SoulInfusionSpell
            {
                applyEffect(soulInfusionEffect); //adding visual effect
                SoundFXManager.Instance.playSoulInFusionCast(transform, 1.3f);
                GameStatus.mana -= SoulInFusionActivation.SoulInFusionActivationCost;   //reducing the mana cost from mana upon activation
                Time.timeScale = 1.0f;//back from slo-mo
                defence.health += stats.health / 2;
                if (defence.health > stats.health)
                    defence.health = stats.health;
                attack.currentEnergy = 100;
                SpellsAviability.EnhancedMode = false;   //disablling the Full Energy Mode of thee Skeleton King
                SoulInFusionActivation.SoulInFusionSelectionMode = false;
              
            }
        }
    }

    void addCorpse()  //add one to the relevant corpses counter
    {
        switch (stats.theName)
        {
            case "Soldier": GameStatus.SoldierCorpse++; break;
            case "Archer": GameStatus.ArcherCorpse++; break;
            case "Mage": GameStatus.MageCorpse++; break;
            case "Assasin": GameStatus.AssasinCorpse++; break;
            case "Hero": GameStatus.HeroCorpse++; break;

            default: break;
        }

    }
    public void reanimate()  //things gappening to the reanimated undead unit 
    {
        loseControl(); // make unit non-responcive during the reanimation process
        applyEffect(reanimateEffect); //adding visual effect
        unitsLists.undeadToReamimate.Remove(gameObject);
        targetSelector.allies.Add(gameObject);
        gameObject.GetComponent<MeshCollider>().isTrigger = false;
        gameObject.GetComponent<MoveToTarget>().enabled = true;
        
        gameObject.GetComponent<MoveToTarget>().isActing = false;
        gameObject.GetComponent<Attack>().enabled = true;
        gameObject.GetComponent<TargetSelector>().enabled = true;

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; //unfreeze most of the constrains the unit gets upon death
        initiateDefence();

        animator.SetTrigger("Reanimated");
        addOneToGameStatus();
        controlsCounter++;
       

    }
    public void pushed() //the lllose of controll if the unit was pushed
    {
        controlsCounter++;
        loseControl();
        animator.SetTrigger("Pushed");
        // rb.excludeLayers = LayerMask.NameToLayer("Default");
        pushedBool = true;
        gameObject.layer = 7; //in layer 7 the unit can onlly colllide with "ground" layer

    }

    void loseControl() //when the unit loses contol it disables many of it other components for that time period 
    {
        controlLost = true;
        gameObject.GetComponent<MoveToTarget>().target = null;
        gameObject.GetComponent<MoveToTarget>().currentDestination = transform.position;
        gameObject.GetComponent<MoveToTarget>().enabled = false;
        gameObject.GetComponent<MoveToTarget>().thinkingTime = gameObject.GetComponent<MoveToTarget>().reactionTime;
       // gameObject.GetComponent<Attack>().target = null;
       gameObject.GetComponent<Attack>().enabled = false;
        gameObject.GetComponent<MoveToTarget>().target = null;
        gameObject.GetComponent<TargetSelector>().enabled = false;

    }

    public void animating()
    {
        applyEffect(reanimateEffect);
        animator.SetTrigger("Reanimated");
    }

    private void OnCollisionEnter(Collision other)  //checks if the pushed unit is met the fround, for starting the recovery animation
    {
        if ((other.gameObject.tag == "Ground") && pushedBool)
        {

            animator.SetTrigger("Landed");
            //  rb.includeLayers = LayerMask.NameToLayer("Default");
            gameObject.layer = 0;
        }
    }


    void removeOneFromGameStatus() //removing one from relevant active units counter
    {

        switch (stats.theName)
        {
            case "Skeleton Warrior": GameStatus.SkeletonsWarriorsAnimated--; break;
            case "Skeleton Archer": GameStatus.SkeletonsArchersAnimated--; break;
            case "Skeleton Mage": GameStatus.SkeletonMagesAnimated--; break;
            case "Ghost": GameStatus.GhostsAnimated--; break;
            case "Skeleton Captain": GameStatus.SkeletonCaptainAnimated--; break;

            default: break;
        }

    }

    void addOneToGameStatus() //adding one to the relevant active units counter
    {

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

  
    private void OnMouseOver()
    { 
        if (!EventSystem.current.IsPointerOverGameObject())  //if not in menu currentlly
        {


            if ((defence.alive) && (Camera.main.name != "GloryKillCam") && !RoundStatus.draggingIsHappening) //start the outline activation and reset the activation timeer
            {
                outlineTime = 0;
                outline.SetActive(true);
            }
        }
    }

      
        private void OnTriggerStay(Collider collision)  // amore mechanicall allternative to regular collider interaction, works better in this game case
    {
        if ((collision.gameObject.tag == tag) && (RoundStatus.currentgameStatus == RoundStatus.CurrrentGameStatus.Battle) && (collision.gameObject.GetComponent<Defence>().alive) && (defence.alive)) //works only between living units on the same side during the battle stage
        {
            GameObject other = collision.gameObject;
            if (other.transform.position.z > transform.position.z)
            {
                transform.position -= new Vector3(0, 0, 0.01f);
            }
            else if (other.transform.position.z < transform.position.z)
            {
                transform.position += new Vector3(0, 0, 0.01f);
            }

        }
    }

    private bool checkIfIAmElite()  //check if the unit is rare enouth for in middlle-battle glorykill
    {
        int totalArmyNum = targetSelector.allies.Count;
        int meCounter = 0;

        for (int i = 0; i < targetSelector.allies.Count; i++)
        {
            if (targetSelector.allies[i].GetComponent<StatusUpd>().stats == stats)
                meCounter++;
        }

        if (meCounter > totalArmyNum / 5)
            return false;
        else return true;
    }

    private void applyEffect(GameObject effectObject)  //creates an effect-sprite object and makes it follow the target
    {

        //  activation.MarkOfDeathEffect = MarkOfDeathEffect;
        GameObject MEffect = Instantiate(effectObject, transform.position, Quaternion.identity);

        switch (effectObject.name)
        {
            case "MarkOfDeath":
                MEffect.GetComponent<MarkOfDeathEffect>().target = gameObject;
                break;
            case "Reanimation":
                MEffect.GetComponent<ReanimationEffect>().target = gameObject;
                break;
            case "SoulInfusion":
                MEffect.GetComponent<SoulInfusionEffect>().target = gameObject;
                break;

        }

       
       
       
    }

    public void TutorialPlanningModeOverride()
    {
        tutorialPlanningModeOverride = true; 
    }

    
}

