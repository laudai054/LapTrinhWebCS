using SV18T1021246.BusinessLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021246.Web.Controllers
{
    public class ProductAttributeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        [Route("attribute/edit/{method}/{productID}/add")]
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung thuộc tính mặt hàng";
            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0
            };

            return View(model);

        }
        /// <summary>
        /// Thay đổi thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("attribute/edit/{method}/{productID}/{attributeID?}")]
        public ActionResult Edit(string attributeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(attributeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            var model = ProductDataService.GetProductAttribute(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật thuộc tính mặt hàng";
            return View("Create", model);

        }
        [HttpPost]
        public ActionResult Save(ProductAttribute model)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.AttributeID == 0 ? "Bổ sung thuộc tính mặt hàng" : "Cập nhật thuộc tính mặt hàng";
                return View("Create", model);
            }
            if (model.AttributeID == 0)
                ProductDataService.AddProductAttribute(model);
            else
                ProductDataService.UpdateProductAttribute(model);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa thông tin khách hàng khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("delete/{attributeID}")]
        public ActionResult Delete(int attributeID)
        {
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProductAttribute(attributeID);
                return RedirectToAction("Index");
            }
            var model = ProductDataService.GetProductAttribute(attributeID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}
