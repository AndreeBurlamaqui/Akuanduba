using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTuto : MonoBehaviour
{
    private Animator anim;

        void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isInside", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isInside", false);
        }
    }
}
