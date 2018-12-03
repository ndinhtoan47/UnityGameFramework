using Serialize.Row;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Utils;

namespace Serialize.Table
{
    [CreateAssetMenu(fileName = "New Serialize Table", menuName = "SQLite/Serialize Table", order = 2)]
    [System.Serializable]
    public class SerializeTable : ScriptableObject
    {
        [SerializeField] public string TableName;
        [SerializeField] public string Version;
        [SerializeField] private List<string> Columns;

        private List<SerializeRow> rows;

        public int Count
        {
            get { return rows != null ? rows.Count : 0; }
        }

        public int FieldCount
        {
            get { return Columns != null ? Columns.Count : 0; }
        }

        public SerializeRow this[int rowID]
        {
            get { return rows[rowID]; }
            set { rows[rowID] = value; }
        }

        public object this[int rowID, int column]
        {
            get { return rows[rowID][column]; }
            set { rows[rowID][column] = value; }
        }

        public object this[int rowID, string column]
        {
            get { return rows[rowID][Columns.IndexOf(column)]; }
            set { rows[rowID][Columns.IndexOf(column)] = value; }
        }

        public string LocationPath
        {
            get { return Application.dataPath + string.Format(@"\DB\{0}.file", TableName); }
        }

        public void Serialize()
        {
            BinarySerialize.Instance.Serialize<SerializeRow>(LocationPath, rows);
        }

        public void Deserialize()
        {
            if (rows != null) rows.Clear();
            BinarySerialize.Instance.Deserialize<SerializeRow>(LocationPath, ref rows);
        }

        public void LoadRow(SerializeRow row)
        {
            if (rows == null) rows = new List<SerializeRow>();
            rows.Add(row);
        }

        public void Clear()
        {
            if (rows != null)
                rows.Clear();
            if (Columns != null)
                Columns.Clear();
        }

        public void LoadColumn(string col)
        {
            if (Columns == null) Columns = new List<string>();
            if (!Columns.Contains(col))
            {
                Columns.Add(col);
            }
        }

        public void SQLInsert(IDbConnection dbConnection, SerializeRow row)
        {
            string[] cols = new string[Columns.Count];
            object[] values = new object[Columns.Count];

            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = Columns[i];
                values[i] = (i < row.Count) ? row[i] : default(int);
            }

            string colsString = SQLUtils.GetSequenceString(",", cols);
            string valuesString = SQLUtils.GetSequenceString(",", values);

            string cmd = SQLUtils.GetInsertCommand(TableName, colsString, valuesString);
            if (dbConnection.InsertValue(cmd))
                rows.Add(row);
        }

        public void SQLUpdate(IDbConnection dbConnection, int index)
        {
            string[] colFields = new string[Columns.Count];
            SerializeRow row = rows[index];
            for (int i = 0; i < colFields.Length; i++)
            {
                colFields[i] = SQLUtils.MakeKeyValuePair(Columns[i], (i < row.Count) ? row[i] : default(int));
            }
            string cmd = SQLUtils.GetUpdateCommand(TableName, index + 1, SQLUtils.GetSequenceString(",", colFields));
            Debug.Log(cmd);
            dbConnection.UpdateValue(cmd);
        }

        public void SQLDelete(IDbConnection dbConnection, int rowId)
        {
            string cmd = SQLUtils.GetDeleteCommand(TableName, string.Format(" _rowid_ = {0}", rowId + 1));
            if (dbConnection.Delete(cmd))
                rows.RemoveAt(rowId);
        }

        public bool SQLAddColumn(IDbConnection dbConnection, string colName, string dataType, string defaultValue, bool notNull = false)
        {
            string cmd = SQLUtils.GetGenerateColumnCommand(TableName, colName, dataType, defaultValue, notNull);
            return dbConnection.AddColumn(cmd);
        }
    }
}