using System;
using System.Collections;
using SQLite;

namespace YourPSW.Model
{
    public class Stopwatch
    {
        private int id_stopwatch;
        private string stopwatch_name;
        private string img_name;
        private ArrayList timeList;


        public Stopwatch()
        {
        }

        public Stopwatch(string stopwatch_name, string img_name)
        {
            this.stopwatch_name = stopwatch_name;
            this.img_name = img_name;
        }

        public Stopwatch(int id_stopwatch, string stopwatch_name, string img_name)
        {
            this.id_stopwatch = id_stopwatch;
            this.stopwatch_name = stopwatch_name;
            this.img_name = img_name;
        }

        public Stopwatch(int id_stopwatch, string stopwatch_name, string img_name, ArrayList timeList) : this(id_stopwatch, stopwatch_name, img_name)
        {
            this.timeList = timeList;
        }

        public int getId_stopwatch()
        {
            return this.id_stopwatch;
        }

        public void setId_stopwatch(int id_stopwatch)
        {
            this.id_stopwatch = id_stopwatch;
        }

        public string getStopwatch_name()
        {
            return this.stopwatch_name;
        }

        public void setStopwatch_name(string stopwatch_name)
        {
            this.stopwatch_name = stopwatch_name;
        }
        public string getImg_name()
        {
            return this.img_name;
        }

        public void setImg_name(string img_name)
        {
            this.img_name = img_name;
        }

        public ArrayList getTimeList()
        {
            return this.timeList;
        }

        public void setTimeList(ArrayList timeList)
        {
            this.timeList = timeList;
        }


    }
}
