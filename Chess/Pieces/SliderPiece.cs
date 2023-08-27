namespace Chess;


abstract class SliderPiece : Piece
{
    protected SliderPiece(Side side, Square square)
        : base(side, square) { }

    public override HashSet<Square> GetMoveSquares(Board board)
    {
        HashSet<Square> result = new();
        var shifts = GetSquareShifts(board);
        foreach (var shift in shifts)
        {
            var square = Square.Shift(Square, shift);
            while (IsSquareValid(square, board))
            {
                result.Add(square);
                square = Square.Shift(square, shift);
            }
        }
        return result;
    }
}