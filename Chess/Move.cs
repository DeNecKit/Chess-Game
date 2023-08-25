namespace Chess;


readonly struct Move
{
    public readonly Square From, To;
    public readonly bool IsCapture, IsEnPas;
    public readonly Castling CastlingType;
    public readonly PieceType PromotionPieceType;

    public Move(Board board, Square from, Square to,
        PieceType promotionPieceType = PieceType.None)
    {
        var piece = board.PieceAt(from);
        
        From = from;
        To = to;
        IsCapture = board.HasPiece(to);
        IsEnPas = piece is Pawn && to == board.EnPasSquare;
        PromotionPieceType = promotionPieceType;

        CastlingType = Castling.None;
        if (piece is King &&
            (piece.Side == Side.White && from == Square.E1 ||
            piece.Side == Side.Black && from == Square.E8))
        {
            if (to == Square.G1) CastlingType = Castling.WK;
            else if (to == Square.B1) CastlingType = Castling.WQ;
            else if (to == Square.G8) CastlingType = Castling.BK;
            else if (to == Square.B8) CastlingType = Castling.BQ;
        }
    }

    public override string ToString()
    {
        return From.ToString() + To.ToString() + 
            (PromotionPieceType == PieceType.None ?
            "" : PromotionPieceType.ToChar());
    }

    public override int GetHashCode()
    {
        return
            From.GetHashCode() |
            To.GetHashCode() << 6 |
            (int)PromotionPieceType << 6;
    }

    public override bool Equals(object obj)
    {
        if (obj is Move move)
            return move.GetHashCode() == GetHashCode();
        return false;
    }
}