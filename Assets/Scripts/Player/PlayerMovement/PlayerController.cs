using UnityEngine;
using UnityEngine.InputSystem;
using Hellmade.Sound;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Player velocity")]
    public float movementSpeed;
    public float climbSpeed, slopeSpeedReduction, slowSpeed;
    [Space(10)]

    [Header("Raycast related")]
    public float slopeCheckDistance;
    public float slopeCheckDistanceHorizontal, groundCheckRadius, climbCheckDistance, ledgeCheckDistance, pullPushRay;
    [Space(10)]

    [Header("Jump related")]
    public float jumpForce;
    public float jumpPressedTimer = 0.2f, groundedTimer = 0.15f, fallMultiplier = 2.5f, lowJumpMultiplier = 8f, gravity;
    [Space(10)]

    [Header("Slope related")]
    public float maxSlopeAngle;
    public float slopeRotationSpeed;
    [Space(10)]

    [Header("Climb related")]
    public float ladderScapeX;
    public float ladderScapeY, ledgeClimbXOff1, ledgeClimbXOff2, ledgeClimbYOff1, ledgeClimbYOff2;
    [Space(10)]

    [Header("Transform checks")]
    public Transform groundCheck;
    public Transform ledgeCheck, chestCheck;
    [Space(10)]

    [Header("Layers")]
    public LayerMask whatIsGround;
    public LayerMask whatIsLadder, whatIsClimbable, whatIsSlope, whatIsMovable;
    [Space(10)]

    [Header("Materials")]
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;
    [Space(10)]

    [Header("States")]
    public bool isGrounded;
    public bool isClimbing, isOnSlope, isOnWater, isOnMovable, isOnWall, isTrupeHelping, isCrounch, interacting, isHanging = false;
    [Space(10)]

    [Header("Animation related")]
    public Transform spritePlayer;
    public Animator playerAnim;
    public GameObject footstepFX;
    public float footstepRadiusRun, footsteepRadiusCrounch;
    public bool isShooting;
    [Space(10)]

    [Header("Blowpipe related")]
    public Player_Zarabatana zarabatana;
    [Space(10)]

    [Header("Post Processing related")]
    public Volume shiftPP;
    public AnimationCurve blendCurve;
    public float blendTime, fovAlpha;
    private bool isBlending;
    [Space(10)]

    [Header("Audio related")]
    public PlayerAudio audioPlayer;
    public BGAudioPlayer bgPlayer;


    private float xInput, yInput, moveSpeed;
    private float slopeDownAngle, slopeSideAngle;
    private float lastSlopeAngle;
    private float grounded, jumpPressed;


    private bool facingRight = true, canFlip = true;
    private bool canJump = false, isJumpButtonDown, jumpNow = false;
    private bool canWalkOnSlope;
    private bool ledgeDetected = false, canClimbLedge = false, isClimbingLedge = false;

    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;
    private Vector2 cornerPos1, cornerPos2, ledgeBotPos;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Player_Input playerInput;
    [HideInInspector]
    public GameObject currentMovable;
    private TargetJoint2D targetJoint;
    private TrupeController trupeC;


    private void Awake()
    {
        playerInput = new Player_Input();

        playerInput.Player.MovementHorizontal.performed += ctx => xInput = Mathf.Round(ctx.ReadValue<float>());
        playerInput.Player.MovementHorizontal.canceled += ctx => xInput = ctx.ReadValue<float>();

        playerInput.Player.MovementVertical.performed += ctx => yInput = Mathf.Round(ctx.ReadValue<float>());
        playerInput.Player.MovementVertical.canceled += ctx => yInput = ctx.ReadValue<float>();

        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.Jump.canceled += OnJump;

        playerInput.Player.Interact.performed += TryToPull;
        playerInput.Player.Interact.canceled += TryToPull;

        playerInput.Player.Crounch.performed += Crounch;
        playerInput.Player.Crounch.canceled += Crounch;


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
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        capsuleColliderSize = cc.size;
        moveSpeed = movementSpeed;

        trupeC = GameObject.FindWithTag("TrupeController").GetComponent<TrupeController>();
    }

    private void Update()
    {
        CheckInput();

        // ANIMATOR CONTROLLER //

        playerAnim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        playerAnim.SetFloat("yVelocity", rb.velocity.y);
        playerAnim.SetFloat("yInput", yInput);

        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetBool("isCrounch", isCrounch);
        playerAnim.SetBool("jumpButton", isJumpButtonDown);
        playerAnim.SetBool("isHanging", isHanging);
        playerAnim.SetBool("isClimbingLedge", isClimbingLedge);
        playerAnim.SetBool("isShooting", isShooting);
        playerAnim.SetBool("isClimbingRope", isClimbing);

        // ANIMATOR CONTROLLER //


        // HOLD JUMP//

        if (rb.velocity.y < 0)
            {

                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            }
            else if (rb.velocity.y > 0 && !isJumpButtonDown)
            {

                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            // HOLD JUMP //

            if (jumpPressed > 0)
                jumpPressed -= Time.deltaTime;

            if (!isGrounded)
                grounded -= Time.deltaTime;

            if (isGrounded)
                grounded = groundedTimer;

            if ((grounded >= 0) && (jumpPressed > 0) && canWalkOnSlope && !isClimbing && !canClimbLedge)
            {
            jumpNow = true;
                jumpPressed = 0;
                grounded = 0;
            }


        /*//Rotaciona sprite no slope
        if (isGrounded)
        {

            spriteTransform.rotation = Quaternion.RotateTowards(spriteTransform.rotation,Quaternion.Euler( new Vector3(0,0,(slopeDownAngle > 25) ? slopeDownAngle * -1 : slopeDownAngle * 1)), slopeRotationSpeed* Time.deltaTime);
        }
        else
        {
            spriteTransform.rotation = Quaternion.RotateTowards(spriteTransform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), slopeRotationSpeed * Time.deltaTime);

        }*/

        /*if (!isClimbingLedge && spritePlayer.localPosition != Vector3.zero)
        {
            spritePlayer.localPosition = Vector3.zero;
        }*/
    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        ApplyMovement();
        WallLedgeCheck();
        Jump();
        PullPushCheck();
        BlendPP();
    }
    private void BlendPP()
    {
        if (isBlending)
        {

            shiftPP.weight = blendCurve.Evaluate(blendTime);

            GameObject[] fovFound = GameObject.FindGameObjectsWithTag("FOV");

            foreach(GameObject fov in fovFound) {

                fov.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", blendCurve.Evaluate(blendTime / fovAlpha));
            }

            if (isCrounch && isGrounded)
            {
                // aumenta blend
                if (blendTime <= 1)
                {
                    blendTime += Time.deltaTime;
                }
                else
                {
                    isBlending = false;
                }

            } else
            {
                if (blendTime > 0)
                {
                    // diminue blend
                    if (blendTime >= 0)
                    {
                        blendTime -= Time.deltaTime;
                    }
                    else
                    {
                        isBlending = false;
                    }
                }
            }
        } else if(isCrounch && !isGrounded)
        {
            isCrounch = false;
            moveSpeed = movementSpeed;
            isBlending = true;
            audioPlayer.SnapshotTransition(isCrounch);
            bgPlayer.BackgroundMusicTransition(isCrounch);
        }
    }
    private void Crounch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (isGrounded)
            {
                isCrounch = true;
                moveSpeed = slowSpeed;
                isBlending = true;
                audioPlayer.SnapshotTransition(isCrounch);
                bgPlayer.BackgroundMusicTransition(isCrounch);
            }
        }
        else if (context.canceled)
        {
            isCrounch = false;
            moveSpeed = movementSpeed;
            isBlending = true;
            audioPlayer.SnapshotTransition(isCrounch);
            bgPlayer.BackgroundMusicTransition(isCrounch);
        }
    }

    private void TryToPull(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                interacting = true;
                if (currentMovable != null)
                {
                    canFlip = false;
                }
            }
        }

        if (context.canceled)
        {
            interacting = false;
            canFlip = true;
        }
    }

    private void PullPushCheck()
    {
        if (!isOnWater && !canClimbLedge)
        {
            RaycastHit2D hit = Physics2D.Raycast(chestCheck.position, Vector2.right * (facingRight ? 1 : -1), pullPushRay, whatIsMovable);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Movable") || hit.collider.gameObject.CompareTag("Boat"))
                {

                    Debug.DrawRay(chestCheck.position, hit.transform.position, Color.cyan);


                    currentMovable = hit.collider.gameObject;
                    targetJoint = currentMovable.GetComponent<TargetJoint2D>();
                    //hit.rigidbody.freezeRotation = true;

                    playerAnim.SetBool("isPushing", true);

                    if (!canFlip)
                    {
                        if ((xInput > 0 ? 1 : -1) == (facingRight ? -1 : 1) && xInput != 0)
                        {
                            if (isTrupeHelping && !targetJoint.enabled)
                            {
                                float currentForce = targetJoint.maxForce;
                                Rigidbody2D movableRB = currentMovable.GetComponent<Rigidbody2D>();
                                targetJoint.maxForce = (currentForce * movableRB.mass) / (movableRB.mass * 0.2666f);
                            }

                            playerAnim.SetBool("isPulling", true);

                            targetJoint.enabled = true;
                            targetJoint.target = chestCheck.position;
                            moveSpeed = slowSpeed;
                            currentMovable.GetComponent<Rigidbody2D>().freezeRotation = true;

                        }
                        else
                        {
                            //canFlip = true;
                            targetJoint.enabled = false;
                            currentMovable.GetComponent<Rigidbody2D>().freezeRotation = false;
                            moveSpeed = movementSpeed;
                            playerAnim.SetBool("isPulling", false);

                        }

                    }

                }
            }
            else
            {
                if (currentMovable != null)
                {
                    targetJoint.maxForce = 800;
                    currentMovable.GetComponent<Rigidbody2D>().freezeRotation = false;
                    targetJoint.enabled = false;
                    currentMovable = null;
                    canFlip = true;
                    moveSpeed = movementSpeed;
                    playerAnim.SetBool("isPushing", false);
                    playerAnim.SetBool("isPulling", false);

                }
            }
        }
    }

    private void WallLedgeCheck()
    {

        if (!canClimbLedge)
        {
            int facinDir = facingRight ? 1 : -1;


            RaycastHit2D ledgeHit = Physics2D.Raycast(ledgeCheck.position, Vector2.right * facinDir, ledgeCheckDistance * 1.5f, whatIsClimbable);
            RaycastHit2D wallHit = Physics2D.Raycast(chestCheck.position, Vector2.right * facinDir, ledgeCheckDistance, whatIsClimbable);


            if (wallHit.collider != null && ledgeHit.collider == null && !ledgeDetected && !isClimbing && !isGrounded)
            {
                if (!wallHit.collider.gameObject.CompareTag("Boat") && !isOnWater)
                {
                ledgeDetected = true;
                ledgeBotPos = transform.position;
                }
            }
        }

        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (facingRight)
            {
                //cornerPos1 = new Vector2(Mathf.Floor(ledgeBotPos.x - ledgeCheckDistance) - ledgeClimbXOff1, Mathf.Floor(ledgeBotPos.y) + ledgeClimbYOff1);
                //cornerPos2 = new Vector2(Mathf.Ceil(ledgeBotPos.x + ledgeCheckDistance) + ledgeClimbXOff2, Mathf.Floor(ledgeBotPos.y) + ledgeClimbYOff2);
                
                cornerPos1 = new Vector2(ledgeBotPos.x + ledgeCheckDistance - ledgeClimbXOff1, ledgeBotPos.y + ledgeClimbYOff1);
                cornerPos2 = new Vector2(ledgeBotPos.x + ledgeCheckDistance + ledgeClimbXOff2, ledgeBotPos.y + ledgeClimbYOff2);

            }
            else
            {
                //cornerPos1 = new Vector2(Mathf.Ceil(ledgeBotPos.x - ledgeCheckDistance) + ledgeClimbXOff1, Mathf.Floor(ledgeBotPos.y) + ledgeClimbYOff1);
                //cornerPos2 = new Vector2(Mathf.Ceil(ledgeBotPos.x - ledgeCheckDistance) - ledgeClimbXOff2, Mathf.Floor(ledgeBotPos.y) + ledgeClimbYOff2);

                cornerPos1 = new Vector2(ledgeBotPos.x - ledgeCheckDistance + ledgeClimbXOff1, ledgeBotPos.y + ledgeClimbYOff1);
                cornerPos2 = new Vector2(ledgeBotPos.x - ledgeCheckDistance - ledgeClimbXOff2, ledgeBotPos.y + ledgeClimbYOff2);
            }
        }

        if (canClimbLedge)
        {
            isHanging = true;

            transform.position = cornerPos1;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            if(yInput > 0 && isHanging)
            {
                rb.velocity = Vector2.zero;
                isClimbingLedge = true;
                //FinishLedgeClimb();
            }
            else if(yInput < 0 && isHanging)
            {
                rb.velocity = Vector2.zero;

                DropLedgeClimb();
            }
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        ledgeDetected = false;
        isHanging = false;
        isClimbingLedge = false;

        transform.position = cornerPos2;
        rb.gravityScale = gravity;
        //spritePlayer.localPosition = Vector3.zero;
    }

    public void DropLedgeClimb()
    {
        canClimbLedge = false;
        ledgeDetected = false;
        isHanging = false;
        isClimbingLedge = false;

        spritePlayer.localPosition = Vector3.zero;
        rb.gravityScale = gravity;
        transform.position = ledgeBotPos;
    }

    private void Jump()
    {

        if (canJump)
        {
            if (jumpNow)
            {
                rb.velocity = Vector2.zero;
                isGrounded = false;
                rb.AddForce(new Vector2(0f, jumpForce));
                canJump = false;
                jumpPressed = 0;
                grounded = 0;
            }
        }

        jumpNow = false;

    }

    private void CheckInput()
    {

        if (xInput > 0 && !facingRight && !canClimbLedge)
        {
            Flip();
        }
        else if (xInput < 0 && facingRight && !canClimbLedge)
        {
            Flip();
        }

    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnMovable = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsMovable);

        if (isGrounded && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }

        if (!isGrounded)
        {
            slopeCheckDistanceHorizontal = slopeCheckDistance;
        }
        else
        {
            slopeCheckDistanceHorizontal = 0.3f;
        }
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
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistanceHorizontal, whatIsSlope);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistanceHorizontal, whatIsSlope);

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

            if(slopeDownAngle != lastSlopeAngle)
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

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
            cc.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
            cc.sharedMaterial = noFriction;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isJumpButtonDown)
                jumpPressed = jumpPressedTimer;

            isJumpButtonDown = true;
        }

        if (context.canceled)
            isJumpButtonDown = false;
    }   

    private void ApplyMovement()
    {


        if (isGrounded && !isOnSlope && !canClimbLedge) //if not on slope
        {
            rb.velocity = new Vector2(moveSpeed* xInput, 0.0f);
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !canClimbLedge) //If on slope
        {
            rb.velocity = new Vector2((moveSpeed / slopeSpeedReduction)  * slopeNormalPerp.x * -xInput, (movementSpeed / slopeSpeedReduction) * slopeNormalPerp.y * -xInput);
        }
        else if (!isGrounded && !canClimbLedge && canWalkOnSlope) //If in air (tirar caso nao querer aircontrol)
        { 
            rb.velocity = new Vector2(moveSpeed * xInput, rb.velocity.y);
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

    public void ApplyFootstep()
    {
        GameObject footstepGO = Instantiate(footstepFX, groundCheck.position, Quaternion.identity);
        ParticleSystem ps = footstepGO.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule pMain = ps.main;

        if (isCrounch)
        {
            //radius crounch
            pMain.startSize = footsteepRadiusCrounch;
        }
        else
        {
            //radius run
            pMain.startSize = footstepRadiusRun;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrupeToCollect"))
        {
            if (collision.gameObject.activeSelf)
            {
                collision.gameObject.SetActive(false);
                trupeC.AddMember(collision.transform.position);

                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("DartToCollect"))
        {
            zarabatana.dartCount += 5;
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("MinionTP"))
        {
            if (interacting)
            {
                Debug.Log("TP minion");
                trupeC.TeleportMinion(collision.transform);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MinionTP"))
        {
            if (interacting)
            {
                Debug.Log("TP minion");
                trupeC.TeleportMinion(collision.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        int facinDir = facingRight ? 1 : -1;

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawRay(ledgeCheck.position, Vector2.right * facinDir * ledgeCheckDistance * 1.5f);
        Gizmos.DrawRay(chestCheck.position, Vector2.right * facinDir * ledgeCheckDistance);
    }

}

