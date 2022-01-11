#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BlogPostsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogPost> blogposts = _db.BlogPosts;
            return View(blogposts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost bp)
        {
            Debug.WriteLine(bp.ToString());
            if (ModelState.IsValid)
            {
                _db.Add(bp);
                _db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View();
        }

        public IActionResult Read(int? id)
		{
            if (id == null || id == 0)
			{
                return NotFound();
			}
            var bp = _db.BlogPosts.Find(id);
            return View(bp);
		}

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0 )
            {
                return NotFound();
            }
            var blogposts = _db.BlogPosts.Find(id);
            if (blogposts == null)
            {
                return NotFound();
            }
            return View(blogposts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogPost bp)
        {
            if (ModelState.IsValid)
            {
                _db.BlogPosts.Update(bp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bp);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blogposts = _db.BlogPosts.Find(id);
            if (blogposts == null)
            {
                return NotFound();
            }
            return View(blogposts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var blogpost = _db.BlogPosts.Find(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            _db.Remove(blogpost);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
