using UnityEngine;

/*
 * Controls the movement of the camera around the target position.
 */
public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public Transform target;
    public Range pitchMinMax = new Range(-40, 85);
    public float mouseSensitivity = 10;
    public float distanceFromTarget = 2;
    public float rotationSmoothTime = 0.07f;

    private float yaw;
    private float pitch;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public void Awake()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        ActionController actionController = Singleton<ActionController>.Instance;
        actionController.registerMouseMovementListener(mouseMovementListener);
    }
	
    //TODO figure out what you need out of this script yo
    public void mouseMovementListener(float x, float y, float rawX, float rawY)
    {
        yaw += x * mouseSensitivity;
        pitch -= y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.min, pitchMinMax.max);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
	
	}
}