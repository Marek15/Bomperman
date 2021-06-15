using System;
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

    float isGroundedRayLength = 0.2f;
    
    public int lifeCount = 3;
    

    // private Vector2 hearth_pos, player_pos;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(lifeCount);

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
        
        
        
        // player move horizontal logic
        if(joystick.Horizontal > .3f){
            xVelocity = runSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if(joystick.Horizontal < -.3f){
            xVelocity = -runSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }else 
            xVelocity = 0f;

        // player move vertical logic
        bool grounded = IsGrounded;
        if(grounded && joystick.Vertical >= 0.5f)
            yVelocity = jumpPower;
        else
            yVelocity = rigidBody.velocity.y;

        rigidBody.velocity = new Vector2(xVelocity, yVelocity);

        animator.SetFloat("xVelocity", xVelocity);
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
        animator.SetBool("jumping", !grounded);
    }

    // private void OnTriggerEnter2D(Collider2D other) {   
    //     Debug.Log("huhu");
    //     if (other.gameObject.layer == 9) {
    //         Destroy(other.gameObject);
    //         
    //         if (lifeCount <= 3) lifeCount++;
    //     }
    // }
    //
    // private void OnTriggerEnter(Collider other) {
    //     Debug.Log("huhu");
    // }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     Console.Write("huhu");
    //     if (other.gameObject.layer == 12) {
    //         rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    //         Debug.Log("joo");
    //     }
    // }

    public bool IsGrounded {
        get {
            Vector3 position = transform.position;
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 1f, layerMaskForGround);
            // position.y = GetComponent<Collider2D>().bounds.min.y + 0.1f;
            // float length = isGroundedRayLength + 0.1f;
            // Debug.DrawRay(position, Vector3.down * length);
            // bool grounded = Physics2D.Raycast(position, Vector3.down, length, layerMaskForGround.value);

            // animator.SetBool("jumping", !grounded);

            return raycastHit.collider != null;
        }
    }
}