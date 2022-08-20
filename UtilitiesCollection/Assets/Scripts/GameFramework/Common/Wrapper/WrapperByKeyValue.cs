namespace GameFramework.Common
{
    public class WrapperByKeyValue<K, V>
    {
        public readonly K Key;
        public readonly V Value;

        public WrapperByKeyValue(K inKey, V inValue)
        {
            Key = inKey;
            Value = inValue;
        }
    }
}
