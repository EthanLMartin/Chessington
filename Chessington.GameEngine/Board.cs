﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }
        public IList<Turn> GameTurns { get; } = new List<Turn>();

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            if (IsWithinBounds(square)) {
                return board[square.Row, square.Col];
            }

            return null;
        }

        public bool IsEmpty(Square location)
        {
            return (GetPiece(location) == null);
        }

        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public Piece FindPieceFrom(Piece piece, Direction direction)
        {
            Square location = FindPiece(piece);

            Square newLocation = location + direction;
            while (IsWithinBounds(newLocation) && IsEmpty(newLocation))
            {
                newLocation += direction;
            }

            return GetPiece(newLocation);
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }

        public void ForceMovePiece(Piece piece, Square to)
        {
            Square from = FindPiece(piece);

            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;
        }

        public bool HasMoved(Piece piece)
        {
            return GameTurns.Any(turn => turn.Moves.Any(move => move.MovementPiece == piece));
        }

        public void RemoveAt(Square square)
        {
            if (board[square.Row, square.Col] != null)
            {
                OnPieceCaptured(board[square.Row, square.Col]);
            }

            board[square.Row, square.Col] = null;
        }

        public bool IsWithinBounds(Square square)
        {
            return (square.Row >= 0 && square.Row < GameSettings.BoardSize) &&
                   (square.Col >= 0 && square.Col < GameSettings.BoardSize);
        }

        public List<Square> ClipToBoard(List<Square> moves)
        {
            moves.RemoveAll(s => !IsWithinBounds(s));
            return moves;
        }

        public List<Turn> GetPlayerTurns(Player player)
        {
            switch (player)
            {
                case Player.White:
                    return GameTurns.Where(turn => turn.Player == Player.White).ToList();

                case Player.Black:
                    return GameTurns.Where(turn => turn.Player == Player.Black).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public List<Turn> GetEnemyTurns(Player player)
        {
            switch (player)
            {
                case Player.White:
                    return GetPlayerTurns(Player.Black);

                case Player.Black:
                    return GetPlayerTurns(Player.White);

                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public List<Square> GetEnemyMarkedSquares(Player player)
        {
            return null;
        }

        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
