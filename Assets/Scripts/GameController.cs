using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

    public static int LevelIndex;

    public float startDelay;

    private Level level;
    private GameObject asteroidPrefab;
    private ObjectPool asteroidPool;
    private HashSet<GameObject> spawnedObstacles = new HashSet<GameObject>();
    private bool allSpawned = false;

    private bool AllObstaclesDestroyed {
        get {
            foreach (GameObject obstacle in spawnedObstacles)
                if (obstacle.activeSelf) return false;
            return true;
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

        StartCoroutine(SpawnObstacles());
    }

    private void Update() {
        if (allSpawned && AllObstaclesDestroyed) {
            // win
        }
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
