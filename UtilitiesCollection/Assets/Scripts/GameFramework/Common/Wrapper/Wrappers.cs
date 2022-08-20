namespace GameFramework.Common
{
    /// <summary>
    /// The class uses to wrap an object type of <c>T</c>, to give it an id
    /// </summary>
    public class WrapperById<T> : WrapperByKeyValue<uint, T>
    {
        public uint Id => this.Key;
        public WrapperById(uint initId, T initItem) : base(initId, initItem) { }
    }

    public class WrapperByName<T> : WrapperByKeyValue<string, T>
    {
        public string Name => this.Key;
        public WrapperByName(string initName, T initItem) : base(initName, initItem) { }
    }
}

