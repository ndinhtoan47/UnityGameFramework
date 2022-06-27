#define GF_USE_NEWTONSOFT_JSON
//// Let comment/ uncomment below line to use/ remove this helper
// #undef GF_USE_NEWTONSOFT_JSON

namespace GameFramework.Utilities
{
#if GF_USE_NEWTONSOFT_JSON
    using Newtonsoft.Json.Linq;
    using GameFramework.Logging;

    public static class JSONHelper
    {
        public static string GetString(this JToken token, string key, string def = null)
        {
            try
            {
                return token.Value<string>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetString] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static int GetInt(this JToken token, string key, int def = 0)
        {
            try
            {
                return token.Value<int?>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetInt] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static uint GetUInt(this JToken token, string key, uint def = 0)
        {
            try
            {
                return token.Value<uint?>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetUInt] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static long GetLong(this JToken token, string key, long def = 0)
        {
            try
            {
                return token.Value<long?>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetLong] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static float GetFloat(this JToken token, string key, float def = 0)
        {
            try
            {
                return token.Value<float?>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetFloat] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static bool GetBool(this JToken token, string key, bool def = false)
        {
            try
            {
                return token.Value<bool?>(key) ?? def;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[GetBool] Parse Failed => Return default\nError:{0}", e.ToString()));
                return def;
            }
        }
        public static JToken Select(this JToken token, string key)
        {
            try
            {
                return token.SelectToken(key) ?? null;
            }
            catch (System.Exception e)
            {
                UnityLog.LogW(string.Format("[Select] Parse Failed => Return default\nError:{0}", e.ToString()));
                return null;
            }
        }
    }
#endif
}
