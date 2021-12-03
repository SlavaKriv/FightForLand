using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    [SerializeField] private Transform playerTransform, botTransform;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Globals.gpState == Globals.GameplayState.PlayerTurn)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, startPosition.y, playerTransform.position.z), 2 * Time.deltaTime);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = 
                Color.Lerp(transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color, Color.red, 3* Time.deltaTime);
        }

        if (Globals.gpState == Globals.GameplayState.BotTurn)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(botTransform.position.x, startPosition.y, botTransform.position.z), 2 * Time.deltaTime);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color =
                Color.Lerp(transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color, Color.blue, 3 * Time.deltaTime);
        }
    }
}
