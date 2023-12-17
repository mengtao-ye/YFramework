namespace YFramework
{
    public abstract class BaseUndo  : IUnDo
    {
        public string name { get; private set; }
        public BaseUndo(string name)
        {
            this.name = name;
        }
        public abstract void Undo(); //撤回
        public abstract void Do(); //执行代码快
        public abstract void Redo(); //反撤回
    }
}