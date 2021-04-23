using System.Runtime.ExceptionServices;

namespace GrandTextAdventure.Core
{
    public class Money
    {
        public long Dollar { get; set; }
        public int PilzschafCoins { get; set; }

        public static Money operator +(Money first, Money second)
        {

            return new Money
            {
                Dollar = first.Dollar + second.Dollar,
                PilzschafCoins = first.PilzschafCoins + second.PilzschafCoins
            };
        }

        public static Money operator -(Money first, Money second)
        {

            return new Money
            {
                Dollar = first.Dollar - second.Dollar,
                PilzschafCoins = first.PilzschafCoins - second.PilzschafCoins
            };
        }
    }
}
