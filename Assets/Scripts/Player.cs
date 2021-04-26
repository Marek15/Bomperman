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

    private Rigidbody2D rigidBody;

    private float xVelocity = 0f;
    private float yVelocity = 0f;

    private int lifeCount = 2;
    float isGroundedRayLength = 0.2f;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
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
        if(IsGrounded && joystick.Vertical >= 0.5f)
            yVelocity = jumpPower;
        else
            yVelocity = rigidBody.velocity.y;

        rigidBody.velocity = new Vector2(xVelocity, yVelocity);

        animator.SetFloat("xVelocity", xVelocity);
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 9) {
            Destroy(other.gameObject);
            
            if (lifeCount <= 3) lifeCount++;
        }
    }
    
    public bool IsGrounded {
        get {
            Vector3 position = transform.position;
            position.y = GetComponent<Collider2D>().bounds.min.y + 0.1f;
            float length = isGroundedRayLength + 0.1f;
            Debug.DrawRay(position, Vector3.down * length);
            bool grounded = Physics2D.Raycast(position, Vector3.down, length, layerMaskForGround.value);

            animator.SetBool("jumping", !grounded);

            return grounded;
        }
    }
}