﻿using System.Collections.Generic;
using UnityEngine;

/*
 * Handles high level game constructs such as levels and gameplay settings.
 */
public class GameController : MonoBehaviour {
    public ObserverSpawner observerSpawner;
    public CanvasController canvasController;

    private GameDifficulty gameDifficulty;
    private int lifeCount;

    private Dictionary<GameDifficulty, int> observerCountForGameDifficulty;
    private Dictionary<GameDifficulty, float> observerSpeedsForGameDifficulty;
    private Dictionary<GameDifficulty, int> initialLifeCounts;

    // Singleton field
    public static GameController Instance { get; private set; } = null;
    public static GameController GetInstance() {
        return Instance;
    }

    // Initialization Functions
    private void Awake() {
        InitializeSingleton();

        gameDifficulty = SceneDataTransfer.CurrentGameDifficulty;
        InitializeGameDifficultyMaps();
        lifeCount = initialLifeCounts[gameDifficulty];
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

    private void InitializeGameDifficultyMaps() {
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
    }

    private void InitializeGame() {
        observerSpawner.CreateObservers(observerCountForGameDifficulty[gameDifficulty]);
    }

    // API Functions for Game Controller Data
    public float GetObserverSpeed() {
        return observerSpeedsForGameDifficulty[gameDifficulty];
    }

    public int GetLifeCount() {
        return lifeCount;
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
}
