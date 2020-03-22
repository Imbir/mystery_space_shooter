using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour {

    public static GameUI Instance { get; private set; }

    public Text playerLifeText;

    private void Start() {
        Instance = this;
    }

    public void UpdatePlayerLifeText(int playerLife) {
        playerLifeText.text = playerLife.ToString();
    }
}
