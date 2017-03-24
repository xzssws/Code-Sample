using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
namespace blqw
{
    /// <summary>
    /// c#对象转JSON
    /// </summary>
    public class JsonBuilder
    {
        private Dictionary<object, object> _LoopObject = new Dictionary<object, object>();//循环引用对象缓存区
        protected StringBuilder Buff = new StringBuilder(4096);//字符缓冲区

        public string ToJsonString(object obj)
        {
            Buff.Length = 0;
            AppendObject(obj);
            return Buff.ToString();
        }
        //泛对象
        protected void AppendObject(object obj)
        {
            if (obj == null) Buff.Append("null");
            else if (obj is String) AppendString((String)obj);
            else if (obj is Int32) AppendInt32((Int32)obj);
            else if (obj is Boolean) AppendBoolean((Boolean)obj);
            else if (obj is DateTime) AppendDateTime((DateTime)obj);
            else if (obj is Double) AppendDouble((Double)obj);
            else if (obj is Enum) AppendEnum((Enum)obj);
            else if (obj is Decimal) AppendDecimal((Decimal)obj);
            else if (obj is Char) AppendChar((Char)obj);
            else if (obj is Single) AppendSingle((Single)obj);
            else if (obj is Guid) AppendGuid((Guid)obj);
            else if (obj is Byte) AppendByte((Byte)obj);
            else if (obj is Int16) AppendInt16((Int16)obj);
            else if (obj is Int64) AppendInt64((Int64)obj);
            else if (obj is SByte) AppendSByte((SByte)obj);
            else if (obj is UInt32) AppendUInt32((UInt32)obj);
            else if (obj is UInt64) AppendUInt64((UInt64)obj);
            else if (_LoopObject.ContainsKey(obj) == false)
            {
                _LoopObject.Add(obj, null);
                if (obj is IDictionary) AppendJson((IDictionary)obj);
                else if (obj is IEnumerable) AppendArray((IEnumerable)obj);
                else if (obj is DataSet) AppendDataSet((DataSet)obj);
                else if (obj is DataTable) AppendDataTable((DataTable)obj);
                else if (obj is DataView) AppendDataView((DataView)obj);
                else AppendOther(obj);
                _LoopObject.Remove(obj);
            }
            else
            {
                Buff.Append("undefined");
            }
        }
        protected virtual void AppendOther(object obj)
        {
            Type t = obj.GetType();
            Buff.Append('{');
            string fix = "";
            foreach (var p in t.GetProperties())
            {
                if (p.CanRead)
                {
                    Buff.Append(fix);
                    AppendKey(p.Name, false);
                    object value = p.GetValue(obj, null);
                    AppendObject(value);
                    fix = ",";
                }
            }
            Buff.Append('}');
        }
        /// <summary> "
        /// </summary>
        public const char Quot = '"';
        /// <summary> :
        /// </summary>
        public const char Colon = ':';
        /// <summary> ,
        /// </summary>
        public const char Comma = ',';
        /// <summary> 追加Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="escape">key中是否有(引号,回车,制表符等)特殊字符,需要转义</param>
        protected virtual void AppendKey(string key, bool escape)
        {
            if (escape)
            {
                AppendString(key);
            }
            else
            {
                Buff.Append(Quot);
                Buff.Append(key);
                Buff.Append(Quot);
            }
            Buff.Append(Colon);
        }
        //基本类型转换Json字符串写入Buff
        protected virtual void AppendByte(Byte value) { AppendNumber(value); }
        protected virtual void AppendDecimal(Decimal value) { AppendNumber(value); }
        protected virtual void AppendInt16(Int16 value) { AppendNumber(value); }
        protected virtual void AppendInt32(Int32 value) { AppendNumber(value); }
        protected virtual void AppendInt64(Int64 value) { AppendNumber(value); }
        protected virtual void AppendSByte(SByte value) { AppendNumber(value); }
        protected virtual void AppendUInt16(UInt16 value) { AppendNumber(value); }
        protected virtual void AppendUInt32(UInt32 value) { AppendNumber(value); }
        protected virtual void AppendUInt64(UInt64 value) { AppendNumber(value); }
        protected virtual void AppendDouble(Double value) { AppendNumber(value); }
        protected virtual void AppendSingle(Single value) { AppendNumber(value); }
        protected virtual void AppendBoolean(Boolean value) { Buff.Append(value ? "true" : "false"); }
        protected virtual void AppendChar(Char value)
        {
            Buff.Append(Quot);
            switch (value)
            {
                case '\\':
                case '\n':
                case '\r':
                case '\t':
                case '"':
                    Buff.Append('\\');
                    break;
            }
            Buff.Append(value);
            Buff.Append(Quot);
        }
        protected virtual void AppendString(String value)
        {
            Buff.Append(Quot);

            for (int j = 0; j < value.Length; j++)
            {
                switch (value[j])
                {
                    case '\\':
                    case '\n':
                    case '\r':
                    case '\t':
                    case '"':
                        Buff.Append('\\');
                        break;
                }
                Buff.Append(value[j]);
            }

            Buff.Append(Quot);
        }
        protected virtual void AppendDateTime(DateTime value)
        {
            Buff.Append(Quot);
            if (value.Year < 1000)
            {
                if (value.Year < 100)
                {
                    if (value.Year < 10)
                    {
                        Buff.Append("000");
                    }
                    else
                    {
                        Buff.Append("00");
                    }
                }
                else
                {
                    Buff.Append("0");
                }
            }
            Buff.Append(value.Year)
            .Append('-');
            if (value.Month < 10)
            {
                Buff.Append('0');
            }
            Buff.Append(value.Month).Append('-');

            if (value.Day < 10)
            {
                Buff.Append('0');
            }
            Buff.Append(value.Day).Append(' ');

            if (value.Hour < 10)
            {
                Buff.Append('0');
            }
            Buff.Append(value.Hour).Append(Colon);

            if (value.Minute < 10)
            {
                Buff.Append('0');
            }
            Buff.Append(value.Minute).Append(Colon);

            if (value.Second < 10)
            {
                Buff.Append('0');
            }
            Buff.Append(value.Second).Append(Quot);
        }
        protected virtual void AppendGuid(Guid value)
        {
            Buff.Append(Quot).Append(value.ToString()).Append(Quot);
        }
        //枚举
        protected virtual void AppendEnum(Enum value)
        {
            Buff.Append(Quot).Append(value.ToString()).Append(Quot);
        }
        protected virtual void AppendNumber(IConvertible number)
        {
            Buff.Append(number.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
        }
        //转换数组对象
        protected virtual void AppendArray(IEnumerable array)
        {
            Buff.Append('[');
            var ee = array.GetEnumerator();
            if (ee.MoveNext())
            {
                AppendObject(ee.Current);
                while (ee.MoveNext())
                {
                    Buff.Append(Comma);
                    AppendObject(ee.Current);
                }
            }
            Buff.Append(']');
        }
        //转换键值对对象
        protected virtual void AppendJson(IDictionary dict)
        {
            AppendJson(dict.Keys, dict.Values);
        }
        //分别有键值枚举的对象
        protected virtual void AppendJson(IEnumerable keys, IEnumerable values)
        {
            Buff.Append('{');
            var ke = keys.GetEnumerator();
            var ve = values.GetEnumerator();
            if (ke.MoveNext() && ve.MoveNext())
            {
                AppendKey(ke.Current + "", true);
                AppendObject(ve.Current);
                while (ke.MoveNext() && ve.MoveNext())
                {
                    Buff.Append(Comma);
                    AppendKey(ke.Current + "", true);
                    AppendObject(ve.Current);
                }
            }
            Buff.Append('}');
        }

        protected virtual void AppendArray(IEnumerable enumer, Converter<object, object> getVal)
        {
            Buff.Append('[');
            var ee = enumer.GetEnumerator();
            if (ee.MoveNext())
            {
                AppendObject(ee.Current);
                while (ee.MoveNext())
                {
                    Buff.Append(Comma);
                    AppendObject(getVal(ee.Current));
                }
            }
            Buff.Append(']');
        }

        protected virtual void AppendJson(IEnumerable enumer, Converter<object, string> getKey, Converter<object, object> getVal, bool escapekey)
        {
            Buff.Append('{');

            var ee = enumer.GetEnumerator();
            if (ee.MoveNext())
            {
                AppendKey(getKey(ee.Current), escapekey);
                AppendObject(getVal(ee.Current));
                while (ee.MoveNext())
                {
                    Buff.Append(Comma);
                    AppendKey(getKey(ee.Current), true);
                    AppendObject(getVal(ee.Current));
                }
            }
            Buff.Append('}');
        }

        protected virtual void AppendDataSet(DataSet dataset)
        {
            Buff.Append('{');
            var ee = dataset.Tables.GetEnumerator();
            if (ee.MoveNext())
            {
                DataTable table = (DataTable)ee.Current;
                AppendKey(table.TableName, true);
                AppendDataTable(table);
                while (ee.MoveNext())
                {
                    Buff.Append(Comma);
                    table = (DataTable)ee.Current;
                    AppendKey(table.TableName, true);
                    AppendDataTable(table);
                }
            }
            Buff.Append('}');
        }

        protected virtual void AppendDataTable(DataTable table)
        {
            Buff.Append("{\"columns\":");
            AppendArray(table.Columns, o => ((DataColumn)o).ColumnName);
            Buff.Append(",\"rows\":");
            AppendArray(table.Rows, o => ((DataRow)o).ItemArray);
            Buff.Append('}');
        }

        protected virtual void AppendDataView(DataView tableView)
        {
            Buff.Append("{\"columns\":");
            AppendArray(tableView.Table.Columns, o => ((DataColumn)o).ColumnName);
            Buff.Append(",\"rows\":");
            AppendArray(tableView, o => ((DataRowView)o).Row.ItemArray);
            Buff.Append('}');
        }
    }


