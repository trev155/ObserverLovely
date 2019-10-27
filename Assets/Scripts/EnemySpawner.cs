using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Transform spawnLocation;
    public Transform gameField;
    public EnemyUnit enemyUnitPrefab;

    private void Start() {
        int initialObserverCount = 100;
        CreateEnemyUnits(initialObserverCount);
    }
 
    private void CreateEnemyUnits(int num) {
        for (int i = 0; i < num; i++) {
            CreateEnemyUnit();
        }
    }

    private void CreateEnemyUnit() {
        SetRandomSpawnLocationInGameField();
        EnemyUnit enemyUnit = Instantiate(enemyUnitPrefab, spawnLocation).GetComponent<EnemyUnit>();
        enemyUnit.transform.parent = null;  
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
