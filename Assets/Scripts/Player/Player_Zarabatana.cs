using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Zarabatana : MonoBehaviour
{
    public int dartCount, dartLimit;
    public float maxCooldownAttack, circleRadius, minTimeScale, maxBulletTime;
    public LayerMask whatIsEnemy, hitLineLayer;
    public GameObject muzzleFX, hitFX;
    public LineRenderer lineFX, targetFX;

    private Transform target = null;
    private float cooldownAttack;
    private PlayerController playerC;
    private Player_Input playerInput;
    private bool isShooting = false, onShoot = false;

    private void Start()
    {
        muzzleFX.SetActive(false);
        lineFX.enabled = false;
        targetFX.enabled = false;
        playerC = GetComponentInParent<PlayerController>();
    }

    private void Awake()
    {
        playerInput = new Player_Input();
        playerInput.Player.Blowdart.performed += _ => TryShoot();

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void TryShoot()
    {
        if (!isShooting && targetFX.enabled)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        //Animation//
        playerC.isShooting = onShoot;


        if (dartCount > 0)
        {
            if (Time.timeScale < 0 || Time.timeScale > 1)
            {
                Time.timeScale = Mathf.Clamp(Time.timeScale, minTimeScale, 1f);
            }

            if (cooldownAttack <= 0)
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleRadius, transform.right, 0, whatIsEnemy);

                //for (int x = 0; x < hit.Length; x++)
                //{
                if (hit.collider != null)
                {
                    if (dartCount > 0 && !playerC.isHanging && !playerC.isClimbing)
                    {
                        target = hit.collider.gameObject.transform;

                        SlowTime();
                    }
                }
                if (hit.collider == null)
                {
                    targetFX.enabled = false;

                    target = null;
                }
                //}
            }

            if (cooldownAttack > 0)
            {
                cooldownAttack -= Time.deltaTime;
            }

        }
        

    }

    public void SlowTime()
    {

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.position, hitLineLayer);

        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("HearTarget"))
            {
                if (hit.collider.gameObject.GetComponent<Patrol>().isSleeping == false && !playerC.isHanging && !playerC.isClimbing)
                {
                    targetFX.enabled = true;

                    targetFX.SetPosition(0, muzzleFX.transform.position);
                    targetFX.SetPosition(1, target.position);


                    //wait for input
                    if (isShooting && targetFX.enabled)
                    {
                        hit.collider.gameObject.GetComponent<Patrol>().Sleep();


                        StartCoroutine(Attack());
                        isShooting = false;
                    }

                }
                else if (targetFX.enabled)
                {
                    targetFX.enabled = false;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        onShoot = true;
        targetFX.enabled = false;


        muzzleFX.SetActive(true);



            lineFX.SetPosition(0, muzzleFX.transform.position);
            lineFX.SetPosition(1, target.position);

            cooldownAttack = maxCooldownAttack;

            lineFX.enabled = true;

            yield return 1;

        Instantiate(hitFX, target.position, Quaternion.identity);
        lineFX.enabled = false;
        isShooting = false;
        onShoot = false;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
