using UniRx;

public class Player {
    public ReactiveProperty<int> PlayerHealth = new ReactiveProperty<int>(3);
    public Vector3ReactiveProperty PlayerPosition = new Vector3ReactiveProperty();

    public Player() {
        PlayerHealth = new ReactiveProperty<int>(3);
        PlayerPosition = new Vector3ReactiveProperty();
    }
}
