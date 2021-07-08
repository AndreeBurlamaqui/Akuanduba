using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrupeIA : MonoBehaviour
{

    public LayerMask whatIsGround, whatIsWall, whatIsAfraid, whatIsSlope, whatIsMovable;
    public Transform groundCheck, groundAheadCheck, chestCheck, arcHandler;
    public float moveSpeed, jumpHeight, jumpLong, jumpTargetDistance, jumpCooldownMax, jumpMultiplier, jumpExtraX, boatJumpDistance, boatJumpHeight;
    public Transform player;
    public float playerDistance;
    public float groundCheckRadius, groundRayDist, wallRayDist, movableRayDist, slopeCheckDistance, waterCheckDistance;
    public Vector2 playerDirection;
    public float xDir;
    public float toleranceMovable;

    [Header("Materials")]
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;
    [Space(10)]

    [Header("Slope related")]
    public float maxSlopeAngle;
    public float slopeRotationSpeed;
    private float slopeDownAngle, slopeSideAngle;
    private float lastSlopeAngle;
    [Space(10)]

    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;

    [SerializeField]
    public bool isGrounded;
    public bool groundAhead, isHelping, isOnSlope, canWalkOnSlope, isOnBoat;
    [Space(10)]

    private bool facingRight = true, canFlip = true, helpNow;
    public Vector2 startPos, targetPos, targetArc;
    public float jumpCooldown, startGravity;
    public bool isJumping = false, reachedPeak = false, wallJump = false, nearWater = false, movableOnSight = false, goBackwards = false, seeingWall;
    private float timeX = 0f, timeY = 0f, distFromPlayer;
    private PlayerController playerScript;
    private TrupeController parentController;
    private GameObject doorToOpen;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        capsuleColliderSize = cc.size;


        startGravity = rb.gravityScale;
        jumpCooldown = jumpCooldownMax;

        player = GameObject.FindWithTag("Player").transform;
        playerScript = player.GetComponent<PlayerController>();
        parentController = transform.parent.GetComponent<TrupeController>();

        distFromPlayer = playerDistance;
    }

    void Update()
    {
        if (isJumping && jumpCooldown <= 0)
        {

            /*if (wallJump)
            {

                if (timeY > 1)
                {
                    rb.gravityScale = startGravity;
                    Debug.Log("End TimeY");

                    transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPos.x, timeX / jumpLong), rb.velocity.y);


                }
                else
                {
                    rb.gravityScale = 0;
                    transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPos.x, timeX / jumpLong), Mathf.Lerp(transform.position.y, targetArc.y, timeY / jumpLong));
                    
                    timeY += Time.deltaTime;

                }

                if (timeX < 1)
                {
                    //transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPos.x, timeX / jumpLong), Mathf.Lerp(transform.position.y, targetArc.y, timeY / jumpLong));

                    timeX += Time.deltaTime;

                } else
                {
                    Debug.Log("End TimeX");
                    isJumping = false;
                    wallJump = false;
                    reachedPeak = false;
                    timeX = 0f;
                    timeY = 0f;

                    jumpCooldown = jumpCooldownMax;
                    rb.sharedMaterial = noFriction;
                    cc.sharedMaterial = noFriction;
                }
            }*/

            if(!movableOnSight)
            {

                Debug.Log("Jump");
                rb.AddForce(new Vector2(jumpLong * xDir, jumpHeight * jumpMultiplier), ForceMode2D.Impulse);

                isJumping = false;
                jumpCooldown = jumpCooldownMax;


            }
            else
            {
                rb.AddForce(new Vector2(boatJumpDistance * xDir, boatJumpHeight), ForceMode2D.Impulse);

                isJumping = false;
                jumpCooldown = jumpCooldownMax;
                rb.sharedMaterial = fullFriction;
                cc.sharedMaterial = fullFriction;
                Debug.Log("Jump on boat");

            }


        }

        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
    }

    public void FollowPlayer()
    {
        CheckGround();
        SlopeCheck();
        HelpCheck();
    }

    private void HelpCheck()
    {
        if(playerScript.currentMovable != null)
        {
            if (playerScript.currentMovable.CompareTag("Movable"))
            {
                isHelping = true;
                ApplyMovement();
            }
        }
        else
        {
            if(isHelping)
                parentController.DisableHelp();


            isHelping = false;
            helpNow = false;
        }
    }

    public void ApplyMovement()
    {
        if(isGrounded && groundAhead && !nearWater && !isHelping)
        {

            //Debug.Log(playerDirection);

            if (!isJumping && !movableOnSight && !goBackwards)
            {
                if (isGrounded && !isOnSlope) //if not on slope
                {
                    rb.velocity = new Vector2(moveSpeed * xDir, 0.0f);
                }
                else if (isGrounded && isOnSlope && canWalkOnSlope) //If on slope
                {
                    rb.velocity = new Vector2(moveSpeed * slopeNormalPerp.x * -xDir, moveSpeed * slopeNormalPerp.y * -xDir);

                }
                /*else if (!isGrounded && canWalkOnSlope) //If in air (tirar caso nao querer aircontrol)
                {
                    rb.velocity = new Vector2(moveSpeed * playerDirection.x, rb.velocity.y);
                    //Debug.Log("On Air");

                }*/
            }
            /*else if(!nearWater)
            {
                rb.velocity = new Vector2(moveSpeed * playerDirection.x, rb.velocity.y);
                Debug.Log("GoinFoward");
            }*/

            if (goBackwards)
            {
                rb.velocity = new Vector2(moveSpeed * -xDir, 0.0f);
                rb.sharedMaterial = noFriction;
                cc.sharedMaterial = noFriction;
                Debug.Log("GoingBackwards");
            }
        }

        if (isHelping)
        {

            if (Mathf.Abs((transform.position - player.position).x) < 0.5f && !helpNow)
            {
                parentController.EnableHelp();
                helpNow = true;
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed * xDir, rb.velocity.y);
            }
        }
    }

    public void Jump()
    {

        rb.velocity = Vector2.zero;
        rb.sharedMaterial = fullFriction;
        cc.sharedMaterial = fullFriction;

        if (!movableOnSight)
        {
            /*RaycastHit2D hit = Physics2D.Raycast(chestCheck.position, transform.right * xDir, jumpTargetDistance, whatIsWall);

            if (hit.collider != null)
            {
                //Arrumar pra detectar se o player está acima, diferenciando o walljump do jump normal
                //pq o walljump tem valor maior no height doq distance
                wallJump = true;
                targetPos = new Vector2(hit.point.x + jumpExtraX, hit.point.y);
                startPos = transform.position;
                targetArc = ((targetPos + startPos) / 2) + (Vector2.up * jumpHeight);
                isJumping = true;

                Debug.Log("Extra jump " + hit.collider.name);
            }
            else
            {
                if (!nearWater)
                {
                    wallJump = false;
                    isJumping = true;

                }
                //targetPos = transform.position + (transform.right * (facingRight ? 1 : -1)) * jumpTargetDistance;
            }*/

            if (!nearWater)
            {
                wallJump = false;
                isJumping = true;

            }
        }
        else if (playerScript.isOnMovable && jumpCooldown <= 0 && isGrounded)
        {

            isJumping = true;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        groundAhead = Physics2D.OverlapCircle(groundAheadCheck.position, groundRayDist, whatIsGround);
        RaycastHit2D hit = Physics2D.Raycast(groundAheadCheck.position, Vector2.down, waterCheckDistance);
        RaycastHit2D hitMovable = Physics2D.Raycast(transform.position, Vector2.right * (facingRight ? 1 : -1), movableRayDist, whatIsMovable);
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, Vector2.right * (facingRight ? 1 : -1), wallRayDist, whatIsWall);
        Debug.DrawRay(transform.position, Vector2.right * xDir * wallRayDist, Color.magenta);

        playerDirection = (player.position - transform.position).normalized;
        xDir = Mathf.Round(playerDirection.x);

        if(hitWall.collider != null )
        {
            if (!isOnBoat && !movableOnSight)
            {
                if (FindWhoIsAbove(transform, player))
                {
                    //player em cima

                    if (isGrounded && jumpCooldown <= 0 && !isJumping)
                    {
                        Debug.Log("player em cima agr pula corno");
                        seeingWall = true;
                        Jump();

                    }
                }
                else
                {
                    //player em baixo
                    Debug.Log("player em baixo da paredee");

                    ApplyMovement();
                }
            }
        }
        else
        {
            if (!isJumping && seeingWall)
                seeingWall = false;
        }

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Water"))
            {
                nearWater = true;
            }
            else if(nearWater)
            {
                nearWater = false;
            }

            if (hit.collider.CompareTag("Boat"))
            {
                isOnBoat = true;
            }
            else if(isOnBoat)
            {
                isOnBoat = false;
            }
        }
        

        if(xDir > 0 && !facingRight)
        {
            Flip();
        }
        if (xDir < 0 && facingRight)
        {
            Flip();
        }

        if (isGrounded && slopeDownAngle <= maxSlopeAngle && !isHelping)
        {
            if (!groundAhead && !movableOnSight)
            {
                rb.velocity = Vector2.zero;
                rb.sharedMaterial = fullFriction;
                cc.sharedMaterial = fullFriction;

                if (Vector2.Distance(transform.position, player.position) > playerDistance * 1.7f)
                {
                    if (Mathf.Abs((transform.position - player.position).x) >= playerDistance * 1.7f && jumpCooldown <= 0 && !isJumping && !movableOnSight && !isOnBoat)
                    {
                        if (playerScript.isGrounded)
                        {
                            Jump();
                        }
                        else
                        {
                            rb.velocity = Vector2.zero;
                            rb.sharedMaterial = fullFriction;
                            cc.sharedMaterial = fullFriction;
                        }
                    }

                    /*if(Mathf.Abs((transform.position - player.position).y) >= playerDistance)
                    {
                        ApplyMovement();
                    }*/


                    //If the result is positive, B is above A.
                    //If the result is negative, B is below A.
                    if (!movableOnSight && !isOnBoat)
                    {
                        if (FindWhoIsAbove(transform, player))
                        {
                            Debug.Log("player em baixo");

                            ApplyMovement();
                            rb.AddForce(Vector2.right * (facingRight ? 1 : -1) * 2f, ForceMode2D.Impulse);

                        }
                        else
                        {
                            Debug.Log("player em cima");
                            if (Mathf.Abs((transform.position - player.position).x) >= playerDistance * 1.7f && jumpCooldown <= 0 && !isJumping)
                            {
                                Jump();
                            }
                        }
                    }
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    rb.sharedMaterial = fullFriction;
                    cc.sharedMaterial = fullFriction;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, player.position) > distFromPlayer)
                {
                    ApplyMovement();
                }
                else if(!isHelping)
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }

        if (nearWater)
        {
            rb.velocity = Vector2.zero;
            rb.sharedMaterial = fullFriction;
            cc.sharedMaterial = fullFriction;
        }

        if (hitMovable.collider != null)
        {
           if (hitMovable.collider.CompareTag("Boat"))
            {
                if (!isOnBoat)
                {
                    Debug.Log("going movable");

                    if (Mathf.Abs(hitMovable.distance - movableRayDist) <= toleranceMovable && !movableOnSight)
                    {
                        movableOnSight = true;
                        //goBackwards = false;
                        Jump();
                    }
                    else
                    {
                        Debug.Log(hitMovable.distance + " / " + movableRayDist);
                        goBackwards = true;
                    }
                } 

            }


        } else if (movableOnSight && isGrounded && jumpCooldown <= 0f)
        {
            movableOnSight = false;
            Debug.Log("done movable");
        }


        if (isOnBoat)
        {
            if (isGrounded && playerScript.isOnSlope && jumpCooldown <= 0f && !playerScript.isOnWater)
            {
                Debug.Log("Imout");
                rb.AddForce(new Vector2(boatJumpDistance * xDir, boatJumpHeight), ForceMode2D.Impulse);
                jumpCooldown = jumpCooldownMax;
                goBackwards = false;
            }
        }

        if (Vector2.Distance(transform.position, player.position) > playerDistance * 2f)
        {
            goBackwards = false;
        }

    }
    //If the result is positive, B is above A.
    //If the result is negative, B is below A.
    public static Transform FindWhoIsAbove(Transform a, Transform b)
    {
        return (Vector3.Dot(b.position - a.position, Vector2.up) <= 0) ? a : b;
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));
        //Vector2 checkPos = groundCheck.position;

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsSlope);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsSlope);

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
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsSlope);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
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

        if (isOnSlope && canWalkOnSlope && Vector2.Distance(transform.position, player.position) <= distFromPlayer)
        {
            rb.sharedMaterial = fullFriction;
            cc.sharedMaterial = fullFriction;
        }
        else if(!movableOnSight && groundAhead)
        {
            rb.sharedMaterial = noFriction;
            cc.sharedMaterial = noFriction;
        }
    }

    private void Flip()
    {
        if (canFlip)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void StopFollow()
    {
        if (isGrounded)
        {
            if (rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                rb.sharedMaterial = fullFriction;
                cc.sharedMaterial = fullFriction;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            //StartAnimation
            doorToOpen = collision.gameObject;

            parentController.OpenDoor(doorToOpen ,doorToOpen.GetComponent<DoorMinionNeed>().howManyMinions);

        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundAheadCheck.position, groundRayDist);
        Gizmos.DrawRay(groundAheadCheck.position, Vector2.down * waterCheckDistance);
        //Gizmos.DrawRay(chestCheck.position, transform.right * xDir * jumpTargetDistance);
        Gizmos.DrawRay(transform.position, Vector2.right * xDir  * movableRayDist);



    }
}
