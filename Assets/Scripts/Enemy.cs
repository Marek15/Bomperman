using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public float speed;

    private float zPos;
    private int xDiff, yDiff; // direction
    private Vector2 enemy_pos, player_pos;

    // Start is called before the first frame update
    void Start(){
        zPos = this.transform.position.z;
        
    }

    // Update is called once per frame
    void Update() {
        enemy_pos = this.transform.position;
        player_pos = player.transform.position;

        // rotate enemy
        if (player_pos.x > enemy_pos.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);


        // explode when player close enough
        float distance = Vector3.Distance(enemy_pos, player_pos);
        bool explode = distance <= 5;
        animator.SetBool("explode", explode);

        if (explode) {
            
            // StartCoroutine(FreezePlayer());
            this.transform.position = new Vector3(this.transform.position.x, 100, this.transform.position.z);
            GameObject.Find("Player").GetComponent<Player>().lifeCount -= 1;
            // enemy_pos.y += 10;
            // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            // GameObject.Find("Player").GetComponent<Player>().lifeCount -= 1;
        }

        // move when player close enough
        /*if (distance <= 12 && animator.GetCurrentAnimatorStateInfo(0).IsName("enemy_fly")){
            xDiff = (player_pos.x < enemy_pos.x) ? -1 : 1;
            yDiff = (player_pos.y < enemy_pos.y) ? -1 : 1; 
            this.transform.position = new Vector3(enemy_pos.x + speed * Time.deltaTime * xDiff, enemy_pos.y + speed * Time.deltaTime * yDiff-2, zPos);
        }*/
    }

    IEnumerator FreezePlayer() {
        // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        yield return new WaitForSeconds(2); 
        GameObject.Find("Player").GetComponent<Player>().lifeCount -= 1;
        // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }
}
