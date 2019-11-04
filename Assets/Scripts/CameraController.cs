using UnityEngine;

/*
 * Follow the player.
 */
public class CameraController : MonoBehaviour {
    public Player player;
    private readonly float cameraVerticalOffset = 3.0f;

    private void Update() {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + cameraVerticalOffset, -10);
        transform.position = newPosition;
    }
}
