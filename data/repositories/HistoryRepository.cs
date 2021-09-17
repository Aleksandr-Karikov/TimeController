using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeController.Data;

namespace TimeController.data.repositories
{
    public class HistoryRepository //getting information from table history in database
    {
        private readonly TimeControllerDbContext DbContext;
        public HistoryRepository (TimeControllerDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public IEnumerable<string> lastSevenDays(string userId) //get list of times at last week
        {
            DateTime thisDay = DateTime.Today;
            List<string> lastWeek = new List<string>();
            while (thisDay.DayOfWeek != DayOfWeek.Monday) //get date of last Monday
            {
                thisDay = thisDay.AddDays(-1);
            }
            int count = 0;
            while(count < 7 )// Get last week
            {
                var his = DbContext.history
                    .Where(b => b.UserID == userId && b.Date.Date == thisDay.Date)
                    .FirstOrDefault();
                if (his == null) // if have no record
                {
                    lastWeek.Add("");
                }
                else // parse millisecon at normal view
                {
                    TimeSpan t = TimeSpan.FromMilliseconds(his.Time);
                    lastWeek.Add(string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds));
                }
                thisDay = thisDay.AddDays(1);
                count++;
            }
            return lastWeek;
        }
    }
}
