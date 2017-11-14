using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTarget : MonoBehaviour {

    public bool colliding = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        colliding = true;
    }
}
