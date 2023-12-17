namespace YFramework
{
    public class AIPath
    {
        private NodeManager mNodeManager;
        public NodeManager nodeManager => mNodeManager;
        private CalcPathManager mCalcPathManager;
        public CalcPathManager calcPathManager => mCalcPathManager;
        public void Init()
        {
            mNodeManager = new NodeManager(this);
            mCalcPathManager = new CalcPathManager(this);
            mNodeManager.Awake();
            mCalcPathManager.Awake();
            mNodeManager.Start();
            mCalcPathManager.Start();
        }
    }
}
