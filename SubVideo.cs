using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSub
{
    public class SubVideo
    {
        public int Id { get; set; }
        public string Sub { get; set; }
        public TimeSpan Time { get; set; }
        public string TranSub { get; set; }

        public Image ImageSub { get; set; }

        public SubVideo (int id, string sub, TimeSpan time, string tranSub, Image imageSub)
        {
            this.Id = id;
            this.Sub = sub;
            this.Time = time;
            this.TranSub = tranSub;
            this.ImageSub = imageSub;
        }

        public static List<SubVideo> ListSub = new List<SubVideo>();
    }
}
