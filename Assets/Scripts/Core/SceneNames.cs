namespace TappyTale
{
    // Centralized scene name definitions to avoid typos
    // and facilitate scene management.

    /// Usage: SceneManager.LoadScene(SceneNames.MainMenu);
    public static class SceneNames
    {
        public const string Bootstrap = "Bootstrap";
        public const string MainMenu = "MainMenu";
        public const string CutScene = "CutScene";
        public const string Arena = "Arena";
        public const string Victory = "VictoryScreen";
        public const string Death = "DeathScreen";
        public const string Pause = "PauseScreen";
    }
}
