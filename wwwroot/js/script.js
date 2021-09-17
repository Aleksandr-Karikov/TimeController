var syncronization; //an object for synchronizing the server and client stopwatch
var GlobalTime = 0; //stores the time for sync between paused
var  StartPoint = -1; //time of start
var StopWatchObj; //object of stopwatch
var id = null; // session id
var isRun = false;
window.onbeforeunload = function (evt) { //warning about possible data loss
    if (document.StopWatchForm.stopwatch.value != "00:00:00.000") {
        StartPauseStopWatch()
        var message = "Document is not saved. You can lost the changes if you leave the page.";
        if (typeof evt == "undefined") {
            evt = window.event;
        }
        if (evt) {
            evt.returnValue = message;
        }
        
        return message;
    }
    
}
function StopWatchExecutor() { //  time output
    var _curTime = new Date().getTime() - StartPoint;
    var _ss = ~~(_curTime / 1000) % 60;
    var _mm = ~~(_curTime / 60000) % 60;
    var _hh = ~~(_curTime / 3600000) % 60;
    document.StopWatchForm.stopwatch.value = (_hh < 10 ? '0' + _hh : _hh) + ':' + (_mm < 10 ? '0' + _mm : _mm) + ':' + (_ss < 10 ? '0' + _ss : _ss) + ':' + ((_curTime % 1000) + 1000).toString().substring(1);
}
function getSessionId() { 
    if (id == null) {
        id = document.getElementById("session").getAttribute("data-usreId");
    } 
}
function tester(time) {
    if (Math.abs((new Date().getTime() - StartPoint) - time) > 1000) //if the server time is more than a second behind, synchronize the time
        this.StartPoint = new Date().getTime() - time;
}

function SyncTime() { //sync request
    getSessionId();
    if (~~(Date.getTime / 3600000) % 60 == 23 && ~~(Date.getTime / 60000) % 60 == 59) { //if the day ends we should save datas on this date
        isRun = false;
        ClearStopWatch();
        this.StartPoint = -1;
        StartPauseStopWatch();
    }
    $.ajax({
        type: "POST",
        url: "/Home/getServerStopWatchInf",
        data: { id: id }, //id session
        dataType: 'json',
        success: function (response) {
            if (response.success && response.isrun) { //sync time
                tester(response.time);
            } else {
                this.clearInterval(syncronization);
                this.clearInterval(StopWatchObj);
            }
        },
        error: function (response) {
            alert("error!");  
        }

    });
}

function StartPauseStopWatch() {
    isRun = true;
    getSessionId();
    if (StartPoint == -1) { //start 
        document.StopWatchForm.start.value = "Уйти на перерыв";
        $.ajax({ //start timer on server
            type: "POST",
            url: "/Home/startTimer",
            data: { id: id },
            dataType: "json",
            async: false,
        });
        
        this.StartPoint = new Date().getTime() - GlobalTime; //get datetime
        StopWatchObj = setInterval(function () { StopWatchExecutor(); }, 1); //start output timer
        syncronization = setInterval(function () { SyncTime() }, 1000); // start sync with server
        
        
    }
    else { //stop
        isRun = false;
        clearInterval(StopWatchObj);
        GlobalTime = new Date().getTime() - this.StartPoint;
        document.StopWatchForm.start.value = "Начать работу";
        $.ajax({ //stop timer on server
            type: "POST",
            url: "/Home/stopTimer",
            data: { id: id },
            dataType: "json"

        });
        this.StartPoint = -1;
    }
    
}
function ClearStopWatch() {
    if (isRun) { //before saving dates user must stop stopwatches
        alert("Сначала остановите таймер")
    }
    else {
        getSessionId();
        $.ajax({ //req for saving dates and clear stopwatches

            type: "POST",
            url: "/Home/clearTimer",
            data: { id: id },
            dataType: "json",
            async: false
        });
        this.clearInterval(StopWatchObj); //stop output
        this.clearInterval(syncronization);//stop sync
        this.GlobalTime = 0;
        this.StartPoint = -1;
        document.StopWatchForm.stopwatch.value = "00:00:00.000";
    }
   
}
