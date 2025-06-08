namespace OFG.ChessPeak
{
    public interface ISceneLoader
    {
        public void LoadScene(SceneNames scene);
        public void LoadLevel(LevelData levelData, IGameEndHandler handler);
        public void RepeatLevel();
    }
}
