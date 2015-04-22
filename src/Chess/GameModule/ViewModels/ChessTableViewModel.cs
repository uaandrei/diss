using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel : ViewModelBase, IChessTableViewModel
    {
        private IEventAggregator _eventAggregator;
        private IGameTable _gameTable;
        public ObservableCollection<IChessSquareViewModel> Squares { get; private set; }

        public ChessTableViewModel(IEventAggregator eg, IGameTable gt)
        {
            _gameTable = gt;
            _eventAggregator = eg;
            _gameTable.Start();
            InitializeTableSquares();
            InitializeEventHandlers();
            RedrawTable();
        }

        #region Event Handlers

        private void OnSquareSelected(IChessSquareViewModel square)
        {
            _gameTable.ParseInput(square.Position);
            RedrawTable();
        }

        #endregion

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>().Subscribe(OnSquareSelected);
        }

        private void InitializeTableSquares()
        {
            Squares = new ObservableCollection<IChessSquareViewModel>();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Squares.Add(new ChessSquareViewModel(new Position(x, y)));
                }
            }
        }

        private void RedrawTable()
        {
            Squares.ForEach(s =>
            {
                s.SquareState = SquareState.Empty;
                s.Representation = string.Empty;
            });
            _gameTable.GetPieces().ForEach(piece =>
                Squares.Single(s => s.Position == piece.CurrentPosition).Representation = string.Format("{0}\n{1}", piece.Color, piece.Type)
            );
            SelectSquare(_gameTable.SelectedSquare);
            _gameTable.TableAttacks.ForEach(a => SetSquareState(a, SquareState.PosibleAttack));
            _gameTable.TableMoves.ForEach(a => SetSquareState(a, SquareState.PosibleMove));
        }

        private void SelectSquare(Position pos)
        {
            if (pos == null)
                return;
            SetSquareState(pos, SquareState.Selected);
        }

        private void SetSquareState(Position pos, SquareState state)
        {
            Squares.Single(s => s.Position == pos).SquareState = state;
        }
    }
}