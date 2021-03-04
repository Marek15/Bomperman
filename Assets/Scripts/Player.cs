using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    
    private Rigidbody rigidBodyComponent;

    private float runSpeed = 5f;
    private float horizontalMove = 0f;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
        horizontalMove = joystick.Horizontal * runSpeed;
        
        rigidBodyComponent.velocity = new Vector3(horizontalMove, rigidBodyComponent.velocity.y, 0);

        float verticalMove = joystick.Vertical;

        if (verticalMove >= .5f) {
            isJumping = true;
        }

        

    }

    private void FixedUpdate() {

        if (Physics.OverlapSphere(groundCheckTransform.position, .1f, playerMask).Length == 0) {
            return;
        }
        
        if (isJumping) {
            float jumpPower = 5f;
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            isJumping = false;
        }
    }
}
