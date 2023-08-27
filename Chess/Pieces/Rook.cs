namespace Chess;


class Rook : SliderPiece
{
    public static new readonly PieceType PieceType = PieceType.Rook;

    public Rook(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return squareShifts;
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'R' : 'r';
    }


    private static readonly HashSet<SquareShift> squareShifts =
        new(new SquareShift[]
        {
            new(1, 0), new(0, 1), new(-1, 0), new(0, -1)
        });
}