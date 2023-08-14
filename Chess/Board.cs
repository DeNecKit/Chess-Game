using System.Diagnostics;

namespace Chess;


class Board
{
    private Dictionary<Square, Piece> PieceList;
    private Side Turn;
    private int MoveNumber;
    private int FiftyMoveCount;
    private Castling CastlingPermission;

    public Board()
    {
        PieceList = new();
        Turn = Side.None;
        MoveNumber = 1;
        FiftyMoveCount = 0;
        CastlingPermission = Castling.None;
    }

    public bool HasPiece(Square square)
    {
        return PieceList.ContainsKey(square);
    }

    public Piece PieceAt(Square square)
    {
        Debug.Assert(square.IsOnBoard);
        return PieceList[square];
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
                    board.PieceList.Add(sq, piece);
                }
                file++;
            }
            rank--;
        }

        return board;
    }
}
