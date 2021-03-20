using GD.FinishingSystem.Bussines;
using GD.FinishingSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme)]
    public class PieceController : Controller
    {
        FinishingSystemFactory factory;
        public PieceController()
        {
            factory = new FinishingSystemFactory();
        }
        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceShow, PieceFull, AdminFull")]
        public async Task<ActionResult> Index(int ruloId)
        {
            if (ruloId <= 0) return NotFound();

            var result = await factory.Pieces.GetPiecesFromRuloID(ruloId);
            
            ViewBag.RuloId = ruloId;

            return View(result);
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceAdd,PieceFull,AdminFull")]
        public async Task<ActionResult> Create(int ruloId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = string.Empty;

            if (ruloId <= 0) return NotFound();

            var rulo = await factory.Rulos.GetRuloFromRuloID(ruloId);
            if (rulo == null) return NotFound();

            Piece newPiece = new Piece();
            newPiece.RuloID = ruloId;

            return View("CreateOrUpdate", newPiece);
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceUp,PieceFull,AdminFull")]
        public async Task<ActionResult> Edit(int pieceId)
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";

            Piece existPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);
            if (existPiece == null) return NotFound();

            return View("CreateOrUpdate", existPiece);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Piece piece)
        {
            ViewBag.Error = true;
            string errorMessage = string.Empty;
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var modelStateVal = ModelState[key];
                    foreach (var error in modelStateVal.Errors)
                    {
                        errorMessage += error.ErrorMessage + "<br>";
                    }
                }

                ViewBag.ErrorMessage = errorMessage;

                return View("CreateOrUpdate");
            }

            if (piece.PieceID == 0)
            {
                if (!User.IsInRole("Piece", AuthType.Add)) return Unauthorized();

                await factory.Pieces.Add(piece, int.Parse(User.Identity.Name));
            }
            else
            {
                if (!User.IsInRole("Piece", AuthType.Update)) return Unauthorized();

                var validatePiece = await factory.Pieces.GetPiece(piece.RuloID, piece.PieceID);
                if (validatePiece != null)
                {
                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = "Piece already exist";
                    return View("CreateOrUpdate");
                }

                var foundPiece = await factory.Pieces.GetPieceFromPieceID(piece.PieceID);

                foundPiece.PieceNo = piece.PieceNo;
                foundPiece.Meter = piece.Meter;

                await factory.Pieces.Update(foundPiece, int.Parse(User.Identity.Name));
            }

            return RedirectToAction(nameof(Index), new { piece.RuloID });
        }


        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceShow, PieceFull, AdminFull")]
        public async Task<ActionResult> Details(int pieceId)
        {
            Piece foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            if (foundPiece == null) return NotFound();

            return View();
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceDel, PieceFull, AdminFull")]
        public async Task<ActionResult> Delete(int pieceId)
        {
            var foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            if (foundPiece == null) return NotFound();

            return View(foundPiece);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int pieceId)
        {
            if (!User.IsInRole("Piece", AuthType.Delete)) return Unauthorized();

            var foundPiece = await factory.Pieces.GetPieceFromPieceID(pieceId);

            await factory.Pieces.Delete(foundPiece, int.Parse(User.Identity.Name));

            return RedirectToAction(nameof(Index), new { foundPiece.RuloID });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = SystemStatics.DefaultScheme, Roles = "PieceShow, PieceFull, AdminFull")]
        public async Task<IActionResult> GetRulo(int ruloId)
        {
            if (!User.IsInRole("Piece", AuthType.Show)) return Unauthorized();

            var foundRulo = await factory.Rulos.GetVMRuloFromRuloID(ruloId);
            if (foundRulo == null) NotFound();

            return PartialView(foundRulo);
        }
    }
}
