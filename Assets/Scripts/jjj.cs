using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jjj : MonoBehaviour
{

    public float width = 1f;
    public float height = 1f;
    MeshFilter m_f;
    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        m_f = GetComponent<MeshFilter>();

        mesh = new Mesh();

        m_f.mesh = mesh;

       
        
    }

    void CreateFuckingMesh()
    {
        Vector3[] vert;
        int[] tri;
        //вершины
        vert = new Vector3[9]
        {
            new Vector3(-1, 0, -0.5f), new Vector3(-0.5f, 0, -1f),
            new Vector3(0.5f, 0, -1f), new Vector3(1, 0, -0.5f),
            new Vector3(1, 0, 0.5f), new Vector3(0.5f, 0, 1),
            new Vector3(-0.5f, 0, 1), new Vector3(-1, 0, 0.5f),
            new Vector3(0,0,0)
        };

        //треунгольники
        tri = new int[24];
        tri[0] = 0;
        tri[1] = 8;
        tri[2] = 1;

        tri[3] = 1;
        tri[4] = 8;
        tri[5] = 2;

        tri[6] = 2;
        tri[7] = 8;
        tri[8] = 3;

        tri[9] = 3;
        tri[10] = 8;
        tri[11] = 4;

        tri[12] = 4;
        tri[13] = 8;
        tri[14] = 5;

        tri[15] = 5;
        tri[16] = 8;
        tri[17] = 6;

        tri[18] = 6;
        tri[19] = 8;
        tri[20] = 7;

        tri[21] = 7;
        tri[22] = 8;
        tri[23] = 0;

        //UV (отображеине текстуры

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        CreateFuckingMesh();

    }
}
