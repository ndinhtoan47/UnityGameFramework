

using Json.Interface;
using UnityEngine;

namespace Json
{
    public abstract class JSONBase : IJSON<JSONBase>
    {
        public abstract JSONBase FromJSON();

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
        
    }
}
