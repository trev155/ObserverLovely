﻿using UnityEngine;

public class ObserverSpawner : MonoBehaviour {
    public Transform spawnLocation;
    public Transform gameField;
    public Transform observerContainer;
    public Observer observerPrefab;

    private readonly int INITIAL_OBSERVER_COUNT = 200;

    private void Start() {
        CreateObservers(INITIAL_OBSERVER_COUNT);
    }
 
    private void CreateObservers(int num) {
        for (int i = 0; i < num; i++) {
            CreateObserver();
        }
    }

    private void CreateObserver() {
        SetRandomSpawnLocationInGameField();
        Observer observer = Instantiate(observerPrefab, spawnLocation).GetComponent<Observer>();
        observer.transform.parent = observerContainer;
    }

    private void SetRandomSpawnLocationInGameField() {
        Vector2 gameFieldScale = gameField.transform.localScale;
        Vector2 gameFieldPosition = gameField.transform.position;

        float leftBoundary = gameFieldPosition.x - (gameFieldScale.x / 2.0f);
        float rightBoundary = gameFieldPosition.x + (gameFieldScale.x / 2.0f);
        float topBoundary = gameFieldPosition.y + (gameFieldScale.y / 2.0f);
        float bottomBoundary = gameFieldPosition.y - (gameFieldScale.y / 2.0f);
        
        float leftPosition = Random.Range(leftBoundary, rightBoundary);
        float rightPosition = Random.Range(bottomBoundary, topBoundary);

        Vector2 randomLocation = new Vector2(leftPosition, rightPosition);
        
        spawnLocation.transform.position = randomLocation;
    }

}
