
using Sexy;

namespace Lawn
{
    public class PlantingAI
    {
        private Board _board;
        private float _timeUntilTryPlanting = 12.0f;
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
            _timer.Elapsed -= OnTimer;
            _timer = null;
        }

        private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(_board.mLevelComplete)
            {
                _timer.Elapsed -= OnTimer;
                return;
            }
            TryPlantRandom();
        }



        private void TryPlantRandom()
        {
            SeedType choice = _seeds[0];
            int row = RandomNumbers.NextNumber(8);
            int column = RandomNumbers.NextNumber(4);
            if (_board.CanPlantAt(row, column, choice) == PlantingReason.Ok)
            {
                _board.AddPlant(row, column, choice, SeedType.None);

            }
        }
    }
}
