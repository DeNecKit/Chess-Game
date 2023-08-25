namespace Chess;


readonly struct SquareShift
{
    public readonly int RankShift, FileShift;

    public SquareShift(int rankShift, int fileShift)
    {
        RankShift = rankShift;
        FileShift = fileShift;
    }

    public override int GetHashCode()
    {
        return FileShift + RankShift * 8;
    }

    public override bool Equals(object obj)
    {
        if (obj is SquareShift sqShift)
            return sqShift.GetHashCode() == GetHashCode();
        return false;
    }
}