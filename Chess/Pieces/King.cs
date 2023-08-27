using System.Diagnostics;

namespace Chess;


class King : Piece
{
    public static new readonly PieceType PieceType = PieceType.King;

    public King(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        HashSet<SquareShift> result = new(squareShifts);

        Debug.Assert(Side.IsValid());
        if (Side == Side.White)
        {
            if ((board.CastlingPermission & Castling.WK) != 0 &&
                !board.HasPiece(Square.F1) &&
                !board.HasPiece(Square.G1))
                result.Add(new(2, 0));
            if ((board.CastlingPermission & Castling.WQ) != 0 &&
                !board.HasPiece(Square.B1) &&
                !board.HasPiece(Square.C1) &&
                !board.HasPiece(Square.D1))
                result.Add(new(-2, 0));
        } else
        {
            if ((board.CastlingPermission & Castling.BK) != 0 &&
                !board.HasPiece(Square.F8) &&
                !board.HasPiece(Square.G8))
                result.Add(new(2, 0));
            if ((board.CastlingPermission & Castling.BQ) != 0 &&
                !board.HasPiece(Square.B8) &&
                !board.HasPiece(Square.C8) &&
                !board.HasPiece(Square.D8))
                result.Add(new(-2, 0));
        }

        return result;
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'K' : 'k';
    }


    private static readonly SquareShift[] squareShifts =
        new SquareShift[]
        {
            new(1, -1), new(1, 0), new(1, 1),
            new(0, -1), new(0, 0), new(0, 1),
            new(-1, -1), new(-1, 0), new(-1, 1)
        };
}