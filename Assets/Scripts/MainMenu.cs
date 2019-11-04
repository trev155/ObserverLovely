using UnityEngine;
using UnityEngine.SceneManagement;
 
/*
 * Handles main menu canvas actions.
 */
public class MainMenu : MonoBehaviour {
    private int difficultySelection;    // 1 = easy, 2 = medium, 3 = hard

    private void Awake() {
        difficultySelection = 2;
    }

    public void EasyButton() {
        difficultySelection = 1;
    }

    public void MediumButton() {
        difficultySelection = 2;
    }

    public void HardButton() {
        difficultySelection = 3;
    }

    public void StartGame() {
        SceneDataTransfer.CurrentGameMode = difficultySelection;
        SceneManager.LoadScene("GameField");
    }
}