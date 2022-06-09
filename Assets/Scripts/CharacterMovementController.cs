using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpAccel;
    private bool isJumping;
    private bool isOnGround;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }

    private void FixedUpdate()
    {
        //raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if(!isOnGround && rig.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }else
        {
            isOnGround = false;
        }
        //calculate velocity vector
        Vector2 velocityVector = rig.velocity;

        if(isJumping)
        {
            velocityVector.y += jumpAccel;
            isJumping = false;
        }
        velocityVector.x = Mathf.Clamp (velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
        rig.velocity = velocityVector;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;
            }
        }
    }
}
