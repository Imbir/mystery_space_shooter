using UnityEngine;


public class AsteroidController : MonoBehaviour, IPoolable {

    public float moveSpeed;
    public int durability;
    public float tumbleFactor;
    public GameObject explosionPrefab;

    private ObjectPool parentPool;
    private ObjectPool explosionPool;

    private void Start() {
        explosionPool = ObjectPoolManager.Instance.GetObjectPool(explosionPrefab);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumbleFactor;
    }

    private void OnEnable() {
        GetComponent<Rigidbody>().velocity = Vector3.left * moveSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile"))
            ProjectileHit(other.gameObject);
        if (other.CompareTag("Player"))
            PlayerHit(other.gameObject);

    }

    private void ProjectileHit(GameObject projectile) {
        durability -= projectile.GetComponent<ProjectileController>().damage;
        projectile.GetComponent<IPoolable>().ReturnToPool();
        if (durability <= 0) Die();
    }

    private void PlayerHit(GameObject player) {
        player.GetComponent<PlayerController>().AsteroidHit();
        Die();
    }

    private void Die() {
        GameObject explosion = explosionPool.Get();
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        ReturnToPool();
    }

    public void SetParentPool(ObjectPool parentPool) {
        this.parentPool = parentPool;
    }

    public void ReturnToPool() {
        parentPool.Return(gameObject);
    }
}
