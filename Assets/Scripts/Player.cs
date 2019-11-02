using UnityEngine;

public class Player : MonoBehaviour {
    /*
     * Handle player coming in contact with any observer.
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Observer") {
            CollidedWithObserver();
        }
    }

    private void CollidedWithObserver() {
        Debug.Log("Observer Contact");
    }
}
