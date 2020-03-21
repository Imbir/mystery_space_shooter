using UnityEngine;


public class ProjectileController : MonoBehaviour, IPoolable {

    public int damage;
    public float moveSpeed;

    private ObjectPool parentPool;

    private void OnEnable() {
        GetComponent<Rigidbody>().velocity = Vector3.right * moveSpeed;
    }

    public void ReturnToPool() {
        parentPool.Return(gameObject);
    }

    public void SetParentPool(ObjectPool parentPool) {
        this.parentPool = parentPool;
    }
}
