
using System.Collections.Generic;


using UnityEngine;


public class SpecialAttack : MonoBehaviour
{
    public string theName;
    public bool specialAttackReady = false;
    [SerializeField] SpellsAviability SpellsAviability;
    [SerializeField] Animator animator;
    Attack attack;
    [SerializeField] AnimationFeedback animationFeedback;
    Vector3 MageMissileSpawmPoint = new Vector3(0, 7f, 0);
    [SerializeField] GameObject SpecialProjectile;
    float startTime;
    float coolDown;
    TargetSelector targetSelector;
    MoveToTarget moveToTarget;
    GameObject SpecialAttackTarget;
    public AudioClip specialAbilitySound1;
    public AudioClip specialAbilitySound2;
    public string specialAttackMat;
    [SerializeField] ParticleSystem specialAttackEffect;
    [SerializeField] AudioClip specialAttackEffectSound;



    // Start is called before the first frame update
    void Start()
    {
        moveToTarget = GetComponent<MoveToTarget>();
        if (theName == "Skeleton King")   //Only in case of SKeleton King the SpellsAviabilty object is needed
            SpellsAviability = FindAnyObjectByType<SpellsAviability>();
        if ((theName == "Assasin") || (theName == "Ghost"))  //Only in case ofAssain or Ghost  the TargetSelector component is needed
        {
            targetSelector  =GetComponent<TargetSelector>();
        }
        attack = GetComponent<Attack>();


    }

    // Update is called once per frame
    void Update()
    {
        if (specialAttackReady)
            switch (theName)
            {
                case "Skeleton King":
                    SKupkeep();
                    break;
                case "Mage":
                    if (GetComponent<Defence>().alive == true)
                        mageUpkeep();
                    break;
                case "Assasin":
                    if (GetComponent<Defence>().alive == true)
                        assasinUpkeep();
                    break;
                case "Ghost":
                    if (GetComponent<Defence>().alive == true)
                        ghostUpkeep();
                    break;


                default: break;


            }

    }

    public void activateSpecialAttack()
    {
       
          
        specialAttackReady = true;
        switch (theName)
        {
            case "Skeleton King":
                SKSpecialAttack();
                break;
            case "Mage":
            case "Assasin":
            case "Ghost":
                attackOverride();
                break;


            // most of the units will go back into "Attack" function and perform just a more powerfull attack
            case "Soldier":
            case "Skeleton Warrior":
            case "Hero":
            case "Skeleton Captain":
            case "Archer":
            case "Skeleton Archer":
            case "Skeleton Mage":
                attack.specialAttackReady = true;
                specialAttackReady = false; break;
            default: break;
        }
    }

