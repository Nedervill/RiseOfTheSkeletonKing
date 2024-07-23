
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoundStatus : MonoBehaviour
{
    /* main game component that oversees current game status and most of the gameplay mechanics

    */

    [SerializeField] GameObject settings;
    bool settingsOn=false;
    public RuleTile grassOrnament;
    [SerializeField] private Texture2D RegularCursor;
    [SerializeField] private Texture2D GreenCursor;
    [SerializeField] private Texture2D BlackCursor;
    public GameObject units;
    public static int markedTargets = 0;
    public static CurrrentGameStatus currentgameStatus;
    public static bool draggingIsHappening = false;
    public TheLists TheLists;
    public GameObject AftermathUI;
    public GameObject LoseScreen;
    public Stats[] enemiesStats;
    public Stats[] armyStats;
    [SerializeField] int spawnChance = 10;
    [SerializeField] SpellsAviability spellsAviability;
    public int manaBonus;
    public int manaStepAdd;
    [SerializeField] int CurrentArmyStrength;
    [SerializeField] int CurrentRound;
    Vector2 cursorHotSpot;
    bool specialCursorOn = false;
    [SerializeField] Animator crystalAnimator;
    [SerializeField] AudioClip chargeSound;
    [SerializeField] AudioClip advanceSound;


    public static bool SKprepareToCast = false;
    [SerializeField] Animator SKAnimator;
    [SerializeField] MoveToTarget SKMovement;
    [SerializeField] AnimationFeedback SKAFeedback;
    [SerializeField] Defence SKDefence;
    [SerializeField] StatusUpd SKDStatus;
    public bool isItTutorial = false;
    [SerializeField] string NextTutorialSceneName = "";

    [SerializeField] GameObject blackScreeen;
    [SerializeField] GameStatus gameStatus;
    [SerializeField] MusicManager musicManager;
    bool loseMusiscOn = false;
    public bool backToMenu = false;

   
    void Start()
    {

        cursorHotSpot = new Vector2(GreenCursor.width / 2, GreenCursor.height / 2);
        setRegularCursor();
        SpellsAviability.EnhancedMode = false;
        CurrentRound = GameStatus.RoundNum;
        CurrentArmyStrength = GameStatus.HumanArmyCost;
        // winOrLose = GetComponent<winOrLose>();
        currentgameStatus = CurrrentGameStatus.Planning;
        if (!isItTutorial) //spawn the armies if its not tutoirial
        {
            spawnHumanArmy(); 
            spawnUndeadArmy();
        }

        AftermathUI.SetActive(false);
        LoseScreen.SetActive(false);
        //GameStatus.RoundNum++;
        manaBonus = GameStatus.mana / 3; //calculate the third-of-mana bonus 
        manaStepAdd = GameStatus.RoundNum * GameStatus.manaStep; //caclulate the round bonus
        SKAnimator.Play("SKappear");  //the apper on the throne animation 
        blackScreeen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
            {
            if (!settingsOn)
                showSettings();
            else
                hideSettings();

            }

        if (specialCursorOn && ((Input.GetMouseButtonDown(1)))) // spell use cancelation 
        {
            Time.timeScale = 1.0f;
            SoulInFusionActivation.SoulInFusionSelectionMode = false;
            MarkOfDeathActivation.MarkOfDeathSelectionMode = false;
            specialCursorOn = false;
            setRegularCursor();
        }

        if ((!SoulInFusionActivation.SoulInFusionSelectionMode && !MarkOfDeathActivation.MarkOfDeathSelectionMode) && specialCursorOn) // if the special cursor is stilll on but the mode is over - spell was just used on a unit
        {
            setRegularCursor();
            makeCastingAnimation();
        }

        if (SKprepareToCast == true) // Skeleton King Cast animation activation
        {
            makeCastingAnimation();
            SKprepareToCast = false;
        }


        switch (currentgameStatus)
        {
            case CurrrentGameStatus.Planning:
                PlanningMode(); break;
            case CurrrentGameStatus.Battle:
                BattleMode(); break;
            case CurrrentGameStatus.Aftermath:
                AftermathMode(); break;

        }
    }

    public enum CurrrentGameStatus
    {
        Planning,
        Battle,
        Aftermath

    }

    void PlanningMode()
    {
        if (blackScreeen.GetComponent<BlackScreen>().blacknow)  // if the dcreen is blacked while planning theere can be only one option - the user decided to go back to main menu
            if (backToMenu)
                gameStatus.goToMenu();

        if (!draggingIsHappening) // a fix for a bug that reesets the cursor to the generic one in Itch,io when you grab a unit
            setRegularCursor();

        manaBonus = GameStatus.mana / 3;
        manaStepAdd = GameStatus.RoundNum * GameStatus.manaStep;
    }

    void BattleMode()
    {
        manaBonus = GameStatus.mana / 3;
        manaStepAdd = GameStatus.RoundNum * GameStatus.manaStep;
      
        if (SKAFeedback.specialHit) //resets this "wannabe-trigger" as it used only on aftermathMode
            SKAFeedback.specialHit = false;

        if (winOrLose.victory)
        {

            currentgameStatus = CurrrentGameStatus.Aftermath;
            AftermathUI.SetActive(true); //activate the winning UI
            calculateManaAndValue();  

            //in case of the useer was in the middle of spell sellection
            Time.timeScale = 1.0f; 
            SoulInFusionActivation.SoulInFusionSelectionMode = false;
            MarkOfDeathActivation.MarkOfDeathSelectionMode = false;
            specialCursorOn = false;
            setRegularCursor();


            spellsAviability.afterMathMode();
            if (SKDefence.alive == false) // bring the fallen Skeleton King
                SKDStatus.reanimate();
            crystalAnimator.SetTrigger("Bling");
            GameStatus.RoundNum++;
            SpellsAviability.EnhancedMode = false;
            if (TheLists.undeadToReamimate.Count > 0)
                makeCastingAnimation();
            musicManager.switchMusic();

        }


        if (winOrLose.defeat)
        {
            if (!loseMusiscOn)
            {
                loseMusiscOn = true;
                musicManager.switchMusic();
                updateHighScores();
            }

            //in case of the useer was in the middle of spell sellection
            Time.timeScale = 1.0f;
            SoulInFusionActivation.SoulInFusionSelectionMode = false;
            MarkOfDeathActivation.MarkOfDeathSelectionMode = false;
            specialCursorOn = false;
            setRegularCursor();

            LoseScreen.SetActive(true); //activate the losing UI
            calculateManaAndValue();
            SpellsAviability.EnhancedMode = false;


        }
        //  blackScreeen.SetActive(true);
        //  blackScreeen.GetComponent<BlackScreen>().GoOut();


        if (blackScreeen.GetComponent<BlackScreen>().blacknow) //load the relevant scene when the screen turns to black
            if (backToMenu)
                gameStatus.goToMenu();
            else
                gameStatus.restartTheGame();
    }
    void calculateManaAndValue()//the function recalcullates all the needed values for the next level
    {

        GameStatus.mana += manaBonus + manaStepAdd;

        GameStatus.currentScore += GameStatus.HumanArmyCost;
        GameStatus.HumanArmyCost += manaStepAdd + manaStepAdd * 2 / 3;
        manaBonus = 0;
        manaStepAdd = 0;
    }


    void AftermathMode()
    {
        if (SKAFeedback.specialHit) //reanimate the undedd after the King is used his casting animation
            TheLists.reanimateUndead(); 
        if (TheLists.undeadToReamimate.Count == 0)
        {
           
            SKAnimator.SetTrigger("SitDown"); // the sitting-on-the-throne animation activation
         
        }

        if (SKAFeedback.goNextLevel) // once the dissapearing of the King animation finished
        {
            musicManager.levelEnd = true;
            blackScreeen.SetActive(true);
            blackScreeen.GetComponent<BlackScreen>().GoOut();
        }

     
           
        if (blackScreeen.GetComponent<BlackScreen>().blacknow)
                if (backToMenu)
                    gameStatus.goToMenu();
                else
                NextLevel();
    }

    public void StartBattle() //the function that starts the battle stage of the game
    {
        SoundFXManager.Instance.playSFXClip(chargeSound, transform, 1f);
        spellsAviability.makeSpellsActive();
        TheLists.stopPlanning();
        currentgameStatus = CurrrentGameStatus.Battle;
        musicManager.switchMusic();
    }

    public void NextLevel() // loads the next level
    {
        if (!isItTutorial)
        {
            if (CurrentRound<15)
               SceneManager.LoadScene("NormalGame");
            else
                SceneManager.LoadScene("WinScreen");
        }
        else
            SceneManager.LoadScene(NextTutorialSceneName);
    }

    void spawnHumanArmy() //spawns the army, making sure its random, yet not investing more then hallf of its budget on elite units 
    {
        int CostToUse = GameStatus.HumanArmyCost;
        while (CostToUse >= enemiesStats[0].cost)
        {
            for (int i = 0; i < enemiesStats.Length; i++)
            {
                if (((enemiesStats[i].cost <= CostToUse / 2) && (enemiesStats[i].cost > 300)) || ((enemiesStats[i].cost <= CostToUse) && (enemiesStats[i].cost <= 300)))
                {
                    if (Random.Range(0, 100) < spawnChance)
                    {
                        CostToUse -= enemiesStats[i].cost;
                        spawnEnemy(enemiesStats[i]);
                    }

                }
            }
        }

    }

    void spawnEnemy(Stats stats) //spawns the sellected enemy unit
    {
        GameObject newUnit = Instantiate(stats.prefub, transform.position, Quaternion.identity, units.transform);

        newUnit.layer = 3;
        newUnit.GetComponent<PlanningMode>().enemyPosition();
    }

    void spawnUndeadArmy() //spawns the army gathered in previous rounds
    {
        for (int i = 0; i < armyStats.Length; i++)
        {
            int num = getCurrentArmyNum(armyStats[i]);

            for (int j = 0; j < num; j++)
            {
                spawnUndead(armyStats[i]);

            }
        }
    }

    int getCurrentArmyNum(Stats stats) //getting the needed unit counter
    {
        switch (stats.theName)
        {
            case "Skeleton Warrior": return (GameStatus.SkeletonsWarriorsAnimated);
            case "Skeleton Archer": return (GameStatus.SkeletonsArchersAnimated);
            case "Skeleton Mage": return (GameStatus.SkeletonMagesAnimated);
            case "Ghost": return (GameStatus.GhostsAnimated);
            case "Skeleton Captain": return (GameStatus.SkeletonCaptainAnimated);

            default: return 0;
        }
    }

    private void spawnUndead(Stats stats) //spawns the undead
    {
        GameObject newUnit = Instantiate(stats.prefub, transform.position, Quaternion.identity, units.transform);

        newUnit.layer = 3;
        newUnit.GetComponent<PlanningMode>().reanimatePosition();
    }

    public void setGreenCursor() //set cursor to the soulllInfusion spell Cursor
    {
        specialCursorOn = true;
        Cursor.SetCursor(GreenCursor, cursorHotSpot, CursorMode.Auto);
    }

    public void setBlackCursor()  //set cursor to the markOfDeath spell Cursor
    {
        specialCursorOn = true;
        Cursor.SetCursor(BlackCursor, cursorHotSpot, CursorMode.Auto);
    }

    private void setRegularCursor() //set cursor to regular Cursor
    {
        specialCursorOn = false;
        Cursor.SetCursor(RegularCursor, Vector2.zero, CursorMode.Auto);
    }


    void makeCastingAnimation() //makes thee skeleton kind do the casting animation
    {
        SKAnimator.SetTrigger("SpecialAttack");
        SKMovement.isActing = true;
    }

    public void loadMenu()
    {
        backToMenu = true;
    }

    void updateHighScores()  //if the new score is high enough, it will be placed among the others
    {
        int highScore1st = PlayerPrefs.GetInt("highScore1st");
        int highScore2nd = PlayerPrefs.GetInt("highScore2nd");
        int highScore3rd = PlayerPrefs.GetInt("highScore3rd");

        if (GameStatus.currentScore > highScore1st)
        {
            highScore3rd = highScore2nd;
            highScore2nd = highScore1st;
            highScore1st = GameStatus.currentScore;
        }
        else if (GameStatus.currentScore > highScore2nd)
        {
            highScore3rd = highScore2nd;
            highScore2nd = GameStatus.currentScore;

        }
        else if (GameStatus.currentScore > highScore3rd)
            highScore3rd = GameStatus.currentScore;


        PlayerPrefs.SetInt("highScore1st", highScore1st);
        PlayerPrefs.SetInt("highScore2nd", highScore2nd);
        PlayerPrefs.SetInt("highScore3rd", highScore3rd);

    }

    public void showSettings() //opens the settings menu
    {
        Time.timeScale = 0;
       
        settings.SetActive(true);
  
        settings.GetComponent<MakeMenuAppear>().appearing = true;
        settingsOn = true;
    }

    public void hideSettings() //sloses the settings menu
    {
        Time.timeScale = 1;

      //  settings.SetActive(true);

        settings.GetComponent<MakeMenuAppear>().appearing = false;
        settingsOn = false;
    }
}
