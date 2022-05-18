namespace GameFramework
{
    /// <summary>
    /// The structure uses to wrap an object type of <c>T</c>, to give it an id
    /// </summary>
    public struct WrapperById<T>
    {
        public readonly uint id;
        public readonly T item;
        public WrapperById(uint initId, T initItem)
        {
            id = initId;
            item = initItem;
        }
    }
}

