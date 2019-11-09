using UnityEngine;

/*
 * Handles high level game constructs such as levels and gameplay settings.
 */
public class GameController : MonoBehaviour {
    public ObserverSpawner observerSpawner;
    private GameDifficulty gameDifficulty;

    private void Awake() {
        gameDifficulty = SceneDataTransfer.CurrentGameDifficulty;

        InitializeGame();
    }
    
    public void InitializeGame() {
        observerSpawner.CreateObservers(observerSpawner.GetInitialObserverCount(gameDifficulty));
    }
}
