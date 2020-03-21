using UnityEngine;


public class Asteroid : MonoBehaviour {

    public int durability;
    public float tumbleFactor;

    void Start() {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumbleFactor;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile"))
            ProjectileHit(other.gameObject);
    }

    private void ProjectileHit(GameObject projectileObject) {

    }
}
