using UnityEngine;


public class ProjectileController : MonoBehaviour {

    public int damage;
    public float moveSpeed;

    private void Start() {
        GetComponent<Rigidbody>().velocity = Vector3.right * moveSpeed;
    }
}
