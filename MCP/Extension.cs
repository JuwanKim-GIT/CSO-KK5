using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.Linq;
using System.Data.Common;
using System.Linq.Expressions;
using System.Dynamic;

namespace MCP
{
  
    public static class Extension
    {
        #region sqldatareader-------------------------------------
        public static string GetStringSafe(this IDataReader reader, int colIndex)
        {
            return GetStringSafe(reader, colIndex, string.Empty);
        }

        public static string GetStringSafe(this IDataReader reader, int colIndex, string defaultValue)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return defaultValue;
        }

        public static string GetStringSafe(this IDataReader reader, string indexName)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName));
        }

        public static string GetStringSafe(this IDataReader reader, string indexName, string defaultValue)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName), defaultValue);
        }
        #endregion

        #region DataRow ------------------------------------------------
        public static string GetStringSafe(this DataRow reader, int colIndex)
        {
            return GetStringSafe(reader, colIndex, string.Empty);
        }

        public static string GetStringSafe(this DataRow dr, int colIndex, string defaultValue)
        {
            if (!dr.IsNull(colIndex))
                return dr.Field<string>(colIndex);
            else
                return defaultValue;
        }

        public static string GetStringSafe(this DataRow dr, string indexName)
        {
            return GetStringSafe(dr, dr.Table.Columns[indexName].Ordinal);
        }

        public static string GetStringSafe(this DataRow dr, string indexName, string defaultValue)
        {
            return GetStringSafe(dr, dr.Table.Columns[indexName].Ordinal, defaultValue);
        }
        #endregion
        public static void open(this DbConnection con)
        {
            if (con.State == ConnectionState.Open) return;
            else con.Open();
        }

        //DynamicRow class is similiar to ExpandoObject but with addition of indexer
        public class DynamicRow : DynamicObject
        {
            private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

            public object this[string propertyName]
            {
                get
                {
                    object result = null;
                    TryGetMember(propertyName, out result);
                    return result;
                }
                set { TrySetMember(propertyName, value); }
            }
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return TryGetMember(binder.Name, out result);
            }

            private bool TryGetMember(string propertyName, out object result)
            {
                return _data.TryGetValue(propertyName.ToLower(), out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                return TrySetMember(binder.Name, value);
            }

            private bool TrySetMember(string propertyName, object value)
            {
                _data[propertyName.ToLower()] = value;
                return true;
            }

        }
        public static IEnumerable<dynamic> ExecuteQuery(this DataContext ctx, string query, DbParameter[] parameters = null)
        {
            using (DbCommand cmd = ctx.Connection.CreateCommand())
            {
                cmd.CommandText = query;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                ctx.Connection.open();
                using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (rdr.Read())
                    {
                        dynamic row = new DynamicRow();
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[rdr.GetName(i)] = rdr[i];
                        }
                        yield return row;
                    }
                }
                
            }
        }

        //public static IEnumerable<object[]> ExecuteQuery(this DataContext ctx, string query)
        //{
        //    using (DbCommand cmd = ctx.Connection.CreateCommand())
        //    {
        //        cmd.CommandText = query;
        //        ctx.Connection.open();

        //        using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //        {
        //            while (rdr.Read())
        //            {
        //                object[] res = new object[rdr.FieldCount];
        //                rdr.GetValues(res);
        //                yield return res;
        //            }
        //        }
        //    }
        //}


        public static T Def<T>(this SqlDataReader r, int ord)
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) return default(T);
            return ((INullable)t).IsNull ? default(T) : (T)t;
        }

        public static T? Val<T>(this SqlDataReader r, int ord) where T : struct
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) return null;
            return ((INullable)t).IsNull ? (T?)null : (T)t;
        }

        public static T Ref<T>(this SqlDataReader r, int ord) where T : class
        {
            var t = r.GetSqlValue(ord);
            if (t == DBNull.Value) return null;
            return ((INullable)t).IsNull ? null : (T)t;
        }

        public static T CheckNull<T>(object obj)
        {
            return (obj == DBNull.Value ? default(T) : (T)obj);
        }

        
        private static bool IsNullableType(Type theValueType)
        {
            return (theValueType.IsGenericType && theValueType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        /// Returns the value, of type T, from the SqlDataReader, accounting for both generic and non-generic types.
        /// </summary>
        /// <typeparam name="T">T, type applied</typeparam>
        /// <param name="theReader">The SqlDataReader object that queried the database</param>
        /// <param name="theColumnName">The column of data to retrieve a value from</param>
        /// <returns>T, type applied; default value of type if database value is null</returns>
        public static T GetValue<T>(this SqlDataReader theReader, string theColumnName)
        {
            // Read the value out of the reader by string (column name); returns object
            object theValue = theReader[theColumnName];

            // Cast to the generic type applied to this method (i.e. int?)
            Type theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != theValue)
            {
                // We have a null, do we have a nullable type for T?
                if (!IsNullableType(theValueType))
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(theValue, theValueType);
                }
                else
                {
                    // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                    NullableConverter theNullableConverter = new NullableConverter(theValueType);

                    return (T)Convert.ChangeType(theValue, theNullableConverter.UnderlyingType);
                }
            }
            //string type then return String.Empty
            if (typeof(T) == typeof(String)) return (T)(object)String.Empty;

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }
        public static T GetValue<T>(this DataRow dataRow, string theColumnName)
        {
            // Read the value out of the reader by string (column name); returns object
            object theValue = dataRow[theColumnName];

            // Cast to the generic type applied to this method (i.e. int?)
            Type theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != theValue)
            {
                // We have a null, do we have a nullable type for T?
                if (!IsNullableType(theValueType))
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(theValue, theValueType);
                }
                else
                {
                    // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                    NullableConverter theNullableConverter = new NullableConverter(theValueType);

                    return (T)Convert.ChangeType(theValue, theNullableConverter.UnderlyingType);
                }
            }
            //string type then return String.Empty
            if (typeof(T) == typeof(String)) return (T)(object)String.Empty;

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }
        public static T GetValue<T>(this DataRow dataRow, int col)
        {
            // Read the value out of the reader by string (column name); returns object
            object theValue = dataRow[col];

            // Cast to the generic type applied to this method (i.e. int?)
            Type theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != theValue)
            {
                // We have a null, do we have a nullable type for T?
                if (!IsNullableType(theValueType))
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(theValue, theValueType);
                }
                else
                {
                    // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                    NullableConverter theNullableConverter = new NullableConverter(theValueType);
                    
                    return (T)Convert.ChangeType(theValue, theNullableConverter.UnderlyingType);
                }
            }
            //string type then return String.Empty
            if (typeof(T) == typeof(String)) return (T)(object)String.Empty;

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }

    }
}
