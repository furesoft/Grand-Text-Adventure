namespace GrandTextAdventure.Core;

public record Money(long Dollar, int PilzschafCoins)
{
    public static Money operator +(Money first, Money second)
    {
        return new Money(
            first.Dollar + second.Dollar,
            first.PilzschafCoins + second.PilzschafCoins
        );
    }

    public static Money operator -(Money first, Money second)
    {
        return new Money(
                        first.Dollar - second.Dollar,
                        first.PilzschafCoins - second.PilzschafCoins
                    );
    }
}
