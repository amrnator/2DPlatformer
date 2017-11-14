using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Blink.
/// Short range teleport, player inputs a direction, then two things happen
/// 1. a gameobject is moved some distance in that direction
/// 2. a ray is fired from the player, some distance in the desired direction
/// 
/// if the gameobject is inside an obstacle, then teleport to where the ray collided,
/// otherwise teleport to the gameobject
/// 
/// </summary>
public class Blink : MonoBehaviour {

	//distance of blink
	public float blinkDistance = 20f;

    //duration of teleport, or delay between transition
    public float blinkDuration = .3f;

	//layer that will be used to stop ray
	public LayerMask collisionMask;

    //gameobject that will be moved to targeted area
    Transform target;



    //posotion of mouse when engaging blink
    Vector3 blinkPos;

    public bool blinkTracking = false;

    bool targetIsTouching = false;



    StaminaSystem stamina;
    Player player;
    Camera camera;
    MeshRenderer targetRenderer;
    BoxCollider2D targetCollider;

    void Start () {
        target = transform.GetChild(0);
        player = GetComponent<Player>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        stamina = GetComponent<StaminaSystem>();
        targetRenderer = target.GetComponent<MeshRenderer>();
        targetCollider = target.GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (blinkTracking)
        {
            blinkPos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
            blinkPos.z = -.1f;

            //make target visisble
            if (!targetRenderer.enabled) {
                targetRenderer.enabled = true;
            }

            Vector3 rayDirection = blinkPos - transform.position;

            Vector3 blinkDirection = rayDirection.normalized * blinkDistance;

            if (Vector3.Distance(blinkPos, transform.position) >= blinkDistance)
            {
                target.transform.position = transform.position + blinkDirection;
            }
            else {
                target.transform.position = blinkPos;
            }

            if (targetCollider.IsTouchingLayers(collisionMask))
            {
                targetIsTouching = true;
            }
            else {
                targetIsTouching = false;
            }

            blinkPos.z = 0;
        }
    }

	public void BlinkTeleport(){

        //make target invisible
        targetRenderer.enabled = false;

        blinkTracking = false;

        if (stamina.Stamina <= 0)
        {
            return;
        }

        stamina.Stamina--;

        Vector3 rayDirection = blinkPos - transform.position;

        Debug.DrawRay(transform.position, rayDirection.normalized * blinkDistance, Color.green);

        Vector3 blinkDirection = rayDirection.normalized * blinkDistance;

        if (!targetIsTouching)
        {
            if (Vector3.Distance(blinkPos, transform.position) > blinkDistance)
            {

                StartCoroutine("blinkRoutine", transform.position + blinkDirection);
            }
            else {
                StartCoroutine("blinkRoutine", blinkPos);
            }
        }
        else {
            //move to hit collisiosn
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, blinkDistance, collisionMask);

            if (hit) {
                StartCoroutine("blinkRoutine",  (Vector3) (hit.point + (hit.normal * (.5f - 0.01f))));
            }

        }
	}

    IEnumerator blinkRoutine(Vector3 position) {
        iTween.MoveTo(this.gameObject, position, blinkDuration);
        yield return new WaitForSeconds(blinkDuration);
        //player.velocity.y = 0f;
    }
}
