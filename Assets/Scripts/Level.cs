using UnityEngine;


[System.Serializable]
public class Level {
    private int id;
    private int seed = -1;
    private bool rolled = false;
    private int obstacleCount = 0;
    private AsteroidType obstacleType = AsteroidType.NONE;

    public LevelStatus Status { get; set; }

    public Level(int id, int seed) {

    }

    public int ObstacleCount {
        get => 0;
    }
    public AsteroidType ObstacleType {
        get => 0;
    }
    

    public Level() {
        seed = Random.Range(0, 10000);
    }
}
