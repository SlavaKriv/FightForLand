using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(-Vector3.right * 3 * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * 3 * Time.deltaTime);
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up * 3 * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) transform.Translate(-Vector3.up * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider target)
    {
        Debug.Log("EFE");
        MySlicerController.instance.Cut(target.gameObject);
    }
}
