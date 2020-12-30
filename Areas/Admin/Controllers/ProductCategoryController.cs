using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShop_1.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string Seachstring, int page = 1,int pagesize = 2 )
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAll(Seachstring,page, pagesize);
            ViewBag.SearchString = Seachstring;
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory model)
        {
            var dao = new ProductCategoryDao();

            if (ModelState.IsValid)
            {
                long id = dao.Insert(model);
                if (id > 0)
                {
                    //SetAlert("Thêm user thành công", "success");
                    ModelState.AddModelError("", "Thêm danh mục thành công");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var dao = new ProductCategoryDao();
            var check = dao.Delete(id);
            if (check)
            {
                ModelState.AddModelError("", "Xóa thành công");
                return Redirect("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa không thành công");
                return Redirect("Index");
            }
        }
        public ActionResult Edit(long id)
        {
            var dao = new ProductCategoryDao().ViewDetail(id);
            return View(dao);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            var dao = new ProductCategoryDao();
            if (ModelState.IsValid)
            {
                var x = dao.Edit(productcategory);
                if (x)
                {
                    ModelState.AddModelError("", "Sửa thành công");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa không thành công");
                    return Redirect("Index");
                }
            }

            return View("Index");
        }

    }
}