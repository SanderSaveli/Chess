namespace OFG.ChessPeak
{
    public readonly struct SignalStartLoadScene
    {
        public readonly SceneNames SceneName;

        public SignalStartLoadScene(SceneNames sceneName)
        {
            SceneName = sceneName;
        }
    }
}
