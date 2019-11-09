using UnityEngine;
using UnityEngine.SceneManagement;
 
/*
 * Handles main menu canvas actions.
 */
public class MainMenu : MonoBehaviour {
    private GameDifficulty difficultySelection;    // 1 = easy, 2 = medium, 3 = hard

    private void Awake() {
        difficultySelection = GameDifficulty.NORMAL;    // default
    }

    public void EasyButton() {
        difficultySelection = GameDifficulty.EASY;
    }

    public void MediumButton() {
        difficultySelection = GameDifficulty.NORMAL;
    }

    public void HardButton() {
        difficultySelection = GameDifficulty.HARD;
    }

    public void StartGame() {
        SceneDataTransfer.CurrentGameDifficulty = difficultySelection;
        SceneManager.LoadScene("GameField");
    }
}