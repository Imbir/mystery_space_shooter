using UnityEngine;


public class Boundary : MonoBehaviour {
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
