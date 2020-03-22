using System.Collections;
using UnityEngine;


public class Explosion : MonoBehaviour, IPoolable {

    private float lifetime = 1.0f;
    private ObjectPool parentPool;

    private void OnEnable() {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DieAfterLifetime());
    }

    private IEnumerator DieAfterLifetime() {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    public void ReturnToPool() {
        parentPool.Return(gameObject);
    }

    public void SetParentPool(ObjectPool parentPool) {
        this.parentPool = parentPool;
    }
}
