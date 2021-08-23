﻿using StarSecurityService.Application.Branchs;
using StarSecurityService.Application.CategoryServiceoofers;
using StarSecurityService.Application.Vacancys;
﻿using StarSecurityService.Application.Branchs;
using StarSecurityService.Application.Clients;
using StarSecurityService.Application.Histories;
using StarSecurityService.Application.ServiceOffers;
using StarSecurityService.ApplicationCore.Entities;
using StarSecurityService.EntityFramework.Data;
using StarSecurityService.Web.Areas.Admin.Controllers;
using StarSecurityService.Web.Helpers;
using StarSecurityService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;

namespace StarSecurityService.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IVacancyService _vacancyService;

        private StarServiceDbContext db;
        private readonly IClientAppService _clientAppServices;
        private readonly IServiceOfferService _serviceOfferService;
        private readonly IBrachAppService _branchService;
        private readonly IHistoryService _historyService;
        private readonly IShareHolderService _shareHolderService;
        private readonly IServiceOfferAppService _CategoryServiceOfferRepository;
        public HomeController()
        {
            db = new StarServiceDbContext();
            _clientAppServices = new ClientAppServices();
            _serviceOfferService = new ServiceOfferService();
            _branchService = new BranchAppService();
            _CategoryServiceOfferRepository = new CategoryServiceOfferService();
            _historyService = new HistoryService();
            _shareHolderService = new ShareHolderService();
            _vacancyService = new VacancyService();

        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AboutUs()
        {
            var model = new HomeAboutUs();
            model.HistoryModel = await _historyService.GetAll();
            model.ShareHolderModel = await _shareHolderService.GetAll();
            return View(model);
        }

        public async Task<ActionResult> Career()
        {
            var items = await _vacancyService.GetAllByStatus();
            return View(items);
        }


        public async Task<ActionResult> JobDetails(int id)
        {
            var items = await _vacancyService.FirstOrDefaultAsync(id);
            ViewBag.Branch = await _branchService.FirstOrDefaultAsync(items.BranchId);
            ViewBag.Service = await _serviceOfferService.FirstOrDefaultAsync(items.ServiceOfferId);
            string[] image = ViewBag.Service.Url.Split(' ');
            List<string> path = new List<string>();
            foreach (string img in image)
            {
                path.Add(img);
            }
            ViewBag.Image = path;
            return View(items);
        }

        public async Task<ActionResult> ContactUs()
        {
            var db = await _branchService.GetAllByStatus();
            return View(db);
        }
        public ActionResult Divisions()
        {
            return View();
        }
        public async Task<ActionResult> Profesional(int id)
        {
          
            return View((await _serviceOfferService.GetAllByStatus()).Where(x => x.CategoryServiceOfferId == id).Select(x => new ServiceOffer
                            {
                                Id = x.Id,
                                Title = x.Title,
                                Introduce = x.Introduce,
                                Description = x.Description,
                                Details = x.Details,
                                Url = x.Url,
                            })?.AsEnumerable());
        }

        public async Task<JsonResult> GetServiceOfferByCateId(int id)
        {
           // var res = new Response<List<ComboboxCommonDto>>();
            return Json(1,JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetNavPartialServiceOffer()
        {
            var data = await _CategoryServiceOfferRepository.GetAll(status:true);

            return PartialView("~/Views/Shared/NavServiceOffer.cshtml",data);
        }
        public ActionResult Emiratisation()
        {
            return View();
        }
        public ActionResult DubaiDivision()
        {
            return View();
        }
        public ActionResult Recruitment()
        {
            return View();
        }
        public ActionResult Supervision()
        {
            return View();
        }
        public ActionResult Training()
        {
            return View();
        }
        public async Task<ActionResult> Facilities()
        {
            var data = db.ServiceOffers.FirstOrDefault();
            if (data != null)
            {
                var thumb = GetControllerHelper.GetThumb(data.Url);
                ViewBag.thumbData = thumb;
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.serviceData = data;
            return View();
        }
    }
}