using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    public static MainMenuController Instance { get; private set; }

    [SerializeField]
    private Canvas loadingBarContainer;

    void Start() {
        Instance = this;
    }

    public void LoadLevel(int levelIndex) {
        GameController.LevelIndex = levelIndex;
        loadingBarContainer.enabled = true;
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync() {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Level");
        while (!loading.isDone) {
            loadingBarContainer.GetComponentInChildren<Slider>().value = loading.progress;
            yield return null;
        }
    }
}
