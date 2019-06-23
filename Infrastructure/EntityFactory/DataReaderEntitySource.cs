/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：DataReaderEntitySource
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:21:15 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFactory
{
    internal class DataReaderEntitySource : IEntityDataSource
    {
        private class ReaderColumnNameEnumerator : IEnumerator<string>
        {
            IEnumerator m_InternalEnumerator;

            public ReaderColumnNameEnumerator(IDataReader dr)
            {
                DataTable schemaTable = dr.GetSchemaTable();
                m_InternalEnumerator = schemaTable.Rows.GetEnumerator();
            }

            public string Current
            {
                get
                {
                    DataRow row = m_InternalEnumerator.Current as DataRow;
                    return row["ColumnName"] as string;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return m_InternalEnumerator.MoveNext();
            }

            public void Dispose()
            {
                return;
            }

            public void Reset()
            {
                m_InternalEnumerator.Reset();
            }
        }

        private IDataReader e_DataReader;

        public DataReaderEntitySource(IDataReader dr)
        {
            e_DataReader = dr;
        }

        public object this[string columnName]
        {
            get { return e_DataReader[columnName]; }
        }

        public object this[int index]
        {
            get { return e_DataReader[index]; }
        }
        public IEnumerator<string> GetEnumerator()
        {
            return new ReaderColumnNameEnumerator(e_DataReader);
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)this).GetEnumerator();
        }

        public bool ContainsColumn(string columnName)
        {
            DataTable schemaTable = e_DataReader.GetSchemaTable();
            foreach (DataRow row in schemaTable.Rows)
            {
                if (string.Compare(row["ColumnName"].ToString().Trim(), columnName.Trim(), true) == 0)
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (e_DataReader != null)
            {
                e_DataReader.Dispose();
            }
        }
    }
}
