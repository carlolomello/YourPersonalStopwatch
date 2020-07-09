namespace YourPSW.Model
{
    public class Timewatch
    {
        
        public int position { get; set; }
        public int id_stopwatch { get; set; }
        public string time_name { get; set; }
        public string duration_time { get; set; }
        public string sound_name { get; set; }

        public Timewatch()
        {
        }

        public Timewatch(string time_name, string duration_time, string sound_name)
        {
            
            this.time_name = time_name;
            this.duration_time = duration_time;
            this.sound_name = sound_name;
        }


        public Timewatch(int position, int id_stopwatch, string time_name, string duration_time, string sound_name)
        {
            this.position = position;
            this.id_stopwatch = id_stopwatch;
            this.time_name = time_name;
            this.duration_time = duration_time;
            this.sound_name = sound_name;
        }

        public int getPosition()
        {
            return this.position;
        }
        public int getId_stopwatch()
        {
            return this.id_stopwatch;
        }
        public string getTime_name()
        {
            return this.time_name;
        }
        public string getDuration_time()
        {
            return this.duration_time;
        }
        public string getSound_name()
        {
            return this.sound_name;
        }

    }
}
