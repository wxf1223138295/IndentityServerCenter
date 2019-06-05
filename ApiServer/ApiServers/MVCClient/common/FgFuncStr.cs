using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MVCClient.common
{
      public class FgFuncStr
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

     
        /// <summary>
        /// System.Windows.Forms.Keys类型转换为String
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        

        /// <summary>
        /// 转换为字符串,空对象转换为空字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullToStr(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return "";
            else
                return obj.ToString().Trim();
        }

        /// <summary>
        /// 为空返回字符串"null",否则返回其实际值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string IntToStr(int? obj)
        {
            if (obj.HasValue)
                return obj.Value.ToString();
            else
                return "null";
        }

        /// <summary>
        /// 为空返回字符串"null",否则返回其实际值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DecimalToStr(decimal? obj)
        {
            if (obj.HasValue)
                return obj.Value.ToString();
            else
                return "null";
        }

        /// <summary>
        /// 转换为字符串,空对象转换为空字符串,并带单引号
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullToStrWithExpr(object obj)
        {
            if (string.IsNullOrEmpty(NullToStr(obj)))
                return Expr("");
            return Expr(obj.ToString().Trim());
        }

        /// <summary>
        /// 为字符串加单引号
        /// </summary>
        /// <param name="Str">要处理的字符串</param>
        /// <returns></returns>
        public static string Expr(string Str)
        {
            return "'" + Str.Replace("'", "''") + "'";
        }

        /// <summary>
        /// 转换为int,空对象转换为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int NullToInt(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0;
            else
                return ToInt32(obj.ToString());
        }

        /// <summary>
        /// 转换为Int64(long),空对象转换为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long NullToLong(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0;
            else try
                {
                    return long.Parse(obj.ToString());
                } 
                catch { return 0; }
        }

        /// <summary>
        /// 转换为Int16(short),空对象转换为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short NullToShort(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0;
            else try
                {
                    return short.Parse(obj.ToString());
                }
                catch { return 0; }
        }

        /// <summary>
        /// 转换为Int16(short),空对象转换为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float NullToFloat(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0;
            else try
                {
                    return float.Parse(obj.ToString());
                }
                catch { return 0; }
        }

        /// <summary>
        /// 把一个字符串类型转化为可能的整型
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public static int ToInt32(string S)
        {
            S = S.Trim();
            if (S.Trim() == "") return (0);
            if (S.IndexOf("-") >= 0)
            {   //有负号，但不是开头，则转换为0
                if (S.StartsWith("-") == false) return (0);
                if (S.StartsWith("--")) return (0);
            }
            for (int i = 0; i < S.Length; i++)

            {
                switch (S.Substring(i, 1))
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        break;
                    case "-":
                        if (S.Length == 1) return (0);
                        break;
                    default:
                        if (i == 0)
                            return (0);
                        else
                            try { return (Convert.ToInt32(S.Substring(0, i))); }
                            catch { return 0; }
                }
            }
            try { return (Convert.ToInt32(S)); }
            catch { return 0; }
        }

        /// <summary>
        /// 判断字符串是否包含非数字字符
        /// </summary>
        /// <returns>返回是否</returns>
        public static bool IsNumber(string str)
        {
            foreach (char _chr in str)
                if (!Char.IsNumber(_chr))
                    return false;

            return true;
        }

        /// <summary>
        /// 判断字符串是否数字
        /// </summary>
        /// <returns>返回是否</returns>
        public static bool IsNumeric(object str)
        {
            try
            {
                decimal _d = Convert.ToDecimal(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 转换为Decimal,空对象转换为0.0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal NullToDecimal(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0.0M;
            else
                return FgFuncStr.ToDecimal(obj.ToString());
        }
        /// <summary>
        /// 转换为double,空对象转换为0.0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double NullToDouble(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)	//数据库空可以不判断,系统会自动转换为null
                return 0.0;
            else
                return FgFuncStr.ToDouble(obj.ToString());
        }

        /// <summary>
        /// 把一个字符串类型转化为可能的俘点型
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public static double ToDouble(string S)
        {
            bool Flag = false;
            S = S.Trim();
            if (S.Trim() == "") return (0);
            if (S.IndexOf("-") >= 0)
            {   //有负号，但不是开头，则转换为0
                if (S.StartsWith("-") == false) return (0);
                if (S.StartsWith("--")) return (0);
            }
            for (int i = 0; i < S.Length; i++)
            {
                switch (S.Substring(i, 1))
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        break;
                    case "-":
                        if (S.Length == 1) return (0);      //只有一个点的情况
                        break;
                    case ".":
                        if (S.Length == 1) return (0);      //只有一个点的情况
                        if (Flag == false)
                        {
                            Flag = true;
                        }
                        else
                        {   //如果出现2个点
                            if (i == 0)  //实际不可能 I=0
                                return (0);
                            else         //取这个点之前的内容来进行计算
                                try { return (Convert.ToDouble(S.Substring(0, i))); }
                                catch { return (0); }
                        }
                        break;
                    default:   //出现非法数字
                        if (i == 0)
                            return (0);
                        else
                            try { return (Convert.ToDouble(S.Substring(0, i))); }
                            catch { return (0); }
                }
            }
            try { return (Convert.ToDouble(S)); }
            catch { return 0; }
        }

        /// <summary>
        /// 把一个字符串类型转化为可能的俘点型
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string S)
        {
            bool Flag = false;
            S = S.Trim();
            if (S.Trim() == "") return (0);
            if (S.IndexOf("-") >= 0)
            {   //有负号，但不是开头，则转换为0
                if (S.StartsWith("-") == false) return (0);
                if (S.StartsWith("--")) return (0);
            }
            for (int i = 0; i < S.Length; i++)
            {
                switch (S.Substring(i, 1))
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        break;
                    case "-":
                        if (S.Length == 1) return (0M);
                        break;
                    case ".":
                        if (S.Length == 1) return (0M);      //只有一个点的情况
                        if (Flag == false)
                        {
                            Flag = true;
                        }
                        else
                        {   //如果出现2个点
                            if (i == 0)  //实际不可能 I=0
                                return (0M);
                            else         //取这个点之前的内容来进行计算
                                try { return (Convert.ToDecimal(S.Substring(0, i))); }
                                catch { return 0m; }
                        }
                        break;
                    default:   //出现非法数字
                        if (i == 0)
                            return (0M);
                        else
                            try { return (Convert.ToDecimal(S.Substring(0, i))); }
                            catch { return 0m; }
                }
            }
            try { return (Convert.ToDecimal(S)); }
            catch { return 0m; }
        }

        /// <summary>
        /// 取字符串srcs左边lenn个字符组成的串
        /// </summary>
        /// <param name="srcs"></param>
        /// <param name="lenn"></param>
        /// <returns></returns>
        public static String LeftStr(String srcs, int lenn)
        {
            return MidStr(srcs, 0, lenn);
        }

        /// <summary>
        /// 取字符串srcs右边lenn个字符组成的串
        /// </summary>
        /// <param name="srcs"></param>
        /// <param name="lenn"></param>
        /// <returns></returns>
        public static String RightStr(String srcs, int lenn)
        {
            if (srcs.Length >= lenn)
                return MidStr(srcs, srcs.Length - lenn);
            else
                return srcs;
        }

        /// <summary>
        /// 取字符串srcs从startp开始到结尾的字符组成的字符串
        /// </summary>
        /// <param name="srcs"></param>
        /// <param name="startp"></param>
        /// <returns></returns>
        public static String MidStr(String srcs, int startp)
        {
            if ((startp >= 0) && (srcs.Length >= startp))
                return srcs.Substring(startp);
            else
                return "";
        }

        /// <summary>
        /// 取字符串srcs从startp开始的lenn个长度字符组成的字符串
        /// </summary>
        /// <param name="srcs"></param>
        /// <param name="startp"></param>
        /// <param name="lenn"></param>
        /// <returns></returns>
        public static String MidStr(String srcs, int startp, int lenn)
        {
            if ((startp >= 0) && (lenn > 0) && (srcs.Length >= startp))
            {
                if (lenn > srcs.Length) lenn = srcs.Length;
                return srcs.Substring(startp, lenn);
            }
            else
                return "";
        }

        /// <summary>
        /// 从字符串strs中取从startp位置开始遇到的第一个subs所在的位置
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="subs"></param>
        /// <param name="startp"></param>
        /// <returns></returns>
        public static int PosStr(String strs, String subs, int startp)
        {
            if (startp >= strs.Length)
                return -1;
            else
                return strs.IndexOf(subs, startp);
        }

        /// <summary>
        /// 从字符串strs中取遇到的第一个subs所在的位置
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="subs"></param>
        /// <returns></returns>
        public static int PosStr(String strs, String subs)
        {
            return PosStr(strs, subs, 0);
        }

        /// <summary>
        /// 将字符串srs中标识为ids的值删除，其中dpts为分隔符，equs为等值符
        /// 字符串strs的格式为：&lt;name&gt;&lt;equs&gt;&lt;value&gt;[&lt;dpts&gt;&lt;name&gt;&lt;equs&gt;&lt;value&gt;]
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="dpts"></param>
        /// <param name="equs"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static String DelParmStr(String strs, String dpts, String equs, String ids)
        {
            int pos0, pos1, pos2;
            strs = dpts + strs;
            pos0 = PosStr(strs.ToUpper(), (dpts + ids + equs).ToUpper());
            if (pos0 >= 0)
            {
                pos1 = pos0 + ((string)(dpts + ids + equs)).Length;
                pos2 = PosStr(strs, dpts, pos1);
                if (pos2 >= 0)
                {
                    if (pos0 >= 1)
                        strs = LeftStr(strs, pos0) + MidStr(strs, pos2);
                    else
                        strs = MidStr(strs, pos2);
                }
                else
                {
                    if (pos0 >= 1)
                        strs = LeftStr(strs, pos0);
                    else
                        strs = dpts;
                }
            }
            strs = MidStr(strs, dpts.Length);
            return strs;
        }

        /// <summary>
        /// 将字符串srs中标识为ids的值删除，其中dpts为分隔符，等值符为"="
        /// 字符串strs的格式为：&lt;name&gt;=&lt;value&gt;[&lt;dpts&gt;&lt;name&gt;=&lt;value&gt;]
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="dpts"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static String DelParmStr(String strs, String dpts, String ids)
        {
            return DelParmStr(strs, dpts, "=", ids);
        }

        /// <summary>
        /// 将字符串srs中标识为ids的值删除，其中分隔符为";"，等值符为"="
        /// 字符串strs的格式为：&lt;name&gt;=&lt;value&gt;[;&lt;name&gt;=&lt;value&gt;]
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static String DelParmStr(String strs, String ids)
        {
            return DelParmStr(strs, ";", ids);
        }

        /// <summary>
        /// 将一个整数Ascii码转化为一个字符
        /// </summary>
        /// <param name="ascode"></param>
        /// <returns></returns>
        public static char Chr(int ascode)
        {
            return (char)ascode;
        }

        /// <summary>
        /// 从字符串str中取出第一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char Chr(string str)
        {
            return Chr(str, 0);
        }

        /// <summary>
        /// 从字符串str中取出第chrpos个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chrpos"></param>
        /// <returns></returns>
        public static char Chr(string str, int chrpos)
        {
            return str.ToCharArray(chrpos, 1)[0];
        }

        /// <summary>
        /// 获取字符chr的Ascii码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static int Asc(char chr)
        {
            return Convert.ToInt32(chr);
        }

        /// <summary>
        /// 获取字符串str中第一个字符的Ascii码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Asc(string str)
        {
            return Asc(str, 0);
        }

        /// <summary>
        /// 获取字符串str中第chrpos个字符的Ascii码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chrpos"></param>
        /// <returns></returns>
        public static int Asc(string str, int chrpos)
        {
            return Asc(str.ToCharArray(chrpos, 1)[0]);
        }

        /// <summary>
        /// 转换实体内DateTime?类型到字符串
        /// </summary>
        /// <param name="_Date">DateTime? 类型数据</param>
        /// <param name="psFormatStr">样式</param>
        /// <returns>返回字符串</returns>
        public static string NullToDateTimeStr(DateTime? _Date, string psFormatStr)
        {
            string _lsRT = "";
            if (_Date != null)
                _lsRT = Convert.ToDateTime(_Date).ToString(psFormatStr);

            return _lsRT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_list"></param>
        /// <returns></returns>
        public static string GetString<T>(List<T> _list)
        {
            var _Str = string.Empty;
            if (_list == null) { return _Str; }
            _list.ForEach(p => _Str += p.ToString() + ";");
            return _Str;
        }

        /// <summary>
        /// object 转换成 DataTime
        /// </summary>
        /// <param name="_Obj"></param>
        /// <returns></returns>
        public static DateTime Obj2DateTime(object _Obj)
        {
            if (_Obj == null || _Obj is DBNull) { return DateTime.MinValue; }
            if (_Obj is DateTime) { return (DateTime)_Obj; }

            var _Val = DateTime.MinValue;
            bool _IsExp = false;
            try { _Val = Convert.ToDateTime(_Obj.ToString()); }
            catch { _IsExp = true; };
            try
            {
                if (_IsExp)
                {
                    var _Formats = new string[]
                    {
                        "yyyyMMdd",
                        "yyyy/MM/dd",
                        "yyyy-MM-dd",
                        "yyyy.MM.dd",
                        "yyyy MM dd",
                        "yyyy年MM月dd日",
                        "MM/dd/yyyy",
                        "MM-dd-yyyy",
                        "MM.dd.yyyy",
                        "MM dd yyyy",
                        "MM月dd日",
                        "yyyy年MM月",
                        "yyyyMMddHHmm",
                        "yyyyMMddHHmmss",
                        "yyyyMMdd HHmmss",
                        "yyyy/MM/dd HH:mm:ss",
                        "yyyy/MM/dd H:mm:ss",
                        "yyyy.MM.dd HH:mm:ss",
                        "yyyy.MM.dd H:mm:ss",
                        "yyyy-MM-dd HH:mm:ss",
                        "yyyy-MM-dd H:mm:ss",
                        "yyyy-M-d HH:mm:ss",
                        "yyyy-M-d H:mm:ss",
                        "HHmmss",
                        "HH:mm:ss",
                    };
                    _Val = DateTime.ParseExact(_Obj.ToString(), _Formats, null, System.Globalization.DateTimeStyles.None);
                }
            }
            catch { return DateTime.MinValue; }
            return _Val;
        }

        /// <summary>
        /// 连续按实际长度获取字符串中的数据 从第1位开始计算start
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="Start"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string MidStrB(string Str, int Start, int Length)
        {
            int L = LenB(Str);
            if (L < Start) return "";

            string T = "";
            for (int i = 0; i < Str.Length; i++)
            {
                if (LenB(Str.Substring(0, i + 1)) >= Start)
                    T = T + Str.Substring(i, 1);

                if (LenB(T) >= Length)
                    return MyTrim(T);
            }
            return ""; //MidStrB = MyTrim(MidStrB);
        }

        /// <summary>
        /// 字符串长度包括汉字两位
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int LenB(string Str)
        {
            return System.Text.Encoding.Default.GetByteCount(Str);
        }

        /// <summary>
        /// 可以将Chr(0)结束的字符串截取空格
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string MyTrim(string Str)
        {
            string T = "";
            int i;

            int L = Str.Length;
            for (i = 0; i < L; i++)
            {
                T = Str.Substring(i, 1);
                if (T != " " && Convert.ToInt32(Convert.ToChar(T)) != 0)
                {
                    break;
                }
            }
            Str = Str.Substring(i, L - i);

            L = Str.Length;
            for (i = L - 1; i >= 0; i--)
            {
                T = Str.Substring(i, 1);
                if (T != " " && Convert.ToInt32(Convert.ToChar(T)) != 0)
                {
                    break;
                }
            }
            Str = Str.Substring(0, i + 1);

            return Str;
        }

        /// <summary>
        /// 字符串补空格
        /// </summary>
        /// <param name="psStr">字符串</param>
        /// <param name="piCount">总长度</param>
        /// <returns>返回传入字符串</returns>
        public static string SetStrNSP(string psStr, int piCount)
        {
            while (Encoding.Default.GetByteCount(psStr) <= piCount)
            {
                psStr += " ";
            }
            return psStr;
        }

        /// <summary>
        /// 获取类的某属性名称 (eg:  string s = GetPropName&lt;T>(p => p.prop1);)
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="expr">lambda表达式</param>
        /// <returns>属性的名称</returns>
        public static string GetPropName<T>(Expression<Func<T, object>> expr)
        {
            try
            {
                switch (expr.Body.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)expr.Body).Member.Name;
                    case ExpressionType.Convert:
                        return ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
                    default:
                        return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取时间类型sql字符串
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="IsOracle">是否是oracle数据库</param>
        /// <returns></returns>
        public static string GetSqlTimeStr(DateTime? dt, bool IsOracle = false)
        {
            if (dt == null) return "NULL";
            if (IsOracle)
                return "TO_DATE('" + ((DateTime)dt).ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')";
            return "'" + ((DateTime)dt).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }

        /// <summary>
        /// 针对可null值类型做数据库insert时转换成"NULL"
        /// </summary>
        /// <param name="obj">值类型如:int? decimal? double? ...</param>
        /// <returns></returns>
        public static string NullToDBStr(object obj)
        {
            if (obj == null) { return "NULL"; }
            return obj.ToString();
        }

        /// <summary>
        /// 字符串转换成bool值
        /// </summary>
        /// <param name="_str">字符串</param>
        /// <param name="_bool">bool值的默认值</param>
        /// <returns></returns>
        public static bool GetBool(string _str, bool _bool)
        {
            if (!string.IsNullOrEmpty(_str))
                try { _bool = Convert.ToBoolean(_str); }
                catch { }
            return _bool;
        }

        /// <summary>
        /// 字符串转换成Int值
        /// </summary>
        /// <param name="_str">字符串</param>
        /// <param name="_defaut">int默认值</param>
        /// <param name="_max">最大值</param>
        /// <param name="_min">最小值</param>
        /// <returns></returns>
        public static int GetInt(string _str, int _defaut, int _max = 0, int _min = 0)
        {
            if (string.IsNullOrEmpty(_str)) return _defaut;

            int _temp = 0;
            try { _temp = FgFuncStr.NullToInt(_str); }
            catch { return _defaut; }

            if ((_temp >= _min && _temp <= _max) || (_max == _min && _max == 0))
                return _temp;

            return _defaut;
        }

        /// <summary>
        /// 字符串转换成List string
        /// </summary>
        /// <param name="_str">字符串</param>
        /// <param name="_chr">截取方式</param>
        /// <returns></returns>
        public static List<string> GetList(string _str, char _chr = ',')
        {
            List<string> _list = new List<string>();
            if (!string.IsNullOrEmpty(_str))
                try { _list = _str.Split(_chr).ToList(); }
                catch { }
            return _list;
        }

        /// <summary>
        /// 字符串转换成double
        /// </summary>
        /// <param name="_str">字符串</param>
        /// <param name="_double">默认值</param>
        /// <returns></returns>
        public static double GetDouble(string _str, double _double)
        {
            if (!string.IsNullOrEmpty(_str))
                try { _double = FgFuncStr.NullToDouble(_str); }
                catch { }
            return _double;
        }

        /// <summary>
        /// 字符串转换成decimal
        /// </summary>
        /// <param name="_str">字符串</param>
        /// <param name="_decimal">默认值</param>
        /// <returns></returns>
        public static decimal GetDecimal(string _str, decimal _decimal)
        {
            if (!string.IsNullOrEmpty(_str))
                try { _decimal = FgFuncStr.NullToDecimal(_str); }
                catch { }
            return _decimal;
        }

        /// <summary>
        /// 发票号累加
        /// </summary>
        /// <param name="_invoiceNumber">发票号</param>
        /// <param name="_count">发票张数</param>
        /// <returns></returns>
        public static string InvoiceNumberAdd(string _invoiceNumber, int _count)
        {
            int _index = _invoiceNumber.ToList().FindLastIndex(p => !Char.IsDigit(p));

            string _head = _index >= 0 ? _invoiceNumber.Substring(0, _index + 1) : string.Empty;
            string _number = _index >= 0 ? _invoiceNumber.Substring(_index + 1) : _invoiceNumber;

            long _result = 0;
            if (long.TryParse(_number, out _result))
                _result += _count;

            return _head + _result.ToString().PadLeft(_number.Length, '0');
        }
    }
}