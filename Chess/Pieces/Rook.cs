namespace Chess;


class Rook : Piece
{
    public static new readonly PieceType PieceType = PieceType.Rook;

    public Rook(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return new();
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'r' : 'R';
    }
}