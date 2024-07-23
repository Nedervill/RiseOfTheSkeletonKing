
using UnityEngine;


public class MoveToTarget : MonoBehaviour
{
    [SerializeField] float actingDelay = 0.3f;
    public float meeleRange = 1.5f;
    public float timeSinceStoppedActing = 0;
    public bool isActing = false;
    public GameObject target;
    public float moveSpeed;
    public float range;
    public bool targetWithinMeeleReach = false;
    public bool targetWithinRngedReach = false;
    public bool ranged;
    public Animator animator;
 
    private Collider colidr;
    public Vector3 currentDestination;
    public SpriteRenderer SR;
    [SerializeField] AnimationFeedback animationFeedback;
    public float thinkingTime = 1f;
    public float reactionTime = 1f;
    TargetSelector targetSelector;
     int NetSize = 16;
    float timeSinceVictory = 0;
    // Update is called once per frame
    private void Start()
    {
        targetSelector = GetComponent<TargetSelector>();
        colidr = GetComponent<MeshCollider>();

        target = null;

    }
    void Update()
    {
        thinkingTime += Time.deltaTime;

        if (animationFeedback.stoppedAttacking)
        {
            animationFeedback.stoppedAttacking = false;
            timeSinceStoppedActing = Time.timeSinceLevelLoad;
            isActing = false;
        }

        if (!GetComponent<Defence>().underCC)
            if (target != null)
            {

                float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);




             
                if (dist <= meeleRange)   //check if target is within meele range. no need to move then
                {
                    targetWithinMeeleReach = true;
                    animator.SetBool("Move", false);


                }
                else
                    targetWithinMeeleReach = false;


                if ((ranged) && (dist <= range))  //check if target is within ranged attack range. no need to move then
                {
                    targetWithinRngedReach = true;
                    animator.SetBool("Move", false);
                    if (transform.position.x > target.transform.position.x)  //  flip  the sprite of needed
                        SR.flipX = true;
                    else if (transform.position.x < target.transform.position.x)
                        SR.flipX = false;
                    //   agent.SetDestination(transform.position);
                }
                else
                    targetWithinRngedReach = false;  //willl be always fallse for non=ranged units 

                if (!((targetWithinRngedReach) || (targetWithinMeeleReach)))
                {
                    if (!(ranged))  
                    {
                        if (thinkingTime >= reactionTime)  //made mostly for performance prpouse 
                        {
                            thinkingTime =0;
                            currentDestination = pathToTarget(transform.position, target);
                            int targetsTried = 0;
                            while ((currentDestination == transform.position) && (target != null) && (targetsTried<5))  // currentDestination retrives the gamobjexts pos means it cannot find path to current path. The unit will look to get to other targets up to 5 timers
                            {
                                targetsTried++;
                                target = targetSelector.selectAnotherTarget(targetSelector.enemies);
                                if (target != null)
                                    currentDestination = pathToTarget(transform.position, target);
                            }
                        }

                    }
                    else
                        currentDestination = target.transform.position; //ranged units just go straight to the target. does not cause trouble 90% of time xd

                    if (transform.position.x > currentDestination.x)  //  flip  the sprite of needed
                        SR.flipX = true;
                    else if (transform.position.x < currentDestination.x)
                        SR.flipX = false;

                    if ((Vector3.Distance(transform.position, currentDestination) >= 0.5f) && (!isActing) && (timeSinceStoppedActing + (actingDelay) < Time.timeSinceLevelLoad))  // moving the unit after checking no other animation is happening
                    {
                        gameObject.transform.position = Vector3.MoveTowards(transform.position, currentDestination, moveSpeed * Time.deltaTime);
                        animator.SetBool("Move", true);
                    }
                    else
                        animator.SetBool("Move", false);
                }


                else
                {
                    if (transform.position.x > target.transform.position.x)  //  flip  the sprite of needed
                        SR.flipX = true;
                    else if (transform.position.x < target.transform.position.x)
                        SR.flipX = false;
                    //  agent.SetDestination(transform.position);
                    animator.SetBool("Move", false);
                    // timeSinceStoppedActing = Time.timeSinceLevelLoad;
                }
            }
            else
            {
                animator.SetBool("Move", false);
                // timeSinceStoppedActing = Time.timeSinceLevelLoad;
            }

