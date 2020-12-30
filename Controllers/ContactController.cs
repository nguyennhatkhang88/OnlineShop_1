using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShop_1.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public ActionResult Send(string txtName, string txtMobile, string txtAddress, string txtEmail, string txtContent)
        {
            var feedback = new Feedback();
            feedback.Name = txtName;
            feedback.Email = txtEmail;
            feedback.CreateDate = DateTime.Now;
            feedback.Phone = txtMobile;
            feedback.Content = txtContent;
            feedback.Address = txtAddress;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {

                return RedirectToAction("Index");
                //return Json(new
                //{
                //    status = true
                //});
                //send mail
            }
            else
            {
                return RedirectToAction("Index");
            }
            //else
            //    return Json(new
            //    {
            //        status = false
            //    });

        }
    }
}