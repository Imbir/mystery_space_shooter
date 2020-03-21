using UnityEngine;
using UniRx;


public class PlayerView : MonoBehaviour {

    public void SetPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }
}
