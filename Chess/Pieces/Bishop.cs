﻿namespace Chess;


class Bishop : Piece
{
    public static new readonly PieceType PieceType = PieceType.Bishop;

    public Bishop(Side side, Square square) : base(side, square) { }

    public override HashSet<SquareShift> GetSquareShifts(Board board)
    {
        return new();
    }

    public override char ToChar()
    {
        return Side == Side.White ? 'b' : 'B';
    }
}