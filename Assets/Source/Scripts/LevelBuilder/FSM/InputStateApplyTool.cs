namespace OFG.ChessPeak.LevelBuild
{
    public class InputStateApplyTool : BuilderInputState
    {
        public InputStateApplyTool(BuilderInputFSM_Context context) : base(context)
        { }
        public override void OnUpdate()
        {
            ToolController.OnUpdate();
        }
    }
}
