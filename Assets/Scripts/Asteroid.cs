using UnityEngine;
using UniRx;


public class Asteroid : MonoBehaviour {

    public int durability;
    public float tumbleFactor;

    void Start() {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumbleFactor;
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
        // return to pool
        Destroy(gameObject);
    }
}
