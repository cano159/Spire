using System;
using System.Threading.Tasks;

namespace OnlineMultiplayerMod
{
    public class ThrottleCalculator
    {
        public double Integral = 0;
        public DateTime LastCalculationTime;
        public double LastError;
        public double Measured;
        public readonly int Throttle;
        public double TotalError;
        private const double Dt = 30d; // rate of change of time. calculcations every ms;
        private const double Kd = -.1d; // derivative gain
        private const double Ki = -.1d; // integral gain

        private const double Kp = -.1d; // proportional gain

        public ThrottleCalculator(int throttle)
        {
            Throttle = throttle;
            LastCalculationTime = DateTime.MinValue;
        }

        public async Task CalculateThrottle()
        {
            Measured += 1;
            if (LastCalculationTime == DateTime.MinValue)
                LastCalculationTime = DateTime.Now;
            double elapsed = DateTime.Now.Subtract(LastCalculationTime)
                .TotalMilliseconds;
            if (elapsed > Dt)
            {
                LastCalculationTime = DateTime.Now;
                double error = Throttle / (1000d / Dt) - Measured;
                TotalError += error;
                double integral = TotalError;
                double derivative = (error - LastError) / elapsed;
                double actual = Kp * error + Ki * integral + Kd * derivative;
                double output = actual;

                if (output < 1)
                    output = 0;

                if (output > Dt * 4)
                    output = Dt * 4;

                if (output > 0)
                    await Task.Delay((int) output);
                Measured = 0;
                LastError = error;
            }
        }
    }
}