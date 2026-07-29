using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace ERP
{
    class XmlParse
    {

        public bool threading = false;
        public FMain fmain = null;
        public XmlParse(FMain f)
        {
            fmain = f;
        }
        public string removeZeroBeforestr(string str)
        {
            string c;
            int find = -1;

            if (str.Length == 1) return str;

            for (int i = 0; i < str.Length; i++)
            {
                c = str.Substring(i, 1);
                if (i == 0 && c != "0")
                {
                    find = 0;
                    break;
                }
                if (c == "0") continue;

                find = i;
                break;
            }
            if (find == -1) return "0";

            return str.Substring(find).Trim();

        }


        public string GetDocType(string path)
        {
            string find = string.Empty;
            try
            {               
                XDocument xd = XDocument.Load(path);                

                var q = (from d in xd.Root.Descendants("EDI_DC40")
                         select d.Element("IDOCTYP")).SingleOrDefault();
                if (q == null) return "";

                //var q = (from d in xd.Root.Descendants("EDI_DC40")
                //         select d.Element("IDOCTYP")).FirstOrDefault();
                
                return q.Value.ToString();
            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
                return "";
            }
        }

        //This method is to handle if element is missing
        public string ElementValueNull(XElement element)
        {
            if (element != null)
                return element.Value;

            return "";
        }

        //This method is to handle if attribute is missing
        public string AttributeValueNull(XElement element, string attributeName)
        {
            if (element == null)
                return "";
            else
            {
                XAttribute attr = element.Attribute(attributeName);
                return attr == null ? "" : attr.Value;
            }
        }


        // mimast parsing--------------------------------------------
        public bool ParseZMATMS(string path)
        {
            try
            {
                XDocument xd = XDocument.Load(path);

                var q = from doc in xd.Root.Descendants("IDOC")
                        select new
                        {
                            docnm = doc.Element("EDI_DC40").Element("DOCNUM").Value.Trim(),
                            credat = doc.Element("EDI_DC40").Element("CREDAT").ElementValueNull(),
                            cretim = doc.Element("EDI_DC40").Element("CRETIM").ElementValueNull(),

                            q2 = from doc2 in doc.Descendants("E1MARAM")
                                 select new
                                 {
                                     matnr = doc2.Element("MATNR").Value.Trim(),
                                     matnrdesc = doc2.Element("E1MAKTM").ElementElement().Element("MAKTX").ElementValueNull(),
                                     mtype = doc2.Element("MTART").ElementValueNull(),
                                     mgroup = doc2.Element("MATKL").ElementValueNull(),
                                     BaseUnit = doc2.Element("MEINS").ElementValueNull(),
                                     SizeDim = doc2.Element("GROES").ElementValueNull(),
                                     gwgt = doc2.Element("BRGEW").ElementValueNull0(),
                                     nwgt = doc2.Element("NTGEW").ElementValueNull0(),
                                     wunit = doc2.Element("GEWEI").ElementValueNull(),
                                     vol = doc2.Element("VOLUM").ElementValueNull0(),
                                     vunit = doc2.Element("VOLEH").ElementValueNull()                                     
                                 }
                        };

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    string credat;
                    string cretim;
                    string ls;
                    string[] strs;
                   
                    foreach (var r in q)
                    {
                        credat = r.credat;
                        cretim = r.cretim;

                        ls = DateTime.Now.ToString("yyyyMMddhhmmss");

                        credat = ls.Substring(0, 8);
                        cretim = ls.Substring(8, 6);

                        foreach (var x in r.q2)
                        {
                            string matnr = removeZeroBeforestr(x.matnr);
                            string str = "";

                            mimast mast = db.mimasts.SingleOrDefault(m => m.mast_cd == matnr);
                            if (mast == null)
                            {
                                mimast t = new mimast();

                                t.mast_date = credat;
                                t.mast_time = cretim;

                                t.mast_cd = matnr;

                                if (x.matnrdesc.Length > 40)
                                    t.mast_desc = x.matnrdesc.Substring(0, 40);
                                else
                                    t.mast_desc = x.matnrdesc;

                                if (x.mtype.Length > 4)
                                    t.mast_type = x.mtype.Substring(0, 4);
                                else
                                    t.mast_type = x.mtype;

                                if (x.mgroup.Length > 9)
                                    t.mast_grp = x.mgroup.Substring(0, 9);
                                else
                                    t.mast_grp = x.mgroup;

                                t.mast_old = "";

                                if (x.BaseUnit.Length > 3)
                                    t.mast_bunit = x.BaseUnit.Substring(0, 3);
                                else
                                    t.mast_bunit = x.BaseUnit;

                                if (x.SizeDim.Length > 32)
                                    t.mast_szdm = x.SizeDim.Substring(0, 32);
                                else
                                    t.mast_szdm = x.SizeDim;

                                if (x.gwgt.Trim() == "")
                                    t.mast_gwgt = 0.000m;
                                else
                                    t.mast_gwgt = Convert.ToDecimal(x.gwgt);

                                if (x.nwgt.Trim() == "")
                                    t.mast_nwgt = 0.000m;
                                else
                                    t.mast_nwgt = Convert.ToDecimal(x.nwgt);

                                if (x.wunit.Length > 3)
                                    t.mast_wunit = x.wunit.Substring(0, 3);
                                else
                                    t.mast_wunit = x.wunit;

                                if (x.vol.Trim() == "")
                                    t.mast_vol = 0.000m;
                                else
                                    t.mast_vol = Convert.ToDecimal(x.vol);

                                if (x.vunit.Length > 3)
                                    t.mast_vunit = x.vunit.Substring(0, 3);
                                else
                                    t.mast_vunit = x.vunit;
                             
                                t.mast_flag = "0";
                                t.mast_canqty = 0;


                                str = x.matnrdesc;
                                //strs = x.matnrdesc.Trim().Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                                //if (strs.Length != 0)
                                //    str = strs[strs.Length - 1].Trim();

                                strs = x.matnrdesc.Trim().Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                                if (strs.Length != 0)
                                {
                                    str = strs[strs.Length - 1].Trim();
                                    try
                                    {
                                        if ((str.Length < 4) && str.IndexOf("PG") >= 0)
                                        {
                                            if (strs.Length >= 2)
                                                str = strs[strs.Length - 2].Trim() + " " + str;
                                        }
                                    }
                                    catch (Exception E) { }
                                    finally { }
                                }

                                if (str.Length > 24)
                                    t.mast_desc1 = str.Substring(0, 24);
                                else
                                    t.mast_desc1 = str;
                           
                                db.mimasts.InsertOnSubmit(t);
                                db.SubmitChanges();

                             
                            }
                            else
                            {
                                mast.mast_date = credat;
                                mast.mast_time = cretim;

                                if (x.matnrdesc.Length > 40)
                                    mast.mast_desc = x.matnrdesc.Substring(0, 40);
                                else
                                    mast.mast_desc = x.matnrdesc;

                                if (x.mtype.Length > 4)
                                    mast.mast_type = x.mtype.Substring(0, 4);
                                else
                                    mast.mast_type = x.mtype;

                                if (x.mgroup.Length > 9)
                                    mast.mast_grp = x.mgroup.Substring(0, 9);
                                else
                                    mast.mast_grp = x.mgroup;

                                mast.mast_old = "";

                                if (x.BaseUnit.Length > 3)
                                    mast.mast_bunit = x.BaseUnit.Substring(0, 3);
                                else
                                    mast.mast_bunit = x.BaseUnit;

                                if (x.SizeDim.Length > 32)
                                    mast.mast_szdm = x.SizeDim.Substring(0, 32);
                                else
                                    mast.mast_szdm = x.SizeDim;

                                if (x.gwgt.Trim() == "")
                                    mast.mast_gwgt = 0.000m;
                                else
                                    mast.mast_gwgt = Convert.ToDecimal(x.gwgt);

                                if (x.nwgt.Trim() == "")
                                    mast.mast_nwgt = 0.000m;
                                else
                                    mast.mast_nwgt = Convert.ToDecimal(x.nwgt);

                                if (x.wunit.Length > 3)
                                    mast.mast_wunit = x.wunit.Substring(0, 3);
                                else
                                    mast.mast_wunit = x.wunit;

                                if (x.vol.Trim() == "")
                                    mast.mast_vol = 0.000m;
                                else
                                    mast.mast_vol = Convert.ToDecimal(x.vol);

                                if (x.vunit.Length > 3)
                                    mast.mast_vunit = x.vunit.Substring(0, 3);
                                else
                                    mast.mast_vunit = x.vunit;

                                str = x.matnrdesc;
                                ////strs = x.matnrdesc.Trim().Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                                ////if (strs.Length != 0)
                                ////    str = strs[strs.Length - 1].Trim();

                                strs = x.matnrdesc.Trim().Split(new char[1] { ' ' }, StringSplitOptions.None).ToArray<string>();
                                if (strs.Length != 0)
                                {
                                    str = strs[strs.Length - 1].Trim();
                                    try
                                    {
                                        if ((str.Length < 4) && str.IndexOf("PG") >= 0)
                                        {
                                            if (strs.Length >= 2)
                                                str = strs[strs.Length - 2].Trim() + " " + str;
                                        }
                                    }
                                    catch (Exception E) { }
                                    finally { }
                                }

                                if (str.Length > 24)
                                    mast.mast_desc1 = str.Substring(0, 24);
                                else
                                    mast.mast_desc1 = str;

                                db.SubmitChanges();

                                decimal vol = Convert.ToDecimal(x.vol);
                                db.ExecuteCommand(@"update miplti set plti_pksz = {0} where plti_prod = {1}", vol, matnr);

                            }
                        }                     
                    }
                }
                return true;
            }
            catch (Exception E)
            {
                EventLog.Event_Log("ERPIF.txt", path, E.Message, true);
                return false; 
            }        
        }

        // miwmto parsing--------------------------------------------
        public bool ParseWMTO(string path)
        {        
            try
            {
                XDocument xd = XDocument.Load(path);
                var q = from doc in xd.Root.Descendants("IDOC")
                        select new
                        {
                            docnum = doc.Element("EDI_DC40").Element("DOCNUM").Value.Trim(),
                            credat = doc.Element("EDI_DC40").Element("CREDAT").ElementValueNull(),
                            cretim = doc.Element("EDI_DC40").Element("CRETIM").ElementValueNull(),

                            lgnum = doc.Element("E1LTORH").ElementElement().Element("LGNUM").ElementValueNull(),
                            tanum = doc.Element("E1LTORH").ElementElement().Element("TANUM").ElementValueNull(),
                            bwlvs = doc.Element("E1LTORH").ElementElement().Element("BWLVS").ElementValueNull(),
                            trart = doc.Element("E1LTORH").ElementElement().Element("TRART").ElementValueNull(),
                            bname = doc.Element("E1LTORH").ElementElement().Element("BNAME").ElementValueNull(),

                            q2 = from doc2 in doc.Element("E1LTORH").Descendants("E1LTORI")
                                 select new
                                 {
                                     tapos = doc2.Element("TAPOS").Value.Trim(),
                                     matnr = doc2.Element("MATNR").Value.Trim(),
                                     plant = doc2.Element("WERKS").ElementValueNull(),

                                     charg = doc2.Element("CHARG").ElementValueNull(),
                                     bestq = doc2.Element("BESTQ").ElementValueNull(),
                                     sobkz = doc2.Element("SOBKZ").ElementValueNull(),
                                     lsonr = doc2.Element("LSONR").ElementValueNull(),
                                     meins = doc2.Element("MEINS").ElementValueNull(),
                                     wdatu = doc2.Element("WDATU").ElementValueNull(),
                                     wenum = doc2.Element("WENUM").ElementValueNull(),
                                     vltyp = doc2.Element("VLTYP").ElementValueNull(),
                                     vsolm = doc2.Element("VSOLM").ElementValueNull0(),
                                     vol   = doc2.Element("VOLUM").ElementValueNull0(),
                                     nltyp = doc2.Element("NLTYP").ElementValueNull(),
                                     maktx = doc2.Element("MAKTX").ElementValueNull(),
                                     vfdat = doc2.Element("VFDAT").ElementValueNull(),

                                     lgort = doc2.Element("LGORT").ElementValueNull()

                                 }
                        };

                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                  
                    foreach (var r in q)
                    {
                        string docnum = removeZeroBeforestr(r.docnum);

                        foreach (var x in r.q2)
                        {
                            miwmto wmto = db.miwmtos.Where(m => m.docnum == docnum &&
                                                            m.tanum == Convert.ToInt32(r.tanum) &&
                                                            m.tapos == Convert.ToInt32(x.tapos)).SingleOrDefault();
                            if (wmto == null)
                            {
                                miwmto w = new miwmto();

                                #region ---- r foreach ---------
                                w.docnum = removeZeroBeforestr(r.docnum);

                                string ls = DateTime.Now.ToString("yyyyMMddhhmmss");
                                w.credat = ls.Substring(0, 8);
                                w.cretim = ls.Substring(8, 6);

                                //if (r.credat.Length > 8) w.credat = r.credat.Substring(0, 8);
                                //else w.credat = r.credat;
                                //if (r.cretim.Length > 6) w.cretim = r.cretim.Substring(0, 6);
                                //else w.cretim = r.cretim;

                                if (r.lgnum.Length > 3) w.lgnum = r.lgnum.Substring(0, 3);
                                else w.lgnum = r.lgnum;

                                w.tanum = Convert.ToInt32(r.tanum);

                                if (r.bwlvs.Length > 3) w.bwlvs = r.bwlvs.Substring(0, 3);
                                else w.bwlvs = r.bwlvs;

                                if (r.trart.Length > 1) w.trart = r.trart.Substring(0, 1);
                                else w.trart = r.trart;

                                if (r.bname.Length > 12) w.bname = r.bname.Substring(0, 12);
                                else w.bname = r.bname;
                                #endregion


                                #region ---- x foreach ---------
                                w.tapos = Convert.ToInt32(x.tapos);
                                w.matnr = removeZeroBeforestr(x.matnr);

                                if (x.plant.Length > 4) w.plant = x.plant.Substring(0, 4);
                                else w.plant = x.plant;

                                w.charg = removeZeroBeforestr(x.charg);

                                if (x.bestq.Length > 1) w.bestq = x.bestq.Substring(0, 1);
                                else w.bestq = x.bestq;

                                if (x.sobkz.Length > 1) w.sobkz = x.sobkz.Substring(0, 1);
                                else w.sobkz = x.sobkz;

                                if (x.lsonr.Length > 24) w.lsonr = x.lsonr.Substring(0, 24);
                                else w.lsonr = x.lsonr;

                                if (x.meins.Length > 3) w.meins = x.meins.Substring(0, 3);
                                else w.meins = x.meins;

                                if (x.wdatu.Length > 8) w.wdatu = x.wdatu.Substring(0, 8);
                                else w.wdatu = x.wdatu;

                                if (x.wenum.Length > 10) w.wenum = x.wenum.Substring(0, 10);
                                else w.wenum = x.wenum;

                                if (x.vltyp.Length > 3) w.vltyp = x.vltyp.Substring(0, 3);
                                else w.vltyp = x.vltyp;

                                if (x.vsolm == "")
                                    w.vsolm = 0;
                                else
                                    w.vsolm = Convert.ToDecimal(x.vsolm);
                             
                                if (w.vsolm != 0)
                                    w.pksz = Convert.ToDecimal(x.vol) / Convert.ToDecimal(x.vsolm);
                                else
                                    w.pksz = 0.000m;

                                if (x.nltyp.Length > 3) w.nltyp = x.nltyp.Substring(0, 3);
                                else w.nltyp = x.nltyp;

                                if (x.maktx.Length > 40) w.maktx = x.maktx.Substring(0, 40);
                                else w.maktx = x.maktx;

                                if (x.vfdat.Length > 8) w.vfdat = x.vfdat.Substring(0, 8);
                                else w.vfdat = x.vfdat;

                                if (x.lgort.Length > 4) w.lgort = x.lgort.Substring(0, 4);
                                else w.lgort = x.lgort;

                                w.rqty = 0;
                                w.fqty = 0;
                                w.flag = "";                              
                                w.hdate = "";
                                w.htime = "";

                                switch (r.bwlvs)
                                {
                                    case "101":
                                    case "202":
                                    case "521":
                                    case "302":
                                    case "312":
                                    case "552":
                                    case "632":
                                    case "256":  
                                    case "651":
                                        w.io = "I";
                                        break;
                                    case "309":
                                    case "321":
                                        w.io = "C";                                       
                                        break;
                                    case "102":
                                    case "201":
                                    case "522":
                                    case "301":
                                    case "311":
                                    case "652":
                                    case "255":
                                    case "551":
                                    case "631":
                                        w.io = "$";
                                        break;
                                    default:
                                        w.io = "";
                                        break;
                                }
                                #endregion

                                db.miwmtos.InsertOnSubmit(w);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
                return true; ;
            }
            catch (Exception E)
            {
                EventLog.Event_Log("ERPIF.txt", path, E.Message, true);
                return false;
            }
        }

        // delivery parsing--------------------------------------------
        public bool ParseDelivery(string path)
        {
            
            int count = 0;
            try
            {
                XDocument xd = XDocument.Load(path);

                #region q ----------
                var q = from d in xd.Root.Descendants("IDOC")
                select new
                {
                    docnum = d.Element("EDI_DC40").Element("DOCNUM").Value,
                    credat = d.Element("EDI_DC40").Element("CREDAT").ElementValueNull(),
                    cretim = d.Element("EDI_DC40").Element("CRETIM").ElementValueNull(),

                    #region q2 ---------
                    q2 = from d2 in d.Descendants("E1EDL20")
                    select new
                    {
                        sdno = d2.Element("VBELN").Value.Trim(),
                        route = d2.Element("ROUTE").ElementValueNull(),
                        routedesc = d2.Element("E1EDL22").ElementElement().Element("ROUTE_BEZ").ElementValueNull(),
                        vsbed = d2.Element("VSBED").ElementValueNull(),
                        ablad = d2.Element("ABLAD").ElementValueNull(),

                        //twgt = d2.Element("BTGEW").Value,
                        //tnwgt = d2.Element("NTGEW").Value,
                        //twunit = d2.Element("GEWEI").Value,
                        //tvol = d2.Element("VOLUM").Value,
                        //tvunit = d2.Element("VOLEH").Value,

                        deltyp = d2.Element("E1EDL21").ElementElement().Element("LFART").ElementValueNull(),
                        deltypdesc = d2.Element("E1EDL21").ElementElement().Element("E1EDL23").ElementElement().Element("LFART_BEZ").ElementValueNull(),

                        #region q3 ---------
                        q3 = from d3 in d2.Descendants("E1ADRM1")
                        where (d3.Element("PARTNER_Q").ElementValueNull() == "AG" || d3.Element("PARTNER_Q").ElementValueNull() == "WE")
                        orderby d3.Element("PARTNER_Q").ElementValueNull()
                        select new
                        {
                            partner_q = d3.Element("PARTNER_Q").ElementValueNull(),
                            cust = d3.Element("PARTNER_ID").ElementValueNull(),
                            cust_name1 = d3.Element("NAME1").ElementValueNull(),
                            cust_name2 = d3.Element("NAME2").ElementValueNull(),
                            street = d3.Element("STREET1").ElementValueNull(),
                            post = d3.Element("POSTL_COD1").ElementValueNull(),
                            city = d3.Element("CITY1").ElementValueNull(),
                            tel = d3.Element("TELEPHONE1").ElementValueNull(),
                            contry = d3.Element("COUNTRY1").ElementValueNull(),
                            region = d3.Element("REGION").ElementValueNull()
                        },
                        #endregion

                        #region q4 ----
                        q4 = from d4 in d2.Descendants("E1EDT13")
                        where (d4.Element("QUALF").ElementValueNull() == "007")
                        select new
                        {
                            duedate = d4.Element("NTEND").ElementValueNull()
                        },
                        #endregion

                        #region q41----
                        // shipping instruction
                        q41 = from d41 in d2.Descendants("E1TXTH8")
                        where (d41.Element("TDID").ElementValueNull() == "0012")
                        select new
                        {
                            q42 = from d42 in d41.Descendants("E1TXTP8")
                                select new
                                {
                                    tdline = d42.Element("TDLINE").ElementValueNull()
                                }
                        },

                        #endregion

                        #region q45----
                        // internal instruction
                        q45 = from d45 in d2.Descendants("E1TXTH8")
                        where (d45.Element("TDID").ElementValueNull() == "Z002")
                        select new
                        {
                            q46 = from d46 in d45.Descendants("E1TXTP8")
                                    select new
                                    {
                                        tdline = d46.Element("TDLINE").ElementValueNull()
                                    }
                        },

                        #endregion

                        #region q5----

                        q5 = from d5 in d2.Descendants("E1EDL24")
                        select new
                        {
                            posnr = d5.Element("POSNR").ElementValueNull(),
                            matnr = d5.Element("MATNR").ElementValueNull(),

                            matnrdesc = d5.Element("ARKTX").ElementValueNull(),
                            plant = d5.Element("WERKS").ElementValueNull(),
                            lgort = d5.Element("LGORT").ElementValueNull(),
                            charg = d5.Element("CHARG").ElementValueNull(),

                            qty = d5.Element("LFIMG").ElementValueNull0(),
                            gwgt = d5.Element("BRGEW").ElementValueNull0(),

                            nwgt = d5.Element("NTGEW").ElementValueNull0(),
                            wunit = d5.Element("GEWEI").ElementValueNull(),
                            vol = d5.Element("VOLUM").ElementValueNull0(),
                            vunit = d5.Element("VOLEH").ElementValueNull(),

                            vgbel = d5.Element("VGBEL").ElementValueNull(),

                            pstyv = d5.Element("E1EDL26").ElementElement().Element("PSTYV").ElementValueNull(),
                            pstyvdesc = d5.Element("E1EDL26").ElementElement().Element("E1EDL27").ElementElement().Element("PSTYV_BEZ").ElementValueNull(),

                            sono = d5.Element("E1EDL43").ElementElement().Element("BELNR").ElementValueNull(),
                            soposnr = d5.Element("E1EDL43").ElementElement().Element("POSNR").ElementValueNull0(),
                            sodate = d5.Element("E1EDL43").ElementElement().Element("DATUM").ElementValueNull(),

                            custpo = d5.Element("E1EDL41").ElementElement().Element("BSTNR").ElementValueNull(),
                            custpodate = d5.Element("E1EDL41").ElementElement().Element("BSTDT").ElementValueNull()
                        }
                        #endregion
                    }
                    #endregion end of q2
                };

                #endregion end of q

             
                string ls = "";
                using (DBDataContext db = new DBDataContext(Config.DBCon))
                {
                    db.Log = new DebugTextWriter();

                    string docnum, credat, cretim, sdno, route="", routedesc="", deltyp="", deltypdesc="", cust="", cust_name1="", cust_name2="", street="", post="", city="", tel="", contry="", region="";
                    string wecust="", wecust_name1="", wecust_name2="", westreet="", wepost="", wecity="", wetel="", wecontry="", weregion="", parcel="", rmrk="", cmmt="", duedate="", vgbel="", flag = "";
                    string vsbed="", ablad="";


                    foreach (var d in q)
                    {
                        foreach (var d2 in d.q2)
                        {
                            cust = ""; cust_name1 = ""; cust_name2 = ""; street = ""; post = ""; city = ""; tel = ""; contry = ""; region = ""; vgbel = "";
                            wecust = ""; wecust_name1 = ""; wecust_name2 = ""; westreet = ""; wepost = ""; wecity = ""; wetel = ""; wecontry = ""; weregion = ""; flag = "";
                            duedate = "";

                            docnum = removeZeroBeforestr(d.docnum);

                            ls = DateTime.Now.ToString("yyyyMMddhhmmss");
                            credat = ls.Substring(0, 8);
                            cretim = ls.Substring(8, 6);

                            //credat = d.credat;
                            //cretim = d.cretim;

                            sdno = removeZeroBeforestr(d2.sdno);
                            route = d2.route;
                            routedesc = d2.routedesc;

                            vsbed = d2.vsbed;
                            ablad = d2.ablad;

                            deltyp = d2.deltyp;
                            deltypdesc = d2.deltypdesc;                             

                            foreach (var d3 in d2.q3)
                            {
                                if (d3.partner_q.TrimEnd() == "AG")
                                {
                                    cust = removeZeroBeforestr(d3.cust);
                                    cust_name1 = d3.cust_name1;
                                    cust_name2 = d3.cust_name2;
                                    street = d3.street;
                                    post = d3.post;
                                    city = d3.city;
                                    tel = d3.tel;
                                    contry = d3.contry;
                                    region = d3.region;
                                }
                                else if (d3.partner_q.TrimEnd() == "WE")
                                {
                                    wecust = removeZeroBeforestr(d3.cust);
                                    wecust_name1 = d3.cust_name1;
                                    wecust_name2 = d3.cust_name1;
                                    westreet = d3.street;
                                    wepost = d3.post;
                                    wecity = d3.city;
                                    wetel = d3.tel;
                                    wecontry = d3.contry;
                                    weregion = d3.region;
                                }
                            }

                            foreach (var d4 in d2.q4)
                            {
                                duedate = d4.duedate;
                            }

                            rmrk = "";
                            cmmt = "";
                            if (ablad != "") parcel = "1"; else parcel = "";
                            flag = "0";

                            // shipping instruction
                            foreach (var d41 in d2.q41)
                            {
                                foreach (var d42 in d41.q42)
                                {
                                    if (cmmt == "") cmmt = d42.tdline;
                                    else cmmt = cmmt + '\n' + d42.tdline;
                                }
                            }
                            cmmt = cmmt.Trim();

                            // internal instruction
                            foreach (var d45 in d2.q45)
                            {
                                foreach (var d46 in d45.q46)
                                {
                                    if (rmrk == "") rmrk = d46.tdline;
                                    else rmrk = rmrk + '\n' + d46.tdline;
                                }
                            }
                            rmrk = rmrk.Trim();

                            foreach (var d5 in d2.q5) 
                            {
                                if (d5.qty.Trim() == "") continue;
                                if (d5.lgort.Trim() == "") continue;
                                if (d5.charg.Trim() == "") continue;

                                count++;
                                int posnr = Convert.ToInt32(d5.posnr);

                                #region ---mirodi    

                                miordi m = db.miordis.Where(x => x.docnum == docnum && x.sdno == sdno && x.posnr == posnr).SingleOrDefault();
                                if (m == null)
                                {
                                    miordi t = new miordi();

                                    #region --header ----
                                    t.docnum = docnum;
                                    t.credat = credat;
                                    t.cretim = cretim;

                                    if (sdno.Length > 10)
                                        t.sdno = sdno.Substring(0, 10);
                                    else t.sdno = sdno;

                                    if (route.Length > 6)
                                        t.route = route.Substring(0, 6);
                                    else t.route = route;

                                    t.routedesc = routedesc;

                                    if (deltyp.Length > 4)
                                        t.deltyp = deltyp.Substring(0, 4);
                                    else
                                        t.deltyp = deltyp;

                                    t.deltypdesc = deltypdesc;

                                    if (cust.Length > 17)
                                        t.cust = cust.Substring(0, 17);
                                    else
                                        t.cust = cust;

                                    t.cust_name1 = cust_name1;
                                    t.cust_name2 = cust_name2;

                                    t.street = street;

                                    if (post.Length > 10)
                                        t.post = post.Substring(0, 10);
                                    else
                                        t.post = post;

                                    t.city = city;

                                    if (tel.Length > 30)
                                        t.tel = tel.Substring(0, 30);
                                    else
                                        t.tel = tel;

                                    if (contry.Length > 3)
                                        t.contry = contry.Substring(0, 3);
                                    else
                                        t.contry = contry;


                                    if (region.Length > 3)
                                        t.region = region.Substring(0, 3);
                                    else
                                        t.region = region;


                                    if (wecust.Length > 17)
                                        t.wecust = wecust.Substring(0, 17);
                                    else
                                        t.wecust = wecust;

                                    t.wecust_name1 = wecust_name1;
                                    t.wecust_name2 = wecust_name2;
                                    t.westreet = westreet;

                                    if (wepost.Length > 10)
                                        t.wepost = wepost.Substring(0, 10);
                                    else
                                        t.wepost = wepost;

                                    t.wecity = wecity;

                                    if (wetel.Length > 30)
                                        t.wetel = wetel.Substring(0, 30);
                                    else
                                        t.wetel = wetel;

                                    if (wecontry.Length > 3)
                                        t.wecontry = wecontry.Substring(0, 3);
                                    else
                                        t.wecontry = wecontry;

                                    if (weregion.Length > 3)
                                        t.weregion = weregion.Substring(0, 3);
                                    else
                                        t.weregion = weregion;

                                    if (duedate.Length > 8)
                                        t.duedate = duedate.Substring(0, 8);
                                    else
                                        t.duedate = duedate;

                                    t.cmmt = cmmt;
                                    t.rmrk = rmrk;

                                    if (parcel.Length > 1)
                                        t.parcel = parcel.Substring(0, 1);
                                    else
                                        t.parcel = parcel;

                                    t.ablad = ablad;

                                    #endregion

                                    if (d5.vgbel.Length > 10)
                                        t.vgbel = d5.vgbel.Substring(0, 10);
                                    else
                                        t.vgbel = d5.vgbel;


                                    if (d5.posnr == "") t.posnr = 0;
                                    else t.posnr = Convert.ToInt32(d5.posnr);

                                    if (d5.matnr == "") t.matnr = "";
                                    else t.matnr = removeZeroBeforestr(d5.matnr);

                                    if (d5.matnrdesc.Length > 40)
                                        t.matnrdesc = d5.matnrdesc.Substring(0, 40);
                                    else
                                        t.matnrdesc = d5.matnrdesc;

                                    if (d5.lgort.Length > 4)
                                        t.lgort = d5.lgort.Substring(0, 4);
                                    else
                                        t.lgort = d5.lgort;


                                    t.charg = removeZeroBeforestr(d5.charg);

                                    if(d5.plant.Length > 4)
                                        t.plant = d5.plant.Substring(0,4);
                                    else
                                        t.plant = d5.plant;

                                    if (d5.qty == "") t.qty = 0;
                                    else t.qty = Convert.ToDecimal(d5.qty);

                                    if (d5.gwgt == "") t.gwgt = 0;
                                    else t.gwgt = Convert.ToDecimal(d5.gwgt);

                                    if (d5.nwgt == "") t.nwgt = 0;
                                    else t.nwgt = Convert.ToDecimal(d5.nwgt);

                                    if (d5.wunit.Length > 3) t.wunit = d5.wunit.Substring(0, 3);
                                    else t.wunit = d5.wunit;

                                    if (d5.vol == "") t.vol = 0;
                                    else t.vol = Convert.ToDecimal(d5.vol);

                                    if (d5.vunit.Length > 3) t.vunit = d5.vunit.Substring(0, 3);
                                    else t.vunit = d5.vunit;

                                    if (d5.pstyv.Length > 4) t.pstyv = d5.pstyv.Substring(0, 4);
                                    else t.pstyv = d5.pstyv;

                                    t.pstyvdesc = d5.pstyvdesc;
                                    t.sono = d5.sono;

                                    if (d5.soposnr == "") t.soposnr = 0;
                                    else t.soposnr = Convert.ToInt32(d5.soposnr);

                                    if (d5.sodate.Length > 8)
                                        t.sodate = d5.sodate.Substring(0, 8);
                                    else
                                        t.sodate = d5.sodate;

                                    t.custpo = d5.custpo;

                                    if (d5.custpodate.Length > 8)
                                        t.custpodate = d5.custpodate.Substring(0, 8);
                                    else
                                        t.custpodate = d5.custpodate;


                                    t.rqty = 0; t.fqty = 0; t.flag = "0";
                                    t.arrival = t.westreet;
                                    t.car_no = "";
                                    t.car_step = "";
                                    t.car_sno = 0;
                                    t.print_step = "";
                                    t.ordi_seq = 0;
                                    t.ordi_check = "";
                                    t.remark = "";
                                    t.bachadate = "";
                                    t.ordi_ltqty = t.vol;
                                        
                                    if (vsbed.Length > 2)
                                        t.vsbed = vsbed.Substring(0, 2);
                                    else
                                        t.vsbed = vsbed;

                                    if (t.qty != 0)
                                        t.ordi_size = t.vol / t.qty;
                                    else
                                        t.ordi_size = 1;


                                    t.hdate = "";
                                    t.htime = "";
                                    t.recv_dt = DateTime.Now;


                                    db.miordis.InsertOnSubmit(t);

                                    db.SubmitChanges();

                                } // end of if null
                                #endregion
                            } // end of third foreach 

                        }// end of second foreach 

                    } // end of first foreach 

                } // end of try             
                return true;

            } 
            catch (Exception E)
            {
                
                EventLog.Event_Log("ERPIF.txt", path, E.Message, true);
                return false;
            }            
        }        
    }
}