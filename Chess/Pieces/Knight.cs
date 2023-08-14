namespace Chess;


class Knight : Piece
{
    public static new readonly PieceType PieceType = PieceType.Knight;

    public Knight(Side side, Square square) : base(side, square) { }

    public override char ToChar()
    {
        return Side == Side.White ? 'n' : 'N';
    }
}