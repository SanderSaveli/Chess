namespace OFG.ChessPeak
{
    public readonly struct SignalInputLoadScene
    {
        public readonly SceneNames SceneName;

        public SignalInputLoadScene(SceneNames sceneName) => SceneName = sceneName;
    }
}
