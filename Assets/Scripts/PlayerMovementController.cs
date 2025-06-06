using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour {
    //  public fields
    [Header("Player Movement")]
    public float walkSpeed;
    public float glideSpeed;
    public float rotationSpeed;
    public float jumpSpeed;

    public GameObject modelMain;
    public GameObject modelGlide;
    private GameObject modelCurrent;
    
    [Header("Player Gravity")]
    public float defaultGravityScale;
    public float glidingGravityScale;

    [Header("Player Action Timing")]
    public float jumpButtonGracePeriod;
    public float timeInAirBeforeGlide;

    [Header("Testing Variables")]

    //  private fields
    private CharacterController cc;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private float magnitude;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float timeInAir;
    private bool isJumping = false;
    private bool isGliding = false;

    [SerializeField]
    private Transform cameraTransform;

    void Start() {
        //  initialize character controller
        cc = GetComponent<CharacterController>();
        originalStepOffset = cc.stepOffset;

        modelCurrent = Instantiate(modelMain, transform.position, transform.rotation) as GameObject;
        modelCurrent.transform.parent = transform;

       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        ChangeModel();
        //  player input
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        //  create movement vector
        moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);

        // normalize player movement
        moveDirection.Normalize();

        //  player direction sync with camera direction
        moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;

        //  gravity
        ySpeed += Physics.gravity.y * Time.deltaTime * DecideScalar(isGliding, glidingGravityScale, defaultGravityScale);

        //  check last time grounded
        if (cc.isGrounded) {
            PlayerGrounded();
        }

        //  player jump
        if (Input.GetButtonDown("Jump")) {
            PlayerJump();
        }

        //  player glide start
        if (isJumping == true && CanGlide() == true && Input.GetButtonDown("Jump")) {
            PlayerGlide();
        }

        //  player glide cancel
        if (isGliding == true && Input.GetButtonDown("Cancel")) {
            isGliding = false;
        }

        //  check how long player has been in the air
        if (!cc.isGrounded) {
            timeInAir += Time.deltaTime;
        }
        
        //  define magnitude
        magnitude = Mathf.Clamp01(moveDirection.magnitude) * DecideScalar(isGliding, glideSpeed, walkSpeed);

        //  define velocity
        velocity = moveDirection * magnitude;
        velocity.y = ySpeed;

        //  move with character controller
        cc.Move(velocity * Time.deltaTime);

        //  check if character is moving then make character rotate and face direction it is moving
        if (moveDirection != Vector3.zero) {
            PlayerRotation();
        }

    }

    //----------JUMPING----------
    private void PlayerGrounded() {
        lastGroundedTime = Time.time;
        isJumping = false;
        isGliding = false;
        timeInAir = 0f;

        //no gravity when on the ground
        ySpeed = -0.5f;
    }

    private void PlayerJump() {
        jumpButtonPressedTime = Time.time;

        //  check if player is on the ground within grace period
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            
            //  reset step offset while on ground
            cc.stepOffset = originalStepOffset;

            //  check if jumping within grace period
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod) {
                ySpeed = jumpSpeed;
                isJumping = true;

                //reset nullable fields
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        //  set step offset to 0 while in the air to stop player from pausing when climbing
        else {
            cc.stepOffset = 0;
        }
    }


    //----------GLIDING----------
    //  check if player is able to glide
    public bool CanGlide() {
        if (timeInAir < timeInAirBeforeGlide) {
            return false;
        }

        else {
            return true;
        }
    }

    //  activate player glide
    public void PlayerGlide() {
        isGliding = true;
        isJumping = false;
    }


    //----------ROTATION----------
    //  make character rotate and face direction it is moving
    public void PlayerRotation() {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }


    //----------DECIDE SCALAR----------
    //  decideScalar returns one of two floats arguments depending on the boolean value of 'input'
    private float DecideScalar(bool input, float trueValue, float falseValue) {
        if (input == true) {
            return trueValue;
        }

        else {
            return falseValue;
        }
    }


    //----------AIR CURRENT COLLISION----------
    //Used to detect if the player is colliding with the air current object
    private void OnTriggerStay(Collider other) {
        //checks if the collider has the tag AirCurrent
        if(other.CompareTag("AirCurrent")) {
            ySpeed += 0.4f;
        }
    }

     //----------Goal Scene Transition----------------------
     private void OnTriggerEnter(Collider other){
        if(other.CompareTag("LevelExit")){
            SceneManager.LoadScene(2);
            Debug.Log(SceneManager.GetActiveScene().name);
        } 
    }

    public void ChangeModel(){
        GameObject tModel = modelCurrent;
        if(this.isGliding){
            tModel = Instantiate(modelGlide, transform.position, transform.rotation) as GameObject;
        }else{
            tModel = Instantiate(modelMain, transform.position, transform.rotation) as GameObject;
        }
        Destroy(modelCurrent);
        tModel.transform.parent = transform;
        modelCurrent = tModel;
    }


}

/*
WHAT WILL NEEDED TO BE ADDED/CHANGED WHEN ATTATCHING SCRIPT TO NEW PLAYER OBJECT

- adding character controller
    - change collision mask size
- setting local variables
- adding freemove cinemachine camera
    - change camera focus
    - change what camera follows
    - rig height and radius depending on the model height
*/