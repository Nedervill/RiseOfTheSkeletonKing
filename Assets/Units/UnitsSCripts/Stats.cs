
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    // Start is called before the first frame update
    //misc
    public string theName;
    public Sprite deadSprite;
    public GameObject prefub;
    public int cost;
    public AudioClip spawnSound;

    //movement
    public float moveSpeed = 10;


    //attack 
    public bool ranged = false;
    public float range;
    public float rangedDamage;
    public float attackSpeed = 1;
    public float meeleDamage = 10;
    public float meeleRange = 1.2f;
    public float energyPerAttack = 10;
    public bool meeleAoe = false;
    public float meeleAoeRange = 2;
    public float meelePushForce = 0;
    public AudioClip normalAttackSound;
    public AudioClip rangedAttackSound;
    public string meeleAttackMat;

    //special attack

    public float specialDamage = 0;
    public float specialPushForce = 0;
    public bool specialAoe = false;
    public float specialAoeRange = 0;
    public AudioClip specialAttackSound;
    public AudioClip specialAbilitySound1;
    public AudioClip specialAbilitySound2;
    public string specialAttackMat;


    //defence
    public int targetPriority = 1;
    public float health = 100;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public string ArmourMat;

    //special ability text
    public string specialAbilityText = "";
    public string specialPassiveText = "";


}
