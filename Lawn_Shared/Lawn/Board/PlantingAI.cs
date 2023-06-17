
using Sexy;
using Sexy.TodLib;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using static Community.CsharpSqlite.Sqlite3;
using static IronPython.Modules._ast;
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
        private float _timeUntilTryPlanting = 4.0f;
        private float _currentTime = 0.0f;
        private System.Timers.Timer _timer;
        private AIType _aiType = AIType.Random;
        private bool _columnLikeYouSeeEm = true;
        private bool _fillAllInstantly = true;
        private bool _hasFilled;
        private enum AIType
        {
            Random,
            Planned
        }


        private List<SeedType> _seeds;
        public PlantingAI(Board board, List<SeedType> seeds, float plantSpeed)
        {
            _board = board;
            _seeds = seeds;
            _timeUntilTryPlanting = plantSpeed;
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

            // TryPlantAccordingToPlan();
            if (_fillAllInstantly == true)
            {
                if (_hasFilled == false)
                {
                    _hasFilled = true;
                    TryPlantAllRandom();
                    _timer.Stop();
                    _timer.Elapsed -= OnTimer;
                }
                return;
            }

            if (_aiType == AIType.Random)
            {
                TryPlantRandom();

            }
            else
            {
                TryPlantAccordingToPlan();
            }
        }

        private void PlaceInColumn(int num, int num2, SeedType choice)
        {
            for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
            {
                if (i != num2 && _board.CanPlantAt(num, i, choice) == PlantingReason.Ok)
                {
                    if (choice == SeedType.Wallnut || choice == SeedType.Tallnut)
                    {
                        Plant topPlantAt4 = _board.GetTopPlantAt(num, i, TopPlant.OnlyNormalPosition);
                        if (topPlantAt4 != null && topPlantAt4.mSeedType == choice)
                        {
                            topPlantAt4.Die();
                        }
                    }
                    if (choice == SeedType.Pumpkinshell)
                    {
                        Plant topPlantAt5 = _board.GetTopPlantAt(num, i, TopPlant.OnlyPumpkin);
                        if (topPlantAt5 != null && topPlantAt5.mSeedType == choice)
                        {
                            topPlantAt5.Die();
                        }
                    }
                    _board.AddPlant(num, i, choice, SeedType.None);
                }
            }

        }

        private void StopTimer()
        {
            _timer.Elapsed -= OnTimer;
            _timer.Enabled = false;
        }



        private void TryPlantRandom()
        {
            SeedType choice = _seeds[RandomNumbers.NextNumber(_seeds.Count)];
            int row = 0;

            row = RandomNumbers.NextNumber(4);



            Console.WriteLine("Row is " + row);

            int column = RandomNumbers.NextNumber(6);
            Plant(column, row, choice);
        }

        private Plant Plant(int row, int column, SeedType choice)
        {
            Plant toRet = null;
            if (_columnLikeYouSeeEm)
            {
                if (_board.CanPlantAt(row, column, choice) != PlantingReason.Ok)
                {
                    return toRet;
                }

                toRet = _board.AddPlant(row, column, choice, SeedType.None);
                PlaceInColumn(row, column, choice);
            }

            else if (_board.CanPlantAt(row, column, choice) == PlantingReason.Ok)
            {
                toRet = _board.AddPlant(row, column, choice, SeedType.None);
            }

            return toRet;
        }


        private void TryPlantAllRandom()
        {
            int totalPlants = _seeds.Count;
            int numRows = _board.StageHas6Rows() ? 6 : 4;
            int numCols = 7;
            int totalCells = numRows * numCols;
            int cellsPerPlant = totalCells / totalPlants;
            int remainingCells = totalCells % totalPlants;

            List<int> cellIndices = new List<int>(totalCells);
            for (int i = 0; i < totalCells; i++)
            {
                cellIndices.Add(i);
            }

            Shuffle(cellIndices); // Shuffle the cell indices for random placement

            int currentIndex = 0;
            int plantsPlaced = 0;

            while (plantsPlaced < totalCells)
            {
                if (currentIndex >= totalPlants)
                {
                    // All seed types have been placed
                    currentIndex = 0; // Restart from the first seed type
                }

                SeedType currentSeedType = _seeds[currentIndex];
                int plantCount = cellsPerPlant;

                if (remainingCells > 0)
                {
                    plantCount++;
                    remainingCells--;
                }

                for (int k = 0; k < plantCount; k++)
                {
                    int cellIndex = cellIndices[plantsPlaced];
                    int rowIndex = cellIndex / numCols;
                    int colIndex = cellIndex % numCols;

                    Plant plant = Plant(rowIndex, colIndex, currentSeedType);
                    if (plant != null)
                    {
                        // Plant successfully placed
                        // Additional processing if needed
                    }

                    plantsPlaced++;
                    if (plantsPlaced >= totalCells)
                    {
                        // All cells have been filled
                        break;
                    }
                }

                currentIndex++;
            }
        }


        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void TryPlantAccordingToPlan()
        {
            SeedType choice = _seeds[RandomNumbers.NextNumber(_seeds.Count)];

            if (PLANT_PLANS.ContainsKey(choice))
            {
                foreach (var pos in PLANT_PLANS[choice])
                {
                    int row = pos.Item1;
                    int column = pos.Item2;
                    if (_board.CanPlantAt(row, column, choice) == PlantingReason.Ok)
                    {
                        Console.WriteLine("Placing " + choice + " at " + "{" + row + "," + column + "}");
                        Plant(row, column, choice);
                        break;
                    }
                }
            }
        }
    }
}
