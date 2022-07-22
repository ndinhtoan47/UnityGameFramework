namespace GameFramework.Networking.API
{
    public enum APIMethod
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3,
    }

    public class EndPoint : GameFramework.Common.ICloneable<EndPoint>
    {    
        public string ResourceAt;
        public APIMethod Method;
        public EndPoint Clone()
        {
            return new EndPoint()
            {
                ResourceAt = this.ResourceAt,
                Method = this.Method,
            };
        }

        public string Format(params string[] args)
        {
            return string.Format(ResourceAt, args);
        }
    }
}