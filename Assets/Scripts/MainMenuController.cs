using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;


public class MainMenuController : MonoBehaviour {

    private static MainMenuController instance;

    public static MainMenuController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<MainMenuController>();
            return instance;
        }
    }

    public Canvas loadingBarContainer;

    public void LoadLevel(int levelIndex) {
        GameController.LevelIndex = levelIndex;
        loadingBarContainer.enabled = true;

        SceneManager.LoadSceneAsync("Level").AsAsyncOperationObservable()
            .Subscribe(loading => {
                if (loadingBarContainer != null)
                    loadingBarContainer.GetComponentInChildren<Slider>().value = loading.progress;
            });
    }
}
