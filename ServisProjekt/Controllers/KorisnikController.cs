using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServisProjekt.Data;
using ServisProjekt.Models;
using ServisProjekt.Models.KorisnikViewModel;

namespace ServisProjekt.Controllers
{
    [Authorize(Roles = "Korisnik")]
    public class KorisnikController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KorisnikController (ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> PopisPrijava()
        {
            var user = await _userManager.GetUserAsync(User);
            var popis = _context.Narudžba.Where(x => x.NaručiteljID == user.Id)
                .Select(result => new PopisNarudžbiModel
                {
                    NarudžbaID = result.NarudžbaID,
                    VrijemeNarudžbe = result.VrijemeNaruđbe,
                    Naručitelj = _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Surname,
                    VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == result.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                    SerijskiBrojPrintera = result.PrinterID,
                    Opis = result.Opis.Length < 60? result.Opis : result.Opis.Substring(0,57)+"...",
                    StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == result.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa,
                }).OrderByDescending(x => x.NarudžbaID);
            List<PopisNarudžbiModel> narudžba = new List<PopisNarudžbiModel>();
            foreach (var item in popis)
            {
                narudžba.Add(item);
            }
            return View(narudžba);
        }

        public async Task<IActionResult> NovaPrijavaCountera()
        {
            var user = await _userManager.GetUserAsync(User);
            var searchPrinterModel = new SearchPrinterModel
            {
                PrinterPopis = new List<SelectListItem>()
            };
            foreach (var printer in _context.Printer.Where(x => x.LokacijaID == user.PoduzećeID))
            {
                searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
            }
            var model = new PrijavaCounteraModel
            {
                PopisPrintera = searchPrinterModel
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NovaPrijavaCountera(PrijavaCounteraModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var narudžba = new Narudžba
            {
                Counteri = model.PrijavaServisaModel.Counter,
                NaručiteljID = user.Id,
                PoduzećeID = user.PoduzećeID,
                PrinterID = model.PopisPrintera.PrinterID,
                StatusNarudžbeID = 1,
                VrijemeNaruđbe = DateTime.Now,
                VrstaNarudžbeID = 2
            };

            _context.Add(narudžba);
            _context.SaveChanges();

            return RedirectToAction(nameof(KorisnikController.PopisPrijava), "Korisnik");
        }

        public async Task<IActionResult> NovaPrijavaKvara()
        {
            var user = await _userManager.GetUserAsync(User);
            var searchPrinterModel = new SearchPrinterModel
            {
                PrinterPopis = new List<SelectListItem>()
            };
            foreach (var printer in _context.Printer.Where(x => x.LokacijaID == user.PoduzećeID))
            {
                searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
            }
            var model = new PrijavaKvaraModel
            {
                PopisPrintera = searchPrinterModel
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NovaPrijavaKvara(PrijavaKvaraModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var narudžba = new Narudžba
            {
                Counteri = model.PrijavaServisaModel.Counter,
                Opis = model.PrijavaServisaModel.Opis,
                ŠifraGreške = model.PrijavaServisaModel.ŠifraGreške,
                NaručiteljID = user.Id,
                PoduzećeID = user.PoduzećeID,
                PrinterID = model.PopisPrintera.PrinterID,
                StatusNarudžbeID = 1,
                VrijemeNaruđbe = DateTime.Now,
                VrstaNarudžbeID = 1
            };

            _context.Add(narudžba);
            _context.SaveChanges();

            return RedirectToAction(nameof(KorisnikController.PopisPrijava), "Korisnik");
        }

        public async Task<IActionResult> Detaljnije(int? NarudžbaID)
        {
            if (NarudžbaID == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var pom = _context.Narudžba.SingleOrDefault(x => x.NarudžbaID == NarudžbaID);
            if (pom == null || user.PoduzećeID != pom.PoduzećeID)
            {
                return NotFound();
            }
            var nar = new PopisNarudžbiModel
            {
                NarudžbaID = pom.NarudžbaID,
                VrijemeNarudžbe = pom.VrijemeNaruđbe,
                Naručitelj = _context.Users.Where(x => x.Id == pom.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == pom.NaručiteljID)
                            .FirstOrDefault().Surname,
                VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == pom.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                SerijskiBrojPrintera = pom.PrinterID,
                Opis = pom.Opis,
                StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == pom.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa,
                Counter = pom.Counteri,
                ŠifraGreške = pom.ŠifraGreške
            };
            return View(nar);
        }

        public async Task<IActionResult> DetaljnijePotrošni(int? NarudžbaID)
        {
            if (NarudžbaID == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var pom = _context.Narudžba.SingleOrDefault(x => x.NarudžbaID == NarudžbaID);
            if (pom == null || user.PoduzećeID != pom.PoduzećeID)
            {
                return NotFound();
            }
            var nar = new PopisNarudžbiModel
            {
                NarudžbaID = pom.NarudžbaID,
                VrijemeNarudžbe = pom.VrijemeNaruđbe,
                Naručitelj = _context.Users.Where(x => x.Id == pom.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == pom.NaručiteljID)
                            .FirstOrDefault().Surname,
                VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == pom.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                SerijskiBrojPrintera = pom.PrinterID,
                Opis = pom.Opis,
                StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == pom.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa,
            };
            /*------------------------------------------------------------------------------------*/
            var NarToPred = _context.NarudžbaToPredmet.Where(x => x.NarudžbeID == nar.NarudžbaID);
            var pot = new DetaljnijePotrošniModel
            {
                Potrošni = new List<DodajPredmetModel>()
            };
            foreach (var item in NarToPred)
            {

                pot.Potrošni.Add(new DodajPredmetModel { PredmetID = item.PredmetiID, Količina = item.Količina,
                    Naziv = _context.Predmet.Where(x => x.PredmetID == item.PredmetiID).FirstOrDefault().Naziv });
            }
            pot.Narudžba = nar;
            return View(pot);
        }

        [HttpPost]
        public async Task<IActionResult> StornirajPrijavu(int? NarudžbaID)
        {
            if (NarudžbaID == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var narudžba = _context.Narudžba.SingleOrDefault(x => x.NarudžbaID == NarudžbaID);
            if (narudžba == null || user.PoduzećeID != narudžba.PoduzećeID)
            {
                return NotFound();
            }
            narudžba.StatusNarudžbeID = 5;
            _context.Narudžba.Update(narudžba);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PopisPrijava));
        }

        public async Task<IActionResult> PrijavaPotrošnog()
        {
            var user = await _userManager.GetUserAsync(User);
            var printeri = _context.Printer.Where(x => x.LokacijaID == user.PoduzećeID).Distinct();

            var obiteljPrintera = _context.ObiteljPrintera;
            

            var searchObiteljModel = new ObiteljPrinteraSearchModel
            {
                SerijaPrintera = new List<SelectListItem>()
            };
            foreach (var pri in printeri)
            {
                searchObiteljModel.SerijaPrintera.Add(new SelectListItem{
                    Text = _context.ObiteljPrintera.Where(x => x.ObiteljPrinteraID == pri.VrstaPrinteraID).FirstOrDefault().Obitelj,
                                                          Value = _context.ObiteljPrintera.Where(x => x.ObiteljPrinteraID == pri.VrstaPrinteraID).FirstOrDefault().ObiteljPrinteraID.ToString()
                });
            }
            var model = new PrijavaPotrošnogModel
            {
                ObiteljPrintera = searchObiteljModel
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PrijavaPotrošnog(PrijavaPotrošnogModel model)
        {
            var pom = _context.Predmet.Where(x => x.PrinterID == model.ObiteljPrintera.ObiteljPrinteraID && x.VrstaPredmetaID == 2);
            var popis = new PrijavaPotrošnogModel
            {
                Potrošni = new List<DodajPredmetModel>()
            };
            foreach(var item in pom)
            {
                popis.Potrošni.Add(new DodajPredmetModel {PredmetID = item.PredmetID, Naziv = item.Naziv });
            }
            /*---------------------------------------------------------------------------*/
            var user = await _userManager.GetUserAsync(User);
            var printeri = _context.Printer.Where(x => x.LokacijaID == user.PoduzećeID).Distinct();

            var obiteljPrintera = _context.ObiteljPrintera;


            var searchObiteljModel = new ObiteljPrinteraSearchModel
            {
                SerijaPrintera = new List<SelectListItem>()
            };
            foreach (var pri in printeri)
            {
                searchObiteljModel.SerijaPrintera.Add(new SelectListItem
                {
                    Text = _context.ObiteljPrintera.Where(x => x.ObiteljPrinteraID == pri.VrstaPrinteraID).FirstOrDefault().Obitelj,
                    Value = _context.ObiteljPrintera.Where(x => x.ObiteljPrinteraID == pri.VrstaPrinteraID).FirstOrDefault().ObiteljPrinteraID.ToString()
                });
            }
            popis.ObiteljPrintera = searchObiteljModel;
            return View(popis);
            
        }

        [HttpPost]
        public async Task<IActionResult> NapravljenaPrijava(PrijavaPotrošnogModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var printer = _context.Printer.Where(x => x.LokacijaID == user.PoduzećeID && x.VrstaPrinteraID == model.ObiteljPrintera.ObiteljPrinteraID).FirstOrDefault();
            var nar = new Narudžba
            {
                NaručiteljID = user.Id,
                Opis = model.Opis,
                PoduzećeID = user.PoduzećeID,
                PrinterID = printer.PrinterID,
                StatusNarudžbeID = 1,
                VrijemeNaruđbe = DateTime.Now,
                VrstaNarudžbeID = 3
            };
            _context.Narudžba.Add(nar);
            _context.SaveChanges();
            var gotovaNar = _context.Narudžba.OrderByDescending(x => x.NarudžbaID).FirstOrDefault().NarudžbaID;
            foreach(var item in model.Potrošni)
            {
                if (item.Količina > 0)
                {
                    var narToPre = new NarudžbaToPredmet
                    {
                        Količina = item.Količina,
                        NarudžbeID = gotovaNar,
                        PredmetiID = item.PredmetID
                    };
                _context.NarudžbaToPredmet.Add(narToPre);
                
                }
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(PopisPrijava));
        }

    }
}