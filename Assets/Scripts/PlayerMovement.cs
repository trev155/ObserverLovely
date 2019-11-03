using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Transform movementDestinationIndicator;
    public Transform movementDestinationLocation;

    private bool isMoving;
    private Vector2 movementDestination;
    private float currentSpeed = 0f;

    private readonly float MOVE_DISTANCE = 1.0f;
    private readonly float MAX_SPEED = 4.0f;
    private readonly float STOP_THRESHOLD = 0.01f;
    private readonly float SLOW_DOWN_THRESHOLD = 1.0f;
    private readonly float SPEED_UP_THRESHOLD = 1.0f;

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

    private void SetMovementDestination(Vector2 destination) {
        movementDestination = destination;
        SetIsMoving(true);
    }

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

    public void SetIsMoving(bool isMoving) {
        this.isMoving = isMoving;
    }
}
