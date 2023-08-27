namespace Chess;


public enum RenderType { Console }


class Game
{
    private Board gameBoard;
    
    public Game(RenderType renderType, string fen)
    {
        gameBoard = Board.FromFEN(fen);
        if (renderType == RenderType.Console)
            RenderToConsole();
        var moves = gameBoard.GetLegalMoves();
        Console.WriteLine($"Pseudo-legal moves: {
            string.Join(' ', moves)} ({moves.Count})");
    }

    static readonly string topLine =
        "   ┌" + string.Concat(Enumerable.Repeat("───┬", 7)) + "───┐";
    static readonly string middleLine =
        "   ├" + string.Concat(Enumerable.Repeat("───┼", 7)) + "───┤";
    static readonly string bottomLine =
        "   └" + string.Concat(Enumerable.Repeat("───┴", 7)) + "───┘";

    private void RenderToConsole()
    {
        Console.WriteLine();
        Console.WriteLine(topLine);
        for (Rank rank = Rank.R8; rank >= Rank.R1; rank--)
        {
            Console.Write($" {(int)rank + 1} │");
            for (File file = File.A; file <= File.H; file++)
            {
                Piece piece = null;
                Square sq = new(rank, file);
                if (gameBoard.HasPiece(sq))
                    piece = gameBoard.PieceAt(sq);
                Console.Write($" {(piece != null ? piece.ToChar() : ' ')} │");
            }
            Console.WriteLine();
            if (rank > Rank.R1)
                Console.WriteLine(middleLine);
        }
        Console.WriteLine(bottomLine);
        Console.WriteLine("     a   b   c   d   e   f   g   h");
        Console.WriteLine();
    }


    private const string StartFEN =
        "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    public static Game StartNew(RenderType renderType)
        => new(renderType, StartFEN);
}
