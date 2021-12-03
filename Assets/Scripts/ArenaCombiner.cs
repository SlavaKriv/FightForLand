using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCombiner : MonoBehaviour
{
    [SerializeField] private List<MeshFilter> listMesh;
    //[SerializeField] private MeshFilter targetMeshFilter;
    private GameObject targetMeshFilter;

    public Material redMat, blueMat;
    private List<int> deleteFuck = new List<int>();
    int first = 0;
    [ContextMenu("Combine Meshes")]

    IEnumerator CombineMeshes()
    {

        var combine = new CombineInstance[listMesh.Count];

        for (var i = 0; i < listMesh.Count; i++)
        {
            combine[i].mesh = listMesh[i].sharedMesh;
            combine[i].transform = listMesh[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine);
        switch (Globals.lastColor)
        {
            case Globals.LastColor.RED:
                targetMeshFilter = new GameObject();
                targetMeshFilter.AddComponent<MeshRenderer>().material = redMat;
                targetMeshFilter.AddComponent<MeshFilter>();
                targetMeshFilter.AddComponent<MeshCollider>().convex = true;

                targetMeshFilter.AddComponent<BzKovSoft.ObjectSlicerSamples.ObjectSlicerSample>();
                targetMeshFilter.layer = 6;


                targetMeshFilter.GetComponent<MeshCollider>().sharedMesh = targetMeshFilter.GetComponent<MeshFilter>().mesh;
                targetMeshFilter.GetComponent<MeshFilter>().mesh = mesh;
                targetMeshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
                Globals.widthRed = targetMeshFilter.GetComponent<MeshFilter>().mesh.bounds.size.x;
                Globals.heightRed = targetMeshFilter.GetComponent<MeshFilter>().mesh.bounds.size.z;
                Globals.sizeRed = Globals.widthRed + Globals.heightRed;
                targetMeshFilter.transform.SetParent(transform);

                Globals.lastColor = Globals.LastColor.BLUE;
                break;

            case Globals.LastColor.BLUE:
                targetMeshFilter = new GameObject();
                targetMeshFilter.AddComponent<MeshRenderer>().material = blueMat;
                targetMeshFilter.AddComponent<MeshFilter>();
                targetMeshFilter.AddComponent<MeshCollider>().convex = true;

                targetMeshFilter.AddComponent<BzKovSoft.ObjectSlicerSamples.ObjectSlicerSample>();
                targetMeshFilter.layer = 7;


                targetMeshFilter.GetComponent<MeshCollider>().sharedMesh = targetMeshFilter.GetComponent<MeshFilter>().mesh;
                targetMeshFilter.GetComponent<MeshFilter>().mesh = mesh;
                targetMeshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
                Globals.widthBlue = targetMeshFilter.GetComponent<MeshFilter>().mesh.bounds.size.x;
                Globals.heightBlue = targetMeshFilter.GetComponent<MeshFilter>().mesh.bounds.size.z;
                Globals.sizeBlue = Globals.widthBlue + Globals.heightBlue;
                targetMeshFilter.transform.SetParent(transform);

                Globals.lastColor = Globals.LastColor.RED;
                break;
        }
        
        //Instantiate(targetMeshFilter, transform.GetChild(first).position, Quaternion.identity, transform) ;
        yield return new WaitForSeconds(0.1f);
        //if (Globals.botCanCut) Globals.botCanCut = false;
    }

    public void Update()
    {

        //layer 6-red    7-blue
        if (Globals.canCombineMeshes)
        {
            

            bool isFirst = false;

            listMesh.Clear();
            deleteFuck.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                switch (Globals.lastColor)
                {
                    case Globals.LastColor.RED:
                        if (transform.GetChild(i).gameObject.layer == 6)
                        {
                            if (!isFirst)
                            {


                                first = i;
                                isFirst = true;

                            }
                            deleteFuck.Add(i);
                            listMesh.Add(transform.GetChild(i).GetComponent<MeshFilter>());
                        }
                        break;

                    case Globals.LastColor.BLUE:
                        if (transform.GetChild(i).gameObject.layer == 7)
                        {
                            if (!isFirst)
                            {


                                first = i;
                                isFirst = true;

                            }
                            deleteFuck.Add(i);
                            listMesh.Add(transform.GetChild(i).GetComponent<MeshFilter>());
                        }
                        break;
                }
               
            }

            StartCoroutine(CombineMeshes());
                    
            /*
            objqwe.GetComponent<MeshCollider>().convex = true;
            objqwe.GetComponent<MeshCollider>().inflateMesh = objqwe.GetComponent<MeshFilter>().mesh;
            objqwe.GetComponent<MeshCollider>().sharedMesh = objqwe.GetComponent<MeshFilter>().sharedMesh;
            */
            if (deleteFuck.Count != 0)
            {
                for (int j = 0; j < deleteFuck.Count; j++)
                {
                    //.Log("deeltefuck: " + deleteFuck[j]);
                    Destroy(transform.GetChild(deleteFuck[j]).gameObject);
                }
            }

            //Debug.Log("end globals canCombineMeshes");

            Globals.canUpdateSurface = true;
            //if (Globals.botCanCut) Globals.botCanCut = false;
            Globals.canCombineMeshes = false;

        }
    }
}