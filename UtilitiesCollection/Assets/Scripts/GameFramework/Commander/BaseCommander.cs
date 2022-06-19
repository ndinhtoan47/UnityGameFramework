namespace GameFramework.Pattern
{
    public abstract class BaseCommander<Enum> : IGameCommander<Enum> where Enum : System.Enum
    {
        protected ICommanderManager<Enum> _manager;

        public virtual void Dispose()
        {
            throw new System.NotImplementedException();
        }
        public virtual void SetController(ICommanderManager<Enum> manager)
        {
            _manager = manager;
        }
        public virtual void UpdateCommander() { }
        public abstract Enum GetCommanderType();
    }

}