using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CGSolar.Models;

namespace CGSolar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        GuptaAgroDbContext db = new GuptaAgroDbContext();
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
        [Authorize(Roles ="Admin")]
        public ActionResult Register()
        {
            ViewBag.Roles = db.tbl_roles.Select(r => r).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            ViewBag.Roles = db.tbl_roles.Select(r => r).ToList();
            string empName = form["empName"].ToString();
            string contact = form["contact"].ToString();
            string pwd = form["password"].ToString();
            int role = Convert.ToInt32(form["role"]);
            var roleDesc = db.tbl_roles.Where(r => r.roleid == role).Select(r => r.role).FirstOrDefault();
            var uid = db.usp_generateUserID(empName, contact, roleDesc);

            var userid = uid.First();
            db.usp_RegisterUser(empName,userid,roleDesc,contact,pwd,DateTime.Now,Environment.UserName);
            var empList = db.tbl_employee.Select(e => e).ToList();
            return View(empList);
        }
        [HttpPost]
        public JsonResult UserCheck(string empName,string contact, int role)
        {
            var roleDesc = db.tbl_roles.Where(r => r.roleid == role).Select(r => r.role).FirstOrDefault();
            var uid = db.usp_generateUserID(empName,contact,roleDesc);
            
            var userid = uid.First();
            return Json(userid,JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}
        //public JsonResult LoginValidation(string UserName, string Password)
        //{
        //    return Json(!db.tbl_employee.Any(emp=>(emp.userid == UserName && emp.password == Password)||(emp.ContactNo == UserName && emp.password == Password)),JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public ActionResult Login(LoginModel login)
        //{
        //    try
        //    {
        //        var role = db.tbl_employee.Where(e => e.userid == login.UserName && e.password == login.Password).Select(e => e.Role).FirstOrDefault();
        //        Session["role"] = role;
        //        Session.Timeout = 30;
        //        if (role == "Admin")
        //        {
        //            return RedirectToAction("AdminDashboard");
        //        }
        //        else if (role == "Manager")
        //        {
        //            return RedirectToAction("ManagerDashboard");
        //        }
        //        else if (role == "Field Assitant")
        //        {
        //            return RedirectToAction("FADashboard");
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return View("Error");
        //    }
        //}

        [Authorize(Roles ="Field Assitant,Admin,Manager")]
        public ActionResult OandMSheet()
        {
            ViewBag.EmployeeList = db.tbl_employee.Where(e => e.Role == "Field Assitant").Select(e => e).ToList();
            var list = db.tbl_OandM.Select(o => o).Distinct().ToList();
            if (Session["role"].ToString() == "Field Assitant")
            {
                list = list.Where(o => o.AssignedTo == Session["ID"].ToString()).Select(o => o).ToList();
            }
            else if (Session["role"].ToString() == "Manager")
            {
                list = list.Where(o => o.Created_By == Session["ID"].ToString()).Select(o => o).ToList();
            }
            else if(Session["role"].ToString() == "Admin")
            {
                list = list;
            }
            List<OperationAndMaintenanceModel> oNmList = new List<OperationAndMaintenanceModel>();
            foreach(var maintenanceDetails in list)
            {
                int assignedTo =  Convert.ToInt32(maintenanceDetails.AssignedTo);
                OperationAndMaintenanceModel oNmObj = new OperationAndMaintenanceModel();
                oNmObj.BeneficiaryName = maintenanceDetails.tbl_beneficiary.BeneficiaryName;
                oNmObj.BeneficiaryID = maintenanceDetails.tbl_beneficiary.BeneficiaryID;
                oNmObj.Block = maintenanceDetails.tbl_beneficiary.Block;
                oNmObj.CompletionDate = maintenanceDetails.DateOfCompletion;
                oNmObj.Contact = maintenanceDetails.tbl_beneficiary.ContactNo;
                oNmObj.District = maintenanceDetails.tbl_beneficiary.District;
                oNmObj.ProblemDescription = maintenanceDetails.ProblemType;
                oNmObj.ReportedDate = maintenanceDetails.ProblemreportedOn;
                oNmObj.SystemCapacity = maintenanceDetails.tbl_beneficiary.systemCapacity;
                oNmObj.Status = maintenanceDetails.Status;
                oNmObj.Village = maintenanceDetails.tbl_beneficiary.Village;
                oNmObj.WorkOrderNo = maintenanceDetails.WorkOrderID;
                oNmObj.AssignedTo = assignedTo;/*db.tbl_employee.Where(e=>e.EmployeeName.Trim() == assignedTo.Trim()).Select(e=>e.EmployeeID).FirstOrDefault() ;*/
                oNmObj.AssignedEmployee = db.tbl_employee.Where(e => e.EmployeeID == assignedTo).Select(e => e.EmployeeName).FirstOrDefault();
                oNmList.Add(oNmObj);
            }
            
            return View(oNmList);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Field Assitant")]
        public JsonResult EditOandM(int benID,string benName, string workOrder, string village, string block, string district,string sysCapacity,string contact, string reportDate, string completionDate, string problemDesc, string status, int assignedTo)
        {
            var benObj = db.tbl_beneficiary.Where(b => b.BeneficiaryID == benID).Select(b => b).FirstOrDefault();
            
            benObj.BeneficiaryName = benName;
            benObj.WorkOrderNo = workOrder;
            benObj.Village = village;
            benObj.Block = block;
            benObj.District = district;
            benObj.systemCapacity = sysCapacity;
            benObj.ContactNo = contact;

            var OandMObj = db.tbl_OandM.Where(o => o.BeneficiaryID == benID).Select(o => o).FirstOrDefault();

            OandMObj.DateOfCompletion = Convert.ToDateTime(completionDate);
            OandMObj.ProblemreportedOn = Convert.ToDateTime(reportDate);
            OandMObj.ProblemType = problemDesc;
            OandMObj.Status = status;
            OandMObj.WorkOrderID = workOrder;
            string assignedEmp = db.tbl_employee.Where(e => e.EmployeeID == assignedTo).Select(e => e.EmployeeName).FirstOrDefault();
            OandMObj.AssignedTo = assignedTo.ToString();

            db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult ComplaintForm()
        {
            ViewBag.EmployeeList = db.tbl_employee.Where(e=>e.Role == "Field Assitant").Select(e => e).ToList();
            ViewBag.PumpTypeList = db.tbl_beneficiary.Select(e => e.PumpType).Distinct().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ComplaintForm(FormCollection form)
        {
            string workOrder = form["WorkOrder"].ToString();
            int? beneficiaryID = Convert.ToInt32(form["benID"]);
            string assignedTo = form["AssignedTo"].ToString();
            //string employeeName = db.tbl_employee.Where(e => e.EmployeeID == assignedTo).Select(e => e.EmployeeName).FirstOrDefault();
            string aadhar = db.tbl_beneficiary.Where(b => b.BeneficiaryID == beneficiaryID).Select(b => b.Aadhar).FirstOrDefault();
            DateTime reportedDate = Convert.ToDateTime(form["reportedDate"]).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            string problemType = form["ProblemDescription"].ToString();
            string createdBy = Session["ID"].ToString();

            db.usp_complaint(workOrder,beneficiaryID,assignedTo,aadhar,reportedDate,problemType,createdBy);
            return RedirectToAction("OandMSheet", "Home");
        }
        [Authorize(Roles = "Field Assitant,Admin,Manager")]
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

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {                        
            var list = db.tbl_beneficiary.Where(b => b.BeneficiaryName.ToLower().Contains(prefix.ToLower())).Select(b => new { label = b.BeneficiaryName, value = b.BeneficiaryID }).Distinct();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoFill(int id)
        {

            var list = db.tbl_beneficiary.Where(b => b.BeneficiaryID == id).Select(b => new { name = b.BeneficiaryName, workOrder = b.WorkOrderNo, village = b.Village, block = b.Block, district = b.District, contact = b.ContactNo, systemCapacity = b.systemCapacity, pumpType = b.PumpType }).FirstOrDefault();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}