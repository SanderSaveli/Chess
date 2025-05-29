namespace OFG.ChessPeak
{
    public interface ITutorialManager
    {
        public TutorialSO GetTutorial(int levelNumber);
        public bool LevlHasTutorial(int levelNumber);
        public void CompleteTutorial(int levelNumber);
        public bool IsNeedToShowTutorial(int levelNumber);
    }
}