        if ((winOrLose.victory) && (gameObject.name != "Skeleton King")) //victory march to next level
        {
            if (timeSinceVictory < 2)
                timeSinceVictory += Time.deltaTime;
            else
            {

                SR.flipX = false;
                currentDestination = new Vector3(25, transform.position.y, transform.position.z);
                gameObject.transform.position = Vector3.MoveTowards(transform.position, currentDestination, moveSpeed * Time.deltaTime);
                animator.SetBool("Move", true);
                }
        }

    }



    public Vector3 pathToTarget(Vector3 mePos, GameObject theTarget)
    {
        Vector3 tarPos = theTarget.transform.position;

        if (canWalkDirectly(mePos, tarPos, theTarget))
        { return tarPos; }
        else
        {

            
            Collider targetCollider = theTarget.GetComponent<MeshCollider>();
            float xStep = targetCollider.bounds.size.x;
            float zStep = targetCollider.bounds.size.z;
            Vector3[,] pathpoints = new Vector3[NetSize, NetSize];

            float startZ = tarPos.z - zStep * (NetSize / 2 - 1);                                    // create target surrounding points matrix

            for (int z = 0; z < NetSize; z++)
            {
                float startX = tarPos.x - xStep * (NetSize / 2 - 1);
                for (int x = 0; x < NetSize; x++)
                {
                    pathpoints[x, z] = new Vector3(startX + (xStep * x), 1f, startZ + (zStep * z));
                }
            }

            float shortestPath = 9000000;
            float curentpath;
            Vector3 res = mePos;


            for (int x = 0; x < NetSize; x++)                                                             //looking for a shortest 2 step path
            {
                for (int z = 0; z < NetSize; z++)
                {
                    Vector3 currentPos = pathpoints[x, z];
                    if ((canWalkDirectly(currentPos, mePos, theTarget) && (canWalkDirectly(currentPos, tarPos, theTarget))))
                    {
                        curentpath = Vector3.Distance(currentPos, mePos) + Vector3.Distance(currentPos, tarPos);
                        //  curentpath =  Vector3.Distance(currentPos, mePos);

                        if (curentpath < shortestPath)
                        {
                            res = currentPos;
                            shortestPath = curentpath;
                        }
                    }


                }
            }





            return res;
        }




    }


    float checkMySize()
    {
        Vector3 colidrSize = colidr.bounds.size;
        if (colidrSize.x > colidrSize.z)
        {

            // return  (colidrSize.x / 2f);
            return (colidrSize.x / 2f);
        }
        else
        {
            //return  (colidrSize.z / 2f);
            return (colidrSize.z / 2f);
        }

    }
    public bool canWalkDirectly(Vector3 mePos, Vector3 tarPos, GameObject theTarget)
    {
        bool res = true;
        float mySize = checkMySize();


        Vector3 checkPos = Vector3.MoveTowards(mePos, tarPos, mySize / 3);

        while ((Vector3.Distance(checkPos, tarPos) > mySize) && res)
        {
            res = !isObjectHere(checkPos, mySize, theTarget);
            checkPos = Vector3.MoveTowards(checkPos, tarPos, mySize);
        }

        return res;
    }

    bool isObjectHere(Vector3 position, float size, GameObject theTarget)
    {

        Collider[] intersecting = Physics.OverlapSphere(position, size);

        int intersectCountin = 0;
        for (int i = 0; i < intersecting.Length; i++)
        {

            if ((intersecting[i].gameObject.tag == "Ally") || (intersecting[i].gameObject.tag == "Enemy"))
            {

                if ((intersecting[i].gameObject.GetComponent<Defence>().alive == true) && (intersecting[i].gameObject != gameObject) && (intersecting[i].gameObject != target))
                    intersectCountin++;

            }

        }

        if (intersectCountin == 0)
            return false;
        else return true;


    }

    Vector3 ClosestTargetToAttack()
    {
        return new Vector3(0,0,0);
    }
}

