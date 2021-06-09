using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCam;
    public Transform player;
    public BoxCollider2D mapBounds;
    public float smoothing;
    private float yPos, zPos, yMin;

    // Start is called before the first frame update
    private void Start(){
        zPos = this.transform.position.z;
        yMin = mapBounds.bounds.min.y + mainCam.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate(){
        yPos = (player.position.y > yMin) ? player.position.y : yMin;
        this.transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, yPos, zPos), smoothing * Time.fixedDeltaTime);
    }
}