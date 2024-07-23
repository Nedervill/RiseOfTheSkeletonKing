
using UnityEngine;
using UnityEngine.EventSystems;


public class PlanningMode : MonoBehaviour
{
    /* the component is alowing to grab and move the undead units during the planning miode   
      
     */


    [SerializeField] GameObject Outline;
    float fixedY;
    Vector3 startPos;
    Vector3 dist;
    Rigidbody rb;
    MeshCollider myCollider;
    SpriteRenderer sr;
    int colliding = 0;
    public bool isDragged = false;
    Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatusUpd>().stats;
        sr = GetComponent<StatusUpd>().SpriteHolder.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        myCollider = gameObject.GetComponent<MeshCollider>();
        fixedY = myCollider.bounds.size.y / 2;
        if (tag == "Enemy")
            sr.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y != fixedY)
            transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
        if ((colliding > 0) && (!isDragged))
        {
            reanimatePosition();

        }


        if (colliding > 0)
            sr.color = UnityEngine.Color.red;
        else
            sr.color = UnityEngine.Color.white;

    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //if not in menu mode
            if ((tag == "Ally") && (isActiveAndEnabled))
        {
            Cursor.visible = false;
            RoundStatus.draggingIsHappening = true;
            myCollider.isTrigger = true;
            startPos = transform.position;
            isDragged = true;
            Outline.SetActive(false);
        }
    }

    void OnMouseDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject())//if not in menu mode
            if ((tag == "Ally") && (isActiveAndEnabled))
        {
            Cursor.visible = false;
            RoundStatus.draggingIsHappening = true;
            Outline.SetActive(false);
            Vector3 size = myCollider.bounds.size;
            float planeY = 0;
            Transform draggingObject = transform;

            // dragging the object on the surface of the plane
            Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition - (size / 2));

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if (plane.Raycast(ray, out distance))
            {
                draggingObject.position = ray.GetPoint(distance); // distance along the ray
            }
        }
    }

    private void OnMouseUp() //releasing the unit
    {
          
            if ((tag == "Ally") && (isActiveAndEnabled))
          {
            Cursor.visible = true;
            RoundStatus.draggingIsHappening = false;
            Outline.SetActive(true);

            if (colliding > 0)  // the unit jumps back to its starting position if its position is forbidden
                transform.position = startPos;
            myCollider.isTrigger = false;
            isDragged = false;
            colliding = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((tag == "Ally") && (collision.gameObject.tag != "Ground") && (collision.gameObject.tag != "Tutorial") && (collision.gameObject.tag != "Decoration") && isDragged)
            colliding++;
    }
    private void OnTriggerExit(Collider other)
    {
        if ((tag == "Ally") && (other.gameObject.tag != "Ground") && (other.gameObject.tag != "Tutorial") && (other.gameObject.tag != "Decoration") && isDragged)
            colliding--;
    }

  
    public void reanimatePosition()  //controlled randomization of a spawn of the new reanimated undead and the old units from previous rounds
    {

        float size = checkMySize();
        bool taken = false;
        stats = GetComponent<StatusUpd>().stats;
        float minX, maxX;
        float x = 0;
        float z = 0;


        if (stats.targetPriority == 2) //the warriors go to frontine
        {
            maxX = -2.0f;
            minX = -3.5f;
        }
        else if (stats.ranged)  //the rangies go to thee backlline
        {
            maxX = -5.5f;
            minX = -7.5f;
        }
        else  // the special meele units go to the middle line
        {
            maxX = -3.5f;
            minX = -5.5f;
        }
        while (!taken)
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(-5.5f, 4.5f);
            Vector3 potentialPos = new Vector3(x, fixedY, z);
            if (!(isObjectHere(potentialPos, size)))
                taken = true;
        }

        transform.position = new Vector3(x, fixedY, z);




    }

    public void enemyPosition() //controlled randomization of a spawn of the humans
    {
        float size = checkMySize();
        bool taken = false;
        stats = GetComponent<StatusUpd>().stats;
        float minX, maxX;
        float x = 0;
        float z = 0;

        if (stats.targetPriority == 2) //the solldiers go to frontine
        {
            minX = 2.0f;
            maxX = 3.5f;
        }
        else if (stats.ranged) //the rangies go to thee backlline
        {
            minX = 5.5f;
            maxX = 7.5f;
        }
        else  // the special meele units go to the middle line
        {
            minX = 3.5f;
            maxX = 5.5f;
        }

        while (!taken)
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(-5.5f, 4.5f);
            Vector3 potentialPos = new Vector3(x, fixedY, z);
            if (!(isObjectHere(potentialPos,size)))
            taken = true;
        }

        transform.position = new Vector3(x, fixedY, z);
    }


    bool isObjectHere(Vector3 position, float size) //checks if the unit will collide with other units if will be spwaned there
    {

        Collider[] intersecting = Physics.OverlapSphere(position, size/2);

        int intersectCountin = 0;
        for (int i = 0; i < intersecting.Length; i++)
        {

            if ((intersecting[i].gameObject.tag == "Ally") || (intersecting[i].gameObject.tag == "Enemy"))
            {

             if (intersecting[i].gameObject != gameObject) 
                    intersectCountin++;

            }

        }

        if (intersectCountin == 0)
            return false;
        else return true;


    }

    float checkMySize()
    {
        Vector3 colidrSize = GetComponent<MeshCollider>().bounds.size;
        if (colidrSize.x > colidrSize.z)
        {

            // return  (colidrSize.x / 2f);
            return (colidrSize.x );
        }
        else
        {
            //return  (colidrSize.z / 2f);
            return (colidrSize.z);
        }

    }
}