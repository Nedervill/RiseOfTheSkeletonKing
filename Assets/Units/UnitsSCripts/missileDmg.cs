using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class missileDmg : MonoBehaviour
{

    /* the component determs overalll missllle behaviour
      
     
     */
    public Animator animator;
    public GameObject target;
    float hitrange = 0.5f;
    public bool aoeDmg = false;
    public float explosionRad = 0;
    public float damage;
    public float addF;
    public float moveSpeed;
    public float addX;
    public float addY;
    bool hitTHeTarget = false;
    bool exploading = false;
    public string missleMat;
    Vector3 dir;
    [SerializeField] AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position + new Vector3(addX, addY);
        dir = Vector3.RotateTowards(transform.position, target.transform.position, 360f, 0f);
        dir = new Vector3(90, dir.y, 0);



    }

    // Update is called once per frame
    void Update()
    {
        if (!hitTHeTarget)
        {
           // transform.LookAt(target.transform.position);
            hitTHeTarget = checkForHit();
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
         
        }
        else if (!exploading)
            hit();
    }

    bool checkForHit()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        return (dist<= hitrange);
    }
     void hit()
    {
       
            if (!aoeDmg) //if therer is no areall damage
            {
             
                doDamage(target);
                Destroy(gameObject);
            }
            else
            {
                  SoundFXManager.Instance.playSFXClip(explosionSound, transform, 0.2f);
                   exploading = true;
                animator.SetTrigger("Explode");
               
            }
       
    }
    public void aoeDamage(string boom) //makes areal damage to enemies and decorations
    {
        Collider[] intersecting = Physics.OverlapSphere(transform.position, explosionRad);
        string enemyTag = target.tag;
        destriyNearDeco(explosionRad);
        List<GameObject> affected = new List<GameObject>();

        for (int i = 0; i < intersecting.Length; i++)
        {

            if ((intersecting[i].gameObject.tag == enemyTag) && (intersecting[i].gameObject.GetComponent<Defence>().alive)&& (!affected.Contains(intersecting[i].gameObject)))
            {
                affected.Add(intersecting[i].gameObject);
                doDamage(intersecting[i].gameObject);
            }

        }
        Destroy(gameObject);
    }

    void doDamage(GameObject theTarget)
    {
        SoundFXManager.Instance.hitSound(missleMat, theTarget.GetComponent<Defence>().ArmourMat, target.transform, 0.2f);
        theTarget.GetComponent<Defence>().GetDamage(damage);
        if (addF > 0) //pushes the target if the missle should do that
        {
            Vector3 dir = (theTarget.transform.position - transform.position).normalized;
            dir = new Vector3(dir.x,0,dir.z);
            if ((dir.x == 0) && (dir.z == 0))
                dir += new Vector3 (1f,0,0);
            dir += new Vector3(0, 1f, 0);
            theTarget.GetComponent<Rigidbody>().AddForce(dir * addF);
            theTarget.gameObject.GetComponent<StatusUpd>().pushed();
        }

     
    }

    void destriyNearDeco(float range) //destroys doartion in the explosion range
    {
        Collider[] intersecting = Physics.OverlapSphere(transform.position, range);
        //   circles2Draw.Add(realHere);


        for (int i = 0; i < intersecting.Length; i++)
        {

            if (intersecting[i].gameObject.tag == "Decoration")
            {
                GameObject currentDeco = intersecting[i].gameObject;

                DestroyDeco currendecoToDestroy = currentDeco.GetComponent<DestroyDeco>();

                if (!currendecoToDestroy.destroyed)
                {
                    currendecoToDestroy.destroActivation();


                }


            }

        }
    }
}
