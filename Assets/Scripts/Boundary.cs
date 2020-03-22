using UnityEngine;


public class Boundary : MonoBehaviour {
    private void Start() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector2 screenSize = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize,
            Camera.main.orthographicSize
        );
        collider.size = new Vector3(
            screenSize.y * 2,
            15.0f,
            screenSize.x * 2
        );
    }

    private void OnTriggerExit(Collider other) {
        IPoolable poolable = other.gameObject.GetComponent<IPoolable>();
        if (poolable == null) {
            Destroy(gameObject);
        }
        else {
            poolable.ReturnToPool();
        }
    }
}