    /// <summary>
    /// 数字转大写
    /// </summary>
    public static class NumberToUpper
    {
        #region 固定参数
        //^[-+]?\d*(?<point>[.])?\d*$
        readonly static Regex CheckNumber = new Regex(@"^[\s\t]*0*(?<integer>[-+]?\d*)[.]?(?<decimal>\d*[1-9])?[0]*[\s\t]*$", RegexOptions.Compiled);

        readonly static string[] UpperNumbers = 
{
"零壹貳叁肆伍陸柒捌玖點",
"零一二三四五六七八九点"
};
        readonly static string[] NumberUnits = 
{ 
"仟萬拾佰億負",
"千万十百亿负"
};
        readonly static string[] MoneyUnits =
{
"圓角分",
"元角分"
};

        #endregion

        /// <summary> 将数字文本转换成大写
        /// </summary>
        /// <param name="number">数字文本</param>
        /// <param name="isSimplified">是否只使用简体中文,默认:否</param>
        /// <param name="isMoney">是否是金额模式(忽略小数点后3位,并加上单位"元,角,分或整"),否认:是</param>
        /// <param name="veryBig">是否开启大数字文本模式(接受15位以上整数,及10位以上小数),否认:否</param>
        public static string Go(string number, bool isSimplified = false, bool isMoney = true, bool veryBig = false)
        {
            if (number == null)
            {
                throw new ArgumentNullException("number", "number不能为空");
            }
            //number = number.Trim(' ', '\t'); //去掉多余的空格,制表符
            var m = CheckNumber.Match(number);
            if (m.Success == false)
            {
                throw new ArgumentException("number不是数字", "number");
            }

            unsafe
            {
                fixed (char* p = number)
                fixed (char* upnum = UpperNumbers[isSimplified.GetHashCode()])
                fixed (char* numut = NumberUnits[isSimplified.GetHashCode()])
                fixed (char* monut = MoneyUnits[isSimplified.GetHashCode()])
                {
                    var mdec = m.Groups["decimal"];
                    var mInt = m.Groups["integer"];
                    if (mInt.Length > 15 && veryBig == false)
                    {
                        throw new ArgumentException("不建议转换超过15位的整数,除非将veryBig参数设置为true", "number");
                    }
                    if (mdec.Length > 10 && veryBig == false)
                    {
                        throw new ArgumentException("不建议转换超过10位的小,除非将veryBig参数设置为true", "number");
                    }
                    string integer = ParseInteger(p + mInt.Index, p + mInt.Index + mInt.Length - 1, upnum, numut);

                    if (mdec.Success == false)
                    {
                        string unit = null;
                        if (isMoney)
                            unit = monut[0].ToString() + "整";
                        return integer + unit;
                    }
                    else
                    {
                        if (isMoney)
                        {
                            string jiao = upnum[p[mdec.Index] - '0'].ToString();
                            string fen = mdec.Length == 1 ? "0" : upnum[p[mdec.Index + 1] - '0'].ToString();

                            if (jiao != "0")
                            {
                                jiao += monut[1];
                            }

                            if (fen != "0")
                            {
                                jiao += fen + monut[2];
                            }

                            return integer + monut[0].ToString() + jiao;
                        }
                        else
                        {
                            return integer + ParseDecimal(p + mdec.Index, p + mdec.Index + mdec.Length - 1, upnum);
                        }
                    }
                }
            }





        }

