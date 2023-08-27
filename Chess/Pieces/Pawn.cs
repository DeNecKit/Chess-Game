using System.Diagnostics;

namespace Chess;


class Pawn : Piece
{
    public static new readonly PieceType PieceType = PieceType.Pawn;

    public Pawn(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        HashSet<SquareShift> result = new();

        if (!board.HasPiece(Square.Shift(Square, new(1, 0))))
            result.Add(new(1, 0));

        var sqAtkR = Square.Shift(Square, new(1, 1));
        var sqAtkL = Square.Shift(Square, new(1, -1));
        if (board.HasPiece(sqAtkR) &&
            board.PieceAt(sqAtkR).Side != Side)
            result.Add(new(1, 1));
        if (board.HasPiece(sqAtkL) &&
            board.PieceAt(sqAtkL).Side != Side)
            result.Add(new(1, -1));

        Debug.Assert(Side.IsValid());
        if (Side == Side.White)
        {
            if (Square.Rank == Rank.R2 &&
                !board.HasPiece(Square.Shift(Square, new(1, 0))) &&
                !board.HasPiece(Square.Shift(Square, new(2, 0))))
                result.Add(new(2, 0));

            if (Square.Shift(Square, new(1, 1)) == board.EnPasSquare)
                result.Add(new(1, 1));
            else if (Square.Shift(Square, new(1, -1)) == board.EnPasSquare)
                result.Add(new(1, -1));
        } else
        {
            if (Square.Rank == Rank.R7 &&
                !board.HasPiece(Square.Shift(Square, new(-1, 0))) &&
                !board.HasPiece(Square.Shift(Square, new(-2, 0))))
                result.Add(new(-2, 0));

            if (Square.Shift(Square, new(-1, 1)) == board.EnPasSquare)
                result.Add(new(-1, 1));
            else if (Square.Shift(Square, new(-1, -1)) == board.EnPasSquare)
                result.Add(new(-1, -1));
        }

        return result;
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'P' : 'p';
    }
}