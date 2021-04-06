using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Models
{
    public class Game
    {
        [DisplayName("Board ID")]
        public int id { get; set; }
        public int Userid { get; set; }
        [DisplayName("Time")]
        public int Time { get; set; }
        public string BoardJSON { get; set; }
        public Board board { get; set; }
        [DisplayName("Finished Game")]
        public bool IsFinished { get; set; }
        [DisplayName("Is Done")]
        public bool IsSaved { get; set; }
        [DisplayName("Date Saved")]
        public DateTime date { get; set; }
    }
}
