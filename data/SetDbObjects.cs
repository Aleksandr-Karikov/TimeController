using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeController.Data;

namespace TimeController.data
{
    public class SetDbObjects //saving result of working in database
    {
        public static void AddRecordHistory (TimeControllerDbContext timeControllerDbContext , string id, int time) // add record in table of history
        {
            var record = timeControllerDbContext.history.Find(id, DateTime.Today);
            if (record == null) //if there is not record
            {
                record = new Models.History { //creating
                    Date = DateTime.Today,
                    Time = time,
                    UserID = id
                };
                timeControllerDbContext.Add(record);
            }
            else //overwrite
            {
                record.Time+= time;
            }
            timeControllerDbContext.SaveChanges();
        }

    }
}
