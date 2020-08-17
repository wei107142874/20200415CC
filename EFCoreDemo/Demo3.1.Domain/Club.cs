using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo3._1.Domain
{
    /// <summary>
    /// 俱乐部
    /// </summary>
    public class Club
    {
        public Club()
        {
            Players = new List<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>
        [Column(TypeName="date")]
        public DateTime DateOfEstablishment { get; set; }

        /// <summary>
        /// 历史
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// 所属联赛
        /// </summary>
        public League League { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Player> Players { get; set; }
    }
}
