using UnityEngine;

public class Player : MonoBehaviour {
    private PlayerMovement playerMovement;
    public Transform respawnPosition;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    /*
     * Handle player coming in contact with any observer.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Observer") {
            CollidedWithObserver();
        } else if (collision.gameObject.tag == "EndZone") {
            transform.position = respawnPosition.position;
            playerMovement.SetIsMoving(false);

            // TODO advance the level
        }
    }

    /*
     * Same as the enter, but handles the case where you are already touching the observer initially.
     */
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Observer") {
            CollidedWithObserver();
        }
    }

    private void CollidedWithObserver() {
        if (!IsInSafeZone()) {
            transform.position = respawnPosition.position;
            playerMovement.SetIsMoving(false);
        }
    }

    private bool IsInSafeZone() {
        Collider2D playerCollider = GetComponent<Collider2D>();
        GameObject[] safeZones = GameObject.FindGameObjectsWithTag("SafeZone");
        foreach (GameObject safeZone in safeZones) {
            Collider2D safeZoneCollider = safeZone.GetComponent<Collider2D>();
            if (playerCollider.IsTouching(safeZoneCollider)) {
                return true;
            }
        }
        return false;
    }
}
