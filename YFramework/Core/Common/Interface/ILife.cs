namespace YFramework
{
    public  interface ILife
    {
        void Awake();
        void Start();
        void FixedUpdate(); 
        void Update(); 
        void LaterUpdate();
        void OnDestory();
        void Clear();
    }
}