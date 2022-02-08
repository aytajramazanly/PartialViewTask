using FrontToBack.DataAccessLayer;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _productsCount;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _productsCount = _dbContext.Products.Count();
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = _productsCount;
            return View(new HomeVM
            {
                Slider = _dbContext.Sliders.SingleOrDefault(),
                SliderImages = _dbContext.SliderImages.ToList(),
                Categories = _dbContext.Categories.ToList(),
                Products = _dbContext.Products.Include(x => x.Category).Take(4).ToList(),
                About = _dbContext.Abouts.SingleOrDefault(),
                Advantages = _dbContext.Advantages.ToList(),
                ExpertsHeading = _dbContext.ExpertsHeadings.SingleOrDefault(),
                Experts = _dbContext.Experts.ToList(),
                Subscribe = _dbContext.Subscribes.SingleOrDefault(),
                BlogHeading = _dbContext.BlogHeadings.SingleOrDefault(),
                BlogPosts = _dbContext.BlogPosts.ToList(),
                ExpertsComments = _dbContext.ExpertsComments.Include(x => x.Expert).ToList(),
                InstagramPosts = _dbContext.InstagramPosts.ToList(),
            });
        }

        public IActionResult Load(int skip)
        {
            if (skip >= _productsCount)
            {
                return BadRequest();
            }

            var products = _dbContext.Products.Include(x => x.Category).Skip(skip).Take(4).ToList();

            return PartialView("_ProductPartial", products);
        }
    }
}
