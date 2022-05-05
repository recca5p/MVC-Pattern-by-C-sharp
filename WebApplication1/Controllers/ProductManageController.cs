using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductManageController : Controller
    {
        MobileShopEntities db = new MobileShopEntities();
        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            List<ModelProduct> model_product = new List<ModelProduct>();
            ///Convert Product list to model
            foreach (Product item in products)
            {
                model_product.Add(new ModelProduct()
                {
                    productID = item.productID,
                    productName = item.productName,
                    productCategory = item.productCategory,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Details = item.Details,
                    categoryName = db.Categories.Where(x => x.productCategory == item.productCategory).First().categoryName,
                });

            }
            return View(model_product);
        }
        public ActionResult Create ()
        {
            return View();
        }
        public ActionResult Update_product(int id)
        {
            Product product = db.Products.Where(x => x.productID == id).First();
            ModelProduct modelProduct = new ModelProduct()
            {
                productID = product.productID,
                productName = product.productName,
                productCategory = product.productCategory,
                Details = product.Details,
                Quantity = product.Quantity,
                Price = product.Price,
            };

            return View(modelProduct);
        }

        public ActionResult Add_product(ModelProduct model)
        {
            if (model.productID == 0)
            {
                Product product = new Product()
                {
                    productName = model.productName,
                    productID = model.productID,
                    productCategory = model.productCategory,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    Details = model.Details,
                };
                db.Products.Add(product);
            }
            else
            {
                var product = db.Products.Where(x => x.productID == model.productID).FirstOrDefault();
                product.productID = model.productID;
                product.productName = model.productName;
                product.productCategory = model.productCategory;
                product.Details = model.Details;
                product.Quantity = model.Quantity;
                product.Price = model.Price;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "ProductManage");
        }

        public ActionResult Delete_product(int id)
        {
            var product = db.Products.Where(x => x.productID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index", "ProductManage");
        }

        public ActionResult Cart(int id)
        {
            List<ModelCart> carts = new List<ModelCart>();
            if (Session["cart"] != null)
            {
                carts = (List<ModelCart>)Session["cart"];
            }

            ModelCart model_cart = new ModelCart();
            //lay gia san pham
            if(carts.Where(x=>x.productID == id).Count() > 0) //San pham da ton tai
            { 
                foreach(ModelCart cart in carts)
                {
                    if(cart.productID == id)
                    {
                        cart.quantity = cart.quantity + 1;
                        break;
                    }
                }
            }
            else
            {
                Product product = db.Products.Where(x => x.productID == id).FirstOrDefault();
                model_cart.productID = id;
                model_cart.quantity = 1;
                model_cart.price = product.Price.Value;

                carts.Add(model_cart);
            }
            Session["cart"] = carts;

            return View(carts);
        }

        public ActionResult CancelCart()
        {
            Session["cart"] = null;

            return RedirectToAction("Index", "ProductManage");
        }

        public ActionResult Order(List<ModelCart> model)
        {
            List<Product> products = db.Products.ToList();
            List<ModelProduct> model_product = new List<ModelProduct>();
            ///Convert Product list to model
            foreach (Product item in products)
            {
                model_product.Add(new ModelProduct()
                {
                    productID = item.productID,
                    productName = item.productName,
                    productCategory = item.productCategory,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Details = item.Details,
                    categoryName = db.Categories.Where(x => x.productCategory == item.productCategory).First().categoryName,
                });

            }
            return View(model_product);
        }
    }



}