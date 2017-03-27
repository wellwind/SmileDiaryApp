using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp
{
    public class BadgeData
    {
        public string LastRecordDate { get; set; }

        /// <summary>
        /// 持續的天數
        /// </summary>
        public int RecordDays { get; set; }

        /// <summary>
        /// 分數連續達到100的天數
        /// </summary>
        public int Smile100Days { get; set; }

        /// <summary>
        /// 分數連續高於90的天數
        /// </summary>
        public int Smile90Days { get; set; }

        /// <summary>
        /// 連續高於80
        /// </summary>
        public int Smile80Days { get; set; }

        /// <summary>
        /// 連續低於50的天數
        /// </summary>
        public int SmileLessThen50Days { get; set; }

        /// <summary>
        /// 第一天記錄微笑
        /// </summary>
        public bool Badge001_FirstRecord { get; set; }

        /// <summary>
        /// 連續三天記錄微笑
        /// </summary>
        public bool Badge002_3DaysRecord { get; set; }

        /// <summary>
        /// 連續十天記錄微笑
        /// </summary>
        public bool Badge003_10DaysRecord { get; set; }

        /// <summary>
        /// 連續三十天記錄微笑
        /// </summary>
        public bool Badge004_30DaysRecord { get; set; }

        /// <summary>
        /// 連續一百天記錄微笑
        /// </summary>
        public bool Badge005_100DaysRecord { get; set; }

        /// <summary>
        /// 連續三百天記錄微笑
        /// </summary>
        public bool Badge006_300DaysRecord { get; set; }

        /// <summary>
        /// 三天微笑指數超過80%
        /// </summary>
        public bool Badge101_3Days80 { get; set; }

        /// <summary>
        /// 三天微笑指數超過90%
        /// </summary>
        public bool Badge102_3Days90 { get; set; }

        /// <summary>
        /// 三天微笑指數達到100%
        /// </summary>
        public bool Badge103_3Days100 { get; set; }

        /// <summary>
        /// 十天微笑指數達到100%
        /// </summary>
        public bool Badge104_10Days100 { get; set; }

        /// <summary>
        /// 三十天微笑指數達到100%
        /// </summary>
        public bool Badge105_30Days100 { get; set; }

        /// <summary>
        /// 三天微笑指數低於50%
        /// </summary>
        public bool Badge201_3DaysLessThan50 { get; set; }

        /// <summary>
        /// 十天微笑指數低於50%
        /// </summary>
        public bool Badge202_10DaysLessThan50 { get; set; }
    }
}
