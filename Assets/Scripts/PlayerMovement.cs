using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Transform movementDestinationIndicator;
    public Transform movementDestinationLocation;

    private readonly float MOVE_DISTANCE = 1.0f;
    private readonly float MOVE_SPEED = 4.0f;

    /*
     * Handle player movement.
     */
    private void Update() {
        ArrowMovement();
        ClickMovement();
        TouchMovement();
    }

    /*
     * Keyboard controls for testing on PC.
     */
    private void ArrowMovement() {
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
     * Mouse click movement for testing on PC.
     */
    private void ClickMovement() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IndicatePointedDestination(clickedPosition);
            MoveToLocation(clickedPosition);
        }
    }

    /*
     * Movement for mobile.
     */
    private void TouchMovement() {
        if (Input.touchCount > 0) {
            Vector2 touchedPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            IndicatePointedDestination(touchedPosition);
            MoveToLocation(touchedPosition);
        }
    }

    private void IndicatePointedDestination(Vector2 destination) {
        movementDestinationLocation.position = destination;
        Transform movementDestinationIndicatorObject = Instantiate(movementDestinationIndicator, movementDestinationLocation);
        StartCoroutine(FadeOutAndDestroy(movementDestinationIndicatorObject.gameObject));
    }

    private IEnumerator FadeOutAndDestroy(GameObject objectToDestroy) {
        SpriteRenderer objectSpriteRenderer = objectToDestroy.GetComponent<SpriteRenderer>();
        Color objectColor = objectSpriteRenderer.color;
        while (objectColor.a > 0f) {
            objectColor.a -= Time.deltaTime;
            objectSpriteRenderer.color = objectColor;
            if (objectColor.a <= 0f) {
                objectColor.a = 0f;
            }
            yield return null;
        }
        objectSpriteRenderer.color = objectColor;
        Destroy(objectToDestroy);
    }

    private void MoveToLocation(Vector2 destination) {
        // TODO
    }
}
