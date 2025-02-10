using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementScript : MonoBehaviour {
    //  public fields
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;

    private CharacterController cc;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [SerializeField]
    private Transform cameraTransform;

    void Start() {
        //initialize character controller
        cc = GetComponent<CharacterController>();
        originalStepOffset = cc.stepOffset;
    }

    void Update() {
        //  player input
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        //  create movement vector (determine movement)
        Vector3 movementDirection = new Vector3(horizontalMovement, 0, verticalMovement);

        // normalize player movement (ensure moving diagonally is not faster than movement in other directions)
        movementDirection.Normalize();

        //  player direction sync with camera direction
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        //  set up gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        //  check last time grounded
        if (cc.isGrounded) {
            lastGroundedTime = Time.time;
        }

        //  check last time jump button was pressed
        if (Input.GetButtonDown("Jump")) {
            jumpButtonPressedTime = Time.time;
        }

        //  check if player is on the ground within grace period
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            //no gravity when on the ground
            ySpeed = -0.5f;

            //  reset step offset while on ground
            cc.stepOffset = originalStepOffset;

            //  check if jumping within grace period
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod) {
                ySpeed = jumpSpeed;

                //reset nullable fields
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        //  set step offset to 0 while in the air to stop player from pausing when climbing
        else {
            cc.stepOffset = 0;
        }

        //  determine player magnitude
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        cc.Move(velocity * Time.deltaTime);

        //  check if character is moving
        if (movementDirection != Vector3.zero) {
            //make character rotate and face direction it is moving
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void OnApplicationFocus(bool focus) {
    //  hide mouse cursor when window is focused
        if (focus) {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        else {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }

    //Used to detect if the player is colliding with the air current object
    private void OnTriggerStay(Collider other)
    {
        //checks if the collider has the tag AirCurrent
        if(other.tag == "AirCurrent")
        {
            
            ySpeed += 0.4f;
        }
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
    