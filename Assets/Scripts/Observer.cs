using System.Collections;
using UnityEngine;

/*
 * An observer object is the main "enemy" unit where if the player touches it, the player will "die".
 * Observers move around the game field in random directions.
 */
public class Observer : MonoBehaviour {
    private bool isMoving = false;
    private bool isStopping = false;
    private Vector2 movementDirection;



    private readonly float[] movementIntervals = new float[] { 2.0f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 6.0f, 7.0f };

    /*
     * Repeatedly perform an action depending on the current state of the observer.
     * The observer can be in its move sequence, or it can be stopped.
     */
    private void Update() {
        if (!isMoving) {
            isMoving = true;
            SetRandomMovementDirection();
            StartCoroutine("MoveInDirection");
        } else if (!isStopping) {
            Vector2 destination = new Vector2(transform.position.x + movementDirection.x, transform.position.y + movementDirection.y);
            transform.position = Vector2.MoveTowards(transform.position, destination, GetObserverSpeed() * Time.deltaTime);
        }
    }

    private float GetObserverSpeed() {
        return GameController.Instance.GetObserverSpeed();
    }

    /*
     * If the observer collides with an observer boundary, change its current direction to the opposite direction.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "ObserverBoundary") {
            HandleObserverHittingObserverBoundary();
        } else if (collision.gameObject.tag == "OverflowBoundary") {
            HandleObserverHittingOverflowBoundary(collision);
        }
    }

    /*
     * If the observer collides with an observer boundary, change its current direction to the opposite direction.
     */
    private void HandleObserverHittingObserverBoundary() {
        Vector2 oppositeDirection = new Vector2(movementDirection.x * -1.0f, movementDirection.y * -1.0f);
        movementDirection = oppositeDirection;
    }

    /*
     * If the observer collides with an overflow boundary, move it back to inside the game field.
     */
    private void HandleObserverHittingOverflowBoundary(Collider2D collision) {
        GameController.Instance.observerSpawner.ObserverHitsOutsideBoundary(this, collision);
    }

    /*
     * Move the observer in the direction of movementDirection.
     * The amount of time that we move is randomized, as well as the cooldown stop time afterwards.
     */
    private IEnumerator MoveInDirection() {
        float randomMovementInterval = movementIntervals[Random.Range(0, movementIntervals.Length)];
        yield return new WaitForSeconds(randomMovementInterval);

        isMoving = false;
        isStopping = true;

        float randomStoppingInterval = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(randomStoppingInterval);

        isStopping = false;
    }

    private void SetRandomMovementDirection() {
        movementDirection = GenerateRandomVectorOnUnitCircle(); 
    }

    private Vector2 GenerateRandomVectorOnUnitCircle() {
        float r = Random.Range(0.0f, 1.0f);
        return new Vector2(Mathf.Cos(2 * Mathf.PI * r), Mathf.Sin(2 * Mathf.PI * r));
    }
}
