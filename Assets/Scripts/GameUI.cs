using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public Canvas gameOverMenu;

    public void UpdatePlayerLifeText(int playerLife) {
        playerLifeText.text = playerLife.ToString();
    }

    public void ShowGameOverScreen(bool won) {
        ObjectPoolManager.Instance.Clear();
        if (won) gameOverMenu.GetComponentInChildren<Text>().text = "You win!";
        else gameOverMenu.GetComponentInChildren<Text>().text = "You lost :(\nTry again!";
        gameOverMenu.enabled = true;
    }

    public void GoToMainMenu() {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
