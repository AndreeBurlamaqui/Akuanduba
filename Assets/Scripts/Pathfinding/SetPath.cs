using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetPath : MonoBehaviour
{

    public int arraySize;
    public Transform targetPath;
    public bool isJump;

    private Transform[] path;
    private int[] jumpArray;
    private Transform player;
    private Rigidbody2D playerRB;
    private int countArray = 0;

    private void Awake()
    {
        path = new Transform[arraySize];
        jumpArray = new int[arraySize];

        player = GameObject.FindWithTag("Player").transform;
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

    }

    private void LateUpdate()
    {
        if (countArray >= arraySize)
        {
            countArray = 0;
        }

        if (playerRB.velocity.x > 0 || playerRB.velocity.y > 0)
        {
            while(countArray < arraySize)
            {
                path[countArray] = player.transform;
                

                if (playerRB.velocity.y > 0.5f)
                {
                    jumpArray[countArray] = 1;
                }
                else
                {
                    jumpArray[countArray] = 0;
                }

                countArray++;

            }

        }

        targetPath = path[0];

        if (jumpArray[0] == 1)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }
}
