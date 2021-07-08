using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{

    public GameObject waterEnter;
    public ParticleSystem grassRun;
    public float leavesCountRun, leavesCountCrounch;
    public Vector2 leavesSpeedMinMaxRun, leavesSpeedMinMaxCrounch;
    public bool onGround;

    private Rigidbody2D rb;
    private PlayerController playerParent;
    private ParticleSystem.EmissionModule pEmissionGrass;
    private ParticleSystem.MainModule pMainGrass;

    private void Start()
    {
        waterEnter.SetActive(false);
        rb = transform.parent.GetComponent<Rigidbody2D>();
        playerParent = transform.parent.GetComponent<PlayerController>();
        pEmissionGrass = grassRun.emission;
        pMainGrass = grassRun.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            waterEnter.SetActive(true);

        }

        if (collision.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.5f && onGround)
        {

            if (playerParent.isCrounch)
            {
                pEmissionGrass.rateOverTime = leavesCountCrounch;
                pMainGrass.startSpeed = new ParticleSystem.MinMaxCurve(leavesSpeedMinMaxCrounch.x, leavesSpeedMinMaxCrounch.y);
            }
            else
            {
                pEmissionGrass.rateOverTime = leavesCountRun;
                pMainGrass.startSpeed = new ParticleSystem.MinMaxCurve(leavesSpeedMinMaxRun.x, leavesSpeedMinMaxRun.y);
            }

            if (!grassRun.isPlaying)
            {

                grassRun.Play();
            }
        }
        else
        {
            grassRun.Stop();
        }
    }




}
