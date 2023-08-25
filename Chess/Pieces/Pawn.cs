namespace Chess;


class Pawn : Piece
{
    public static new readonly PieceType PieceType = PieceType.Pawn;

    public Pawn(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return new();
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'p' : 'P';
    }
}