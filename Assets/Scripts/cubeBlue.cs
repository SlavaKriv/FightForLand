using System.Collections;
using UnityEngine;

public class cubeBlue : MonoBehaviour
{
    public LayerMask layerMask;

    public Material blueMat;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BoomBlue());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
    }

    IEnumerator BoomBlue()
    {
        yield return new WaitForSeconds(0.01f);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            hit.transform.gameObject.GetComponent<Renderer>().material = blueMat;
            hit.transform.gameObject.layer = 7;
        }
        Globals.canUpdateSurface = true;
    }
}
