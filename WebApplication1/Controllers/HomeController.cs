using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var home = new TreeNode("root", "category");
            var data = Data.DataList();

            PopulateCategories(data, home);
            var test = home;
            return View();

        }

        private static void PopulateCategories(List<Data> data, TreeNode home)
        {
            var categories = (from category in data
                select category.Category).Distinct();
            foreach (var category in categories)
            {
                var categoryNode = PopulateTypes(data, category);

                home.ChildNodes.Add(categoryNode);
            }
        }

        private static TreeNode PopulateTypes(List<Data> data, string category)
        {
            var typeList = (from type in data
                where type.Category == category
                select type.Product);
            var categoryNode = new TreeNode(category);
            foreach (var type in typeList)
            {
                var productList = (from product in data
                    where product.Type == type
                    select product.Product);
                var typeNode = new TreeNode(type);
                PopulateProducts(productList, typeNode);
                categoryNode.ChildNodes.Add(typeNode);
            }
            return categoryNode;
        }

        private static void PopulateProducts(IEnumerable<string> productList, TreeNode typeNode)
        {
            foreach (var product in productList)
            {
                var productNode = new TreeNode(product);
                typeNode.ChildNodes.Add(productNode);
            }
        }


        public class Data
        {
            public int Id { get; set; }
            public string Product { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }

            public static List<Data> DataList()
            {
                return new List<Data>()
                {
                    new Data(){Category = "Pizza",Id =1, Product = "Margerita", Type = "Vegetarian"},
                    new Data(){Category = "Pizza",Id =1, Product = "Peperoni", Type = "Non Vegetarian"},
                    new Data(){Category = "Burger",Id =1, Product = "Veg Burger", Type = "Vegetarian"},
                    new Data(){Category = "Burger",Id =1, Product = "Beef Burger", Type = "Non Vegetarian"},
                };
            }
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
    }
}