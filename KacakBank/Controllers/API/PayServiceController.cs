using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KacakBank.Models;


namespace KacakBank.Controllers.API
{
    public class ProductModel
    {
        public string MerchantCode { get; set; }
        public string MerchandPassword { get; set; }
        public string CardNo { get; set; }
        public string ExpM { get; set; }
        public string ExpY { get; set; }
        public string CVV { get; set; }
        public decimal Price { get; set; }
    }
    public class PayServiceController : ApiController
    {

        KacakBank_DBEntities db = new KacakBank_DBEntities();

        public IHttpActionResult GetPayServices()
        {
            return Json("Success");
        }

        public IHttpActionResult PostPayServices(string MerchantCode, string MerchandPassword, string CardNo, string ExpM, string ExpY, string CVV, decimal Price)
        {
            string RequestCode = "";

            int merchantCount = db.POSMerchants.Count(x => x.MerchantCode == MerchantCode && x.Password == MerchandPassword);
            if (merchantCount != 0)
            {
                int cardControl = db.CustomerCards.Count(x => x.CardNumber == CardNo);
                if (cardControl != 0)
                {
                    int id= db.CustomerCards.FirstOrDefault(x => x.CardNumber == CardNo).ID;

                    CustomerCards cc = db.CustomerCards.Find(id);
                    if (cc.CVVNumber == CVV)
                    {
                        if (!(Convert.ToInt32(cc.expYear) > DateTime.Now.Year))
                        {
                            if (!(Convert.ToInt32(cc.expMonth) > DateTime.Now.Month))
                            {
                                if (cc.Balance >= Price)
                                {
                                    try
                                    {
                                        cc.Balance -= Price;
                                        db.SaveChanges();
                                        RequestCode = "101";
                                    }
                                    catch
                                    {
                                        RequestCode = "401";
                                    }
                                    
                                }
                                else
                                {
                                    RequestCode = "501";
                                }
                            }
                            else
                            {
                                RequestCode = "601";
                            }
                        }
                        else
                        {
                            RequestCode = "601";
                        }
                    }
                    else
                    {
                        RequestCode = "701";
                    }
                }
                else
                {
                    RequestCode = "801";
                }
            }
            else
            {
                RequestCode = "901";
            }
            return Json(RequestCode);
        }

        public IHttpActionResult PostPayServices(ProductModel model)
        {
            string RequestCode = "";

            int merchantCount = db.POSMerchants.Count(x => x.MerchantCode == model.MerchantCode && x.Password == model.MerchandPassword);
            if (merchantCount != 0)
            {
                int cardControl = db.CustomerCards.Count(x => x.CardNumber == model.CardNo);
                if (cardControl != 0)
                {
                    int id = db.CustomerCards.FirstOrDefault(x => x.CardNumber == model.CardNo).ID;

                    CustomerCards cc = db.CustomerCards.Find(id);
                    if (cc.CVVNumber == model.CVV)
                    {
                        if (!(Convert.ToInt32(cc.expYear) > DateTime.Now.Year))
                        {
                            if (!(Convert.ToInt32(cc.expMonth) > DateTime.Now.Month))
                            {
                                if (cc.Balance >= model.Price)
                                {
                                    try
                                    {
                                        cc.Balance -= model.Price;
                                        db.SaveChanges();
                                        RequestCode = "101";
                                    }
                                    catch
                                    {
                                        RequestCode = "401";
                                    }

                                }
                                else
                                {
                                    RequestCode = "501";
                                }
                            }
                            else
                            {
                                RequestCode = "601";
                            }
                        }
                        else
                        {
                            RequestCode = "601";
                        }
                    }
                    else
                    {
                        RequestCode = "701";
                    }
                }
                else
                {
                    RequestCode = "801";
                }
            }
            else
            {
                RequestCode = "901";
            }
            return Json(RequestCode);
        }
    }
}
