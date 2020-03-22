public enum LevelStatus {
    LOCKED,
    UNLOCKED,
    COMPLETED,
}

public static class EnumExtensions {
    public static bool IsPlayable(this LevelStatus status) {
        if (status == LevelStatus.LOCKED) return false;
        else return true;
    }
}
