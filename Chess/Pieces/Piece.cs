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


public static class PieceTypeExtenstions
{
    public static char ToChar(this PieceType pieceType)
    => pieceType switch
    {
        PieceType.Pawn => 'p',
        PieceType.Knight => 'n',
        PieceType.Bishop => 'b',
        PieceType.Rook => 'r',
        PieceType.Queen => 'q',
        PieceType.King => 'k',
        _ => throw new ArgumentException("Invalid piece type")
    };
}


abstract class Piece : ICloneable
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

    public void SetSquare(Square square)
    {
        Debug.Assert(square.IsOnBoard);
        Square = square;
    }

    public abstract HashSet<SquareShift> GetSquareShifts(Board board);

    protected bool IsSquareValid(Square square, Board board)
    {
        return square.IsOnBoard &&
            (!board.HasPiece(square) ||
            board.PieceAt(square).Side != Side);
    }

    public virtual HashSet<Square> GetMoveSquares(Board board)
    {
        HashSet<Square> result = new();
        var shifts = GetSquareShifts(board);
        foreach (var shift in shifts)
        {
            var square = Square.Shift(Square, shift);
            if (IsSquareValid(square, board))
                result.Add(square);
        }
        return result;
    }

    public HashSet<Move> GetPseudoLegalMoves(Board board)
    {
        HashSet<Move> result = new();
        foreach (var destSquare in GetMoveSquares(board))
        {
            if (this is Pawn &&
                (Side == Side.White && Square.Rank == Rank.R7 ||
                Side == Side.Black && Square.Rank == Rank.R2))
            {
                result.Add(new(board, Square, destSquare, PieceType.Knight));
                result.Add(new(board, Square, destSquare, PieceType.Bishop));
                result.Add(new(board, Square, destSquare, PieceType.Rook));
                result.Add(new(board, Square, destSquare, PieceType.Queen));
            }
            else result.Add(new(board, Square, destSquare));
        }
        return result;
    }

    public HashSet<Move> GetLegalMoves(Board board)
    {
        // TODO
        return GetPseudoLegalMoves(board);
    }

    public abstract char ToChar();

    public object Clone()
    {
        return MemberwiseClone();
    }


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
