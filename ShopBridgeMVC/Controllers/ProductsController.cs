using Newtonsoft.Json;
using ShopBridgeMVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ShopBridgeMVC.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult AddProduct()
        {
            return View(new Products());
        }

        [HttpPost]
        public ActionResult SaveProduct()
        {
            try
            {
                ProductSave product = new ProductSave();

                if (Request.Files.Count > 0)
                {

                    HttpPostedFileBase file = Request.Files[0];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    fname = Path.Combine(Server.MapPath("/ProductImages/"), fname);
                    file.SaveAs(fname);
                    product.ProductImage = file.FileName;
                }
                product.ProductName = Request.Form["ProductName"];
                product.ProductPrice = Convert.ToDecimal(Request.Form["ProductPrice"]);
                product.ProductDesc = Request.Form["ProductDesc"];
                HttpResponseMessage response = GlobalVariables.client.PostAsJsonAsync("Products", product).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new JsonResult { Data = "Saved Successfully"};
                }

                return new JsonResult { Data = "Something Went Wrong. Please try again."};
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public ActionResult ProductDetail(string ProductId)
        {
            Products product;
            HttpResponseMessage response = GlobalVariables.client.GetAsync("Products/" + ProductId.ToString()).Result;
            product = response.Content.ReadAsAsync<Products>().Result;
            return View(product);
        }

        public JsonResult DeleteProduct(string ProductId)
        {
            bool result = false;
            HttpResponseMessage response = GlobalVariables.client.DeleteAsync("Products/" + ProductId.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                result = true;
                TempData["SuccessMessage"] = "Deleted Successfully";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetProducts()
        {
            List<Products> Istproduct = new List<Products>();
            HttpResponseMessage response = GlobalVariables.client.GetAsync("Products").Result;
            Istproduct = response.Content.ReadAsAsync<List<Products>>().Result;
            return Json(Istproduct, JsonRequestBehavior.AllowGet);
        }


    }
}