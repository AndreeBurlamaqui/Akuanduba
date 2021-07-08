using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{

    public float extraMass;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Movable"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            //rb.rotation = Mathf.MoveTowards(rb.rotation, 0, 360f);
            //rb.freezeRotation = true;
            //rb.mass += extraMass;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isOnWater = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isOnWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().isOnWater = false;
        }
    }
}
