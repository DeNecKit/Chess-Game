using System.Diagnostics;

namespace Chess;


public enum Rank { R1, R2, R3, R4, R5, R6, R7, R8, None }
public enum File { A, B, C, D, E, F, G, H, None }


public static class RankFileExtensions
{
    public static Rank ToRank(this char c)
    => c switch
    {
        '1' => Rank.R1,
        '2' => Rank.R2,
        '3' => Rank.R3,
        '4' => Rank.R4,
        '5' => Rank.R5,
        '6' => Rank.R6,
        '7' => Rank.R7,
        '8' => Rank.R8,
        _ => throw new ArgumentException("Invalid rank symbol")
    };

    public static File ToFile(this char c)
    => c switch
    {
        'a' => File.A, 'A' => File.A,
        'b' => File.B, 'B' => File.B,
        'c' => File.C, 'C' => File.C,
        'd' => File.D, 'D' => File.D,
        'e' => File.E, 'E' => File.E,
        'f' => File.F, 'F' => File.F,
        'g' => File.G, 'G' => File.G,
        'h' => File.H, 'H' => File.H,
        _ => throw new ArgumentException("Invalid file symbol")
    };

    public static char ToChar(this Rank rank)
        => Square.RankSymbols[(int)rank];

    public static char ToChar(this File file)
        => Square.FileSymbols[(int)file];
}


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

    public override string ToString()
    {
        return $"{ File.ToChar() }{ Rank.ToChar() }";
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

    public static bool operator ==(Square a, Square b) => a.Equals(b);
    public static bool operator !=(Square a, Square b) => !a.Equals(b);


    static readonly Square none = new(Rank.None, File.None);
    public static Square None => none;

    static readonly char[] rankSymbols =
        new[] { '1', '2', '3', '4', '5', '6', '7', '8' };
    static readonly char[] fileSymbols =
        new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
    public static char[] RankSymbols => rankSymbols;
    public static char[] FileSymbols => fileSymbols;

    public static readonly Square
        E1 = new(Rank.R1, File.E),
        E8 = new(Rank.R8, File.E),
        G1 = new(Rank.R1, File.G),
        B1 = new(Rank.R1, File.B),
        G8 = new(Rank.R8, File.G),
        B8 = new(Rank.R8, File.B);

    public static Square Shift(Square square, SquareShift shift)
    {
        return new(square.Rank + shift.RankShift,
            square.File + shift.FileShift);
    }

    public static Square FromSAN(string san)
    {
        Debug.Assert(san.Length == 2);
        return new(san[0].ToRank(), san[1].ToFile());
    }
}
