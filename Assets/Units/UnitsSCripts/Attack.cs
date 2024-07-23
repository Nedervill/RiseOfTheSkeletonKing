
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.ParticleSystem;


public class Attack : MonoBehaviour
{
    GameObject target;
 
   
    public Animator animator;
    public GameObject SpriteHolder;

    //ranged
    bool targetWithinRngedReach;
    public float rangedDamage;
    public GameObject projectile;
    [SerializeField] GameObject projectileSpawnpoint1;
    [SerializeField] GameObject projectileSpawnpoint2;
   // public AudioClip rangedAttackSound;

    //misc
    public float attackSpeed;
    public float attackCooldown;
    public float lastAttacktime;
    bool AttackHit;
    MoveToTarget moveToTarget;

    //energy
    public float energyPerAttack;
    public float currentEnergy = 0;

    //meele
    bool targetWithinMeeleReach;
    public float meeleDamage;
    public bool meeleAoe = false;
    public float meeleAoeRange = 2;
    public float meelePushForce = 0;
    public string attackMat;
   

    //special attack
    public bool specialAttackReady;
    public float specialDamage;
    public float specialAoeRange;
    public float specialPushForce;
    public bool specialAoe;
    public string specialAttackMat;
    public GameObject specialProjectile;
    SpecialAttack Sattack;
    [SerializeField] ParticleSystem specialAttackEffect;
    [SerializeField] AudioClip specialAttackEffectSound;

    void Start()
    {
        moveToTarget = GetComponent<MoveToTarget>();
        Sattack = GetComponent<SpecialAttack>();
        currentEnergy = 0;
        target = GetComponent<TargetSelector>().target;

        //  stats = gameObject.GetComponent<Stats>();
        attackCooldown = 10 / attackSpeed;
        lastAttacktime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        target = GetComponent<TargetSelector>().target;
        /*  if (lastAttacktime + attackCooldown +2 <= Time.timeSinceLevelLoad)
              SpriteHolder.GetComponent<AnimationFeedback>().StoppedAttacking();

              target = GetComponent<TargetSelector>().target;*/
        if (target != null)
        {
            if ((currentEnergy >= 100) && (!specialAttackReady) &&  // normal case of full energy leading to execution of special attack
                !((moveToTarget.ranged) && targetWithinMeeleReach && (gameObject.name != "Skeleton King"))) // exclluding ranged units that are engaged in meele combat - buy the game llogic those cannot perform speciall attack. The Skeleton king still can get to enhanced spells though
                makeSpecialAttack();

            targetWithinRngedReach = gameObject.GetComponent<MoveToTarget>().targetWithinRngedReach;
            targetWithinMeeleReach = gameObject.GetComponent<MoveToTarget>().targetWithinMeeleReach;

            if ((targetWithinMeeleReach) && (lastAttacktime + attackCooldown <= Time.timeSinceLevelLoad)) // if engaged in meele and the attack time is come, the unit willl perform normal or special attack
            {

                if ((specialAttackReady) && (!moveToTarget.ranged))
                {
                    currentEnergy = 0;
                    animator.SetTrigger("SpecialAttack");

                    specialAttackEffect.Play();   //the particle effect of performing a special attack
                    SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);  //the sound effect of performing a special attack
                    moveToTarget.isActing = true; // isActing bool disables the ability of unit to moce antil it's other animations are finished
                }
                else
                {
                    moveToTarget.isActing = true; // isActing bool disables the ability of unit to moce antil it's other animations are finished
                    animator.SetTrigger("Attacking");

                }



                lastAttacktime = Time.timeSinceLevelLoad; // resetting the attack timer

            }
            else if ((targetWithinRngedReach) && (lastAttacktime + attackCooldown <= Time.timeSinceLevelLoad) && !targetWithinMeeleReach) // case of rangeed unit have its target within reach
            {
                if (specialAttackReady)
                {
                    currentEnergy = 0;
                    animator.SetTrigger("SpecialAttack");
                    SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);    //the sound effect of performing a special attack
                    specialAttackEffect.Play();                 // the particle effect of performing a special attack
                    moveToTarget.isActing = true;               // isActing bool disables the ability of unit to moce antil it's other animations are finished
                }
                else
                {
                    animator.SetTrigger("Shooting");
                    moveToTarget.isActing = true;
                }

                lastAttacktime = Time.timeSinceLevelLoad;
            }

            if ((SpriteHolder.GetComponent<AnimationFeedback>().hit) && (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Aftermath)) // the case of meele attack animation got to it's "hit" triigger
            {
                // moveToTarget.timeSinceStoppedActing = Time.timeSinceLevelLoad;

                bool isAoe; // checking if its an area attack in eather a special or regualr attack
                if (specialAttackReady)
                {

                    isAoe = specialAoe;
                }
                else
                {

                    isAoe = meeleAoe;
                    currentEnergy += energyPerAttack; // adding energy per hit 
                    if (currentEnergy > 100)
                        currentEnergy = 100;
                }


                if (isAoe)
                {
                    aoeDamage();
                }
                else
                {
                    doMeeleDamage(target);
                }

                SpriteHolder.GetComponent<AnimationFeedback>().hit = false; //resetting the "wannabe trigger" boolean
                specialAttackReady = false;

            }

