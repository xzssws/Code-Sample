using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 练习汇总包
{
    public partial class 有道翻译 : Form
    {
        public 有道翻译()
        {
            InitializeComponent();
        }
        private string url = "http://fajyi.youdao.com/openapi.do";
        private string keyfrom = "";
        private string key = "";
        private string docType = "xml";
        private string SendGet(string Str)
        {
            try
            {
                
            }
            catch (Exception)
            {
            }

            StringBuilder strb = new StringBuilder();
            strb.Append("keyfrom=");
            strb.Append(keyfrom);
            strb.Append("&key=");
            strb.Append(key);
            strb.Append("&type=data&doctype=");
            strb.Append(docType);
            strb.Append("&version=1.1&q=");
            strb.Append(str);



        }

    }
}
