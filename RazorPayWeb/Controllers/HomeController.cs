using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using Microsoft.AspNetCore.Http;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace RazorPayWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult RazorPay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RazorPay(string paymentAmount)
        {
            try
            {
                RazorPayPayLoad orderRequest = new RazorPayPayLoad { 
                    ApiKey = "rzp_test_PSawHylUJK5KZi",
                    ApiSecret = "Zz8yhAXr1BMLKHfPLSHR7TpH",
                    Name = "Fake name",
                    Email = "fake@fakemail.com",
                    PhoneNumber = "912084422881",
                    Currency = "INR",
                    Amount = paymentAmount,
                    Receipt = Guid.NewGuid().ToString(),
                    PaymentCapture = "1"
                };
                // Create an order using RazorpayIntegration class
                CreditCardRazorPayBL razorPayIntegration = new CreditCardRazorPayBL();
                var order = razorPayIntegration.ProcessRazorPay(orderRequest);
                return Json(order);
            }
            catch (Exception)
            {
                // Log or handle other exceptions here
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult RazorPayReturn(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            CreditCardRazorPayBL razorPayIntegration = new CreditCardRazorPayBL();
            if (razorPayIntegration.CheckPaymentSuccess(razorpay_payment_id, razorpay_order_id, razorpay_signature))
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Failed()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}