using System.Collections.Generic;
using UnityEngine;

/*
 * Handles high level game constructs such as levels and gameplay settings.
 */
public class GameController : MonoBehaviour {
    public ObserverSpawner observerSpawner;
    public CanvasController canvasController;

    private GameDifficulty gameDifficulty;
    private int lifeCount;
    private int level;

    private Dictionary<GameDifficulty, int> observerCountForGameDifficulty;
    private Dictionary<GameDifficulty, float> observerSpeedsForGameDifficulty;
    private Dictionary<GameDifficulty, int> initialLifeCounts;
    private Dictionary<int, float> playerMaxSpeedPerLevel;

    // ----- Singleton field -----
    public static GameController Instance { get; private set; } = null;
    public static GameController GetInstance() {
        return Instance;
    }

    // ----- Initialization Functions -----
    private void Awake() {
        InitializeSingleton();

        gameDifficulty = SceneDataTransfer.CurrentGameDifficulty;
        InitializeDataMaps();
        lifeCount = initialLifeCounts[gameDifficulty];
        level = 1;
        canvasController.InitializeGUI();

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

    private void InitializeDataMaps() {
        observerCountForGameDifficulty = new Dictionary<GameDifficulty, int>() {
            { GameDifficulty.EASY, 180 },
            { GameDifficulty.NORMAL, 240 },
            { GameDifficulty.HARD, 300 }
        };
        observerSpeedsForGameDifficulty = new Dictionary<GameDifficulty, float>() {
            { GameDifficulty.EASY, 0.8f },
            { GameDifficulty.NORMAL, 1.1f },
            { GameDifficulty.HARD, 1.4f }
        };
        initialLifeCounts = new Dictionary<GameDifficulty, int>() {
            { GameDifficulty.EASY, 20 },
            { GameDifficulty.NORMAL, 10 },
            { GameDifficulty.HARD, 5 }
        };
        playerMaxSpeedPerLevel = new Dictionary<int, float>() {
            { 1, 6.0f },
            { 2, 5.6f },
            { 3, 5.4f },
            { 4, 5.2f },
            { 5, 5.0f },
            { 6, 4.6f },
            { 7, 4.3f },
            { 8, 4.1f },
            { 9, 4.0f },
            { 10, 3.8f },
            { 11, 3.6f },
            { 12, 3.3f },
            { 13, 3.0f },
            { 14, 2.7f },
            { 15, 2.5f },
            { 16, 2.3f },
            { 17, 2.0f },
            { 18, 1.8f },
            { 19, 1.6f },
            { 20, 1.4f },
        };
    }

    private void InitializeGame() {
        observerSpawner.CreateObservers(observerCountForGameDifficulty[gameDifficulty]);
    }

    // ----- API Functions for Game Controller Data -----
    public float GetObserverSpeed() {
        return observerSpeedsForGameDifficulty[gameDifficulty];
    }

    public int GetLifeCount() {
        return lifeCount;
    }

    public int GetLevel() {
        return level;
    }

    public string GetGameDifficulty() {
        switch (gameDifficulty) {
            case GameDifficulty.EASY:
                return "Easy";
            case GameDifficulty.NORMAL:
                return "Normal";
            case GameDifficulty.HARD:
                return "Hard";
            default:
                return "unknown";
        }
    }

    public void DecreaseLifeCount() {
        lifeCount--;
        if (lifeCount == 0) {
            Debug.Log("GAME OVER");
        }
    }

    /*
     * Advance the level. 
     */
    public void LevelCompleted() {
        CheckApplyBonusLives();
        level++;
        canvasController.UpdateLevelText(); 
    }

    private void CheckApplyBonusLives() {
        if (level == 5 || level == 10 || level == 15) {
            lifeCount += 3;
            canvasController.UpdateLivesText();
        }
    }

    public float GetPlayerMaxSpeed() {
        if (playerMaxSpeedPerLevel.ContainsKey(level)) {
            return playerMaxSpeedPerLevel[level];
        } else {
            Debug.Log("No max speed value exists for this level in the mappings.");
            return 3.0f;
        }
    }
}
