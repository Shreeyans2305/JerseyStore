using System;

namespace JerseyStore
{
    internal static class AppRandom
    {
        // Single shared Random instance for the app to avoid duplicate seeds
        public static readonly Random Rng = new Random();
    }
}