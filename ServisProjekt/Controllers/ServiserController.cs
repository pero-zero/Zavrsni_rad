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
using ServisProjekt.Models.ServiserViewModel;

namespace ServisProjekt.Controllers
{
    [Authorize(Roles = "Serviser")]
    public class ServiserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PopisPrijava()
        {
            var nar = _context.Narudžba
                    .Select(result => new PopisNarudžbiModel
                    {
                        NarudžbaID = result.NarudžbaID,
                        VrijemeNarudžbe = result.VrijemeNaruđbe,
                        Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == result.PoduzećeID)
                            .FirstOrDefault().Naziv,
                        Naručitelj = _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Surname,
                        VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == result.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                        SerijskiBrojPrintera = result.PrinterID,
                        Opis = result.Opis.Length < 60 ? result.Opis : result.Opis.Substring(0, 57) + "...",
                        StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == result.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa
                    }).OrderByDescending(x => x.NarudžbaID);
            List<PopisNarudžbiModel> narudžba = new List<PopisNarudžbiModel>();
            foreach (var item in nar)
            {
                narudžba.Add(item);
            }
            /*-----------------------------------------*/
            var searchPoduzećeModel = new SearchPoduzećeModel
            {
                PoduzećePopis = new List<SelectListItem>()
            };
            foreach (var pod in _context.Poduzeće)
            {
                searchPoduzećeModel.PoduzećePopis.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
            }
            /*-----------------------------------------*/
            var searchPrinterModel = new SearchPrinterModel
            {
                PrinterPopis = new List<SelectListItem>()
            };
            foreach (var printer in _context.Printer)
            {
                searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
            }
            /*-----------------------------------------*/
            var model = new PopisPrijavaISearchPoPrinteruIPoduzećuModel
            {
                PopisNarudžbi = narudžba,
                PopisPoduzeća = searchPoduzećeModel,
                PopisPrintera = searchPrinterModel
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PopisPrijava(PopisPrijavaISearchPoPrinteruIPoduzećuModel model)
        {
            if (model.PopisPoduzeća.PoduzećeID != 0 && model.PopisPoduzeća.IsChanged == true)
            {
                var nar = _context.Narudžba.Where(mod => mod.PoduzećeID == model.PopisPoduzeća.PoduzećeID)
                    .Select(result => new PopisNarudžbiModel
                    {
                        NarudžbaID = result.NarudžbaID,
                        VrijemeNarudžbe = result.VrijemeNaruđbe,
                        Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == result.PoduzećeID)
                            .FirstOrDefault().Naziv,
                        Naručitelj = _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Surname,
                        VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == result.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                        SerijskiBrojPrintera = result.PrinterID,
                        Opis = result.Opis.Length < 60 ? result.Opis : result.Opis.Substring(0, 57) + "...",
                        StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == result.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa
                    }).OrderByDescending(x => x.NarudžbaID);
                List<PopisNarudžbiModel> narudžba = new List<PopisNarudžbiModel>();
                foreach (var item in nar)
                {
                    narudžba.Add(item);
                }
                /*-----------------------------------------*/
                var searchPoduzećeModel = new SearchPoduzećeModel
                {
                    PoduzećePopis = new List<SelectListItem>()
                };
                foreach (var pod in _context.Poduzeće)
                {
                    searchPoduzećeModel.PoduzećePopis.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
                }
                /*-----------------------------------------*/
                var searchPrinterModel = new SearchPrinterModel
                {
                    PrinterPopis = new List<SelectListItem>()
                };
                foreach (var printer in _context.Printer.Where(mod => mod.LokacijaID == model.PopisPoduzeća.PoduzećeID))
                {
                    searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
                }
                /*-----------------------------------------*/
                model = new PopisPrijavaISearchPoPrinteruIPoduzećuModel
                {
                    PopisNarudžbi = narudžba,
                    PopisPoduzeća = searchPoduzećeModel,
                    PopisPrintera = searchPrinterModel
                };

                return View(model);
            }
            /*--------------------------------------------------------------------------------------------*/
            else if (model.PopisPoduzeća.IsChanged == false)
            {

                var nar = _context.Narudžba.Where(mod => mod.PrinterID == model.PopisPrintera.PrinterID)
                    .Select(result => new PopisNarudžbiModel
                    {
                        NarudžbaID = result.NarudžbaID,
                        VrijemeNarudžbe = result.VrijemeNaruđbe,
                        Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == result.PoduzećeID)
                            .FirstOrDefault().Naziv,
                        Naručitelj = _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Name + " " +
                            _context.Users.Where(x => x.Id == result.NaručiteljID)
                            .FirstOrDefault().Surname,
                        VrstaNarudžbe = _context.VrstaNarudžbe.Where(x => x.VrstaNarudžbeID == result.VrstaNarudžbeID)
                            .FirstOrDefault().Naziv,
                        SerijskiBrojPrintera = result.PrinterID,
                        Opis = result.Opis.Length < 60 ? result.Opis : result.Opis.Substring(0, 57) + "...",
                        StatusNarudžbe = _context.StatusNarudžbe.Where(x => x.StatusNarudžbeID == result.StatusNarudžbeID)
                            .FirstOrDefault().OpisStatusa
                    }).OrderByDescending(x => x.NarudžbaID);
                List<PopisNarudžbiModel> narudžba = new List<PopisNarudžbiModel>();
                foreach (var item in nar)
                {
                    narudžba.Add(item);
                }
                /*-----------------------------------------*/
                var searchPoduzećeModel = new SearchPoduzećeModel
                {
                    PoduzećePopis = new List<SelectListItem>()
                };
                foreach (var pod in _context.Poduzeće)
                {
                    searchPoduzećeModel.PoduzećePopis.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
                }
                /*-----------------------------------------*/
                var searchPrinterModel = new SearchPrinterModel
                {
                    PrinterPopis = new List<SelectListItem>()
                };
                if (model.PopisPoduzeća.PoduzećeID != 0)
                {
                    if (model.PopisPrintera.PrinterID == "0")
                    {
                        model.PopisPoduzeća.IsChanged = true;
                        return PopisPrijava(model);
                    }
                    foreach (var printer in _context.Printer.Where(mod => mod.LokacijaID == model.PopisPoduzeća.PoduzećeID))
                    {
                        searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
                    }
                }
                else
                {
                    if (model.PopisPrintera.PrinterID == "0")
                    {
                        return PopisPrijava();
                    }
                    foreach (var printer in _context.Printer)
                    {
                        searchPrinterModel.PrinterPopis.Add(new SelectListItem { Text = printer.PrinterID, Value = printer.PrinterID });
                    }
                }
                /*-----------------------------------------*/
                model = new PopisPrijavaISearchPoPrinteruIPoduzećuModel
                {
                    PopisNarudžbi = narudžba,
                    PopisPoduzeća = searchPoduzećeModel,
                    PopisPrintera = searchPrinterModel
                };

                return View(model);
            }
            /*--------------------------------------------------------------------------------------------*/
            else
            {
                return RedirectToAction(nameof(AdminController.PopisPrijava));
            }


        }

        public IActionResult Detaljnije(int? NarudžbaID)
        {
            if(NarudžbaID == null)
            {
                return NotFound();
            }
            var pom = _context.Narudžba.SingleOrDefault(x => x.NarudžbaID == NarudžbaID);
            if (pom == null)
            {
                return NotFound();
            }
            var nar = new PopisNarudžbiModel
            {
                NarudžbaID = pom.NarudžbaID,
                VrijemeNarudžbe = pom.VrijemeNaruđbe,
                Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == pom.PoduzećeID)
                            .FirstOrDefault().Naziv,
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

        public IActionResult DetaljnijePotrošni(int? NarudžbaID)
        {
            if (NarudžbaID == null)
            {
                return NotFound();
            }
            var pom = _context.Narudžba.SingleOrDefault(x => x.NarudžbaID == NarudžbaID);
            if (pom == null)
            {
                return NotFound();
            }
            var nar = new PopisNarudžbiModel
            {
                NarudžbaID = pom.NarudžbaID,
                VrijemeNarudžbe = pom.VrijemeNaruđbe,
                Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == pom.PoduzećeID)
                            .FirstOrDefault().Naziv,
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
                Potrošni = new List<DetaljnijePredmetModel>()
            };
            foreach (var item in NarToPred)
            {

                pot.Potrošni.Add(new DetaljnijePredmetModel
                {
                    PredmetID = item.PredmetiID,
                    Količina = item.Količina,
                    Naziv = _context.Predmet.Where(x => x.PredmetID == item.PredmetiID).FirstOrDefault().Naziv
                });
            }
            pot.Narudžba = nar;
            return View(pot);
        }

        public IActionResult PopisKorisnika(PopisKorisnikaISearchModel model = null)
        {
            if (model.PopisPoduzeća == null && model.PopisKorisnika == null)
            {
                var searchModel = new SearchPoduzećeModel
                {
                    PoduzećePopis = new List<SelectListItem>()
                };
                foreach (var pod in _context.Poduzeće)
                {
                    searchModel.PoduzećePopis.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
                }
                /*-----------------------------------------*/
                var popisKorisnikaModel = _context.Users
                    .Select(result => new PopisKorisnikaModel
                    {
                        Ime = result.Name,
                        Prezime = result.Surname,
                        Email = result.Email,
                        BrojTelefona = result.PhoneNumber,
                        Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == result.PoduzećeID)
                        .FirstOrDefault().Naziv
                    });
                List<PopisKorisnikaModel> popisKorisnika = new List<PopisKorisnikaModel>();
                foreach (var kor in popisKorisnikaModel)
                {
                    popisKorisnika.Add(kor);
                }

                model = new PopisKorisnikaISearchModel
                {
                    PopisKorisnika = popisKorisnika,
                    PopisPoduzeća = searchModel
                };
                return View(model);
            }

            else
            {
                var searchModel = new SearchPoduzećeModel
                {
                    PoduzećePopis = new List<SelectListItem>()
                };
                foreach (var pod in _context.Poduzeće)
                {
                    searchModel.PoduzećePopis.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
                }
                var popisKorisnikaModel = _context.Users.Where(x => x.PoduzećeID == model.PopisPoduzeća.PoduzećeID)
                    .Select(result => new PopisKorisnikaModel
                    {
                        Ime = result.Name,
                        Prezime = result.Surname,
                        Email = result.Email,
                        BrojTelefona = result.PhoneNumber,
                        Poduzeće = _context.Poduzeće.Where(x => x.PoduzećeID == result.PoduzećeID)
                        .FirstOrDefault().Naziv
                    });
                List<PopisKorisnikaModel> popisKorisnika = new List<PopisKorisnikaModel>();
                foreach (var kor in popisKorisnikaModel)
                {
                    popisKorisnika.Add(kor);
                }

                model = new PopisKorisnikaISearchModel
                {
                    PopisKorisnika = popisKorisnika,
                    PopisPoduzeća = searchModel
                };
                return View(model);
            }
        }

        public IActionResult PopisPoduzeća()
        {
            var model = _context.Poduzeće
                .Select(result => new Poduzeće
                {
                    PoduzećeID = result.PoduzećeID,
                    Naziv = result.Naziv,
                    Adresa = result.Adresa,
                    Grad = result.Grad
                });

            return View(model);
        }

        [HttpGet]
        public IActionResult RadniNalog(int NarudžbaID)
        {
            var pod = _context.Narudžba.Where(x => x.NarudžbaID == NarudžbaID).FirstOrDefault().PrinterID;
            var printer = _context.Printer.Where(x => x.PrinterID == pod).FirstOrDefault();
            var predmet = _context.Predmet.Where(x => x.PrinterID == printer.VrstaPrinteraID);
            var dijelovi = new List<OtpisPredmetaModel>();
            dijelovi.Add(new OtpisPredmetaModel
            {
                Predmet = new List<SelectListItem>()
            }); 
            foreach (var item in predmet)
            {
                dijelovi[0].Predmet.Add(new SelectListItem{Text = item.PredmetID+" | "+item.Naziv,
                    Value = item.PredmetID});
            }
            var model = new NalogPredmetNarudžbaModel
            {
                Narudžba = NarudžbaID,
                Otpis = dijelovi
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RadniNalog(NalogPredmetNarudžbaModel model)
        {
            if (model.DodajJoš)
            {
                var pod = _context.Narudžba.Where(x => x.NarudžbaID == model.Narudžba).FirstOrDefault().PrinterID;
                var printer = _context.Printer.Where(x => x.PrinterID == pod).FirstOrDefault();
                var predmet = _context.Predmet.Where(x => x.PrinterID == printer.VrstaPrinteraID);
                model.Otpis.Add(new OtpisPredmetaModel());
                for (var i = 0; i < model.Otpis.Count(); i++)
                {
                    model.Otpis[i].Predmet = new List<SelectListItem>();
                    foreach (var item in predmet)
                    {
                        model.Otpis[i].Predmet.Add(new SelectListItem { Text = item.PredmetID + " | " + item.Naziv,
                            Value = item.PredmetID });
                    }
                }

                return View(model);
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                var nalog = new RadniNalog
                {
                    KorisnikID = user.Id,
                    NarudžbaID = model.Narudžba,
                    Opis = model.Nalog.Opis,
                    VrijemeDolaska = model.Nalog.VrijemeDolaska,
                    VrijemeOdlaska = model.Nalog.VrijemeOdlaska,
                    Counter = model.Nalog.Counter,
                    Datum = DateTime.Now
                };
                _context.RadniNalog.Add(nalog);
                _context.SaveChanges();
                nalog = _context.RadniNalog.Where(x => x.NarudžbaID == model.Narudžba).FirstOrDefault();
                foreach (var item in model.Otpis)
                {
                    if(item.Količina > 0)
                    {
                        var otpis = new OtpisSaSkladišta
                        {
                            PredmetID = item.PredmetID,
                            RadniNalogID = nalog.RadniNalogID,
                            Kolicina = item.Količina
                        };
                        _context.OtpisSaSkladišta.Add(otpis);
                    }
                }
                var narudžba = _context.Narudžba.Where(x => x.NarudžbaID == model.Narudžba).FirstOrDefault();
                narudžba.StatusNarudžbeID = 4;
                _context.Narudžba.Update(narudžba);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(PopisPrijava));
        }

        public IActionResult PrikazRadniNalog (int NarudžbaID)
        {
            var rn = _context.RadniNalog.Where(x => x.NarudžbaID == NarudžbaID).FirstOrDefault();
            var rnpredmet = _context.OtpisSaSkladišta.Where(x => x.RadniNalogID == rn.RadniNalogID);
            var model = new DetaljnijeRadniNalogModel
            {
                RadniNalog = new RadniNalogModel
                {
                    Counter = rn.Counter,
                    Opis = rn.Opis,
                    RadniNalogID = rn.RadniNalogID,
                    VrijemeDolaska = rn.VrijemeDolaska,
                    VrijemeOdlaska = rn.VrijemeOdlaska,
                    Serviser = _context.Users.Where(x => x.Id == rn.KorisnikID).FirstOrDefault().Name + " " 
                    + _context.Users.Where(x => x.Id == rn.KorisnikID).FirstOrDefault().Surname
                },
                PopisPredmeta = new List<PopisPredmetaRNModel>()
            };
            foreach(var item in rnpredmet)
            {
                model.PopisPredmeta.Add(new PopisPredmetaRNModel { PredmetID = item.PredmetID, Količina = item.Kolicina,
                    Naziv = _context.Predmet.Where(x => x.PredmetID == item.PredmetID).FirstOrDefault().Naziv});
            }

            return View(model);
        }

    }    
}