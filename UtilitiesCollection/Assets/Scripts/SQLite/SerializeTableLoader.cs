using Serialize.SQL;
using Serialize.Table;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Serialize.Loader
{
    public sealed class SerializeTableLoader : MonoBehaviour
    {
        public SQLiteSerializeConnection SQL;
        public SerializeTable TableData;

        private void OnApplicationQuit()
        {
            TableData.Serialize();
        }

        public void AddColumn(string colName, string dataType, string defaultValue, bool notNull = false)
        {
            if (TableData.SQLAddColumn(SQL.DbConnection, colName, dataType, defaultValue, notNull))
            {
                TableData.Version = null;
                Load();
            }
        }

        public void Load()
        {
            if (!SQL.CheckVersion(TableData))
            {
                SQL.LoadTable(TableData);
                if (File.Exists(TableData.LocationPath))
                    FileUtil.DeleteFileOrDirectory(TableData.LocationPath);
            }
            else
            {
                TableData.Deserialize();
            }
        }
    }
}