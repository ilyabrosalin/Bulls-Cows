public struct BullsAndCows
{
    int bulls;
    int cows;

    public BullsAndCows(int bulls, int cows)
    {
        this.bulls = bulls;
        this.cows = cows;
    }

    public int Cows { get => cows; set => cows = value; }
    public int Bulls { get => bulls; set => bulls = value; }
}
