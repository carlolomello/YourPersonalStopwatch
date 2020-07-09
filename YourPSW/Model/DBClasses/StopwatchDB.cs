using System;
using System.Collections;
using SQLite;

namespace YourPSW.Model
{
    public class StopwatchDB
    {
        [PrimaryKey, AutoIncrement] public int id_stopwatch { get; set; }
        public string stopwatch_name {get; set;}
        public string img_name {get; set; }

    }
}
