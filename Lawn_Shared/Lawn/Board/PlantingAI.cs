
using Sexy;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using static Community.CsharpSqlite.Sqlite3;
using static IronPython.SQLite.PythonSQLite;

namespace Lawn
{
    public class PlantingAI
    {
        public static Dictionary<SeedType, List<Tuple<int, int>>> PLANT_PLANS = new Dictionary<SeedType, List<Tuple<int, int>>>
{
    { SeedType.Sunflower, new List<Tuple<int, int>>
        {
            // Sunflower coordinates in column 0
            new Tuple<int, int>(0, 0),
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(0, 2),
            new Tuple<int, int>(0, 3),
            new Tuple<int, int>(0, 4),


            // Add more rows if needed
        }
    },
    { SeedType.Peashooter, new List<Tuple<int, int>>
        {
            // Peashooter coordinates in column 1
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(1, 1),
            new Tuple<int, int>(1, 2),
            new Tuple<int, int>(1, 3),
            new Tuple<int, int>(1, 4),

        }
    },
       { SeedType.Wallnut, new List<Tuple<int, int>>
        {
            // Peashooter coordinates in column 1
            new Tuple<int, int>(3, 0),
            new Tuple<int, int>(3, 1),
            new Tuple<int, int>(3, 2),
            new Tuple<int, int>(3, 3),
            new Tuple<int, int>(3, 4),

        }
    },
          { SeedType.Potatomine, new List<Tuple<int, int>>
        {
            // Peashooter coordinates in column 1
            new Tuple<int, int>(5, 0),
            new Tuple<int, int>(5, 1),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),

        }
    }
};
        private class PlantPlan
        {
            public SeedType SeedType;
            public List<Tuple<int, int>> TargetCoords;
        }
        private Board _board;
        private float _timeUntilTryPlanting = 6.0f;
        private float _currentTime = 0.0f;
        private System.Timers.Timer _timer;

        private SeedType[] _seeds;
        public PlantingAI(Board board, SeedType[] seeds)
        {
            _board = board;
            _seeds = seeds;

            _timer = new System.Timers.Timer(_timeUntilTryPlanting * 1000);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += OnTimer;

        }


        ~PlantingAI()
        {
            StopTimer();
            _timer = null;
        }

        private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_board.mLevelComplete)
            {
                StopTimer();
                return;
            }

            if (_board.mCutScene.mSeedChoosing == true)
            {
                return;
            }

            TryPlantAccordingToPlan();
        }


        private void StopTimer()
        {
            _timer.Elapsed -= OnTimer;
            _timer.Enabled = false;
        }



        private void TryPlantRandom()
        {
            SeedType choice = _seeds[RandomNumbers.NextNumber(_seeds.Length)];
            int row = RandomNumbers.NextNumber(4);
            int column = RandomNumbers.NextNumber(8);
            if (_board.CanPlantAt(row, column, choice) == PlantingReason.Ok)
            {
                _board.AddPlant(row, column, choice, SeedType.None);

            }
        }

   

        private void TryPlantAccordingToPlan()
        {
            SeedType choice = _seeds[RandomNumbers.NextNumber(_seeds.Length)];

            if (PLANT_PLANS.ContainsKey(choice))
            {
                foreach(var pos in PLANT_PLANS[choice])
                {
                    int row = pos.Item1;
                    int column = pos.Item2; 
                    if(_board.CanPlantAt(row,column, choice) == PlantingReason.Ok)
                    {
                        Console.WriteLine("Placing " + choice + " at " + "{" + row + "," + column + "}");
                        _board.AddPlant(row, column, choice, SeedType.None);
                        break;
                    }
                }
            }
        }
    }
}
