using Entity.ViewModel;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Service.BaseBiz;

namespace GuoChe.Controllers
{
    public class ChargeController : BaseController
    {

        public ActionResult ChargeBase(string name,int status=-1)
        {
            List<ChargingBaseEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || status > -1)
            {
                mList = ChargeService.GetChargingBaseInfoByRule(name,status);
            }
            else
            {
                mList = ChargeService.GetAllChargingBaseEntity();
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.ChageBase = mList;
            return View();
        }

        public ActionResult ChargePile(string name, int status = -1,int cid=-1)
        {
            List<ChargingPileEntity> mList = null;
            if (!string.IsNullOrEmpty(name) || status > -1 || cid>-1)
            {
                mList = ChargeService.GetChargingPileInfoByRule(name, status, cid);
            }
            else
            {
                mList = ChargeService.GetAllChargingPileEntity();
            }
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.ChargePile = mList;
            return View();
        }

        public ActionResult EditChargeBase(string cbId)
        {
            ViewBag.Province = BaseDataService.GetAllProvince();
            ViewBag.PayType = BaseDataService.GetAllPayType();
            if (string.IsNullOrEmpty(cbId))
            {
                ViewBag.ChargeBase = new ChargingBaseEntity();
            }
            else
            {
                ViewBag.ChargeBase = ChargeService.GetChargingBaseById(cbId.ToInt(0));
            }

            return View();
        }

        public ActionResult EditChargePile(string cpId)
        {
            ViewBag.AllBase = ChargeService.GetAllChargingBaseEntity().Where(t => t.IsUse == 1).ToList();
            if (string.IsNullOrEmpty(cpId))
            {
                ViewBag.ChargePile = new ChargingPileEntity();
            }
            else
            {
                ViewBag.ChargePile = ChargeService.GetChargingPileEntityById(cpId.ToLong(0));
            }

            return View();
        }

        public void ModifyChargeBase(ChargingBaseEntity entity)
        {
            ChargeService.ModifyChargingBase(entity);
            Response.Redirect("/Charge/ChargeBase");
        }

        public void ModifyChargePile(ChargingPileEntity entity)
        {
            ChargeService.ModifyChargingPile(entity);
            Response.Redirect("/Charge/ChargePile");
        }

        public void RemoveChargeBase(int cbId)
        {
            ChargeService.RemoveChargingBase(cbId);
            Response.Redirect("/Charge/ChargeBase");
        }

        public void RemoveChargePile(long cpId)
        {
            ChargeService.RemoveChargingPiple(cpId);
            Response.Redirect("/Charge/ChargePile");
        }

    }
}
