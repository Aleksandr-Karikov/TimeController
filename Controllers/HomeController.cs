using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeController.Areas.Identity.Data;
using TimeController.data;
using TimeController.Data;
using TimeController.Models;
using TimeController.data.repositories;
namespace TimeController.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<TimeControllerUser> _userManager;
        private static Dictionary<string,Stopwatch> stopWatches = new Dictionary<string, Stopwatch>(); //dic to store client's stopwatches 
        private readonly TimeControllerDbContext context;
        public HomeController( UserManager<TimeControllerUser> userManager , TimeControllerDbContext context)
        {
            _userManager = userManager;
            this.context = context;
        }
        public IActionResult History() //Last week history
        {
            HistoryRepository rep = new HistoryRepository(context);
            var history = rep.lastSevenDays(_userManager.GetUserId(User));
            return View(history);
        }
        public IActionResult Index() //main page with stopwatch
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public void startTimer(string id) //start timer using sesion id
        {
            if (!stopWatches.ContainsKey(id))
            {
                stopWatches.Add(id,new Stopwatch());
                stopWatches[id].Start();
            }
            if (!stopWatches[id].IsRunning) stopWatches[id].Start();
        }
        [HttpPost]
        public void stopTimer(string id)//stop timer using sesion id
        {
            if (stopWatches.ContainsKey(id))
            {
                if (stopWatches[id].IsRunning)
                {
                    stopWatches[id].Stop();
                }
            }
        }
        [HttpPost]
        public  void clearTimer(string id) //save datas and clear dictionary of stopwatches
        {
            if (stopWatches.ContainsKey(id))
            {
                SetDbObjects.AddRecordHistory(context, _userManager.GetUserId(User), (int)stopWatches[id].ElapsedMilliseconds);
                stopWatches.Remove(id);
            }
        }
        [HttpPost]
        public ActionResult getServerStopWatchInf(string id) //get information about user's stopwatch
        {
            if (stopWatches.ContainsKey(id))
            {
                return Json(new { success = true, time = stopWatches[id].ElapsedMilliseconds, isrun = stopWatches[id].IsRunning });
            }
            return Json(new { success = false});
        }
    }
}
