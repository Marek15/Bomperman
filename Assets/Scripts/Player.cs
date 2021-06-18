using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Sprite life;
    [SerializeField] private LayerMask layerMaskForGround;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private GameObject[] hearts;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider2D;

    private float xVelocity = 0f;
    private float yVelocity = 0f;

    private int lifeCount = 3;

    private bool destroy = false;
    private bool stopMovingPlayer = false;
    private GameObject enemy;
    

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (lifeCount < 1 ) {
            Destroy(hearts[0].gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (lifeCount < 2 ) {
            Destroy(hearts[1].gameObject);
        }
        else if (lifeCount < 3 ) {
            Destroy(hearts[2].gameObject);
        }


        if (stopMovingPlayer) {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        }
        else {
            
        
            // player move horizontal logic
            if(joystick.Horizontal > .3f){
                xVelocity = runSpeed;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }else if(joystick.Horizontal < -.3f){
                xVelocity = -runSpeed;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }else 
                xVelocity = 0f;

            bool grounded = IsGrounded;
            // player move vertical logic
            if(grounded && joystick.Vertical >= 0.5f)
                yVelocity = jumpPower;
            else
                yVelocity = rigidBody.velocity.y;

            rigidBody.velocity = new Vector2(xVelocity, yVelocity);
            

        animator.SetFloat("xVelocity", xVelocity);
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
        animator.SetBool("jumping", !grounded);
        }
    }

    private void FixedUpdate() {
        if (destroy) {
            Destroy(enemy);
            destroy = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.layer == 12 && enemy != other.gameObject) {
            // Destroy(other.gameObject);
            enemy = other.gameObject;
            StartCoroutine(Freeze());
        }


        if (other.gameObject.layer == 13) {
            PlayerPrefs.SetFloat("timeFromStart",Time.timeSinceLevelLoad);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    IEnumerator Freeze() {
        stopMovingPlayer = true;
        yield return new WaitForSeconds(1.5f);
        lifeCount--;
        stopMovingPlayer = false;
        destroy = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public bool IsGrounded {
        get {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 1f, layerMaskForGround);
            return raycastHit.collider != null;
        }
    }
}