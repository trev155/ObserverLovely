using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/*
 * Handles main menu canvas actions.
 */
public class MainMenu : MonoBehaviour {
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    private GameDifficulty difficultySelection;
    
    private void Start() {
        DefaultSelection();
    } 

    private void DefaultSelection() {
        difficultySelection = GameDifficulty.NORMAL;
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(normalButton.gameObject, pointer, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(normalButton.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        
    }

    public void EasyButton() {
        SceneDataTransfer.CurrentGameDifficulty = GameDifficulty.EASY;
    }

    public void NormalButton() {
        SceneDataTransfer.CurrentGameDifficulty = GameDifficulty.NORMAL;
    }

    public void HardButton() {
        SceneDataTransfer.CurrentGameDifficulty = GameDifficulty.HARD;
    }

    public void StartGame() {
        SceneManager.LoadScene("GameField");
    }
}