using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using HGVC.Common.Logging;
using Hgvc.Business.Common;
using Hgvc.Common.CommunicationLibrary.DTO;
using Hgvc.WinService.CacheManager.Workers;
using Hgvc.Common;

namespace WinServiceTopShelf
{
    public static class CacheManagerBusiness
    {
        private static bool _isPropertyWorkerRunning = false;
        private static bool _isTourWorkerRunning = false;
        static readonly int WorkerSleepTimer = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["WorkerSleepTimer"]);
        static readonly int NumberofThreads = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["NumberOfThreads"]);
        static readonly ParallelOptions options = new ParallelOptions();
        static readonly List<string> CountToRun = new List<string>();
        static readonly Timer WorkerPropertyTimer = new Timer();
        static readonly Timer WorkerTourTimer = new Timer();

        static object PropertyLock = new object();

        public static void Execute()
        {
            LogManager.Log("Workers will run every " + WorkerSleepTimer + " milliseconds. Activating " + NumberofThreads + " threads");
            //set number of threads
            options.MaxDegreeOfParallelism = NumberofThreads;
            try
            {
                CachingAvailability.ResetTrackerTable();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex, "Error on ResetTrackerTable.", LogLevel.Error);
            }

            WorkerPropertyTimer.Elapsed += OnTimedEvent_StartPropertyWorkers;
            WorkerPropertyTimer.Interval = WorkerSleepTimer;
            WorkerPropertyTimer.Start();
        }

        private static void OnTimedEvent_StartPropertyWorkers(object source, ElapsedEventArgs e)
        {
            try
            {
                LogManager.Log("Attempting to acquire lock.", LogLevel.Debug);

                lock (PropertyLock)
                {
                    if (_isPropertyWorkerRunning)
                        return;
                    else
                        _isPropertyWorkerRunning = true;
                }

                var propertiesToRefresh = new List<CacheAvailabilityTracker>();

                for (int i = 0; i < NumberofThreads; i++)
                {
                    var tracker = CachingAvailability.GetPropertyToRefresh();
                    tracker.AssignedWorker = "Property Worker: " + i;
                    propertiesToRefresh.Add(tracker);
                }

                Parallel.ForEach(propertiesToRefresh, options, Logical.PropertyWorker);

                lock (PropertyLock)
                {
                    _isPropertyWorkerRunning = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException("Timer tick failed", ex, LogLevel.Error);
                lock (PropertyLock)
                {
                    _isPropertyWorkerRunning = false;
                }
            }
        }
    }
}
