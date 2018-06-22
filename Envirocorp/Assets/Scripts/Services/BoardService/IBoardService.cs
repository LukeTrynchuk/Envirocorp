using UnityEngine;
using FireBullet.Enviro.Board;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The IBoardService is a contract that all
    /// boards must implement. A board service
    /// is responsible for keeping reference
    /// to the board.
    /// </summary>
    public interface IBoardService : IService
    {
        int Width { get; }
        int Height { get; }
        HexCell[] GetBoard();
        HexCell GetCellAt(HexCoordinate coordinate);
    }
}
