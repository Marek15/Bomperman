using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    private int xDiff, yDiff; // direction
    private Vector2 enemy_pos, player_pos;

    // Update is called once per frame
    void Update() {
        enemy_pos = this.transform.position;
        player_pos = player.transform.position;

        // rotate enemy
        if (player_pos.x > enemy_pos.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.layer == 8 ) {
            animator.SetBool("explode", true);
        }
    }

}
