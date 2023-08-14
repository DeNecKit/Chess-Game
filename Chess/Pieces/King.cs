namespace Chess;


class King : Piece
{
    public static new readonly PieceType PieceType = PieceType.King;

    public King(Side side, Square square) : base(side, square) { }

    public override char ToChar()
    {
        return Side == Side.White ? 'k' : 'K';
    }
}