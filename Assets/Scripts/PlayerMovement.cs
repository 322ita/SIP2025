using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum movimento
{
    running,
    wallriding,
    sliding,
    climbing
}

public class PlayerMovement : MonoBehaviour
{
    
    public movimento stateM = new movimento();
    [SerializeField] CameraController camcontroller;
    [SerializeField] Transform camera1;
    [SerializeField] bool canDash=true;
    [SerializeField] float dashForce = 20000f;
    [SerializeField] Transform climbmax;
    [SerializeField] Transform Orientation;
    [SerializeField] float playerSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float slideSpeed;
    [SerializeField] float speedVelocity = 3f;
    [SerializeField] float originalSpeed;
    [SerializeField] bool CanchangeSpeed;
    [SerializeField] float toChangeSpeed;
    [SerializeField] float onGroundSpeed;
    [SerializeField] float playerSpeedForce;
    [SerializeField] float speedMultiplier ;
    private float dist = 0.2f;
    public float GravityMultiplier=1f;
    public bool grounded;
    [SerializeField] float jumpForce = 12f;
    public float Hor = 0f, Ver = 0f;
    public Vector2 moveV;
    [SerializeField] float drag = 0.95f;
    [SerializeField] Rigidbody rb;

    bool onSlope = false;
    [SerializeField] float maxSlopeAngle = 40f;
    RaycastHit slopeHit;
    Vector3 projectedSlope;
    bool exitingSlope;
    public float exitingSlopeTime;
    float exitingSlopeTimer;

    [SerializeField] private int jumpcount=0;

