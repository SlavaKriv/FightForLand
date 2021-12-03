using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class agentMove : MonoBehaviour
{
    NavMeshAgent agent;

    const float PI = 3.14164534f;
    float i=0, angel = 0, radius = 4;
    bool stopRot = false, dontSpam1= false, dontSpam2 = false;
    int cast = 0, frames = 0;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(RandomNavmeshLocation(Random.Range(-3, 3)));
        StartCoroutine(newDir());

    }


    void FixedUpdate()
    {

        //i++;
        //angel = i * (PI / 180);

        //Vector3 krugVector = new Vector3(-Mathf.Cos(angel)*radius, 0, -Mathf.Sin(angel)*radius);
        //agent.SetDestination(transform.position + krugVector);
        if (Globals.gpState == Globals.GameplayState.BotTurn)
        {

            dontSpam1 = false;
            if (cast == 1)
            {
                frames++;

                if (frames > 15)
                {
                    frames = 0;
                    cast = 2;
                }
                    
            }

            if (cast == 2)
            {
                frames++;
                if (!stopRot)
                {
                    agent.enabled = false;
                    transform.Rotate(0, 10, 0);
                }

                if (!dontSpam2)
                {
                    StartCoroutine(stopRotating());
                    dontSpam2 = true;
                }
               

            }
        } else
        {
            frames = 0;
            cast = 1;
            agent.enabled = true;
            dontSpam2 = false;

            if (!dontSpam1) {
                frames = 0;
                agent.SetDestination(RandomNavmeshLocation(Random.Range(-3, 3)));
                StartCoroutine(newDir());
                dontSpam1 = true;
            }
        }
        //if (i > 360) i = 0;
        Debug.Log(frames);
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

    IEnumerator stopRotating()
    {

        yield return new WaitForSecondsRealtime((Random.Range(0, 10) / 10) + 0.4f);
        stopRot = false;
        Globals.botCanCut = true;

    }

    IEnumerator newDir()
    {
        yield return new WaitForSecondsRealtime(2f);
        agent.SetDestination(RandomNavmeshLocation(Random.Range(0, 100)));
        
        StartCoroutine(newDir());
    }


}
