using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaSystem : MonoBehaviour
{
    public Animator linkDoor;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("TrupeMember"))
        {
            anim.SetBool("isPressed", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("TrupeMember"))
        {
            anim.SetBool("isPressed", false);
        }
    }

    public void CloseDoor()
    {
        linkDoor.SetBool("isOpen", false);
    }

    public void OpenDoor()
    {
        linkDoor.SetBool("isOpen", true);

    }
}
