using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool isSee, isSleeping;
    public GameObject player;
    public float moveSpeed;    
    //controle do raycast
    public float ViewDistance, wallDistance, sleepingTime, idleTime;
    public Transform castPoint, groundCheck, headView, hearGO;
    public LayerMask layerToSee, layerFovCollide;
    public Animator anim;

    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle, slopeRotationSpeed;
    [SerializeField]
    private LayerMask whatIsGround, whatIsWall;
    [SerializeField]
    private PhysicsMaterial2D noFriction, fullFriction;

    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;

    private bool facingRight = true;
    private bool isOnSlope;
    private bool canWalkOnSlope;

    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private GameObject fovInstantiated;
    private bool isFliping;

    public Transform prefabFoV;
    public FieldOfView FoVscript;
    public float FOV, FOVDistance;

    [ColorUsage(true, true)]
    public Color colorConeIdle, colorConeDetected;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        capsuleColliderSize = cc.size;

        

        player = GameObject.FindWithTag("Player").transform.Find("ChestCheck").gameObject;


        fovInstantiated = Instantiate(prefabFoV, null).gameObject;
        FoVscript = fovInstantiated.GetComponent<FieldOfView>();

        _propBlock = new MaterialPropertyBlock();
        _renderer = fovInstantiated.GetComponent<Renderer>();

        FoVscript.SetFoV(FOV);
        FoVscript.SetViewDistance(FOVDistance);
        FoVscript.SetLayerMask(layerFovCollide);

        if (!isSee && hearGO != null)
        {
            hearGO.GetComponent<CircleCollider2D>().radius = FOVDistance;
        }
    }

    [SerializeField]
    private Vector2 direction;
    public void Walk()
    {
        if (!isFliping)
        {
            if (cc.sharedMaterial != noFriction)
                cc.sharedMaterial = noFriction;

            direction = Vector2.right * (facingRight ? 1 : -1);

            RaycastHit2D wallInfo = Physics2D.Raycast(headView.position, direction, wallDistance, whatIsWall);

            if (wallInfo.collider != null)
            {
                if (wallInfo.collider == true)
                {
                    StartCoroutine(Flip());
                }
            }

            RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, ViewDistance, whatIsGround);

            if (groundInfo.collider == null)
            {
                StartCoroutine(Flip());
            }


            if (groundInfo && !isOnSlope) //if not on slope
            {
                rb.velocity = new Vector2(moveSpeed * direction.x, 0.0f);
            }
            else if (groundInfo && isOnSlope && canWalkOnSlope) //If on slope
            {
                rb.velocity = new Vector2(moveSpeed * slopeNormalPerp.x * -direction.x, moveSpeed * slopeNormalPerp.y * -direction.x);
            }
        }
        else
        {
            cc.sharedMaterial = fullFriction;
        }

    }

    IEnumerator Flip()
    {
        isFliping = true;

        yield return new WaitForSeconds(idleTime);

        rb.velocity = Vector2.zero;

        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        isFliping = false;

    }

    private void FixedUpdate()
    {
        anim.SetBool("isFliping", isFliping);

        if (!isSleeping)
        {
            if (!fovInstantiated.activeSelf)
            {
                fovInstantiated.SetActive(true);
            }

            SlopeCheck();
            Walk();
            FieldOfView();
        }
        else
        {
            rb.velocity = Vector2.zero;
            fovInstantiated.SetActive(false);
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance / 2f, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance / 2f, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;

                //Por o sprite no angulo do slope
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }


        //ESSA PARTE DO CODIGO FAZ ELE PARAR NO SLOPE, NAO SEI SE PRECISA
        /*if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
            cc.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
            cc.sharedMaterial = noFriction;
        }*/
    }


    public void FieldOfView()
    {
        FoVscript.SetOrigin(headView.position);
        FoVscript.SetAimDirection(direction);

        FindPlayer();
    }

    public void FindPlayer()
    {
        if (isSee)
        {
            if (Vector3.Distance(headView.position, player.transform.position) < FOVDistance)
            {
                // primeiro pega os valores do material q está no renderer
                _renderer.GetPropertyBlock(_propBlock);

                //põe os valores
                _propBlock.SetColor("_ColorMesh", colorConeIdle);

                //aplica os valores editados
                _renderer.SetPropertyBlock(_propBlock);

                //inside view distance
                Vector3 dirToPlayer = (player.transform.position - headView.position).normalized;

                if (Vector3.Angle(direction, dirToPlayer) < FOV / 2f)
                {


                    //inside fov
                    //Debug.Log("InsideFov");

                    RaycastHit2D hitRay = Physics2D.Raycast(headView.position, dirToPlayer, FOVDistance, layerToSee);


                    if (hitRay.collider != null)
                    {
                        Debug.DrawRay(headView.position, dirToPlayer * FOVDistance, Color.magenta);


                        if (hitRay.collider.gameObject.GetComponent<PlayerController>() != null)
                        {
                            AttackPlayer();
                        }
                        else
                        {

                        }
                    }

                }
            }
        }
    }

    public void AttackPlayer()
    {
        // primeiro pega os valores do material q está no renderer
        _renderer.GetPropertyBlock(_propBlock);

        //põe os valores
        _propBlock.SetColor("_ColorMesh", colorConeDetected);

        //aplica os valores editados
        _renderer.SetPropertyBlock(_propBlock);

        Debug.Log("Game Over");

        StartCoroutine(RestartLevel());
    }

    public void Sleep()
    {
        StartCoroutine(SleepingTime());
    }
    private IEnumerator RestartLevel()
    {
        Time.timeScale = 0.6f;

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
            
        if(GameObject.FindWithTag("GameManag"))
            GameObject.FindWithTag("GameManag").GetComponent<GameManager>().GameOver();


    }

    private IEnumerator SleepingTime()
    {

        isSleeping = true;

        yield return new WaitForSeconds(sleepingTime);

        isSleeping = false;
    }
 
}



