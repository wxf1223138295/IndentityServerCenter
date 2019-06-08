using System;

namespace ToolClient.common
{
     public class FgFuncAge
    {
        /// <summary>
        /// 输入年龄,转化为出生日期 ,最后一个字符为 D 的转化为天,M 转化为月,Y 转化为年
        /// </summary>
        /// <param name="Age">年龄 (如40D(40天),3M(3个月),5Y(5岁),或20(20岁))</param>
        /// <returns></returns>
        public static string GetBirthday(string Age)
        {
            if (string.IsNullOrEmpty(Age)) return string.Empty;
            string _Tag = string.Empty;
            int _Num = 0;
            int _Length = 0; //记录Age字符串的长度  
            int _Count = 0;  //记录Age中的非数字字符的个数
            bool _IsNumber = true; //Age是否是数字

            _Length = Age.Length;

            foreach (char chr in Age)//检查Age里面是否含有字母
                if (!Char.IsNumber(chr))
                {
                    switch (chr)
                    {
                        case 'M':
                        case 'm':
                        case 'd':
                        case 'D':
                        case 'Y':
                        case 'y':
                        case '-':
                        case '/':
                            break;
                        default:
                            return "";
                    }
                    _IsNumber = false;
                    _Count++;
                }

            if (_Length > 10 || _Count > 2) return "";

            if (_IsNumber)//如果都是数字
            {
                _Num = Int32.Parse(Age); //把字符串转换成数字
                if (_Num > 0 && _Num < 200)//如果年龄大于0小于200
                    return (DateTime.Now.AddYears(-_Num).ToString("yyyy-MM-dd"));
                if (_Length == 8) //如果出生日期为20111121,8位数的形式
                    try { return DateTime.Parse(Age.Substring(0, 4) + "-" + Age.Substring(4, 2) + "-" + Age.Substring(6, 2)).ToString("yyyy-MM-dd"); }
                    catch { return ""; }
                return "";
            }

            if (_Count == 1) //包含一个字母的情况(如 结尾的M D Y)
            {
                try { _Num = Int32.Parse(Age.Substring(0, _Length - 1)); }//去掉最后一个字符,转换成数字
                catch { return ""; } //转化失败返回原字符串

                _Tag = Age.Substring(_Length - 1, 1).ToUpper();  //取最后一个字符 如M D Y...
                switch (_Tag)
                {
                    case "D": //出生天数
                        return (DateTime.Now.AddDays(-_Num).ToString("yyyy-MM-dd"));
                    case "M": //出生月数
                        return (DateTime.Now.AddMonths(-_Num).ToString("yyyy-MM-dd"));
                    case "Y": // 岁数
                        return (DateTime.Now.AddYears(-_Num).ToString("yyyy-MM-dd"));
                    default:  //其他未知情况
                        return "";
                }
            }
            try { return Convert.ToDateTime(Age).ToString("yyyy-MM-dd"); }
            catch { return ""; }//转换成日期失败,返回原字符串
        }

                /// <summary>
        /// 根据出生日期算出年龄
        /// </summary>
        /// <param name="Brithday">出生日期</param>
        /// <returns></returns>
        public static string GetAge(string Brithday)
        {
            return GetAge(Brithday, true);
        }

        /// <summary>
        /// 根据出生日期算出年龄
        /// </summary>
        /// <param name="Brithday">出生日期</param>
        /// <param name="DisArea">是否中文格式显示</param>
        /// <returns></returns>
        public static string GetAge(string Brithday, bool DisArea = false)
        {
            string _sRtAge = string.Empty;// 年龄的字符串表示
            DateTime _Brithday = DateTime.Now;
            DateTime _DtNow = DateTime.Now;
            int _iYear = 0;// 岁
            int _iMonth = 0;// 月
            int _iDay = 0;// 天
            //转换成日期失败,返回原字符串
            try { _Brithday = FgFuncStr.Obj2DateTime(Brithday); }
            catch { return ""; }

            //计算天数
            _iDay = _DtNow.Day - _Brithday.Day;
            if (_iDay < 0)
            {
                _DtNow = _DtNow.AddMonths(-1);
                _iDay += DateTime.DaysInMonth(_DtNow.Year, _DtNow.Month);
            }
            //计算月数
            _iMonth = _DtNow.Month - _Brithday.Month;
            if (_iMonth < 0)
            {
                _iMonth += 12;
                _DtNow = _DtNow.AddYears(-1);
            }
            //计算年数
            _iYear = _DtNow.Year - _Brithday.Year;

            //一岁以下可以输出天数
            if (_iYear < 1)
            {
                if (_iMonth < 1)//小于1个月按天显示，如25天
                    _sRtAge = _iDay.ToString() + "天";
                else if (_iMonth >= 1 && _iMonth < 3)//大于等于1个月小于3个月按月+天显示，如 1月12天
                {
                    _sRtAge = _iMonth.ToString() + "月";
                    if (_iDay != 0) _sRtAge += _iDay.ToString() + "天";
                }
                else if (_iMonth >= 3 && _iMonth < 12)//大于等于3个月，小于12个月按月显示，如6月
                    _sRtAge = _iMonth.ToString() + "月";
            }
            else
            {
                // if (_iYear >= 1 && _iYear < 14)//大于等于1岁小于14岁，按年+月显示，如3岁5月
                // {
                //     _sRtAge = _iYear.ToString() + "岁";
                //     if (_iMonth != 0) _sRtAge += _iMonth.ToString() + "月";
                // }
                // else if (_iYear >= 14)//大于等于14岁，按年显示，如16岁
                //     _sRtAge = _iYear.ToString() + "岁";
                _sRtAge = _iYear.ToString() + "岁"+_iMonth.ToString() + "月"+_iDay.ToString() + "天";

            }
            if (DisArea)
                _sRtAge = _sRtAge.Replace('岁', 'Y').Replace('月', 'M').Replace('天', 'D');
            return _sRtAge;
        }

        /// <summary>
        /// 格式化日期显示
        /// </summary>
        /// <param name="_date">20130606</param>
        /// <returns>2013-06-06</returns>
        public static string FormatDateStr(string _date)
        {
            if (string.IsNullOrEmpty(_date) || _date.Length != 8) return _date;
            return _date.Insert(4, "-").Insert(7, "-");
        }

        /// <summary>
        /// 格式化时间显示
        /// </summary>
        /// <param name="_time">235959</param>
        /// <returns>23:59:59</returns>
        public static string FormatTimeStr(string _time)
        {
            if (string.IsNullOrEmpty(_time) || _time.Length != 6) return _time;
            return _time.Insert(2, ":").Insert(5, ":");
        }
    }
}