using Model.Dao;
using Model.EF;
using OnLineShop_1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShop_1.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
       
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);

            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var culture = Session[CommonContants.CurrentCulture];
                var dao = new ContentDao();
                var result = dao.Update(model);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonContants.USER_SEESION];
                model.CreatedBy = session.UserName;
                var culture = Session[CommonContants.CurrentCulture];
                model.Language = culture.ToString();
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}