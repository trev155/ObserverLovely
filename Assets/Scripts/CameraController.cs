using UnityEngine;

public class CameraController : MonoBehaviour {
    public Player player;

    private void Update() {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + 3, -10);
        transform.position = newPosition;
    }
}
