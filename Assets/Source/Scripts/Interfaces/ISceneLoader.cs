namespace OFG.ChessPeak
{
    public interface ISceneLoader
    {
        public void LoadScene(SceneNames scene);
        public void LoadGameLevel(int levelNumber);
        public void LoadGameLevelDirectly(LevelData levelData, int levelNumber);
        public void LoadCustomLevel(LevlelNetworkData data);
    }
}
