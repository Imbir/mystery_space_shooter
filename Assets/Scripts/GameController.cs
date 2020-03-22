using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class GameController : MonoBehaviour {

    private static GameController instance;

    public static GameController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<GameController>();
            return instance;
        }
    }

    public static int LevelIndex;

    public float startDelay;

    private Level level;
    private GameObject asteroidPrefab;
    private ObjectPool asteroidPool;
    private HashSet<GameObject> spawnedObstacles = new HashSet<GameObject>();
    private bool allSpawned = false;
    private ReactiveProperty<bool> levelCompleted;
    private IReadOnlyReactiveProperty<bool> gameOver;

    private bool AllObstaclesDestroyed {
        get {
            foreach (GameObject obstacle in spawnedObstacles)
                if (obstacle != null && obstacle.activeSelf) return false;
            return true;
        }
    }

    public bool IsGameOver {
        get {
            return gameOver.Value;
        }
    }

    private void Start() {
        level = PlayerData.Instance.GetLevelDescription(LevelIndex);
        
        switch (level.ObstacleType) {
            case AsteroidType.SMALL:
                asteroidPrefab = Resources.Load<GameObject>("Asteroid_Small");
                break;
            case AsteroidType.MEDUIM:
                asteroidPrefab = Resources.Load<GameObject>("Asteroid_Medium");
                break;
            case AsteroidType.BIG:
                asteroidPrefab = Resources.Load<GameObject>("Asteroid_Big");
                break;
        }

        asteroidPool = ObjectPoolManager.Instance.GetObjectPool(asteroidPrefab);

        var playerHealth = FindObjectOfType<PlayerController>().GetPlayerModel().PlayerHealth;

        playerHealth
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(
                playerLife => GameUI.Instance.UpdatePlayerLifeText(playerLife)
            ).AddTo(this);

        levelCompleted = new ReactiveProperty<bool>();

        Observable.EveryUpdate()
            .Subscribe(_ => {
                levelCompleted.Value = allSpawned && AllObstaclesDestroyed;
            });

        gameOver = playerHealth
            .CombineLatest(
                levelCompleted,
                (health, completed) => health <= 0 || completed
            ).ToReactiveProperty();

        gameOver
            .ObserveEveryValueChanged(x => x.Value)
            .Where(x => x)
            .Subscribe(_ => {
                GameUI.Instance.ShowGameOverScreen(playerHealth.Value > 0);
                if (playerHealth.Value > 0)
                    PlayerData.Instance.LevelCompleted(LevelIndex);
                ObjectPoolManager.Instance.Clear();
            }).AddTo(this);

        
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles() {
        yield return new WaitForSeconds(startDelay);
        Random.InitState(level.Seed);
        Vector2 screenSize = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize - asteroidPrefab.transform.localScale.x,
            Camera.main.orthographicSize
        );

        for (int i = 0; i < level.ObstacleCount; i++) {
            Vector3 spawnPosition = new Vector3(
                    screenSize.y * 2 + Random.Range(5.0f, 10.0f),
                    0.0f,
                    Random.Range(-screenSize.x, screenSize.x)
                );

            GameObject obstacle = asteroidPool.Get();
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;
            spawnedObstacles.Add(obstacle);

            yield return new WaitForSeconds(level.SpawnPause);
        }

        allSpawned = true;
    }
}
