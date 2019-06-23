/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：DataCommand
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:17:57 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Infrastructure.EntityFactory;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.DataAccess
{
    public class DataCommand : ICloneable
    {
        protected DbCommand e_DbCommand;
        protected string e_DatabaseName;


        public DataCommand(string databaseName, DbCommand command)
        {
            e_DatabaseName = databaseName;
            e_DbCommand = command;
            e_DbCommand.CommandTimeout = 600;
           
        }

        public DataCommand()
        {
        }

        public DbCommand Command
        {
            get { return e_DbCommand; }
        }

        public object Clone()
        {
            DataCommand cmd = new DataCommand();
            if (e_DbCommand != null)
            {
                if (e_DbCommand is ICloneable)
                {
                    cmd.e_DbCommand = ((ICloneable)e_DbCommand).Clone() as DbCommand;
                }
                else
                {
                    throw new ApplicationException("A class that implements IClonable is expected.");
                }
            }
            cmd.e_DatabaseName = e_DatabaseName;
            return cmd;
        }


        protected Database ActualDatabase
        {
            //get { return DatabaseManager.GetDatabase(e_DatabaseName); }
            get { return new SqlDatabase(e_DatabaseName); }
        }



        #region parameters
        /// <summary>
        /// 追加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="value">参数值</param>
        public void AddInputParameter(string name, DbType dbType, Object value)
        {
            ActualDatabase.AddInParameter(e_DbCommand, name, dbType, value);
        }

        public void AddOutParameter(string name, DbType dbType,int size)
        {
            ActualDatabase.AddOutParameter(e_DbCommand, name, dbType, size);
        }

        /// <summary>
        /// 追加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="dbType">参数类型</param>
        public void AddInputParameter(string name, DbType dbType)
        {
            ActualDatabase.AddInParameter(e_DbCommand, name, dbType);
        }

        /// <summary>
        /// 获取参数的值
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <returns>实际值</returns>
        public Object GetParameterValue(string paramName)
        {
            return ActualDatabase.GetParameterValue(e_DbCommand, paramName);
        }

        /// <summary>
        /// 给参数设置值
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="val">实际值</param>
        public void SetParameterValue(string paramName, Object val)
        {
            ActualDatabase.SetParameterValue(e_DbCommand, paramName, val);
        }

        /// <summary>
        /// 用参数值替换参数例如用1替换@id 
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="val">实际值</param>
        public void ReplaceParameterValue(string paramName, string paramValue)
        {
            if (null != e_DbCommand) e_DbCommand.CommandText = e_DbCommand.CommandText.Replace(paramName, paramValue);
        }
        #endregion


        #region execution
        /// <summary>
        /// Executes the scalar.
        /// Throws an exception if an error occurs.
        /// </summary>
        /// <returns></returns>
        public T ExecuteScalar<T>()
        {
            try
            {
                return (T)ActualDatabase.ExecuteScalar(e_DbCommand);
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
        }


        /// <summary>
        /// returns the number of rows affected.
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            try
            {
                return ActualDatabase.ExecuteNonQuery(e_DbCommand);
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
        }

        /// <summary>
        /// 将DataTable的值直接转换成为实体
        /// </summary>
        /// <returns></returns>
        public T ExecuteEntity<T>() where T : class, new()
        {
            IDataReader reader = null;
            try
            {
                reader = ActualDatabase.ExecuteReader(e_DbCommand);
                if (reader.Read())
                {
                    return EntityBuilder.BuildEntity<T>(reader);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 将DataTable的值直接转换成为实体的一个List
        /// </summary>
        /// <returns></returns>
        public List<T> ExecuteEntityList<T>() where T : class, new()
        {
            IDataReader reader = null;
            try
            {
                reader = ActualDatabase.ExecuteReader(e_DbCommand);
                List<T> list = new List<T>();
                while (reader.Read())
                {
                    T entity = EntityBuilder.BuildEntity<T>(reader);
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        public K ExecuteEntityCollection<T, K>()
            where T : class, new()
            where K : ICollection<T>, new()
        {
            IDataReader reader = null;
            try
            {
                reader = ActualDatabase.ExecuteReader(e_DbCommand);
                ICollection<T> list = new K();
                while (reader.Read())
                {
                    T entity = EntityBuilder.BuildEntity<T>(reader);
                    list.Add(entity);
                }
                return (K)list;
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        public IDataReader ExecuteDataReader()
        {
            try
            {
                return ActualDatabase.ExecuteReader(e_DbCommand);
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
        }

        public DataSet ExecuteDataSet()
        {
            try
            {
                return ActualDatabase.ExecuteDataSet(e_DbCommand);
            }
            catch (Exception ex)
            {
                //todo:处理异常信息
                throw;
            }
        }
        #endregion
    }
}
