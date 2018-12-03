using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using Serialize.Table;
using Serialize.Row;

namespace Serialize.SQL
{
    public class SQLiteSerializeConnection : MonoBehaviour
    {
        public SqliteConnection DbConnection { get; private set; }

        private string ConnectionString;

        public string DBPath;

        private void Awake()
        {
            DBPath = Application.dataPath + DBPath;
            ConnectionString = "URI=file:" + DBPath;

            if (File.Exists(DBPath))
            {
                DbConnection = new SqliteConnection(ConnectionString);
            }
        }

        public void LoadTable(SerializeTable table)
        {
            try
            {
                DbConnection.Open();
                using (IDbCommand dbCmd = DbConnection.CreateCommand())
                {
                    dbCmd.CommandText = string.Format("SELECT * FROM {0} ", table.TableName);
                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        table.Clear();

                        if (reader.Read())
                        {
                            LoadColumns(reader, table);
                            do
                            {
                                int fieldCount = table.FieldCount;
                                SerializeRow row = new SerializeRow(fieldCount);
                                for (int i = 0; i < fieldCount; i++)
                                {
                                    row.Add(reader.GetValue(i));
                                }
                                table.LoadRow(row);
                            } while (reader.Read());
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception e)
            {
                DbConnection.Close();
#if UNITY_EDITOR
                Debug.Log(e.ToString());
#endif
            }
        }

        public bool CheckVersion(SerializeTable table)
        {
            try
            {
                DbConnection.Open();
                using (IDbCommand dbCmd = DbConnection.CreateCommand())
                {
                    dbCmd.CommandText = string.Format("SELECT Version FROM {0} ", table.TableName);
                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string version = reader.GetValue(0).ToString();
                            if (version.CompareTo(table.Version) == 0)
                            {
                                DbConnection.Close();
                                return true;
                            }
                            else
                            {
                                table.Version = version;
                            }
                        }
                    }
                }
                DbConnection.Close();
                return false;
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.Log(e.ToString());
#endif
                DbConnection.Close();
                return false;
            }
        }

        private void LoadColumns(IDataReader reader, SerializeTable table)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                table.LoadColumn(reader.GetName(i));
            }
        }
    }
}