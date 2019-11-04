using System.Collections;
using UnityEngine;

/*
 * Handles player movement controls.
 * As soon as the screen is touched / clicked, the player should move to that location.
 * Movement stops when the destination location is reached.
 * Movement should be gradual, accelerate to the player's max speed, then slow down when approaching the destination.
 */
public class PlayerMovement : MonoBehaviour {
    public Transform movementDestinationIndicator;
    public Transform movementDestinationIndicatorSpawnLocation;

    private bool isMoving;
    private Vector2 movementDestination;
    private float currentSpeed = 0f;

    private readonly float MAX_SPEED = 4.0f;
    private readonly float STOP_THRESHOLD = 0.01f;

    private void Awake() {
        SetIsMoving(false);
    }

    /*
     * Handle player movement.
     */
    private void Update() {
        ClickMovement();
        TouchMovement();
        MoveToDestination();
    }

    /*
     * Mouse click movement for testing on PC.
     */
    private void ClickMovement() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IndicatePointedDestination(clickedPosition);
            SetMovementDestination(clickedPosition);
        }
    }

    /*
     * Movement for mobile.
     */
    private void TouchMovement() {
        if (Input.touchCount > 0) {
            Vector2 touchedPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            IndicatePointedDestination(touchedPosition);
            SetMovementDestination(touchedPosition);
        }
    }

    /*
     * Create an object at the touched / clicked position to indicate that location was touched / clicked.
     * First, move a transform object to that location so we can instantiate the movementDestinationIndicator there.
     */
    private void IndicatePointedDestination(Vector2 destination) {
        movementDestinationIndicatorSpawnLocation.position = destination;
        Transform movementDestinationIndicatorObject = Instantiate(movementDestinationIndicator, movementDestinationIndicatorSpawnLocation);
        StartCoroutine(FadeOutAndDestroy(movementDestinationIndicatorObject.gameObject));
    }

    /*
     * Gradually fade out a game object over time by decreasing its alpha value. Once its alpha hits 0, destroy it.
     */
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
    
    /*
     * Set movement destination of the player to the supplied destination.
     */
    private void SetMovementDestination(Vector2 destination) {
        movementDestination = destination;
        SetIsMoving(true);
    }

    /*
     * This should be called in the Update() on every frame. 
     * If we are not at our destination, then move towards it.
     * The speed steadily increases until we reach the player's max speed.
     */
    private void MoveToDestination() {
        if (isMoving) {
            float distanceToDestination = Vector2.Distance(transform.position, movementDestination);
            if (distanceToDestination < STOP_THRESHOLD) {
                SetIsMoving(false);
                currentSpeed = 0;
                return;
            }

            if (currentSpeed < MAX_SPEED) {
                currentSpeed += 0.1f;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, movementDestination, Time.deltaTime * currentSpeed);
        }
    }

    /*
     * Set the variable that controls whether the player should move to its destination.
     */
    public void SetIsMoving(bool isMoving) {
        this.isMoving = isMoving;
    }
}
