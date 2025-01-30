using UnityEngine;

public class MovementScript : MonoBehaviour {
    //  public fields
    public float speed;
    public float rotationSpeed;

    private CharacterController characterController;

    void Start() {
        //initialize character controller
        characterController = GetComponent<CharacterController>();
        
    }

    void Update() {
        //  fields
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        //  create movement vector
        Vector3 movementDirection = new Vector3(horizontalMovement, 0, verticalMovement);

        //determine player magnitude
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        //normalize player movement (ensure moving diagonally is not faster than movement in other directions)
        movementDirection.Normalize();

        //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        characterController.SimpleMove(movementDirection * magnitude);

        //check if character is moving
        if (movementDirection != Vector3.zero) {
            //make character rotate and face direction it is moving
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}