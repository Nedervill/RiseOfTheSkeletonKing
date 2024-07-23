
using UnityEngine;


public class Defence : MonoBehaviour
{
    public Stats stats;
    public Animator animator;
    public float health;
    public float maxHealth;
    public bool alive;
    public bool underCC = false;
    public int targetPriority = 0;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public string ArmourMat;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        // stats = gameObject.GetComponent<Stats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (alive)   //while you alive, remeber to check - maybe you already dead XD
        {
        

            if (health <= 0)
            {
               
                SoundFXManager.Instance.playSFXClip(deathSound, transform, 0.6f);
                alive = false;
                animator.SetTrigger("Dead");
                gameObject.GetComponent<StatusUpd>().initiateDeath();
            }
        }
    }

    public void GetDamage(float damage)
    {
      
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("Hit");
            SoundFXManager.Instance.playSFXClip(hitSound, transform, 0.6f);
        }
         
       

    }

    
   
}
