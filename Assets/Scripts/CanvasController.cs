using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public Text livesText;
    public Text difficultyText;
    public Text levelText;

    public void InitializeGUI() {
        UpdateLivesText();
        UpdateDifficultyText();
        UpdateLevelText();
    }

    public void UpdateLivesText() {
        livesText.text = GameController.Instance.GetLifeCount() + "";
    }

    public void UpdateDifficultyText() {
        difficultyText.text = GameController.Instance.GetGameDifficulty() + "";
    }

    public void UpdateLevelText() {
        levelText.text = GameController.Instance.GetLevel() + "";
    }
}
