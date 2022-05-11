using SV18T1021246.BusinessLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021246.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public static string name;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected void SetAlert(string message, bool type)
        {
            TempData["AlertMessage"] = message;
            if (type == true)
            {
                TempData["AlertType"] = "alert-success";
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            name = username;
            var data = CommonDataService.LoginAccount(username, password);
            if (data)
            {
                ViewBag.Username = name;
                //Ghi lại cookie phiên đăng nhập
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                //Nhảy vào trang chủ
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Username = name;
            ViewBag.Message = "Đăng nhập thất bại";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword(string un)
        {
            un = name;
            var model = CommonDataService.GetAccountUsername(un);

            if (model == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật thông tin người giao hàng";
            return View("ChangePassword", model);
        }
        [HttpPost]
        public ActionResult Save(Account model,bool type = true)
        {
            string message = string.Empty;
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không hợp lệ");
            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "Mật khẩu không được để trống");

            //Nếu dữ liệu đầu vào không hợp lệ thì trả lại giao diện và thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.EmployeeID > 0)
                    ViewBag.Title = "Cập nhật thông tin người giao hàng";
                else
                    ViewBag.Title = "Bổ sung người giao hàng";

                return View("ChangePassword", model);
            }

            //Xử lý lưu dữ liệu vào CSDL
            if (model.EmployeeID > 0)
            {
                CommonDataService.ChangePassword(model);
                message = "Thay đổi mật khẩu thành công!";
                SetAlert(message, type);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}