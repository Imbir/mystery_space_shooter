using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class PlayerData {

    private static PlayerData instance;

    public static PlayerData Instance {
        get {
            if (instance == null)
                instance = LoadPlayerData();
            return instance;
        }
    }

    private static readonly string dataPath = Application.persistentDataPath;
    private static readonly string fileName = "/playerData.bytes";
    private Dictionary<int, int> levelSeeds;
    private Dictionary<int, LevelStatus> levelStatuses;

    public PlayerData() {
        levelSeeds = new Dictionary<int, int>();
        levelStatuses = new Dictionary<int, LevelStatus> {
            [0] = LevelStatus.UNLOCKED
        };
    }

    public Level GetLevelDescription(int levelIndex) {
        if (!levelSeeds.ContainsKey(levelIndex)) {
            levelSeeds[levelIndex] = Random.Range(0, 10000);
            Save();
        }
        return new Level(levelIndex, levelSeeds[levelIndex]);
    }

    public LevelStatus GetLevelStatus(int levelIndex) {
        if (!levelStatuses.ContainsKey(levelIndex))
            levelStatuses[levelIndex] = LevelStatus.LOCKED;
        return levelStatuses[levelIndex];
    }

    public void LevelCompleted(int completed) {
        levelStatuses[completed] = LevelStatus.COMPLETED;
        UnlockLevel(completed + 1);
        Save();
    }

    private void UnlockLevel(int levelToUnlock) {
        if (levelToUnlock < 16)
            levelStatuses[levelToUnlock] = LevelStatus.UNLOCKED;
    }

    private static PlayerData LoadPlayerData() {
        if (File.Exists(dataPath + fileName)) {
            using (FileStream stream = new FileStream(dataPath + fileName, FileMode.Open)) {
                return new BinaryFormatter().Deserialize(stream) as PlayerData;
            }
        }
        else {
            return new PlayerData();
        }
    }

    private void Save() {
        using (FileStream stream = new FileStream(dataPath + fileName, FileMode.Create)) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }
}
