using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   // public CharacterController controller;
    public Rigidbody rb;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float speed = 5f;

    public float jumpHeight = 3f;

    private void Start()
    {
      //  controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       // isGrounded = controller.isGrounded;
    }

    // receives input from InputManager.cs and applies them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

       rb.AddForce(transform.TransformDirection(moveDirection) * speed * Time.deltaTime, ForceMode.VelocityChange);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
    }

    public void Jump()
    {

    }

    public void StealthWalk()
    {

    }

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
