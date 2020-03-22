using System.Collections;
using UniRx;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float attackSpeed;
    public float tiltFactor;
    public Transform[] projectileSources;
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    private ObjectPool projectileObjectPool;
    private Player playerModel;
    private PlayerView playerView;
    private bool isShooting;
    private float xMin, xMax, zMin, zMax;

    private void Start() {
        playerModel = new Player();
        playerView = GetComponent<PlayerView>();
        SetMovementLimits();

        projectileObjectPool = ObjectPoolManager.Instance.GetObjectPool(projectilePrefab);

        playerModel.PlayerHealth
            .ObserveEveryValueChanged(x => x.Value)
            .Where(x => x <= 0)
            .Subscribe(_ => Die())
            .AddTo(this);

        playerModel.PlayerPosition
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(position => playerView.SetPosition(position))
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => !GameController.Instance.IsGameOver)
            .Select(_ => Input.GetMouseButton(0))
            .Subscribe(isMouseDown => isShooting = isMouseDown).AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => !GameController.Instance.IsGameOver)
            .Subscribe(_ => {
                HandleInput();
            }).AddTo(this);

        StartCoroutine(ShootThread());
    }

    public Player GetPlayerModel() {
        return playerModel;
    }

    private void Die() {
        var explosion = ObjectPoolManager.Instance.GetObjectPool(explosionPrefab).Get();
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    public void HandleInput() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rigidbody.rotation = Quaternion.Euler(moveHorizontal * -tiltFactor, 0.0f, 0.0f);

        playerModel.PlayerPosition.Value = new Vector3(
            Mathf.Clamp(playerModel.PlayerPosition.Value.x + moveVertical * moveSpeed * Time.deltaTime, xMin, xMax),
            0.0f,
            Mathf.Clamp(playerModel.PlayerPosition.Value.z - moveHorizontal * moveSpeed * Time.deltaTime, zMin, zMax)
        );
    }

    private IEnumerator ShootThread() {
        while (true) {
            if (isShooting) Shoot();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void Shoot() {
        foreach (Transform projectileSource in projectileSources) {
            GameObject projectile = projectileObjectPool.Get();
            projectile.transform.position = projectileSource.transform.position;
            projectile.transform.rotation = projectileSource.transform.rotation;

        }
    }

    public void AsteroidHit() {
        playerModel.PlayerHealth.Value -= 1;
    }

    private void SetMovementLimits() {
        var screenSize = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize + transform.localScale.x / 2,
            Camera.main.orthographicSize + transform.localScale.z / 2
        );
        zMin = -screenSize.x;
        zMax = screenSize.x;
        xMin = -screenSize.y + Camera.main.transform.position.x;
        xMax = (screenSize.y + Camera.main.transform.position.x) * 0.75f;
    }
}
