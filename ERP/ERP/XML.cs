using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace ERP
{
    public class NeuroXml
    {
        static XmlDocument XmlDoc = new XmlDocument();

        private string XmlLoad(string sXmlFile, string sRoot)        //Xml파일이 존재하는 경로/파일 확인(INI파일 대체용)
        {
            try
            {
                FileInfo fi = new FileInfo(sXmlFile);

                if (!fi.Directory.Exists)       //폴더 체크
                {
                    fi.Directory.Create();
                }

                if (!fi.Exists)     //파일 체크
                {
                    MakeFile(sXmlFile, sRoot);
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void MakeFile(string sXmlFile, string sRoot)
        {
            string sXml;

            sXml = "<?xml version=" + @"""1.0""" + " encoding=" + @"""UTF-8""" + "?>\n" + "<" + sRoot + " />";
            XmlDoc.LoadXml(sXml);

            XmlDoc.Save(sXmlFile);
        }

        //Xml 파일 노드 추가(INI파일 대체용)
        public string IniSaveXml(string sXmlFile, string sPath, string sRoot, string sValue)
        {
            XmlNode xNo;

            string sFirstChield;
            string sMsg;

            try
            {
                sMsg = XmlLoad(sXmlFile, "INI");

                if (sMsg != "")
                {
                    MessageBox.Show(sMsg);
                    return sMsg;
                }

                XmlDoc.Load(sXmlFile);

                XmlNode XmlNo = XmlDoc.DocumentElement;

                if (!XmlNo.HasChildNodes) //NODE 가 존재하지 않으면...
                {
                    goto FindFalse;
                }

                for (int i = 0; i < XmlNo.ChildNodes.Count; i++)
                {
                    sFirstChield = XmlNo.ChildNodes.Item(i).Name;

                    if (sPath == sFirstChield)
                    {
                        for (int iCnt = 0; iCnt < XmlNo.SelectSingleNode(sPath).ChildNodes.Count; iCnt++)
                        {
                            if (sRoot == XmlNo.SelectSingleNode(sPath).ChildNodes.Item(iCnt).Name)
                            {
                                XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath + "/" + sRoot);
                                XmlNo.InnerText = sValue;

                                XmlDoc.Save(sXmlFile);

                                return "";
                            }
                        }
                        goto FindTrue;
                    }
                }

                goto FindFalse;

            FindTrue:
                xNo = XmlDoc.CreateElement(sRoot);
                XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath);
                XmlNo.AppendChild(xNo);

                XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath + "/" + sRoot);
                XmlNo.InnerText = sValue;

                XmlDoc.Save(sXmlFile);

                return "";

            FindFalse:
                xNo = XmlDoc.CreateElement(sPath);
                XmlNo = XmlDoc.SelectSingleNode("INI");
                XmlNo.AppendChild(xNo);

                xNo = XmlDoc.CreateElement(sRoot);
                XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath);
                XmlNo.AppendChild(xNo);

                XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath + "/" + sRoot);
                XmlNo.InnerText = sValue;

                XmlDoc.Save(sXmlFile);

                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }

        //Xml 파일 노드 삭제(INI파일 대체용)
        public string IniDeleteXml(string sXmlFile, string sPath, string sRoot)
        {
            string sXml;
            int i;

            try
            {
                XmlDoc.Load(sXmlFile);
                sXml = XmlDoc.InnerXml;

                XmlDoc.LoadXml(sXml);
                XmlNode XmlNo = XmlDoc.DocumentElement;

                for (i = 0; i < XmlNo.SelectSingleNode(sPath).ChildNodes.Count; i++)
                {
                    sXml = XmlNo.SelectSingleNode(sPath).ChildNodes.Item(i).Name;
                    if (sRoot == XmlNo.SelectSingleNode(sPath).ChildNodes.Item(i).Name)
                    {
                        XmlNo = XmlDoc.SelectSingleNode("INI/" + sPath);
                        XmlNo.RemoveChild(XmlNo.ChildNodes.Item(i));
                        XmlDoc.Save(sXmlFile);

                        return "";
                    }
                }

                return "존재하지 않은 Root입니다";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //해당 노드의 값 불러오기
        public string IniReadNodeValue(string sXmlFile, string sSection, string sKey)
        {
            XmlNode XmlNo;

            try
            {
                XmlDoc.Load(sXmlFile);

                XmlNo = XmlDoc.SelectSingleNode("INI/" + sSection + "/" + sKey);

                //XmlNo = XmlDoc.SelectNodes("INI/" + sSection + "/" + sKey);

                string sValue = XmlNo.InnerText.Trim() != null ? XmlNo.InnerText.Trim() : "";

                return XmlNo.InnerText.Trim();

            }
            catch //(Exception e)
            {
                //MessageBox.Show(e.ToString()); 
                return "";
            }
        }
    }
}
