using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractPieceService
    {
        public abstract Task<IEnumerable<Piece>> GetPieceList();
        public abstract Task<IEnumerable<Piece>> GetPiecesFromRuloID(int ruloID);
        public abstract Task<Piece> GetPiece(int ruloID, int pieceID);
        public abstract Task<Piece> GetPieceFromPieceID(int pieceID);
        public abstract Task Add(Piece piece, int adderRef);
        public abstract Task Update(Piece piece, int updaterRef);
        public abstract Task Delete(Piece piece, int deleterRef);
    }
}
