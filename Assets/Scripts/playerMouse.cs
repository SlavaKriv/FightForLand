using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BzKovSoft.ObjectSlicer;
using UnityEngine.AI;

public class playerMouse : MonoBehaviour
{

    public enum WHOAMI { PLAYER, BOT };
    public WHOAMI whoami = WHOAMI.PLAYER;
    public Material redMat, blueMat;
    public AudioSource soundBoom;
    public LayerMask layerMask;

    private NavMeshAgent agent;
    private bool dontSpam1 = false;
    private bool isSliced = false;

    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //if (whoami == WHOAMI.BOT) agent.SetDestination(RandomNavmeshLocation(Random.Range(0, 10) * Time.deltaTime));
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.2508419f, transform.position.z);
        switch (whoami)
        {
            case WHOAMI.PLAYER:
                if (Globals.gpState == Globals.GameplayState.PlayerTurn)
                {
                    //move and rotate
                    if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, 0, -2);
                    if (Input.GetKey(KeyCode.E)) transform.Rotate(0, 0, 2);
                    if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.up * 2 * Time.deltaTime);
                    if (Input.GetKey(KeyCode.D)) transform.Translate(-Vector3.up * 2 * Time.deltaTime);
                    if (Input.GetKey(KeyCode.W)) transform.Translate(-Vector3.right * 2 * Time.deltaTime);
                    if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.right * 2 * Time.deltaTime);

                    if (Input.GetMouseButtonDown(0))
                    {
                        soundBoom.Play();
                        FightLand();
                        Globals.gpState = Globals.GameplayState.BotTurn;
                    }
                }
               

                break;

            case WHOAMI.BOT:

                transform.localPosition = new Vector3(0, 0.2508419f, 0);

                if (Globals.gpState == Globals.GameplayState.BotTurn)
                {
                    if (!dontSpam1 && Globals.botCanCut)
                    {
                        
                        FightLand();
                        //Globals.gpState = Globals.GameplayState.PlayerTurn;
                        dontSpam1 = true;
                    }
                } else
                {
                    dontSpam1 = false;
                }

                break;
        }


        
        //if (whoami == WHOAMI.PLAYER)
        //    Debug.Log(whoami + ": " + transform.position + ", rotation: " + transform.rotation.eulerAngles + " x: " + transform.position.x + " rot y " + transform.rotation.eulerAngles.y);
        
        //Debug.DrawRay(transform.position + new Vector3(0, -0.4f, 0), -transform.right * 10, Color.cyan);
        //Debug.DrawRay(transform.position + new Vector3(-0.2f, -0.4f, 0), -transform.right* 10 , Color.blue);
       
        //Debug.DrawRay(transform.position + new Vector3(0, -0.07f, 0), (-transform.right + new Vector3(0, 0, 0.1f)) * 10, Color.red);

    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }


    void FightLand()
    {
        switch (whoami)
        {
            case WHOAMI.PLAYER:

                ray = new Ray(transform.position + new Vector3(0, -0.4f, 0), -transform.right);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    IBzSliceableAsync sliceable = hit.transform.GetComponent<IBzSliceableAsync>();

                    Vector3 direction = Vector3.Cross(ray.origin+ new Vector3(0, 2, 0), ray.direction);
                    Plane plane = new Plane(direction, ray.origin);

                    if (sliceable != null)
                    {
                        sliceable.Slice(plane, 0, null);
                    }
                }

                StartCoroutine(brushRay());
                break;

            case WHOAMI.BOT:
                bool fail = false;
                ray = new Ray(transform.position + new Vector3(0, -0.4f, 0f), -transform.right);
                RaycastHit hit_;
                if (Physics.Raycast(ray, out hit_, Mathf.Infinity, layerMask))
                {
                    IBzSliceableAsync sliceable = hit_.transform.GetComponent<IBzSliceableAsync>();

                    Vector3 direction = Vector3.Cross(ray.origin + new Vector3(0, 2, 0), ray.direction);
                    Plane plane = new Plane(direction, ray.origin);

                    if (sliceable != null)
                    {
                        sliceable.Slice(plane, 0, null);
                        soundBoom.Play();
                        Debug.Log("Fight Land (bot)");
                        isSliced = true;
                    }
                    else
                    {
                        fail = true;
                        Globals.gpState = Globals.GameplayState.PlayerTurn;
                    }

                }
                else isSliced = false;

                Globals.botCanCut = false;
                if (!fail)
                {
                    StartCoroutine(brushRay());
                }
                break;
        }
        
    }
    
    IEnumerator brushRay()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        switch (whoami)
        {
            case WHOAMI.PLAYER:
                if ((transform.position.x < 0 && transform.rotation.eulerAngles.y <= 90) | (transform.position.x > 0 && transform.rotation.eulerAngles.y <= 90))
                {
                    //left blue
                    ray = new Ray(transform.position + new Vector3(-0.1f, -0.4f, 0), -transform.right);
                    RaycastHit hit2;

                    if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layerMask))
                    {
                        hit2.transform.GetComponent<Renderer>().material = redMat;
                        hit2.transform.gameObject.layer = 6;
                        Debug.Log("left raycast");
                    }
                }

                if ((transform.position.x < 0 && transform.rotation.eulerAngles.y > 90) | (transform.position.x > 0 && transform.rotation.eulerAngles.y > 90))
                {
                    //left blue
                    ray = new Ray(transform.position + new Vector3(0.1f, -0.4f, 0), -transform.right);
                    RaycastHit hit2;

                    if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layerMask))
                    {
                        hit2.transform.GetComponent<Renderer>().material = redMat;
                        hit2.transform.gameObject.layer = 6;
                        Debug.Log("left raycast");
                    }
                }
                Globals.canCombineMeshes = true;
                break;

            case WHOAMI.BOT:
                if (isSliced)
                {
                    //Globals.botCanCut = false;
                    if ((transform.position.x < 0 && transform.rotation.eulerAngles.y > 270) | (transform.position.x > 0 && transform.rotation.eulerAngles.y > 270))
                    {
                        //left bot
                        ray = new Ray(transform.position + new Vector3(-0.2f, -0.4f, 0), -transform.right);
                        RaycastHit hit2;
                        if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layerMask))
                        {
                            hit2.transform.GetComponent<Renderer>().material = blueMat;
                            hit2.transform.gameObject.layer = 7;
                            Debug.Log("left raycast (bot)");
                        }
                    }

                    if ((transform.position.x < 0 && transform.rotation.eulerAngles.y < 270) | (transform.position.x > 0 && transform.rotation.eulerAngles.y < 270))
                    {
                        //right bot
                        ray = new Ray(transform.position + new Vector3(0.2f, -0.4f, 0), -transform.right);
                        RaycastHit hit2;
                        if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layerMask))
                        {
                            hit2.transform.GetComponent<Renderer>().material = blueMat;
                            hit2.transform.gameObject.layer = 7;
                            Debug.Log("right raycast (bot)");
                        }
                    }
                    Globals.gpState = Globals.GameplayState.PlayerTurn;
                    Globals.canCombineMeshes = true;
                } else
                    Globals.gpState = Globals.GameplayState.PlayerTurn;


                break;
        }
        

        //if ((transform.position.x < 0 && transform.rotation.eulerAngles.y > 90) | (transform.position.x > 0 && transform.rotation.eulerAngles.y > 90))
        /*
        if ((transform.position.x < 0 && transform.rotation.eulerAngles.y >= 90))
        {
            //right red
            ray = new Ray(transform.position + new Vector3(0, -0.07f, 0), (-transform.right + new Vector3(0.1f, 0, 0)));
            RaycastHit hit3;
            if (Physics.Raycast(ray, out hit3, 7))
            {
                hit3.transform.GetComponent<Renderer>().material = redMat;
                hit3.transform.gameObject.layer = 6;
                Debug.Log("red raycast");
            }

            return;
        }
        */
    }

}
