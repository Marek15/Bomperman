using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField] private Joystick joystick;
    [SerializeField] private Image[] hearths;
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] private LayerMask layerMaskForGround;

    private Rigidbody2D rigidBodyComponent;

    private float runSpeed = 5f;
    private float horizontalMove;

    private bool isJumping;
    private float jumpPower = 4f;

    private int lifeCount = 2;
    
    
    float isGroundedRayLength = 0.1f;

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        // player move horizontal logic
        horizontalMove = joystick.Horizontal * runSpeed;
        rigidBodyComponent.velocity = new Vector2(horizontalMove, rigidBodyComponent.velocity.y);

        //check if joysticck up is more than set value
        if (joystick.Vertical >= .5f) {
            isJumping = true;
        }
        
        //check players lifes
        for (int i = 0; i < 3; i++) {
            if (i < lifeCount) {
                hearths[i].sprite = fullHeart;
            }
            else {
                hearths[i].sprite = emptyHeart;
            }
        }
        
        if (transform.position.y < -10) {
            lifeCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void FixedUpdate() {
        
        // check if player is on ground if true return and not jump
        if (isJumping && IsGrounded) {
            rigidBodyComponent.AddForce(Vector2.up * jumpPower, (ForceMode2D) ForceMode.VelocityChange);
            isJumping = false;
        }
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

            return grounded;
        }
    }
}