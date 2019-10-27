using System.Collections;
using UnityEngine;

public class Observer : MonoBehaviour {
    private bool isMoving = false;
    private bool isStopping = false;
    private readonly float[] movementIntervals = new float[] { 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f };
    private Vector2 movementDirection;

    private void Awake() {
        SetRandomMovementDirection();
    }

    private void Update() {
        if (!isMoving) {
            isMoving = true;
            StartCoroutine("MoveInRandomDirection");
        } else if (!isStopping) {
            Vector2 destination = new Vector2(transform.position.x + movementDirection.x, transform.position.y + movementDirection.y);
            transform.position = Vector2.MoveTowards(transform.position, destination, 1.0f * Time.deltaTime);
        }
    }

    private IEnumerator MoveInRandomDirection() {
        SetRandomMovementDirection();        

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
