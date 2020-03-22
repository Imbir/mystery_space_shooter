using UnityEngine;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour {

    public int levelIndex;

    private void Start() {
        switch (PlayerData.Instance.GetLevelStatus(levelIndex)) {
            case LevelStatus.LOCKED:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("locked");
                break;
            case LevelStatus.UNLOCKED:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("unlocked"); ;
                break;
            case LevelStatus.COMPLETED:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("completed"); ;
                break;
        }
    }

    public void Click() {
        if (PlayerData.Instance.GetLevelStatus(levelIndex).IsPlayable())
            MainMenuController.Instance.LoadLevel(levelIndex);
    }
}
