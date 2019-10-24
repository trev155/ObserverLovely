using UnityEngine;

public class CameraController : MonoBehaviour {
    public PlayerUnit playerUnit;

    private void Update() {
        Vector3 newPosition = new Vector3(playerUnit.transform.position.x, playerUnit.transform.position.y + 3, -10);
        transform.position = newPosition;
    }
}