    bool exitingGround;
    public float exitingGroundTime;
    float exitingGroundTimer;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] LayerMask climbLayer;
    private bool isClimb = false;
    [SerializeField] float maxClimbTime = 2f;
    public float ClimbTImer = 0f;
    [SerializeField] float wallLookAngle;
    [SerializeField] float maxWallLookAngle=30;
    
    public bool isSliding = false;
    [SerializeField] float slideDuration = 6f;
    public float SlideTimer;
    [SerializeField] float slideForce;
    
    [SerializeField] float maxRunTime = 2f;
    [SerializeField] LayerMask wallrun;
    public float wallrunTImer = 0f;
    RaycastHit hitRight;
    RaycastHit hitLeft;
    bool destra, sinistra;
    public bool isWallRiding;
    bool exitingWall;
    public float exitingWallTime;
    float exitingWallTimer;
    RaycastHit hit5;
    Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        start=transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeedForce = 10000 * (playerSpeed / 10);
        isWallRiding = (destra || sinistra) && wallrunTImer < maxRunTime && Ver > 0 && !exitingWall && !grounded;
        
        Hor = Input.GetAxisRaw("Horizontal");
        Ver = Input.GetAxisRaw("WS");
        moveV = new Vector2(Hor, Ver);

        changingSpeedHandler();

       


        if (grounded)
            onGroundSpeed = playerSpeed;

        exitingHandler();

        slope();
        slopeVector3();

        if(!isClimb)
            jumpButton();
        // if (Input.GetKeyDown(KeyCode.LeftShift)) StartCoroutine(Dash());
        // if (Ver==1)
        //     climbCheck();
        // if (isClimb)
        //     climb();
        
        // if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding && !isWallRiding && !isClimb)
        // {
        //     startSlide();
        // }
            
        // if (Input.GetKeyUp(KeyCode.LeftControl) && isSliding)
        //     stopSliding();
        
        checkForWall();


        if (grounded && !isClimb && !isSliding && !isWallRiding && !exitingGround)
            rb.linearDamping = drag;
        else rb.linearDamping = 0f;

        if (grounded)
            ClimbTImer = 0;
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(1);
        #endif
        
    }
    private void FixedUpdate()
    {
        
        groundCheck();
        if (!isClimb && !isSliding && !isWallRiding)
            movement();
        if(!isClimb && !isWallRiding)
            rb.useGravity = !onSlope;
        fixedSpeed();
        
        // if(isSliding)
        //     slide();
        // wallRun();
    }

    void exitingHandler()
    {
        if (exitingWall)
            isWallRiding = false;
        if (exitingWallTimer > 0)
            exitingWallTimer -= Time.deltaTime;
        if (exitingWallTimer <= 0)
            exitingWall = false;

        if (exitingGroundTimer > 0)
            exitingGroundTimer -= Time.deltaTime;
        if (exitingGroundTimer <= 0)
            exitingGround = false;

        if (exitingSlopeTimer > 0)
            exitingSlopeTimer -= Time.deltaTime;
        if (exitingSlopeTimer <= 0)
            exitingSlope = false;

    }

    void changingSpeedHandler()
    {
        if (CanchangeSpeed)
        {
            float differenza = Mathf.Abs(originalSpeed - toChangeSpeed);
            if (differenza <= 5)
            {
                playerSpeed = toChangeSpeed;
                CanchangeSpeed = false;
            }

            else
            {
                float lerped;
                if(!isWallRiding && !isClimb)
                {
                    lerped = Mathf.Lerp(playerSpeed, toChangeSpeed, Time.deltaTime * speedVelocity);
                    playerSpeed = lerped;
                    if (toChangeSpeed == lerped)
                        CanchangeSpeed = false;
                }
                   
            }
        }
    }
    void groundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 
            transform.localScale.y+dist, groundLayer);
        if (grounded && !isClimb)
            jumpcount = 0;
    }
    void jumpButton()
    {
        exitingSlope = true;
        exitingSlopeTimer = exitingSlopeTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpcount < 1 && !isWallRiding)
            {
                exitingGround = true;
                exitingGroundTimer = exitingGroundTime;
                if(isSliding && !grounded)
                {
                    //Debug.Log("ciao");
                }
                else rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Orientation.up * jumpForce, ForceMode.Impulse);
                jumpcount++;
            }
            //jumpWall();
        }
        
   

    }
    void movement()
    {
        
        Vector3 move = (Orientation.right * Hor + Orientation.forward * Ver).normalized;
        if(onSlope && !exitingSlope)
        { 
            rb.AddForce(projectedSlope * playerSpeedForce, ForceMode.Force);
            if (rb.linearVelocity.y > 0f)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        
        rb.AddForce(move * playerSpeedForce, ForceMode.Force);

        stateM = movimento.running;

    }
    void fixedSpeed()
    {
        if (onSlope && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > playerSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * playerSpeed;
        }
        else
        {
            Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            if (flatVelocity.magnitude > playerSpeed && grounded)
            {
                Vector3 limitVelocity = flatVelocity.normalized * playerSpeed;
                rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
            }
            else if (flatVelocity.magnitude > onGroundSpeed && !grounded)
            {
                Vector3 limitVelocity = flatVelocity.normalized * onGroundSpeed;
                rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
            }
        }
        
    }
    void changeSpeed(float speed)
    {
        toChangeSpeed = speed;
        originalSpeed = playerSpeed;
        CanchangeSpeed = true;
        
    }


    void slope()
    {
        bool slopeRaycast = Physics.Raycast(transform.position, Vector3.down, out slopeHit, transform.localScale.y + dist, groundLayer);
        if (slopeRaycast) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            onSlope = angle < maxSlopeAngle && angle !=0;

        }
        else onSlope = false;

    }
    void slopeVector3()
    {
        Vector3 direction = Orientation.forward * Ver + Orientation.right * Hor;
        projectedSlope = Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    void climbCheck()
    {
        RaycastHit hit;
        bool centro = Physics.SphereCast(transform.position, 0.3f, Orientation.forward, out hit, 1f, climbLayer);
        wallLookAngle = Vector3.Angle(Orientation.forward, -hit.normal);
        if (centro && wallLookAngle<=maxWallLookAngle && ClimbTImer < maxClimbTime)
        {
            isClimb = true;
            //rb.useGravity = false;
            jumpcount = 99;
            rb.linearVelocity = Vector3.zero;
        }
    }
    void climb()
    {
        stopSliding();
        ClimbTImer += Time.deltaTime;
        RaycastHit hit;
        bool basso = Physics.Raycast(climbmax.position, climbmax.forward, out hit, 1f, climbLayer);
        if ((!basso && isClimb) || ClimbTImer >= maxClimbTime)
        {
            stopClimb();
            return;
        }
        /*
                 Vector3 pos = new Vector3(rb.velocity.x, velocita, rb.velocity.z);
                rb.velocity = pos;
         */
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, playerSpeed, rb.linearVelocity.z);
        
        
        

        stateM = movimento.climbing;
    }
    void stopClimb()
    {
        isClimb = false;
        rb.useGravity = true;
        //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
    void startSlide()
    {
        isSliding = true;
        transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Force);
        SlideTimer = slideDuration;
        changeSpeed(slideSpeed);
    }
    void slide()
    {
        Vector3 direction = Orientation.forward * Ver + Orientation.right * Hor;
        if(!onSlope && rb.linearVelocity.y > -0.1f &&rb.linearVelocity.y < 5f)
        {
            rb.AddForce(direction * slideForce, ForceMode.Force);

            SlideTimer -= Time.deltaTime;
        }
        else if (onSlope || (!grounded && rb.linearVelocity.y < 0.1f))
        {
            rb.AddForce(projectedSlope * slideForce, ForceMode.Force);
        }

        if(SlideTimer <= 0)
            stopSliding();
        stateM = movimento.sliding;
    }
    void stopSliding()
    {
        isSliding = false;
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        changeSpeed(walkSpeed);
    }
    void checkForWall()
    {
        
        destra = Physics.Raycast(transform.position, Orientation.right, out hitRight, 1f, wallrun);
        sinistra = Physics.Raycast(transform.position, -Orientation.right, out hitLeft, 1f, wallrun);
        

    }
    void wallRun()
    {
        
        if (isWallRiding && !exitingWall) 
        {
            stopSliding();
            stateM = movimento.wallriding;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            Vector3 wallNormal = destra ? hitRight.normal : hitLeft.normal;
            Vector3 wallForward = Vector3.Cross(wallNormal, Orientation.up);
            if ((Orientation.forward - wallForward).magnitude > (Orientation.forward - -wallForward).magnitude)
                wallForward *= -1;
            wallrunTImer += Time.deltaTime;
            if (wallrunTImer >= maxRunTime)
            {
                camcontroller.tiltCamera(0f);
                isWallRiding = false;
                rb.useGravity = true;
                return;
            }
            if (destra && wallrunTImer<maxRunTime)
                camcontroller.tiltCamera(15f);
            else if (sinistra && wallrunTImer < maxRunTime)
                camcontroller.tiltCamera(-15f);
            rb.useGravity = false;
            Vector3 movement1 = wallForward * playerSpeedForce;
            rb.AddForce(movement1, ForceMode.Force);
            
        }
        else
        {
            if(grounded)
                wallrunTImer= 0f;
            if(grounded && !onSlope)
                rb.useGravity= true;
            camcontroller.tiltCamera(0f);
            
        }

        
    }
    void jumpWall()
    {
        if(isWallRiding)
        {
            wallrunTImer = 0f;
            exitingWall = true;
            exitingWallTimer = exitingWallTime;
            Vector3 wallNormal = destra ? hitRight.normal : hitLeft.normal;

            
            Vector3 jumpDir = wallNormal + Orientation.up;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
            
            isWallRiding= false;
            rb.useGravity = true;
            
        }

    }
    IEnumerator Dash()
    {
        if (!canDash)
            yield return null;
        else
        {
            canDash= false;
            exitingGround = true;
            exitingGroundTimer = exitingGroundTime;
            rb.AddForce(Vector3.down * dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(5);
            canDash= true;
        }
    }
}

