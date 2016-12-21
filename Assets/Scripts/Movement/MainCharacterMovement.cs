using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
    public float walkSpeed = 2;
    public float turnSmoothTime = .2f;
    public float speedSmoothTime = .1f;

    private Animator animator;
    private Transform cameraTransform;
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    //TODO figure out what you need out of this script yo
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        float targetSpeed = walkSpeed * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeed = 1 * inputDirection.magnitude;

        animator.SetFloat("movementSpeed", animationSpeed, speedSmoothTime, Time.deltaTime);
    }
}