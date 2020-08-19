using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServisProjekt.Data;
using ServisProjekt.Extensions;
using ServisProjekt.Models;
using ServisProjekt.Models.AdminViewModel;
using ServisProjekt.Services;

namespace ServisProjekt.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AdminController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
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
                    }).OrderByDescending(x=> x.NarudžbaID);
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
                else {
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

        public IActionResult PopisKorisnika(PopisKorisnikaISearchModel model = null)
        {
            if (model.PopisPoduzeća == null && model.PopisKorisnika == null) {
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
                foreach(var kor in popisKorisnikaModel)
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

        public async Task<IActionResult> DeleteUser(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult DeleteUserConfirmed (string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            _context.Users.Remove(user);
            if(_context.Narudžba.Where(x => x.NaručiteljID == user.Id) != null)
            {
                _context.Narudžba.RemoveRange(_context.Narudžba.Where(x => x.NaručiteljID == user.Id));
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(PopisKorisnika));
        }

        public IActionResult DodajKorisnika()
        {
            var vm = new AddUserModel
            {
                Poduzeće = new List<SelectListItem>(),
                VrstaKorisnika = new List<SelectListItem>()
            };
            foreach (var pod in _context.Poduzeće)
            {
                vm.Poduzeće.Add(new SelectListItem { Text = pod.Naziv, Value = pod.PoduzećeID.ToString() });
            }
            foreach (var role in _context.Roles)
            {
                if (role.Name == "Administrator")
                {
                    continue;
                }
                vm.VrstaKorisnika.Add(new SelectListItem { Text = role.Name, Value = role.Id });
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajKorisnika(AddUserModel model)
        {
            if (model.Email == null)
            {
                return RedirectToAction(nameof(DodajKorisnika));
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Name = model.Ime,
                    Surname = model.Prezime,
                    PoduzećeID = model.PoduzećeID
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Administrator created a new account with password ({Email}).", user.UserName);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);


                    await _userManager.AddToRoleAsync(user, await _roleManager.GetRoleNameAsync
                        (await _roleManager.FindByIdAsync(model.RoleID)));



                    return View("UspješnoKreiranKorisnik");
                }
                AddErrors(result);
                return View(model);
            }
            return View(model);
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
        public IActionResult DodajPoduzeće()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DodajPoduzeće(Poduzeće model)
        {
            _context.Poduzeće.AddAsync(model);
            _context.SaveChanges();
            _logger.LogInformation("Administrator created new Poduzeće {Naziv}.", model.Naziv);
            return View("UspješnoKreiranoPoduzeće");
        }

        public async Task<IActionResult> DeletePoduzeće(int? PoduzećeID)
        {
            if (PoduzećeID == null)
            {
                return NotFound();
            }

            var poduzeće = await _context.Poduzeće.SingleOrDefaultAsync(x => x.PoduzećeID == PoduzećeID);
            if (poduzeće == null)
            {
                return NotFound();
            }
            return View(poduzeće);
        }

        public IActionResult DeletePoduzećeConfirmed(int? PoduzećeID)
        {
            var poduzeće = _context.Poduzeće.SingleOrDefault(x => x.PoduzećeID == PoduzećeID);
            _context.Poduzeće.Remove(poduzeće);
            _context.SaveChanges();
            _logger.LogInformation("Administrator Deleted Poduzeće {Naziv}.", poduzeće.Naziv);
            return RedirectToAction(nameof(PopisPoduzeća));
        }

        public async Task<IActionResult> EditPoduzeće(int? PoduzećeID)
        {
            if (PoduzećeID == null)
            {
                return NotFound();
            }

            var poduzeće = await _context.Poduzeće.SingleOrDefaultAsync(x => x.PoduzećeID == PoduzećeID);
            if (poduzeće == null)
            {
                return NotFound();
            }
            return View(poduzeće);
        }
        
        [HttpPost]
        public IActionResult EditPoduzeće(Poduzeće model)
        {
            _context.Poduzeće.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(PopisPoduzeća));
        }

        public IActionResult Detaljnije(int? NarudžbaID)
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

        public IActionResult PopisNaSkladištu()
        {
            var predmet = _context.Predmet.Select(result => new PredmetNaSkladištu
            {
                PredmetID = result.PredmetID,
                Naziv = result.Naziv,
                KolicinaNaSkladištu = result.Količina,
                ObiteljPrintera = _context.ObiteljPrintera.Where(x => x.ObiteljPrinteraID == result.PrinterID).FirstOrDefault().Obitelj,
                Skladište = _context.Skladište.Where(x => x.SkladišteID == result.SkladišteID).FirstOrDefault().NazivSkladišta
            }) ;
            return View(predmet);
        }

        public IActionResult DodajPredmet()
        {
            var predmet = new DodajPredmetModel
            {
                ObiteljPrintera = new List<SelectListItem>(),
                VrstaPredmeta = new List<SelectListItem>()
            };
            foreach (var obi in _context.ObiteljPrintera)
            {
                predmet.ObiteljPrintera.Add(new SelectListItem { Text = obi.Obitelj, Value = obi.ObiteljPrinteraID.ToString() });
            }
            foreach (var pred in _context.VrstaPredmeta)
            {
                predmet.VrstaPredmeta.Add(new SelectListItem { Text = pred.Vrsta, Value = pred.VrstaPredmetaID.ToString() });
            }
            return View(predmet);
        }

        [HttpPost]
        public IActionResult DodajPredmet(DodajPredmetModel model)
        {
            if (model.Naziv != null && model.MinimalnaKoličina > 0 && model.ObiteljPrinteraID != 0 
                && model.VrstaPredmetaID != 0 && model.Cijena > 0 && model.Dobavljač!=null)
            {
                var predmet = new Predmet
                {
                    Cijena = model.Cijena,
                    Dobavljač = model.Dobavljač,
                    Količina = model.Količina,
                    MinimalnaKoličina = model.MinimalnaKoličina,
                    Naziv = model.Naziv,
                    PredmetID = model.PredmetID,
                    PrinterID = model.ObiteljPrinteraID,
                    SkladišteID = 1,
                    VrstaPredmetaID = model.VrstaPredmetaID
                };
                _context.Predmet.Add(predmet);
                _context.SaveChanges();
            }
            else if (model.PredmetID != null && model.Količina > 0 && model.Naziv != null && model.MinimalnaKoličina > 0 && model.ObiteljPrinteraID != 0
                && model.VrstaPredmetaID != 0 && model.Cijena > 0 && model.Dobavljač != null)
            {
                var predmet = _context.Predmet.Where(x => x.PredmetID == model.PredmetID).FirstOrDefault();
                predmet.Količina += model.Količina;
                _context.Predmet.Update(predmet);
                _context.SaveChanges();
            }
            else
            {
                model.Error = "Niste popunili formu do kraja!";
                return View(model);
            }
            return RedirectToAction(nameof(AdminController.PopisNaSkladištu));
        }

        public IActionResult PrikazRadniNalog(int NarudžbaID)
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
            foreach (var item in rnpredmet)
            {
                model.PopisPredmeta.Add(new PopisPredmetaRNModel
                {
                    PredmetID = item.PredmetID,
                    Količina = item.Kolicina,
                    Naziv = _context.Predmet.Where(x => x.PredmetID == item.PredmetID).FirstOrDefault().Naziv
                });
            }

            return View(model);
        }

        // Dodatne funkcije
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
