using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float movementSpeed = 5f;
    public float gravity = -9.81f;

    public Transform player;
    public CharacterController playerController;
    public NavMeshAgent playerAgent;

    private float xRotation = 0f;

    private Vector3 velocity;

    public Vector3 newPositionDebug;

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    float xInput;
    float zInput;
    float mouseX;
    float mouseY;
    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        player.Rotate(Vector3.up * mouseX);

        // Vector3 newPosition = transform.right * x + transform.forward * z;
        Vector3 newRight = (transform.right * xInput);
        Vector3 newForward = (transform.forward * zInput);
        Vector3 newPosition = newRight + newForward;
        newPositionDebug = newPosition;

        playerAgent.velocity = (newPosition.normalized * movementSpeed * Time.deltaTime);
        // playerAgent.SetDestination(player.position + newPosition);

        // playerController.Move(newPosition * movementSpeed * Time.deltaTime);

        // velocity.y += gravity * Time.deltaTime;

        // playerController.Move(velocity * Time.deltaTime);
    }

    public float debugXRotation;

    private void LateUpdate() {
        transform.RotateAround(player.position, Vector3.up, mouseX);
        
        transform.Rotate(-Vector3.right, mouseY);
        // var rot =transform.localRotation.eulerAngles;
        // var newX = ClampAngle(rot.x, -90, 90);
        // var newRot = Quaternion.Euler(newX, rot.y, rot.z);
        // transform.localRotation = newRot;
        // xRotation = transform.rotation.eulerAngles.x;
        // if (xRotation >= 180 && xRotation <= 360) {
        //     xRotation = Mathf.Clamp(xRotation, 360-90, 360);
        // }
        // else if (xRotation < 180 && xRotation >= 0) {
        //     xRotation = Mathf.Clamp(xRotation, 0, 90);
        // }
        // debugXRotation = xRotation;
        // // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // transform.rotation.SetEuler(xRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

//https://answers.unity.com/questions/659932/how-do-i-clamp-my-rotation.html
    public static float ClampAngle(float angle, float min, float max)
 {
     angle = Mathf.Repeat(angle, 360);
     min = Mathf.Repeat(min, 360);
     max = Mathf.Repeat(max, 360);
     bool inverse = false;
     var tmin = min;
     var tangle = angle;
     if(min > 180)
     {
         inverse = !inverse;
         tmin -= 180;
     }
     if(angle > 180)
     {
         inverse = !inverse;
         tangle -= 180;
     }
     var result = !inverse ? tangle > tmin : tangle < tmin;
     if(!result)
         angle = min;

     inverse = false;
     tangle = angle;
     var tmax = max;
     if(angle > 180)
     {
         inverse = !inverse;
         tangle -= 180;
     }
     if(max > 180)
     {
         inverse = !inverse;
         tmax -= 180;
     }
 
     result = !inverse ? tangle < tmax : tangle > tmax;
     if(!result)
         angle = max;
     return angle;
 }

}
