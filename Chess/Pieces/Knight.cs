namespace Chess;


class Knight : Piece
{
    public static new readonly PieceType PieceType = PieceType.Knight;

    public Knight(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return squareShifts;
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'n' : 'N';
    }


    private static readonly HashSet<SquareShift> squareShifts =
        new(new SquareShift[]
        {
            new(1, 2), new(-1, 2),
            new(2, 1), new(-2, 1),
            new(2, -1), new(-2, -1),
            new(1, -2), new(-1, -2)
        });
}