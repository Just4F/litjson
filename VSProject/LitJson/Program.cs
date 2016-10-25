using FairyGUI.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Save();

            Load();
        }

        static void Save()
        {
            TestJson tj = new TestJson();
            tj.Name = "tstjson";
            tj.interger = new List<int>();
            tj.interger.Add(123);
            tj.interger.Add(3333);
            tj.dicS2F = new Dictionary<string, float>();
            tj.dicS2F.Add("sdf", 5554.123f);
            tj.dicS2F.Add("111", 6566);
            //tj.vec = new Vector3(6, 4, 3);
            tj.param_s = new TokenJson[2];
            tj.param_s[0] = new TokenJson();
            tj.param_s[1] = new TokenJson();
            var tk = tj.param_s[0];
            tk.name = "0";
            tk.type = "123";
            tk.value = "vvv";

            tk = tj.param_s[1];
            tk.name = "1";
            tk.type = "111";
            tk.value = "333, 444, 56556";

            string jsStr = JsonMapper.ToJson(tj);
            UtilsCommonS.Save2File("C:\\Users\\xRop\\Desktop/js.json", jsStr);
        }

        static void Load()
        {
            string str = UtilsCommonS.LoadFile2String("C:\\Users\\xRop\\Desktop/js.json");
            TestJson tj = JsonMapper.ToObject<TestJson>(str);
            int i = tj.interger[0];
        }

        public class TestJson
        {
            public string Name;
            public List<int> interger;
            public Dictionary<string, float> dicS2F;
            //public Vector3 vec;
            public TokenJson[] param_s;
        }

        public class TokenJson
        {
            public string name;
            public string type;
            public string value;
        }
    }
}
