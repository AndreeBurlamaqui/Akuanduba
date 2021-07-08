using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupeController : MonoBehaviour
{
    public bool waitFollow = true, isOnLever = false;
    public GameObject originalMember;
    public float massAdd;

    public List<GameObject> membersCollected = new List<GameObject>();
    private Player_Input playerInput;
    private Rigidbody2D playerRB;
    private PlayerController playerScript;


    private void Awake()
    {
        playerInput = new Player_Input();
        playerInput.Player.WaitFollow.performed += _ => waitFollow = !waitFollow;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (membersCollected.Count > 0)
        {
            if (waitFollow)
            {
                Follow();
            }
            else
            {
                Wait();
            }
        }
    }
    private void Follow()
    {
        foreach(GameObject member in membersCollected)
        {
            member.GetComponent<TrupeIA>().FollowPlayer();
        }
    }
    private void Wait()
    {
        foreach (GameObject member in membersCollected)
        {
            member.GetComponent<TrupeIA>().StopFollow();
        }
    }

    public void AddMember(Vector2 spawnPos)
    {
        GameObject newMember = Instantiate(originalMember, spawnPos, Quaternion.identity, transform);
        membersCollected.Add(newMember);
    }

    public void EnableHelp()
    {
        foreach (GameObject member in membersCollected)
        {
            playerRB.mass += massAdd;
            playerScript.isTrupeHelping = true;
        }
    }

    public void DisableHelp()
    {
        playerRB.mass = 1f;
        playerScript.isTrupeHelping = false;

    }

    public void TeleportMinion(Transform newPos)
    {
        foreach (GameObject member in membersCollected)
        {
            member.transform.position = newPos.position;
        }
    }

    public void OpenDoor(GameObject door, int minionsToDestroy)
    {
        if(membersCollected.Count >= minionsToDestroy){

            for (int x = 0; x < minionsToDestroy; x++)
            {
                membersCollected.Remove(membersCollected[x]);
            }

            door.GetComponent<Animator>().SetBool("isOpen", true);

        }
        else
        {
            door.GetComponent<Animator>().Play("DoorCant");

        }

    }
}
