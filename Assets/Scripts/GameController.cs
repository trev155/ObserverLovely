using System.Collections.Generic;
using UnityEngine;

/*
 * Handles high level game constructs such as levels and gameplay settings.
 */
public class GameController : MonoBehaviour {
    public ObserverSpawner observerSpawner;
    private GameDifficulty gameDifficulty;

    private Dictionary<GameDifficulty, int> observerCountMappingsToGameDifficulty;
    private Dictionary<GameDifficulty, float> observerSpeedMappingsToGameDifficulty;

    // Singleton field
    public static GameController Instance { get; private set; } = null;
    public static GameController GetInstance() {
        return Instance;
    }

    // Initialization Functions
    private void Awake() {
        InitializeSingleton();

        gameDifficulty = SceneDataTransfer.CurrentGameDifficulty;
        observerCountMappingsToGameDifficulty = new Dictionary<GameDifficulty, int>() {
            { GameDifficulty.EASY, 180 },
            { GameDifficulty.NORMAL, 240 },
            { GameDifficulty.HARD, 300 }
        };
        observerSpeedMappingsToGameDifficulty = new Dictionary<GameDifficulty, float>() {
            { GameDifficulty.EASY, 0.8f },
            { GameDifficulty.NORMAL, 1.1f },
            { GameDifficulty.HARD, 1.4f }
        };

        InitializeGame();
    }

    private void InitializeSingleton() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
        }
    }

    public void InitializeGame() {
        observerSpawner.CreateObservers(observerCountMappingsToGameDifficulty[gameDifficulty]);
    }

    // Other functions
    public float GetObserverSpeed() {
        return observerSpeedMappingsToGameDifficulty[gameDifficulty];
    }
}
