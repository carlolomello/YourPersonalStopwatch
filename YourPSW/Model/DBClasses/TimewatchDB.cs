using System;
using SQLite;

namespace YourPSW.Model
{
    public class TimewatchDB
    {
        public int position { get; set; }
        public int id_stopwatch { get; set; }
        public string time_name { get; set; }
        public string duration_time { get; set; }
        public string sound_name { get; set; }

    }
}
