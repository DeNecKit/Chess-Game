namespace Chess;


class Queen : Piece
{
    public static new readonly PieceType PieceType = PieceType.Queen;

    public Queen(Side side, Square square) : base(side, square) { }

    public override char ToChar()
    {
        return Side == Side.White ? 'q' : 'Q';
    }
}