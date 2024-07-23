using System.Collections.Generic;

using UnityEngine;


public class TargetSelector : MonoBehaviour
{
    public GameObject target = null;

    private GameObject units;
    private TheLists unitsLists;
    List<GameObject> listWithMe;
    public List<GameObject> allies;
    public List<GameObject> enemies;
    float thinkingTime = 0.1f;
    float reactionTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        target = null;
        //  gameObject.GetComponent<MoveToTarget>().target = target;

    }

    // Update is called once per frame
    void Update()
    {
        if ((target != null) && isEnemyAtMeeleRange() && (GetComponent<MoveToTarget>().targetWithinMeeleReach == true) && (target.GetComponent<Defence>().targetPriority >= 2) && (enemies.Contains(target)))  // if unit is in meele battle with a high priority target we are done 
        {
            return;
        }
        else if (isEnemyAtMeeleRange() )  //if any anemy is within my meele range 
        {
                target = meeleEnemySelect(enemies);
                gameObject.GetComponent<MoveToTarget>().target = target;
            return;
        }

    
        else if ((tag == "Ally")&&(RoundStatus.markedTargets > 0) && (target != null) && (target.GetComponent<Defence>().targetPriority != 4)) //check for marked targets not in meele range (undead units only)
        {
            target = closestTargetSelect(enemies);
            gameObject.GetComponent<MoveToTarget>().target = target;

        }
        else if (( (!enemies.Contains(target)) || (target == null) || target.GetComponent<Defence>().targetPriority == 0) && !gameObject.GetComponent<MoveToTarget>().isActing) //check for closest target)
        // if ( ||!((enemies.Contains(target))))
        {
            target = closestTargetSelect(enemies);
            gameObject.GetComponent<MoveToTarget>().target = target;

        }
        
     }


   

    GameObject meeleEnemySelect(List<GameObject> targets)
    {
       // float dist = 9000;
        GameObject result = null;


        int thePriority = -1;

          for (int i = 0; i < targets.Count; i++) //check for targets within meele range
            {


                int currentPriority = targets[i].GetComponent<Defence>().targetPriority;

                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if ((currentDist <= GetComponent<MoveToTarget>().meeleRange) && (currentPriority > thePriority)) // if target is closer to my meelerange and its priority is higher then my current target, i will switch
                {

                    thePriority = currentPriority;

                    result = targets[i];
                    //gameObject.GetComponent<MoveToTarget>().targetWithinMeeleReach = false;


                }
            }
            return (result);
        }
        GameObject closestTargetSelect(List<GameObject> targets)
        {

            float dist = 9000;
            GameObject result = null;
    

        //check for mareked for death targets

        for (int i = 0; i < targets.Count; i++)
            {
                int currentPriority = targets[i].GetComponent<Defence>().targetPriority;
                //   Vector3 goToPos = gameObject.GetComponent<MoveToTarget>().pathToTarget(transform.position, targets[i]);
                //  float currentDist = Vector3.Distance(goToPos, gameObject.transform.position);
                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if (((currentDist < dist) && (currentPriority == 4)))
                {


                    dist = currentDist;
                    result = targets[i];
                    //gameObject.GetComponent<MoveToTarget>().targetWithinMeeleReach = false;


                }

            }
       
      
        if (result == null)  //check for non-0 priority targets out of meele range
        {

            for (int i = 0; i < targets.Count; i++)
            {
                int currentPriority = targets[i].GetComponent<Defence>().targetPriority;
             
                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if (((currentDist < dist) && (currentPriority != 0)))
                {

                   
                    dist = currentDist;
                    result = targets[i];
             


                }

            }
        }
        if (result == null)   //check for 0 priority targets
        {
            for (int i = 0; i < targets.Count; i++)
            {
               
         
                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if (currentDist <= dist) 
                {
                    dist = currentDist;
                    result = targets[i];
               
                }

            }


        }

        return (result);
    }

    public GameObject selectAnotherTarget(List<GameObject> targets) //in case of MoveToTarget cannot find path to the target, other target will be selected
    {
        if (target == null) //for a case no more targets leeft 
        {
            return null;
        }
            float dist = 9000; //probably not the best practice but im sure the distances will be shorteer then that

        float oldDist = Vector3.Distance(transform.position,target.transform.position); //distance to the current target, new target should be farther
        GameObject result = null;
   
      


            for (int i = 0; i < targets.Count; i++)
            {
                int currentPriority = targets[i].GetComponent<Defence>().targetPriority;
              
                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if ((currentDist < dist) && (currentPriority != 0) && (currentDist> oldDist))
                {


                    dist = currentDist;
                    result = targets[i];
                 


                }

            }
       
        if (result == null)   //check for 0 priority targets
        {
            for (int i = 0; i < targets.Count; i++)
            {

                
                float currentDist = Vector3.Distance(targets[i].transform.position, gameObject.transform.position);

                if ((currentDist <= dist)&&  (currentDist > oldDist))
                {
                    dist = currentDist;
                    result = targets[i];
                  
                }

            }


        }
        target = result;
        return (result);
    }


   

   

    public void reselectTarget()
    {
        target = closestTargetSelect(enemies);
    }


    public bool isEnemyAtMeeleRange() //check if meele range target exist
    {
        int intersectCountin = 0;
       
        float meeleRange = GetComponent<MoveToTarget>().meeleRange;
        Collider[] intersecting = Physics.OverlapSphere(transform.position, meeleRange);

      //  int intersectCountin = 0;
        for (int i = 0; i < intersecting.Length; i++)
        {

            if ((intersecting[i].isTrigger == false) && ((intersecting[i].gameObject.tag == "Enemy") || (intersecting[i].gameObject.tag == "Ally")) && (intersecting[i].gameObject.tag != tag)) // count only non-trigger colliders because units have bouth.
            {
                float dist = Vector3.Distance(transform.position, intersecting[i].transform.position);
                if ((intersecting[i].gameObject.GetComponent<Defence>().alive == true) && (dist<= meeleRange))
                    intersectCountin++;

            }

        }

        if (intersectCountin == 0)
       
        return false;
       
        else return true;

       
    }
    



}
