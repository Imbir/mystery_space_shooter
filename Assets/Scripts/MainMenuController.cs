using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    public static MainMenuController Instance { get; private set; }

    [SerializeField]
    private Canvas loadingBarContainer;

    // Start is called before the first frame update
    void Start() {
        Instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void LoadLevel(int levelIndex) {

    }

    private IEnumerator LoadLevelAsync() {
        yield return null;
    }
}
