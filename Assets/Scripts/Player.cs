﻿using UnityEngine;

public class Player : MonoBehaviour {
    private readonly float MOVE_DISTANCE = 1.0f;
    private readonly float MOVE_SPEED = 4.0f;

    /*
     * Handle player movement.
     */
    private void Update() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        bool up = Input.GetAxisRaw("Vertical") > 0;
        bool down = Input.GetAxisRaw("Vertical") < 0;
        bool left = Input.GetAxisRaw("Horizontal") < 0;
        bool right = Input.GetAxisRaw("Horizontal") > 0;

        if (up) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + MOVE_DISTANCE), Time.deltaTime * MOVE_SPEED);
        }
        if (down) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - MOVE_DISTANCE), Time.deltaTime * MOVE_SPEED);
        }
        if (left) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - MOVE_DISTANCE, transform.position.y), Time.deltaTime * MOVE_SPEED);
        }
        if (right) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + MOVE_DISTANCE, transform.position.y), Time.deltaTime * MOVE_SPEED);
        }
    }
    
    /*
     * Handle player coming in contact with any observer.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Observer") {
            CollidedWithObserver();
        }
    }

    private void CollidedWithObserver() {
        Debug.Log("Observer Contact");
    }
}
