using System.Collections.Generic;

namespace SVGDesignService.model
{
    public class TrafficResult
    {
        /// <summary>
        /// 状态码
        /// 本次API访问状态，如果成功返回0，如果失败返回其他数字。
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 对status的中文描述
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 组成规则：整体拥堵情况概述+拥堵路段。
        /// 示例："该区域整体拥堵。京包高速：北向南,从开拓路5到京新高速拥堵。
        ///     京新上地桥：北向南,京新上地桥拥堵。
        ///     京新高速：北向南,从京包高速到耳通百安拥堵。
        ///     小营西路：西向东,上地三街附近拥堵。"
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Evaluation evaluation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Road_traffic> road_traffic { get; set; }

        public class Evaluation
        {
            /// <summary>
            /// 道路的整体拥堵评价，较status更为细致，
            /// 分为：畅通、较为畅通、缓行、轻微拥堵、拥堵、严重拥堵
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 如："拥堵"
            /// </summary>
            public string status_desc { get; set; }

        }
        public class Road_traffic
        {
            /// <summary>
            /// 如："信息路"、"北五环"
            /// </summary>
            public string road_name { get; set; }
            /// <summary>
            /// 若道路上有拥堵路段，则返回该字段。
            /// 若无拥堵路段，则不返回该字段
            /// 注意：拥堵路段是依据拥堵情况、车流量、拥堵距离等因素综合计算得到，并不完全参考拥堵情况
            /// </summary>
            public List<Congestion_sections> congestion_sections { get; set; } = null;

            public class Congestion_sections
            {
                /// <summary>
                /// 如：南向北，北新桥地铁站附近严重拥堵
                /// </summary>
                public string section_desc { get; set; }

                /// <summary>
                /// 支持以下值：0：未知路况1：畅通2：缓行3：拥堵4：严重拥堵
                /// </summary>
                public int status { get; set; }

                /// <summary>
                /// 当前路段的平均通行速度 单位：千米/小时
                /// </summary>
                public double speed { get; set; }
                /// <summary>
                /// 	单位：米
                /// </summary>
                public int congestion_distance { get; set; }
                /// <summary>
                /// 相较10分钟前拥堵变化情况，支持以下值：
                /// 持平：与10分钟前变化不大
                /// 缓解：较10分钟前拥堵有所缓解
                /// 加重：较10分钟前拥堵加重       
                /// </summary>
                public string congestion_trend { get; set; }
            }
        }
    }
}
