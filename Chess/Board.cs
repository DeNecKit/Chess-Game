using System.Diagnostics;

namespace Chess;


class Board
{
    private readonly Dictionary<Square, Piece> Pieces;
    public Side Turn { get; private set; }
    public int MoveNumber { get; private set; }
    public int FiftyMoveCount { get; private set; }
    public Castling CastlingPermission { get; private set; }
    public Square EnPasSquare { get; private set; }

    public Board()
    {
        Pieces = new();
        Turn = Side.None;
        MoveNumber = 1;
        FiftyMoveCount = 0;
        CastlingPermission = Castling.None;
        EnPasSquare = Square.None;
    }

    public bool HasPiece(Square square)
    {
        return Pieces.ContainsKey(square);
    }

    public Piece PieceAt(Square square)
    {
        Debug.Assert(square.IsOnBoard);
        return Pieces[square];
    }

    public void SetPiece(Square square, Piece piece)
    {
        Pieces.Add(square, piece);
    }

    public void RemovePiece(Square square)
    {
        Pieces.Remove(square);
    }

    public void MovePiece(Square from, Square to)
    {
        Piece piece = PieceAt(from).Clone() as Piece;
        RemovePiece(from);
        piece.SetSquare(to);
        SetPiece(to, piece);
    }

    public HashSet<Move> GetLegalMoves()
    {
        HashSet<Move> result = new();
        foreach (var piece in Pieces.Values)
            if (piece.Side == Turn)
                foreach (var move in piece.GetLegalMoves(this))
                    result.Add(move);
        return result;
    }


    public static Board FromFEN(string fen)
    {
        Board board = new Board();

        string[] fenParts = fen.Split();
        Debug.Assert(fenParts.Length == 6);

        string[] ranks = fenParts[0].Split('/');
        Debug.Assert(ranks.Length == 8);

        string turn = fenParts[1],
            castlingPerm = fenParts[2],
            enPasSq = fenParts[3],
            fiftyMoves = fenParts[4],
            moveNum = fenParts[5];
        Debug.Assert(turn == "w" || turn == "b");
        Debug.Assert(castlingPerm.Length == 4
            || castlingPerm == "-");
        Debug.Assert(enPasSq == "-" || enPasSq.Length == 2);
        Debug.Assert(int.TryParse(fiftyMoves,
            out int fiftyMoveCount) && fiftyMoveCount >= 0);
        Debug.Assert(int.TryParse(moveNum,
            out int moveNumber) && moveNumber > 0);

        Rank rank = Rank.R8;
        foreach (string files in ranks)
        {
            File file = File.A;
            foreach (char c in files)
            {
                if (char.IsDigit(c))
                    file += (int)char.GetNumericValue(c);
                else
                {
                    Square sq = new(rank, file);

                    Piece piece = Piece.FromChar(c, sq);
                    board.SetPiece(sq, piece);
                }
                file++;
            }
            rank--;
        }

        board.Turn = turn == "w" ? Side.White : Side.Black;
        if (castlingPerm.Length == 4)
            foreach (var c in castlingPerm)
                board.CastlingPermission |= c switch
                {
                    'K' => Castling.WK,
                    'k' => Castling.BK,
                    'Q' => Castling.WQ,
                    'q' => Castling.BQ,
                    '-' => Castling.None,
                    _ => throw new ArgumentException(
                        "Invalid castling symbol")
                };
        if (enPasSq != "-")
            board.EnPasSquare = Square.FromSAN(enPasSq);
        board.FiftyMoveCount = fiftyMoveCount;
        board.MoveNumber = moveNumber;

        return board;
    }
}
