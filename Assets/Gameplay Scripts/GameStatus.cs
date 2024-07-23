using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    //bank of paramethers that the game have to maintain between the levels 
    public static int mana = 300;
 
    //The Active Army
    public static int SkeletonsWarriorsAnimated = 1;
    public static int SkeletonsArchersAnimated = 1;
    public static int SkeletonMagesAnimated = 0;
    public static int GhostsAnimated = 0;
    public static int SkeletonCaptainAnimated = 0;

    // the enemy army "budget"
    public static int HumanArmyCost = 500;


    //The Availble Corpses
    public static int SoldierCorpse = 0;
    public static int ArcherCorpse = 0;
    public static int MageCorpse = 0;
    public static int AssasinCorpse = 0;
    public static int HeroCorpse = 0;


    public static int manaStep = 100;
    public static int RoundNum = 1;
    static bool justStarted = true;

    public static int lastBattleTrackUsed = -1; // allows noit to rpeat the same battle music over and over
    public static int currentScore=0;



    /// /////////////////////////////////////


    // input for the editor to play with
    [SerializeField] int StartingSkeletons = 1;
    [SerializeField] int StartingArhers = 1;
    [SerializeField] int StartingMages = 0;
    [SerializeField] int StartingGhosts = 0;
    [SerializeField] int StartingCaptains = 0;


    [SerializeField] int StartingWarCorpses = 0;
    [SerializeField] int StartingArherCorpses = 0;
    [SerializeField] int StartingMagesCorpses = 0;
    [SerializeField] int StartingAssassinCorpses = 0;
    [SerializeField] int StartingHeroCorpses = 0;

    [SerializeField] int StartingMana = 200;
    [SerializeField] int StartingArmyValue = 500;
    [SerializeField] int StartingRound = 1;
    [SerializeField] int CurrentManastep = 100;
    [SerializeField] RoundStatus RoundStatus;


    // Start is called before the first frame update


    void Awake()
    {
        if ((justStarted) && (!RoundStatus.isItTutorial))
        {
            justStarted = false;
            winOrLose.defeat = false;
            mana = StartingMana;
            //The Active Army
            SkeletonsWarriorsAnimated = StartingSkeletons;
            SkeletonsArchersAnimated = StartingArhers;
            SkeletonMagesAnimated = StartingMages;
            GhostsAnimated = StartingGhosts;
            SkeletonCaptainAnimated = StartingCaptains;

            HumanArmyCost = StartingArmyValue;
            //The Availble Corpses
            SoldierCorpse = StartingWarCorpses;
            ArcherCorpse = StartingArherCorpses;
            MageCorpse = StartingMagesCorpses;
            AssasinCorpse = StartingAssassinCorpses;
            HeroCorpse = StartingHeroCorpses;

            manaStep = CurrentManastep;
            RoundNum = StartingRound;
            restartTheGame();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void manaUpd(int manaUpd)
    {
        mana += manaUpd;
    }

    public void restartTheGame()
    {
        currentScore = 0;
        winOrLose.defeat = false;
        mana = StartingMana;
        //The Active Army
        SkeletonsWarriorsAnimated = StartingSkeletons;
        SkeletonsArchersAnimated = StartingArhers;
        SkeletonMagesAnimated = StartingMages;
        GhostsAnimated = StartingGhosts;
        SkeletonCaptainAnimated = StartingCaptains;

        HumanArmyCost = StartingArmyValue;
        //The Availble Corpses
        SoldierCorpse = StartingWarCorpses;
        ArcherCorpse = StartingArherCorpses;
        MageCorpse = StartingMagesCorpses;
        AssasinCorpse = StartingAssassinCorpses;
        HeroCorpse = StartingHeroCorpses;

        manaStep = CurrentManastep;
        RoundNum = StartingRound;
        lastBattleTrackUsed = -1;

        SceneManager.LoadScene("NormalGame");
    }

    public void goToMenu()
    {
        currentScore = 0;
         winOrLose.defeat = false;
        mana = StartingMana;
        //The Active Army
        SkeletonsWarriorsAnimated = StartingSkeletons;
        SkeletonsArchersAnimated = StartingArhers;
        SkeletonMagesAnimated = StartingMages;
        GhostsAnimated = StartingGhosts;
        SkeletonCaptainAnimated = StartingCaptains;

        HumanArmyCost = StartingArmyValue;
        //The Availble Corpses
        SoldierCorpse = StartingWarCorpses;
        ArcherCorpse = StartingArherCorpses;
        MageCorpse = StartingMagesCorpses;
        AssasinCorpse = StartingAssassinCorpses;
        HeroCorpse = StartingHeroCorpses;

        manaStep = CurrentManastep;
        RoundNum = StartingRound;
        lastBattleTrackUsed = -1;

        SceneManager.LoadScene("MainMenu");
    }
}
