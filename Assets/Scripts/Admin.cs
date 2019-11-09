using UnityEngine;

/*
 * Class for debugging with key combinations.
 */
public class Admin : MonoBehaviour {
    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1)) {
            Debug.Log("ADMIN: " + SceneDataTransfer.CurrentGameDifficulty);
        }
    }
}
