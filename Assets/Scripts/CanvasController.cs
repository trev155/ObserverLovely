using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public Text livesText;
    public Text difficultyText;

    public void InitializeGUI() {
        UpdateLivesText();
        UpdateDifficultyText();
    }

    public void UpdateLivesText() {
        livesText.text = GameController.Instance.GetLifeCount() + "";
    }

    public void UpdateDifficultyText() {
        difficultyText.text = GameController.Instance.GetGameDifficulty() + "";
    }

}
