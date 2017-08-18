using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace learningC
{
    class Program
    {
        static void Main(string[] args)
        {
            //HXTest();
            //jsonTest();
            //XMLClassToJsonTest();
            dtTest();
        }

        static void HXTest()
        {
            string s = System.IO.File.ReadAllText(@"d:\HXin.xml");
            Console.Out.WriteLine(s);
            Console.Out.WriteLine();
        }
        public static void dtTest()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BZ", Type.GetType("System.String"));
            DataRow newRow, newRow2;
            newRow = dt.NewRow();
            newRow["BZ"] = "这个地方是单元格的值";
            dt.Rows.Add(newRow);
            newRow2 = dt.NewRow();
            newRow2["BZ"] = @"""#qweqeqweqeqweqwe'";
            dt.Rows.Add(newRow2);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.Out.WriteLine(dt.Rows[i]["BZ"].ToString());
            }
            DeleteQuotesFromBZ(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.Out.WriteLine(dt.Rows[i]["BZ"].ToString());
            }
        }
        public static void DeleteQuotesFromBZ(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = dt.Rows[i]["BZ"].ToString();
                    s = s.Replace("\"", " ");
                    s = s.Replace("\'", " ");
                    dt.Rows[i]["BZ"] = s;
                }
            }
            catch (Exception ee)
            {
                Console.Out.WriteLine("替换符号失败" + ee);
            }
        }
        static void jsonTest()
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            //string s = Console.In.ReadLine();
            string s = @"""";
            dic.Add(s, 1);
            dic.Add("left", 2);
            dic.Add("invKind", 3);
            dic.Add("useCount", 4);
            dic.Add("invRollInfo", new invRollInfo());
            string info = JSONHelper.ObjectToJSON(dic);
            Console.Out.WriteLine(info);
        }
        static void XMLClassToJsonTest()
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            dic.Add("nextInvNo", 1);
            dic.Add("left", 2);
            dic.Add("invKind", 3);
            dic.Add("useCount", 4);
            dic.Add("XMLinvRollInfo", new XMLinvRollInfo());
            string info = JSONHelper.ObjectToJSON(dic);
            Console.Out.WriteLine(info);
        }
    }
    [XmlRoot("XMLinvRollInfo")]
    public class XMLinvRollInfo
    {
        [XmlElement("COUNT")]
        public int count;
        [XmlElement("GROUP")]
        public List<XMLinvGroup> group = new List<XMLinvGroup>();
        public XMLinvRollInfo()
        {
            group.Add(new XMLinvGroup());
            group.Add(new XMLinvGroup("a", "b"));
            count = 2;
        }
    }
    public class XMLinvGroup
    {
        [XmlElement("XH")]
        public string xh;
        [XmlElement("FPDM")]
        public string fpdm;
        public XMLinvGroup()
        {
            xh = "A";
            fpdm = "B";
        }
        public XMLinvGroup(string xh, string fpdm)
        {
            this.xh = xh;
            this.fpdm = fpdm;
        }
    }
    public class invRollInfo
    {
        public int count;
        public List<invGroup> group = new List<invGroup>();
        public invRollInfo()
        {
            group.Add(new invGroup());
            group.Add(new invGroup("a", "b"));
            count = 2;
        }
    }
    public class invGroup
    {
        public string xh;
        public string fpdm;
        public invGroup()
        {
            xh = "A";
            fpdm = "B";
        }
        public invGroup(string xh, string fpdm)
        {
            this.xh = xh;
            this.fpdm = fpdm;
        }
    }
    public static class JSONHelper
    {
        /// <summary> 
        /// 对象转JSON 
        /// </summary> 
        /// <param name="obj">对象</param> 
        /// <returns>JSON格式的字符串</returns> 
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                jss.MaxJsonLength = Int32.MaxValue;
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {

                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }
    }
}
