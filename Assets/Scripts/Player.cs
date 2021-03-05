using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask bonusLifeMask;

    private Rigidbody rigidBodyComponent;

    private float runSpeed = 5f;
    private float horizontalMove = 0f;

    private bool isJumping = false;
    private float jumpPower = 4f;

    private int lifeCount = 2;

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = joystick.Horizontal * runSpeed;

        rigidBodyComponent.velocity = new Vector3(horizontalMove, rigidBodyComponent.velocity.y, 0);

        //check if joysticck up is more than set value
        if (joystick.Vertical >= .5f) {
            isJumping = true;
        }
    }

    private void FixedUpdate() {
        // check if player is on ground if true return and not jump
        if (Physics.OverlapSphere(groundCheckTransform.position, .1f, playerMask).Length == 0) return;

        if (isJumping) {
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            isJumping = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 9) {
            Destroy(other.gameObject);
            lifeCount++;
        }
    }
}