using System;

namespace CharacterManager.Model.Providers
{
    public class RandomProvider : IRandomProvider
    {
        private static readonly Random random = new();
        private static readonly object syncLock = new();

        public int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next( min, max );
            }
        }
    }
}
