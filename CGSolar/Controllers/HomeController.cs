using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CGSolar.Models;

namespace CGSolar.Controllers
{
    public class HomeController : Controller
    {
        GuptaAgroDBContext db = new GuptaAgroDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult OandMSheet()
        {
            

            return View();
        }

        public ActionResult ComplaintForm()
        {

            return View();
        }

        public ActionResult BeneficiaryDetails()
        {
            var categoryList = db.tbl_beneficiary.OrderBy(b => b.category).Select(b => b.category).ToList().Distinct();
            ViewBag.category = categoryList;

            return View();
        }
        [HttpPost]
        public ActionResult BeneficiaryDetails(BeneficiaryDetailsModel model)
        {
            var categoryList = db.tbl_beneficiary.OrderBy(b => b.category).Select(b => b.category).ToList().Distinct();
            ViewBag.category = categoryList;

            var beneficiaryList = db.tbl_beneficiary.Where(b => b.category == model.Category).Select(b => b).ToList();

            BeneficiaryDetailsModel benDetails = new BeneficiaryDetailsModel();
            benDetails.BeneficiaryList = beneficiaryList;
            return View(benDetails);
        }
    }
}