            if (SpriteHolder.GetComponent<AnimationFeedback>().shoot) // the case of ranged attack animation got to it's "shoot" triigger
            {
                

                    if (target != null)
                        shootProjectile(); // a projectile will be created
                    SpriteHolder.GetComponent<AnimationFeedback>().shoot = false; //resetting the "wannabe trigger" boolean
                    specialAttackReady = false;
                    currentEnergy += energyPerAttack;   // adding energy per hit 
                    if (currentEnergy > 100)
                        currentEnergy = 100;

                

            }
        }
    }
    void aoeDamage()   //the function will check for every enemy in an areea around the unit and apply to it the needed damage and force
    {
        float currentAoeRange;   //checking what damage should be done
        if (specialAttackReady)
            currentAoeRange = specialAoeRange;
        else
            currentAoeRange = meeleAoeRange;

        destriyNearDeco(currentAoeRange);  // destroy any battlfield decorations in a range in a battlefield
        Collider[] intersecting = Physics.OverlapSphere(transform.position, currentAoeRange);
        string enemyTag;
        if (tag == "Ally")
            enemyTag = "Enemy";
        else
            enemyTag = "Ally";
        List<GameObject> affected = new List<GameObject>();

        for (int i = 0; i < intersecting.Length; i++)
        {

            if ((intersecting[i].gameObject.tag == enemyTag) && (intersecting[i].gameObject.GetComponent<Defence>().alive) 
                    && (!affected.Contains(intersecting[i].gameObject))) // units have 2 colliders so it must be ensured we dont apply the damage twice
            {
                affected.Add(intersecting[i].gameObject);
                doMeeleDamage(intersecting[i].gameObject);
            }

        }
      
    }

     void doMeeleDamage(GameObject theTarget)
    {
        float theDamage;
        float thePush;
       

        if (specialAttackReady)
        {
            SoundFXManager.Instance.hitSound(specialAttackMat, theTarget.GetComponent<Defence>().ArmourMat,target.transform, 0.2f); //make proper hit sound
            theDamage = specialDamage;
            thePush = specialPushForce;
           // bool ispush = (specialPushForce > 0);
        }
        else
        {
            SoundFXManager.Instance.hitSound(attackMat, theTarget.GetComponent<Defence>().ArmourMat, target.transform, 0.2f); //make proper hit sound
                theDamage = meeleDamage;
            thePush = meelePushForce;
        }
        theTarget.GetComponent<Defence>().GetDamage(theDamage);
        if (thePush > 0)
        {
            Vector3 dir = (theTarget.transform.position - transform.position).normalized + new Vector3(0,1f,0); 
            theTarget.GetComponent<Rigidbody>().AddForce(dir * thePush);
            theTarget.gameObject.GetComponent<StatusUpd>().pushed();
        }

        
    }


    void shootProjectile()  //creates projectile in a proper spawnpoint
    {
        GameObject closerPoint;
        GameObject theprojectile;
        float theDamage;

        if ((Vector3.Distance(target.transform.position, projectileSpawnpoint1.transform.position)) < (Vector3.Distance(target.transform.position, projectileSpawnpoint2.transform.position)))
            closerPoint = projectileSpawnpoint1;
        else
            closerPoint = projectileSpawnpoint2;

        if (specialAttackReady)
        {
            theprojectile = specialProjectile;
            theDamage = specialDamage;
        }
        else
        {
            theprojectile = projectile;
            theDamage = rangedDamage;

        }

        GameObject missile = Instantiate(theprojectile, closerPoint.transform.position, Quaternion.identity);
      //  missile.GetComponent<SpriteRenderer>().flipX = SpriteHolder.GetComponent<SpriteRenderer>().flipX;
       // missile.GetComponent<SpriteRenderer>().flipY = SpriteHolder.GetComponent<SpriteRenderer>().flipX;
        missile.GetComponent<missileDmg>().target = target;
        missile.GetComponent<missileDmg>().damage = theDamage;
    }

     void OnEnable()
    {

        lastAttacktime = Time.timeSinceLevelLoad; //reset attacktime when ressurected or control ended
    }

   void makeSpecialAttack()
    {
      
        Sattack.activateSpecialAttack();
    }

    void destriyNearDeco(float range)  // searching and destroying nearby terrain decorations in a case of areal attack
    {
        Collider[] intersecting = Physics.OverlapSphere(transform.position, range);
        //   circles2Draw.Add(realHere);


        for (int i = 0; i < intersecting.Length; i++)
        {

            if (intersecting[i].gameObject.tag == "Decoration")
            {
                GameObject currentDeco = intersecting[i].gameObject;

                DestroyDeco currendecoToDestroy = currentDeco.GetComponent<DestroyDeco>();

                if (!currendecoToDestroy.destroyed )
                {
                    currendecoToDestroy.destroActivation();


                }


            }

        }
    }
}
