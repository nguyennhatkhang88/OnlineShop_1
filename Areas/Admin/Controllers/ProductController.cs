using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnLineShop_1.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString,int page = 1,int pagesize = 10)
        
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductDao().Delete(id);
            SetAlert("Xóa sản phẩm thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            var dao = new ProductDao();
            var result = dao.Insert(product);
            if (result > 0)
            {
                SetAlert("Thêm sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(long id)
        {
            var model = new ProductDao().Edit(id);
            SetViewBag(model.CategoryID);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Index");
        }
        public void SetViewBag(long? Categoryid = null)
        {
            var dao = new ProductDao();
            
            ViewBag.CategoryID = new SelectList(dao.SelectCategoryID(), "ID", "Name", Categoryid);
        }
    }
}