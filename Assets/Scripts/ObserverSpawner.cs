using UnityEngine;

/*
 * Handles the creation of observer objects in the game.
 */
public class ObserverSpawner : MonoBehaviour {
    public Transform spawnLocation;
    public Transform gameField;
    public Transform observerContainer;
    public Observer observerPrefab;

    public Transform observerResetPointTop;
    public Transform observerResetPointBottom;
    public Transform observerResetPointLeft;
    public Transform observerResetPointRight;

    /*
     * Create num observers on the game field at random locations.
     */
    public void CreateObservers(int num) {
        for (int i = 0; i < num; i++) {
            CreateObserver();
        }
    }

    private void CreateObserver() {
        SetRandomSpawnLocationInGameField();
        Observer observer = Instantiate(observerPrefab, spawnLocation).GetComponent<Observer>();
        observer.transform.parent = observerContainer;
    }
    
    /*
     * Move the spawnLocation to a random location in the gameField
     */
    private void SetRandomSpawnLocationInGameField() {
        Vector2 gameFieldScale = gameField.transform.localScale;
        Vector2 gameFieldPosition = gameField.transform.position;

        float leftBoundary = gameFieldPosition.x - (gameFieldScale.x / 2.0f);
        float rightBoundary = gameFieldPosition.x + (gameFieldScale.x / 2.0f);
        float topBoundary = gameFieldPosition.y + (gameFieldScale.y / 2.0f);
        float bottomBoundary = gameFieldPosition.y - (gameFieldScale.y / 2.0f);

        // don't spawn directly at edges
        leftBoundary += 1.0f;
        rightBoundary -= 1.0f;
        topBoundary -= 1.0f;
        bottomBoundary += 1.0f;

        float leftPosition = Random.Range(leftBoundary, rightBoundary);
        float rightPosition = Random.Range(bottomBoundary, topBoundary);

        Vector2 randomLocation = new Vector2(leftPosition, rightPosition);
        
        spawnLocation.transform.position = randomLocation;
    }

    /*
     * Handle an observer hitting an outside boundary.
     * This is so observers are always contained inside the game field.
     */
    public void ObserverHitsOutsideBoundary(Observer observer, Collider2D collision) {
        if (collision.gameObject.name == "LeftBoundary") {
            ResetObserverPosition(observer, observerResetPointLeft);
        } else if (collision.gameObject.name == "RightBoundary") {
            ResetObserverPosition(observer, observerResetPointRight);
        } else if (collision.gameObject.name == "TopBoundary") {
            ResetObserverPosition(observer, observerResetPointTop);
        } else if (collision.gameObject.name == "BottomBoundary") {
            ResetObserverPosition(observer, observerResetPointBottom);
        }
    }

    private void ResetObserverPosition(Observer observer, Transform resetPosition) {
        observer.transform.position = resetPosition.position;
    }
}
