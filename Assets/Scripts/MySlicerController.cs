using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class MySlicerController : MonoBehaviour
{
    public static MySlicerController instance;
    //public GameObject _target;

    void Start()
    {
        instance = this;
    }

    public void Cut(GameObject target)
    {

        var sliceable = target.GetComponent<IBzSliceableAsync>();


        if (sliceable == null) { return; }

        Plane plane = new Plane(Vector3.up, 0f);
        sliceable.Slice(plane, 0, null);
        Debug.Log("Katamna");
    }
}
