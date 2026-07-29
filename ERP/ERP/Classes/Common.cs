using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;


namespace ERP
{
    class DebugTextWriter : System.IO.TextWriter
    {
        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debug.Write(new String(buffer, index, count));
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
    class Common
    {
        public static object SyncFile = new object();

        // Get directory files  
        public static string[] GetFiles(string folderPath)
        {
          
            string[] files = System.IO.Directory.GetFiles(folderPath).OrderBy(f => f).ToArray();
                                
            return files;
        }
      

       
        public static void MoveSuccessFile(string sFileName)
        {
            string backdir = Config.BackupDir;
            string sFile = Path.GetFileName(sFileName);

            string sNextDay = backdir + DateTime.Now.AddDays(1).Day.ToString() + "\\" + sFile;
            string destfile = backdir + DateTime.Now.Day.ToString() + "\\" + sFile;

            FileInfo Next_File_Info = new FileInfo(sNextDay);
            FileInfo File_Info = new FileInfo(destfile);
            try
            {
                if (Next_File_Info.Directory.Exists)       //한달 이전데이타 삭제
                {
                    Next_File_Info.Directory.Delete(true);
                }
                if (!File_Info.Directory.Exists)       
                {
                    File_Info.Directory.Create();
                }
                File.Copy(sFileName, destfile, true);
                File.Delete(sFileName);
            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
            }
        }
        public static void MoveFailureFile(string sFileName)
        {
            string backdir = Config.FailDir;
            string sFile = Path.GetFileName(sFileName);

            string sNextDay = backdir +  DateTime.Now.AddDays(1).Day.ToString() + "\\" + sFile;
            string destfile = backdir +  DateTime.Now.Day.ToString() + "\\" + sFile;

            FileInfo Next_File_Info = new FileInfo(sNextDay);
            FileInfo File_Info = new FileInfo(destfile);
            try
            {
                if (Next_File_Info.Directory.Exists)       //한달 이전데이타 삭제
                {
                    Next_File_Info.Directory.Delete(true);
                }
                if (!File_Info.Directory.Exists)
                {
                    File_Info.Directory.Create();
                }
                File.Copy(sFileName, destfile, true);
                File.Delete(sFileName);
            }
            catch (Exception E)
            {
               // MessageBox.Show(E.Message);
            }
        }
        public static void MoveExceptionFile(string sFileName)
        {
            string backdir = Config.ExceptionDir;
            string sFile = Path.GetFileName(sFileName);

            string sNextDay = backdir + DateTime.Now.AddDays(1).Day.ToString() + "\\" + sFile;
            string destfile = backdir + DateTime.Now.Day.ToString() + "\\" + sFile;

            FileInfo Next_File_Info = new FileInfo(sNextDay);
            FileInfo File_Info = new FileInfo(destfile);
            try
            {
                if (Next_File_Info.Directory.Exists)       //한달 이전데이타 삭제
                {
                    Next_File_Info.Directory.Delete(true);
                }
                if (!File_Info.Directory.Exists)
                {
                    File_Info.Directory.Create();
                }
                File.Copy(sFileName, destfile, true);
                File.Delete(sFileName);
            }
            catch (Exception E)
            {
               // MessageBox.Show(E.Message);
            }
        }
        public void SetUpdateCheckStatus(DataContext dataContext, string Tablename, UpdateCheck updateCheckStatus)
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
    }
}
