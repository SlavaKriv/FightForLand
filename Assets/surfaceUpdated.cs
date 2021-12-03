using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class surfaceUpdated : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Globals.canUpdateSurface)
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            Globals.canUpdateSurface = false;
        }
    }
}
