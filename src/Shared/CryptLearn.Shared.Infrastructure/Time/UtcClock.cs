using CryptLearn.Shared.Abstractions.Time;

namespace CryptLearn.Shared.Infrastructure.Time
{
    internal class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}