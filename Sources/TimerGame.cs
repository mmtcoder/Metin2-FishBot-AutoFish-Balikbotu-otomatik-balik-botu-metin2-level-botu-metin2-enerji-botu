using MusicPlayerApp.Debugs;
using System;
using System.Diagnostics;
using System.Threading;


namespace MusicPlayerApp.Sources
{
    internal class TimerGame
    {
        private long StartedMilliSecTime = 0L;
        private long StartedSecondTime = 0L;
        private long StartedMinuteTime = 0L;

        private int breakTime = 0;

        private bool isBreakTimeDefined = false;

        private static readonly float FAST_PC_CPU_MHZ = 150;

        public static volatile bool IS_PC_SLOW = DecideSlowOrFastPC();

        public static volatile int MIN_WORK_TIME = 0;
        public static volatile int MAX_WORK_TIME = 0;

        public static volatile int MIN_BREAK_TIME = 0;
        public static volatile int MAX_BREAK_TIME = 0;
        public static volatile int GAME_STOP_TIME = 0;

        public static int MakeRandomValue(int minValue, int maxValue)
        {

            if (minValue >= maxValue)
            {
                maxValue = minValue;
                minValue = 0;              
            }
            Random random = new Random();
            return random.Next(minValue,maxValue);
        }

        public static bool DecideSlowOrFastPC()
        {
            float MyCpuSpeed = MeasureCPUSpeed();

            if(FAST_PC_CPU_MHZ - MyCpuSpeed >= 55)
            {
                DebugPfCnsl.println("This Pc is slow :( ");
                return true;
            }

            DebugPfCnsl.println("This Pc is Fast :) ");
            return false;
        }

        public static float MeasureCPUSpeed()
        {
            // İşlemci hızını öğrenmek için PerformanceCounter kullan
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");

            // İlk okuma genellikle 0 döner, bu yüzden bir okuma daha yapıyoruz
            float initialValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000); // 1 saniye bekleyerek işlemci hızının doğru ölçülmesini sağla
            float cpuFrequency = cpuCounter.NextValue();

            Console.WriteLine($"İşlemci Hızı: {cpuFrequency} MHz");

            return cpuFrequency;
        }

        public static void MeasureProcessSpeed()
        {
            // Ortalama işlem süresini ölçmek için değişkenler
            const int numberOfIterations = 100;
            long totalElapsedTicks = 0;

            for (int i = 0; i < numberOfIterations; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                // Ölçmek istediğiniz işlem burada olmalı
                for (int k = 0; k < 1000; k++)
                {
                    // Basit bir işlem: döngü ile bir şeyler yap
                    Math.Sqrt(k);
                }
                stopwatch.Stop();
                totalElapsedTicks += stopwatch.ElapsedTicks;
            }

            // Ortalama süreyi hesapla
            double averageElapsedMilliseconds = (totalElapsedTicks / (double)numberOfIterations) / Stopwatch.Frequency * 1000;

            Console.WriteLine($"Ortalama İşlem Süresi: {averageElapsedMilliseconds} ms");

            // Ortalama işlem hızını hesapla (işlemler/ms)
            double operationsPerMillisecond = 1000.0 / averageElapsedMilliseconds;
            Console.WriteLine($"Ortalama İşlem Hızı: {operationsPerMillisecond} işlemler/ms");

        }
        public static int MakeRandomTimeSecond(int  minValue, int maxValue)
        {
            if (minValue >= maxValue)
            {
                maxValue = minValue;
                minValue = 0;
            }
            Random random = new Random();
            return random.Next(minValue, maxValue) + 1000;
        }

        public static void SleepRandom(int minValue,int maxValue)
        {
            Thread.Sleep(MakeRandomValue(minValue,maxValue));
           
        }

        public static void SleepRandomForPlayers(int minValue, int maxValue,
            int minValuePlayers,int maxValuePlayers)
        {
            if(ThreadGlobals.isAdaptableFishing)
            {
                if (ThreadGlobals.isAnotherPlayerDetected)
                {
                    Thread.Sleep(MakeRandomValue(minValuePlayers, maxValuePlayers));
                }
                else
                {
                    Thread.Sleep(MakeRandomValue(minValue, maxValue));
                }
            }
            else
            {
                Thread.Sleep(MakeRandomValue(minValue, maxValue));
            }
           
        }

        public static void SleepRandomMinute(int minValue,int maxValue)
        {
            int oneMinuteFromMilis = (60 * 1000);
            Thread.Sleep(MakeRandomValue(minValue,maxValue) * oneMinuteFromMilis);  
        }

        
        public bool CheckDelayTimeInSecond(long delayTime)
        {
            if (StartedSecondTime <= 0L) SetStartedSecondTime();
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - StartedSecondTime < delayTime)
            {
                return true;
            }
            return false;

        }
        public bool CheckDelayTimeInMilliSec(long delayTime)
        {
            if (StartedMilliSecTime <= 0L) SetStartedMilliSecTime();
            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - StartedMilliSecTime < delayTime)
            {
                return true;
            }
            return false;

        }

        public bool CheckDelayTimeInMinute(long delaytime)
        {
            if (StartedMinuteTime <= 0L) SetStartedMinuteTime();
            if(((DateTimeOffset.UtcNow.ToUnixTimeSeconds()) - StartedMinuteTime)/60 < delaytime)
            {
                return true;
            }

            return false;
        }

        public bool CheckCountDownMinute(int minValue, int maxValue =0)
        {
            
            if(!isBreakTimeDefined)
            {
                isBreakTimeDefined = true;
                breakTime = MakeRandomValue(minValue,maxValue);
                SetStartedMinuteTime();
            }

            if (!CheckDelayTimeInMinute(breakTime))
            {                            
                isBreakTimeDefined = false;
                return true;
            }

            return false;
        }

        public bool CheckCountDownSecond(int minValue, int maxValue = 0)
        {

            if (!isBreakTimeDefined)
            {
                isBreakTimeDefined = true;
                breakTime = MakeRandomValue(minValue, maxValue);
                SetStartedSecondTime();
            }

            if (!CheckDelayTimeInSecond(breakTime))
            {
                isBreakTimeDefined = false;
                return true;
            }

            return false;
        }

        public void SetStartedMilliSecTime()
        {
            StartedMilliSecTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        public void SetStartedSecondTime()
        {
            StartedSecondTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public void SetStartedMinuteTime()
        {
            StartedMinuteTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public void StartTimeBreakMinute(int minValue, int maxValue)
        {
            DebugPfCnsl.println("TimeBreaking now ...");
            SetStartedMinuteTime();

            int waitResult = MakeRandomValue(minValue,maxValue);
            
            while(CheckDelayTimeInMinute(waitResult))
            {
                if(ThreadGlobals.CheckGameIsStopped()) return;
                ThreadGlobals.isCharStopped = true;
            }

            ThreadGlobals.isCharStopped = false;
        }

        public void StartTimeBreakSecond(int minValue, int maxValue)
        {
            DebugPfCnsl.println("TimeBreaking now ...");
            SetStartedSecondTime();

            int waitResult = MakeRandomValue(minValue, maxValue);

            while (CheckDelayTimeInSecond(waitResult))
            {
                if (ThreadGlobals.CheckGameIsStopped()) return;
                ThreadGlobals.isCharStopped = true;
            }

            ThreadGlobals.isCharStopped = false;
        }

    }
}
