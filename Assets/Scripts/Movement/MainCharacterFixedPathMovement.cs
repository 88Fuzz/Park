using UnityEngine;

/*
 * Controls the main character walking from point to point.
 */
[RequireComponent(typeof(Animator))]
public class MainCharacterFixedPathMovement : MonoBehaviour, IWalkTargetTriggerListener
{
    public WalkTargetProviderSelector walkTargetProviderSelector;
    public float rotationSpeed = 2;

    public float walkSpeed = 2;
    public float turnSmoothTime = .2f;
    public float speedSmoothTime = .1f;

    private Animator animator;
    private IWalkTargetProvider walkTargetProvider;
    private WalkTarget currentTarget;
    private WalkDirection walkDirection;

    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;

    public void Awake()
    {
        walkTargetProvider = walkTargetProviderSelector.getPlayerWalkTargetProvider();
        walkDirection = walkTargetProvider.getStartingWalkDirection();
        currentTarget = walkTargetProvider.getStartingTarget();
        //Set the position of the player to the first position, to prep for the walk to the second target.
        transform.position = currentTarget.transform.position;
        currentTarget = walkTargetProvider.getNextWalkTarget(currentTarget, walkDirection);
        transform.LookAt(currentTarget.transform);

        animator = GetComponent<Animator>();

        ActionController actionController = Singleton<ActionController>.Instance;
        actionController.registerMovementListener(move);
    }

    //TODO figure out what you need out of this script yo
    //public void Update()
    public void move(float x, float z, float rawX, float rawZ)
    {
        if (currentTarget == null)
            return;

        //float movement = Input.GetAxisRaw("Vertical");
        float movement = z;
        if (movement <= 0)
        {
            movement = 0;
        }
        else
        {
            //We only want to change our trajectory if we are currently moving.
            Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            Vector3 targetRotation3 =  targetRotation.eulerAngles;
            targetRotation3.x = 0;
            targetRotation3.z = 0;
            Quaternion superTargetRotation = Quaternion.Euler(targetRotation3);

            transform.rotation = Quaternion.Lerp(transform.rotation, superTargetRotation, rotationSpeed * Time.deltaTime);
        }

        float targetSpeed = walkSpeed * movement;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        animator.SetFloat("movementSpeed", movement, speedSmoothTime, Time.deltaTime);
    }

    public void walkTargetTriggered(int walkTargetId)
    {
        if(walkTargetId == currentTarget.transformId)
        {
            //TODO how to handle when you are at the end of the list and getNextWalkTarget returns null
            currentTarget = walkTargetProvider.getNextWalkTarget(currentTarget, walkDirection);
        }
    }
}