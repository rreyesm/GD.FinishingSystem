using GD.FinishingSystem.Bussines.Abstract;
using GD.FinishingSystem.DAL.Abstract;
using GD.FinishingSystem.DAL.Concrete;
using GD.FinishingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Concrete
{
    public class PieceManager : AbstractPieceService
    {
        IAsyncRepository<Piece> repository = null;
        public PieceManager(DbContext context)
        {
            this.repository = new GenericRepository<Piece>(context);
        }
        public async override Task Add(Piece piece, int adderRef)
        {
            await repository.Add(piece, adderRef);
        }

        public async override Task Delete(Piece piece, int deleterRef)
        {
            await repository.Remove(piece.PieceID, deleterRef);
        }

        public async override Task<Piece> GetPieceFromPieceID(int pieceID)
        {
            return await repository.GetByPrimaryKey(pieceID);
        }

        public async override Task<IEnumerable<Piece>> GetPieceList()
        {
            return await repository.GetWhere(x => !x.IsDeleted);
        }

        public async override Task Update(Piece piece, int updaterRef)
        {
            await repository.Update(piece, updaterRef);
        }

        public async override Task<IEnumerable<Piece>> GetPiecesFromRuloID(int ruloID)
        {
            return await repository.GetWhere(x => !x.IsDeleted && x.RuloID == ruloID);
        }

        public async override Task<Piece> GetPiece(int ruloID, int pieceNo)
        {
            var result = await repository.GetWhere(x=> !x.IsDeleted && x.RuloID == ruloID && x.PieceNo == pieceNo);

            return result.ToList().FirstOrDefault();
        }
    }
}
