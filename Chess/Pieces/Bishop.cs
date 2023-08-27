namespace Chess;


class Bishop : SliderPiece
{
    public static new readonly PieceType PieceType = PieceType.Bishop;

    public Bishop(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return squareShifts;
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'B' : 'b';
    }


    private static readonly HashSet<SquareShift> squareShifts =
        new(new SquareShift[]
        {
            new(1, 1), new(1, -1), new(-1, 1), new(-1, -1)
        });
}