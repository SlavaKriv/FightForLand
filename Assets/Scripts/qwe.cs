using BzKovSoft.ObjectSlicer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qwe : MonoBehaviour
{

    //public Material redMat, blueMat;
    public GameObject _target;

    void Start()
    {
        var sliceable = _target.GetComponent<IBzSliceableAsync>();
        if (sliceable != null)
        {
            
            sliceable.Slice(new Plane(Vector3.forward, 0), 0, null);
        }
    }


    void Update()
    {
        
    }
}
