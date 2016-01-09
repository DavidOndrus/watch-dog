namespace WatchDog
{
    public class ProgramItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int runningTimeInMins { get; private set; }

        public ProgramItem(string name, string path)
        {
            this.Name = name;
            this.Path = path;
            this.runningTimeInMins = 0;
        }

        public override string ToString()
        {
            return this.Name + "; " + this.Path;
            //return "Name: " + this.Name + "; Full path: " + this.Path + "; Running time: " + runningTimeInMins;
        }

        public string GetNamePathInfo()
        {
            return this.Name + Constants.SEPARATOR + this.Path;
        }

        public void increaseRunningTime(int minutes)
        {
            this.runningTimeInMins += minutes;
        }
    }
}
