﻿using PP.WaiMai.Model;
using PP.WaiMai.Model.Enums;
using PP.WaiMai.Model.ViewModels;
using PP.WaiMai.Util.Security;
using PP.WaiMai.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PP.WaiMai.Web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult LoginDialog()
        {
            //获取本地的IP地址
            var ip = WaiMai.Util.IPAddress.GetInternalIP();
            //从数据库里查找，看有没有相关的。如果存在，则显示出用户名

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginDialog(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (OperateContext.Current.Login(model))
                {
                    return JsonMsgOk("登陆成功");
                }
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return JsonMsgNoOk("用户名或密码错误");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (OperateContext.Current.Login(model))
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误.");
                }
            }
            //跳转到原来的地址
            ViewBag.ReturnUrl = returnUrl;
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.IPAddress = WaiMai.Util.Http.CheckIP.GetIP();
            return View(model);
        }

        #region --用户注册
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var isExist = BLLSession.IUserService.GetListBy(m => m.UserName == registerViewModel.UserName).Count() > 0;
                if (isExist)
                {
                    ModelState.AddModelError("", "该用户名已经存在，请直接登陆.");
                    return View(registerViewModel);
                }
                User model = new Model.User();
                //判断是否是6楼
                if (OperateHelper.Is6F())
                {
                    model = Register_6F(model, registerViewModel);
                }
                else
                {
                    model = Register_10F(model, registerViewModel);
                }
                //保存信息到Session和写入到Cookies
                WebHelper.OperateContext.Current.SetUserToSessionAndCookies(model, true);
                return Redirect("/home");
            }

            return View(registerViewModel);
        }

        //10楼样式
        private Model.User Register_10F(User model, RegisterViewModel registerViewModel)
        {
            model.UserName = registerViewModel.UserName;
            model.IPAddress = registerViewModel.IPAddress;
            model.Password = Util.Security.UEncypt.MD5(registerViewModel.Password);
            model.Amount = 0;
            model.CreateDate = DateTime.Now;
            model.IsDel = false;
            model.DepartmentType = registerViewModel.DepartmentType;
            BLLSession.IUserService.Add(model);
            return model;
        }

        //6楼充值样式
        private Model.User Register_6F(User model, RegisterViewModel registerViewModel)
        {
            model.UserName = registerViewModel.UserName;
            model.IPAddress = registerViewModel.IPAddress;
            model.Password = Util.Security.UEncypt.MD5(registerViewModel.Password);
            model.Amount = 10000;//默认充值10000元
            model.CreateDate = DateTime.Now;
            model.IsDel = false;
            model.DepartmentType = registerViewModel.DepartmentType;
            BLLSession.IUserService.Add(model);

            //插入充值表,默认10000元
            Recharge rechargeModel = new Recharge();
            rechargeModel.UserID = model.UserID;
            rechargeModel.RechargeAmount = 10000;
            rechargeModel.Status = (int)RechargeStatusEnum.Succeed;
            rechargeModel.IsDel = false;
            rechargeModel.CreateDate = DateTime.Now;
            rechargeModel.OpeningBalance = 0;
            rechargeModel.CurrentBalance = 10000;
            rechargeModel.RechargeUserName = "sys";
            BLLSession.IRechargeService.Add(rechargeModel);

            //插入数据到消费流水表
            BLLSession.IExpendLogService.Add(new ExpendLog()
            {
                UserID = model.UserID,
                ConsumeAmount = 0,
                RechargeAmount = rechargeModel.RechargeAmount,
                CreateDate = DateTime.Now,
                ExpendLogTypeID = rechargeModel.RechargeID,
                ExpendLogType = (int)ExpendLogTypeEnum.Recharge,
                Description = "充值完成增加金额"
            });
            return model;
        }

        #endregion

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebHelper.OperateContext.Current.LogOff();
            return RedirectToAction("Index", "Home");
        }




        #region --修改密码+ChangePassword
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!OperateHelper.IsLogin())
                {
                    return JsonMsgNoOk("请先登陆");
                }
                var oldPassword = UEncypt.MD5(model.OldPassword);
                var newModel = BLLSession.IUserService.GetListBy(m => m.UserID == OperateHelper.User.UserID && m.Password == oldPassword).FirstOrDefault();
                if (newModel != null)
                {
                    newModel.Password = UEncypt.MD5(model.NewPassword);
                    BLLSession.IUserService.ModifyModel(newModel);
                    return JsonMsgOk("修改密码成功，下次登陆生效");
                }
                return JsonMsgErr("原密码错误");
            }
            return JsonMsgErr("修改密码失败，请联系管理员");
        }
        #endregion

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}