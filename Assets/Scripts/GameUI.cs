using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour {

    private static GameUI instance;

    public static GameUI Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<GameUI>();
            return instance;
        }
    }

    public Text playerLifeText;

    private void Start() {
    }

    public void UpdatePlayerLifeText(int playerLife) {
        playerLifeText.text = playerLife.ToString();
    }
}
