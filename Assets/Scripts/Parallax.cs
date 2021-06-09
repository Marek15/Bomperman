using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{
    public Camera mainCam;
    public Transform player;

    Vector2 startPos;
    float startZ;
    float spriteWidth;

    [SerializeField]
    float speed = 0f;

    float distanceFromSubject => transform.position.z - player.position.z;
    float clippingPlane => mainCam.transform.position.z + (distanceFromSubject > 0 ? mainCam.farClipPlane : mainCam.nearClipPlane);
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    // Start is called before the first frame update
    void Start(){
        startPos = transform.position;
        startZ = transform.position.z;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update(){
        Vector2 newPos = startPos + (Vector2)mainCam.transform.position * parallaxFactor;
        transform.position = new Vector3(newPos.x+Time.time*-speed, newPos.y, startZ);

        if (mainCam.transform.position.x > transform.position.x + spriteWidth)
            startPos.x += spriteWidth;
        else if (mainCam.transform.position.x < transform.position.x)
            startPos.x -= spriteWidth;
    }
}
