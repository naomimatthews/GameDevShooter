using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    private Vector3 playerVelocity;

    public float speed = 5f;

    public float jumpHeight = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    // receives input from InputManager.cs and applies them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

       rb.AddForce(transform.TransformDirection(moveDirection) * speed * Time.deltaTime, ForceMode.VelocityChange);

        // movement animation.
       /* if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }*/

       /*f (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;*/
    }

  /*  public void Walk(Vector2 input)
    {
        if (moveDirection != Vector3.zero && !input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 1f);
        }
            animator.SetFloat("Speed", 0.5f);
   
    }*/

    public void Boost(float buff)
    {
        Debug.Log("q ability");
        speed = speed * buff;
    }
    public void ResetBoost(float buff)
    {
        Debug.Log("q ability");
        speed = speed / buff;
    }

    public void SBBoost()
    {
        Debug.Log("e ability");
        speed = speed * 2.5f;
    }
    public void ResetSBBoost()
    {
        Debug.Log("e ability");
        speed = speed / 2.5f;
    }
}
