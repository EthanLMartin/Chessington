﻿using System.Collections.Generic;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class KingTests
    {
        [Test]
        public void KingsCanMoveToAdjacentSquares()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);

            var moves = king.GetAvailableMoves(board);

            var expectedMoves = new List<Square>
            {
                Square.At(3, 3),
                Square.At(3, 4),
                Square.At(3, 5),
                Square.At(4, 3),
                Square.At(4, 5),
                Square.At(5, 3),
                Square.At(5, 4),
                Square.At(5, 5)
            };

            moves.ShouldAllBeEquivalentTo(expectedMoves);
        }

        [Test]
        public void Kings_CannotLeaveTheBoard()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(0, 0), king);

            var moves = king.GetAvailableMoves(board);

            var expectedMoves = new List<Square>
            {
                Square.At(1, 0),
                Square.At(1, 1),
                Square.At(0, 1)
            };

            moves.ShouldAllBeEquivalentTo(expectedMoves);
        }

        [Test]
        public void Kings_CanTakeOpposingPieces()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            var pawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(4, 5), pawn);

            var moves = king.GetAvailableMoves(board);
            moves.Should().Contain(Square.At(4, 5));
        }

        [Test]
        public void Kings_CannotTakeFriendlyPieces()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(4, 5), pawn);

            var moves = king.GetAvailableMoves(board);
            moves.Should().NotContain(Square.At(4, 5));
        }

        [Test]
        public void Kings_CanCastleRook_IfNeitherHaveMovedBefore()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 5), king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 0), rook);

            var moves = king.GetAvailableMoves(board);
            moves.Should().Contain(Square.At(7, 3));
        }

        [Test]
        public void Kings_CannotCastleRook_IfKingHasMovedBefore()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 6), king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(7, 0), rook);

            king.MoveTo(board, new Square(7, 5));

            var moves = king.GetAvailableMoves(board);
            moves.Should().NotContain(Square.At(7, 3));
        }

        [Test]
        public void Kings_CannotCastleRook_IfRookHasMovedBefore()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(7, 5), king);
            var rook = new Rook(Player.White);
            board.AddPiece(Square.At(4, 0), rook);

            rook.MoveTo(board, new Square(7, 0));

            var moves = king.GetAvailableMoves(board);
            moves.Should().NotContain(Square.At(7, 3));
        }
    }
}