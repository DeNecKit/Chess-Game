using System.Diagnostics;

namespace Chess;


public enum PieceType
{
    None, Pawn, Knight, Bishop, Rook, Queen, King
}

public enum Side {
    None, White, Black
}

[Flags] public enum Castling
{
    None = 0, WK = 1, WQ = 2, BK = 4, BQ = 8
}


public static class SideExtensions
{
    public static bool IsValid(this Side side)
    {
        return side == Side.White || side == Side.Black;
    }

    public static Side Opposite(this Side side)
    {
        return side == Side.White ? Side.Black : Side.White;
    }
}


abstract class Piece
{
    public static readonly PieceType PieceType;
    public readonly Side Side;
    public Square Square { get; private set; }

    public Piece(Side side, Square square)
    {
        Debug.Assert(side.IsValid());
        Side = side;
        Square = square;
    }

    public void Move(Square square)
    {
        Debug.Assert(square.IsOnBoard);
        Square = square;
    }

    public abstract char ToChar();


    public static Piece FromChar(char c, Square square)
    {
        return c switch
        {
            'P' => new Pawn(Side.White, square),
            'p' => new Pawn(Side.Black, square),
            'N' => new Knight(Side.White, square),
            'n' => new Knight(Side.Black, square),
            'B' => new Bishop(Side.White, square),
            'b' => new Bishop(Side.Black, square),
            'R' => new Rook(Side.White, square),
            'r' => new Rook(Side.Black, square),
            'Q' => new Queen(Side.White, square),
            'q' => new Queen(Side.Black, square),
            'K' => new King(Side.White, square),
            'k' => new King(Side.Black, square),
            _ => throw new ArgumentException("Invalid piece char"),
        };
    }
}
