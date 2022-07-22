namespace GameFramework.Pattern
{
    public interface IGameCommander<Enum> : System.IDisposable where Enum : System.Enum
    {
        void UpdateCommander();
        Enum GetCommanderType();
        void SetController(ICommanderManager<Enum> manager);
    }

    public interface ICommanderManager<Enum> where Enum : System.Enum
    {
        void UpdateCommanders();

        bool RemoveCommander(Enum type);

        bool RegisterCommander(IGameCommander<Enum> commander);
        
        IGameCommander<Enum> GetCommander(Enum type);

        T GetCommander<T>(Enum type) where T : IGameCommander<Enum>;
    }
}