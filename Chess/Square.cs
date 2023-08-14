namespace Chess;


public enum Rank { R1, R2, R3, R4, R5, R6, R7, R8, None }
public enum File { A, B, C, D, E, F, G, H, None }


readonly struct Square
{
    public readonly Rank Rank = Rank.None;
    public readonly File File = File.None;

    public bool IsOnBoard =>
        Rank >= Rank.R1 && Rank <= Rank.R8 &&
        File >= File.A && File <= File.H;

    public Square(Rank rank, File file)
    {
        Rank = rank;
        File = file;
    }

    public override int GetHashCode()
    {
        return (int)Rank * 8 + (int)File;
    }

    public override bool Equals(object obj)
    {
        if (obj is Square sq)
            return sq.GetHashCode() == GetHashCode();
        return false;
    }


    static readonly Square none = new(Rank.None, File.None);
    public static Square None => none;
}