        //操作小数部分
        static unsafe string ParseDecimal(char* p, char* end, char* upnum)
        {
            StringBuilder sb = new StringBuilder((int)(end - p));
            sb.Append(upnum[10]);
            while (p <= end)
            {
                sb.Append(upnum[*p - '0']);
                p++;
            }
            return sb.ToString();
        }

        //操作整数部分,为了效率不写递归.....
        static unsafe string ParseInteger(char* p, char* end, char* upnum, char* numut)
        {
            int length = (int)(end - p) + 1;
            StringBuilder sb = new StringBuilder(length * 3);

            if (*p == '-')
            {
                sb.Append(numut[5]);
                p++;
                length--;
                if (*p == '.')
                {
                    sb.Append(upnum[5]);
                }
            }
            else if (*p == '+')
            {
                p++;
                length--;
            }

            bool ling = false;
            bool yi = false;
            bool wan = false;

            while (p <= end)
            {
                int num = *p - '0'; //获得当前的数0-9

                if (num != 0 && ling == true)//需要加 零
                {
                    sb.Append(upnum[0]);
                    ling = false; //重置 参数
                }

                if (length % 8 == 1) //判断是否在"亿"位
                { //如果是
                    int n = length / 8; //计算应该有几个"亿"

                    if (num != 0 || yi == true) //判断是否需要加 单位
                    {
                        if (num != 0) //如果不为 0
                        {
                            sb.Append(upnum[num]); //加入字符串
                        }
                        if (n > 0) sb.Append(numut[4], n);
                        if (ling) ling = false; //重置 参数
                        yi = false; //重置 参数
                        if (wan) wan = false; //重置 参数
                    }
                }
                else //十千百万
                {
                    var uIndex = length % 4; //单位索引
                    if (uIndex == 1) //判断是否在"万"位
                    {
                        if (num != 0 || wan == true) //判断是否需要加 单位
                        {
                            if (num != 0) //如果不为 0
                            {
                                sb.Append(upnum[num]); //加入字符串
                            }
                            sb.Append(numut[uIndex]);
                            if (ling) ling = false; //重置 参数
                            wan = false; //重置 参数
                            if (!yi) yi = true; //设定 参数
                        }
                    }
                    else //十千百
                    {
                        if (num != 0) //如果不为 0
                        {
                            if ((uIndex == 2 && num == 1) == false) //排除 "一十二" 只显示 "十二"
                            {
                                sb.Append(upnum[num]); //加入字符串
                            }
                            sb.Append(numut[uIndex]);//加入单位
                            if (!yi) yi = true; //设定 参数
                            if (!wan) wan = true; //设定 参数
                        }
                        else if (ling == false)
                        {
                            ling = true;
                        }
                    }
                }

                length--;
                p++;
            }
            return sb.ToString();
        }

    }
}
