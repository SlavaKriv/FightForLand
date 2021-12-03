using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textGUI : MonoBehaviour
{
    //public enum TEXT_STATE { blueSize, redSize };
    //public TEXT_STATE textState = TEXT_STATE.blueSize;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //1text
        text.text = "red width: " + Globals.widthRed + ", h: " + Globals.heightRed + ", summ: " + Globals.sizeRed +"; \n blue w: " + Globals.widthBlue + ", h: " + Globals.widthBlue + ", size: " + Globals.sizeBlue;
    }
}
