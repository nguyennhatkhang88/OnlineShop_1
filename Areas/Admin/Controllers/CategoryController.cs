using Model.Dao;
using Model.EF;
using OnLineShop_1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnLineShop_1.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString,int page =1,int pagesize=10)
        {
           // string SearchString = convertToUnSign2(searchString);
            var model = new CategoryDao().pageList(searchString,page,pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //public string convertToUnSign2(string s)
        //{
        //    string stFormD = s.Normalize(NormalizationForm.FormD);
        //    StringBuilder sb = new StringBuilder();
        //    for (int ich = 0; ich < stFormD.Length; ich++)
        //    {
        //        System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
        //        if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
        //        {
        //            sb.Append(stFormD[ich]);
        //        }
        //    }
        //    sb = sb.Replace('Đ', 'D');
        //    sb = sb.Replace('đ', 'd');
        //    return (sb.ToString().Normalize(NormalizationForm.FormD));
        //}
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonContants.CurrentCulture];
                model.Language = currentCulture.ToString();
                var id = new CategoryDao().Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm danh mục tin tức thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryfailed);
                }
            }
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CategoryDao().Delete(id);
            SetAlert("Xóa danh mục tin tức thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long id)
        {
            var model = new CategoryDao().ViewDetailEdit(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            var result = new CategoryDao().Update(model);
            if (result)
            {
                SetAlert("Sửa danh mục tin tức thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}