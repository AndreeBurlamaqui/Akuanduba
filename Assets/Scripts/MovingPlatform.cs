using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float lerpTime, extraWaitTime;
    public const float waitTime = 2;

    private Vector2 returnPoint, goPoint;
    private bool isStanding, isWorking;
    void Start()
    {
        returnPoint = transform.Find("ReturnPoint").transform.position;
        goPoint = transform.Find("GoPoint").transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("TrupeMember"))
        {
            isWorking = false;
            isStanding = true;

            StartCoroutine(Elevator());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("TrupeMember"))
        {
            isStanding = false;
            isWorking = false;

            StartCoroutine(Elevator(extraWaitTime));
        }
    }

    private void Update()
    {
        if (isStanding)
        {

            if (isWorking)
                transform.position = Vector2.MoveTowards(transform.position, goPoint, lerpTime * Time.deltaTime);

        }
        else
        {
            if (isWorking)
                transform.position = Vector2.MoveTowards(transform.position, returnPoint, lerpTime * Time.deltaTime);
        }
    }

    IEnumerator Elevator(float waitSeconds = waitTime)
    {

        yield return new WaitForSeconds(waitSeconds);

            isWorking = true;

    }
}
