using UnityEngine;


public class Level {
    public int Seed;
    public int ObstacleCount;
    public AsteroidType ObstacleType;
    public float SpawnPause;

    public Level(int levelIndex, int seed) {
        Seed = seed;
        Random.InitState(Seed);
        ObstacleCount = levelIndex + Random.Range(5, 10);
        ObstacleType = (AsteroidType)Random.Range(1, 3);
        SpawnPause = Random.Range(1.0f, 2.0f);
    }
}
