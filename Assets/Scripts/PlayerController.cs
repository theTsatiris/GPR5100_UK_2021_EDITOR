using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        UiHandler.PLAYER_DATA.playerPositsion = transform.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "bad")
        {
            UiHandler.PLAYER_DATA.playerScore -= 10;
        }
        else if(collider.gameObject.tag == "good")
        {
            UiHandler.PLAYER_DATA.playerScore += 10;
        }

        Destroy(collider.gameObject);
    }
}
