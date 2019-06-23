/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：DataRowEntitySource
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:21:47 AM
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
    internal class DataRowEntitySource : IEntityDataSource
    {
        private class RowColumnNameEnumerator : IEnumerator<string>
        {
            private IEnumerator m_InternalEnumeator;

            public RowColumnNameEnumerator(DataRow dr)
            {
                m_InternalEnumeator = dr.Table.Columns.GetEnumerator();
            }

            public string Current
            {
                get
                {
                    DataColumn column = m_InternalEnumeator.Current as DataColumn;
                    return column.ColumnName;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return m_InternalEnumeator.MoveNext();
            }

            public void Reset()
            {
                m_InternalEnumeator.Reset();
            }

            public void Dispose()
            {
                return;
            }
        }


        private DataRow e_DataRow;

        public DataRowEntitySource(DataRow dr)
        {
            e_DataRow = dr;
        }

        public object this[string columnName]
        {
            get { return e_DataRow[columnName]; }
        }

        public object this[int iIndex]
        {
            get { return e_DataRow[iIndex]; }
        }

        public bool ContainsColumn(string columnName)
        {
            return e_DataRow.Table.Columns.Contains(columnName);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new RowColumnNameEnumerator(e_DataRow);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)this).GetEnumerator();
        }

        public void Dispose()
        {
            return;
        }
    }
}
