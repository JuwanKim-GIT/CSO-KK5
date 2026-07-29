using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Dynamic;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Drawing;

namespace KK5
{
    public static class Common
    {
        public static string userid = "";
        public static string username = "";
        public static string role = "1";   // '': 일반 '1' super '2':상차  3:공장 


        static Common()
        {
            try
            {
                if (!Directory.Exists(ProjectDir)) Directory.CreateDirectory(ProjectDir);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static string ProjectDir = @"C:\Asrs";
        public const string Plant = "P200";

        public static bool Testmode = false; //for barcode Test

        public static string DBContionString
        {
            get
            { if (Config.DBCONNECTION == "MAIN") return Config.MainDBConnectionString;
                else return Config.BackupDBConnectionString;
            }
        }
        
        public static string strUser = string.Empty;
        public static string strPwd = string.Empty;

        public static string XmlConfigFile
        {
            get
            {
                if (File.Exists(ProjectDir + "\\XMLConfig.xml"))
                {
                    return ProjectDir + "\\XMLConfig.xml";
                }
                else
                { 

                    return Application.StartupPath + "\\XMLConfig.xml";
                }
            }
        }
        public static void ExtractDataToCSV(DataGridView dgv)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].HeaderText + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            string ls = string.Empty;
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        //Append the cells data followed by a comma to delimit.

                        if (dgvRow.Cells[c].Value == null)
                        {
                            dgvRow.Cells[c].Value = "";
                            ls = "";
                            
                        }else
                        {
                            ls = dgvRow.Cells[c].Value.ToString();
                            ls = ls.Replace((char)14, ' ');
                            ls = ls.Replace((char)10, ' ');

                        }

                        if (c == (dgvRow.Cells.Count - 1))
                        {
                            if (ls.IndexOf(',') >= 0)
                            {
                                sb.Append("\"" + ls + "\"" );
                            }
                            else
                            {
                                sb.Append(ls);
                            }
                        }
                        else
                        {
                            if (ls.IndexOf(',') >= 0)
                            {
                                sb.Append("\"" + ls + "\"" + ",");
                            }
                            else
                            {
                                sb.Append(ls + ",");
                            }

                        }

                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
                // Confirm to the user it has been completed.
                MessageBox.Show("CSV file saved.");
            }

        }

        #region -- Datagridview 관련
        public static void RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,

                LineAlignment = StringAlignment.Center
            };
            //get the size of the string
            Size textSize = TextRenderer.MeasureText(rowIdx, grid.Font);
            //if header width lower then string width then resize
            if (grid.RowHeadersWidth < textSize.Width + 30)
            {
                grid.RowHeadersWidth = textSize.Width + 30;
            }
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        #endregion --


        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
              
        //#region structure to array vice versa===================
        //public static byte[] GetBytes<T>(T str)
        //{
        //    int size = Marshal.SizeOf(str);

        //    byte[] arr = new byte[size];

        //    GCHandle h = default(GCHandle);

        //    try
        //    {
        //        h = GCHandle.Alloc(arr, GCHandleType.Pinned);

        //        Marshal.StructureToPtr<T>(str, h.AddrOfPinnedObject(), false);
               
        //    }
        //    finally
        //    {
        //        if (h.IsAllocated)
        //        {
        //            h.Free();
        //        }
        //    }

        //    return arr;
        //}

        //public static T FromBytes<T>(byte[] arr) where T : struct
        //{
        //    T str = default(T);

        //    GCHandle h = default(GCHandle);

        //    try
        //    {
        //        h = GCHandle.Alloc(arr, GCHandleType.Pinned);

        //        str = Marshal.PtrToStructure<T>(h.AddrOfPinnedObject());

        //    }
        //    finally
        //    {
        //        if (h.IsAllocated)
        //        {
        //            h.Free();
        //        }
        //    }

        //    return str;
        //}
        //#endregion

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        //struct TypeScanRollToPlc
        //{
        //    ushort wCodeLen;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        //    public Char[] cBarcode;
        //    ushort wSurfix;
        //}
        public static byte[] getBytes(object str)
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public static T fromBytes<T>(byte[] arr)
        {
            T str = default(T);

            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(arr, 0, ptr, size);

            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);

            return str;
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
        public static void SetUpdateCheckStatus(DataContext dataContext, string Tablename, UpdateCheck updateCheckStatus)
        {
            var tables = dataContext.Mapping.GetTables();
            foreach (var table in tables)
            {
                if (table.TableName.ToString() != Tablename) continue;

                var dataMembers = table.RowType.DataMembers;
                foreach (var dataMember in dataMembers)
                {
                    if (!dataMember.IsPrimaryKey)
                    {
                        //if dataMember.Name nessa

                        var dataMemberType = dataMember.GetType();
                        if (dataMemberType.Name == "AttributedMetaDataMember")
                        {
                            var underlyingAttributeField = dataMember.GetType().GetField("attrColumn", BindingFlags.Instance | BindingFlags.NonPublic);
                            if (underlyingAttributeField != null)
                            {
                                var underlyingAttribute = underlyingAttributeField.GetValue(dataMember) as ColumnAttribute;
                                if (underlyingAttribute != null)
                                { underlyingAttribute.UpdateCheck = updateCheckStatus; }
                            }
                        }
                        else
                        {
                            var underlyingField = dataMember.Type.GetField("updateCheck", BindingFlags.Instance | BindingFlags.NonPublic);
                            if (underlyingField != null)
                            { underlyingField.SetValue(dataMember, updateCheckStatus); }
                        }
                    }
                }
            }
        }

        #region// Memory copy structure to byte array Serialize vice vera. 
        public static void ByteArrayToStructure<T>(byte[] bytearray, ref T obj) where T : struct
        {
            int len = Marshal.SizeOf(obj);
            IntPtr i = Marshal.AllocHGlobal(len);
            Marshal.Copy(bytearray, 0, i, len);
            obj = (T)Marshal.PtrToStructure(i, typeof(T));
            Marshal.FreeHGlobal(i);
        }


        #endregion
   
    }

}