    void SKSpecialAttack()
    {
        SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);
        specialAttackEffect.Play();
        SpellsAviability.EnhancedMode = true;

    }
    void SKupkeep()
    {
        if (SpellsAviability.EnhancedMode == true)
        {

            attack.currentEnergy = 0; //while "enhanced" skil is not used the king will not gather more energy with his attacks 
        }
        else
        {
         //   animator.SetTrigger("SpecialAttack");
            specialAttackReady = false;
            specialAttackEffect.Stop();
      
        }
            
        if (GetComponent<Defence>().alive == false)  //dsable the spells if the king is dead
        {
            specialAttackEffect.Stop();
            SpellsAviability.EnhancedMode = false;
       //     SpellsAviability.makeSpellsUnActive();
        }
    }

    void attackOverride() //disables the attack component and takes it reponsability on itself untill the special attack is performed 
    {
        startTime = attack.lastAttacktime;
        coolDown = attack.attackCooldown;

        attack.enabled = false;




    }
    void assasinUpkeep()
    {

        if ((Time.timeSinceLevelLoad >= startTime + coolDown) && (specialAttackReady))     //start the special attack when the attack time is ready
        {
            coolDown = 3000;

            specialAttackEffect.Play(); //activate the special attack particle effect
            animator.SetTrigger("SpecialAttack");
            SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);
            moveToTarget.isActing = true; // isActing is aboolean that disables movement while animation is performed

        }

        if ((animationFeedback.teleport)) // if the telleport trigger is activated, ter unit willl be repositioned behind his target
        {
            SoundFXManager.Instance.playSFXClip(specialAbilitySound1, transform, 1);
            assasinTargetSelector();
            transform.position = behindTarget();
            animationFeedback.teleport = false; //reset the "wannabe-trigger"

            if (SpecialAttackTarget != null)
            {
                if (transform.position.x > SpecialAttackTarget.transform.position.x)  //  flip  the sprite of needed
                    attack.SpriteHolder.GetComponent<SpriteRenderer>().flipX = true;
                else if (transform.position.x < SpecialAttackTarget.transform.position.x)
                    attack.SpriteHolder.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (animationFeedback.specialHit) //on sepcial hit the unit will apply the damage and return to norma attack mode 
        {
           
            moveToTarget.timeSinceStoppedActing = Time.timeSinceLevelLoad;
            animationFeedback.specialHit = false;
            specialAttackReady = false;
            attack.enabled = true;
            attack.specialAttackReady = false;
            
            attack.currentEnergy = 0;
            if (SpecialAttackTarget != null)
            {
                SpecialAttackTarget.GetComponent<Defence>().GetDamage(attack.specialDamage);
                SoundFXManager.Instance.hitSound(specialAttackMat, SpecialAttackTarget.GetComponent<Defence>().ArmourMat, SpecialAttackTarget.transform, 0.2f);
            }
               
            targetSelector.enabled = true;
        }


    }

    void ghostUpkeep()
    {

        if ((Time.timeSinceLevelLoad >= startTime + coolDown) && (specialAttackReady))   //start the special attack when the attack time is ready
        {
            SoundFXManager.Instance.playSFXClip(specialAbilitySound1, transform, 1);
            coolDown = 3000;
            SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);
            specialAttackEffect.Play(); //activate the special attack particle effect
            animator.SetTrigger("SpecialAttack"); 
           moveToTarget.isActing = true; // isActing is aboolean that disables movement while animation is performed

        }

        if ((animationFeedback.teleport)) // if the telleport trigger is activated, ter unit willl be repositioned behind his target
        {
            SoundFXManager.Instance.playSFXClip(specialAbilitySound2, transform, 1);
            ghostTargetSelector();
            transform.position = behindTarget();
            animationFeedback.teleport = false; //reset the "wannabe-trigger"


            if (SpecialAttackTarget != null)
            {
                if (transform.position.x > SpecialAttackTarget.transform.position.x)  //  flip  the sprite of needed
                    attack.SpriteHolder.GetComponent<SpriteRenderer>().flipX = true;
                else if (transform.position.x < SpecialAttackTarget.transform.position.x)
                    attack.SpriteHolder.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (animationFeedback.specialHit) //on sepcial hit the unit will apply the damage and return to norma attack mode 
        {
            if (SpecialAttackTarget != null)
            {
                SoundFXManager.Instance.hitSound(specialAttackMat, SpecialAttackTarget.GetComponent<Defence>().ArmourMat, SpecialAttackTarget.transform, 0.2f);
                SpecialAttackTarget.GetComponent<Defence>().GetDamage(attack.specialDamage);
            }
               
            targetSelector.enabled = true;
            moveToTarget.timeSinceStoppedActing = Time.timeSinceLevelLoad;
            animationFeedback.specialHit = false; ;
            specialAttackReady = false;
            attack.specialAttackReady = false;
            attack.enabled = true;
            attack.currentEnergy = 0;
           
        }

    }

    void ghostTargetSelector()
    {
       
        if ((targetSelector.target!=null)&&(targetSelector.target.GetComponent<StatusUpd>().stats.ranged) && (moveToTarget.targetWithinMeeleReach) && targetSelector.target.GetComponent<Defence>().alive) //if the current allive target is already ranged 
        {
            SpecialAttackTarget = targetSelector.target;
            return;
        }
        List<GameObject> enemies = targetSelector.enemies;

        SpecialAttackTarget = null;
        targetSelector.enabled = false;
        
        float dist = 9000;  //probably not the best practice but im sure the distances will be shorteer then that
        bool rangedFound = false;
      
            for (int i = 1; i < enemies.Count; i++) // loock for closest ranged units
            {
                if ((enemies[i].GetComponent<StatusUpd>().stats.ranged) && (enemies[i].GetComponent<Defence>().alive))
                {
                    rangedFound = true;
                    float currentDist = Vector3.Distance(transform.position, enemies[i].transform.position);
                    if (currentDist < dist)
                    {
                        SpecialAttackTarget = enemies[i];
                        dist = currentDist;

                    }
                }
            }
       
        if (!rangedFound) // if no more ranged units left to attack
            assasinTargetSelector();
    }
    void assasinTargetSelector() // will try to assasinate the targit, prefering ones that are killble while apllying the most possible damage, i.e will pick the healthiest targeet among ones that will die after thee attack
    {
        
        float currentTargetRemainingHealth = 90000; //not thee best practice, bit i could not get on to something in the time
        float remainingHealth;
        float killingBlow= -900000; //not thee best practice, bit i could not get on to something in the time
        List<GameObject> enemies = targetSelector.enemies;
        SpecialAttackTarget = null;
        targetSelector.enabled = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            if ((Vector3.Distance(transform.position, enemies[i].transform.position) <= attack.specialAoeRange)) // check if the target is within the range
            {
                remainingHealth = enemies[i].GetComponent<Defence>().health - attack.specialDamage;

                if (remainingHealth > 0)
                {
                    if (remainingHealth < currentTargetRemainingHealth) // checks if its the closest to kill result yet
                    {
                        SpecialAttackTarget = enemies[i];
                        currentTargetRemainingHealth = remainingHealth;
                    }
                }
                else
                {
                    currentTargetRemainingHealth = 0;
                    if (remainingHealth > killingBlow) // the closer the remaining health to 0 from the negative side, the more "effective" the killing blow
                    {
                        SpecialAttackTarget = enemies[i];
                        killingBlow = remainingHealth;
                    }

                }
            }
        }

    }

    Vector3 behindTarget() //positions the unit behind its target
    {
        if (SpecialAttackTarget != null)
        {

       
            bool LookingRight = SpecialAttackTarget.GetComponent<MoveToTarget>().SR.flipX;
            float TargetL = SpecialAttackTarget.GetComponent<MeshCollider>().bounds.size.x;
            float MyL = GetComponent<MeshCollider>().bounds.size.x;
            Vector3 dist = new Vector3 ((TargetL + MyL) / 2,0, 0);
            if (LookingRight)
              return (SpecialAttackTarget.transform.position + dist);
       
            else
                return (SpecialAttackTarget.transform.position - dist);
        }
        else return transform.position;
    }

    void mageUpkeep()
    {
        if ((Time.timeSinceLevelLoad >= startTime + coolDown) && (specialAttackReady) && (!GetComponent<TargetSelector>().isEnemyAtMeeleRange())) // mage cannot perform speciall attack while engaged in meele fight
        {
            coolDown = 3000;
            SoundFXManager.Instance.playSFXClip(specialAttackEffectSound, transform, 0.6f);
            specialAttackEffect.Play();
            animator.SetTrigger("SpecialAttack");
     //       moveToTarget.isAttacking = true;
        }


        if ((animationFeedback.shoot)) //create special projectile in the sky
        {
           
            moveToTarget.timeSinceStoppedActing = Time.timeSinceLevelLoad;
            specialAttackReady = false;
            attack.enabled = true;

            animationFeedback.shoot = false;
            attack.currentEnergy = 0;
            GameObject target = GetComponent<TargetSelector>().target;
            if (target != null)
            {


                GameObject missile = Instantiate(SpecialProjectile, MageMissileSpawmPoint + target.transform.position, Quaternion.identity);

                missile.GetComponent<missileDmg>().target = target;
                missile.GetComponent<missileDmg>().damage = attack.specialDamage;
            }
        }

    }


   
}
