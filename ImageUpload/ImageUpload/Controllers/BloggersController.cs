using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageUpload;

namespace ImageUpload.Controllers
{
    public class BloggersController : Controller
    {
        private BlogPostEntities db = new BlogPostEntities();

        // GET: Bloggers
        public ActionResult Index()
        {
            return View(db.Bloggers.ToList());
        }

        // GET: Bloggers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogger blogger = db.Bloggers.Find(id);
            if (blogger == null)
            {
                return HttpNotFound();
            }
            return View(blogger);
        }

        // GET: Bloggers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bloggers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        var supportedTypes = new[] { "jpg", "png", "gif", "tif" };
        //        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
        //            if (!supportedTypes.Contains(fileExt))
        //            {
        //                ModelState.AddModelError("ImagePath", "File Extension Is InValid - Only Upload JPEG/PNG/GIF/TIFF File");

        //            }
        //            else if (file == null)
        //            {
        //                ModelState.AddModelError("ImagePath", "Please upload file. It will make you blog nice");
        //            }
        //            if (ModelState.IsValid)
        //            {
        //                if (file != null)
        //                {
        //                    string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
        //file.SaveAs(path);

        //                    db.Bloggers.Add(new Blogger
        //                    {
        //                        Title = blogger.Title,
        //                        Category = blogger.Category,
        //                        Post = blogger.Post,
        //                        Date = DateTime.Now,
        //                        PostedBy = blogger.PostedBy,
        //                        ImagePath = "~/Images/" + file.FileName
        //                    });
        //                    db.SaveChanges();
        //                }

        //                return RedirectToAction("Index");
        //            }

        //            return View(blogger);
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blogger blogger, HttpPostedFileBase file)
        {


            try
            {
                var supportedTypes = new[] { "jpg", "png", "gif", "tif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                //if (!supportedTypes.Contains(fileExt))
                //{
                //    ModelState.AddModelError("ImagePath", "File Extension Is InValid - Only Upload JPEG/PNG/GIF/TIFF File");
                //}
                //else if (file==null)
                //{
                //    ModelState.AddModelError("ImagePath", "Please upload file. It will make your blog nice");
                //}
                if (ModelState.IsValid)
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("ImagePath", "File Extension Is InValid - Only Upload JPEG/PNG/GIF/TIFF File");
                    }
                    else if (file.ContentLength > (1 * 1024))
                    {
                        ModelState.AddModelError("ImagePath", "File size is too big");
                    }
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        db.Bloggers.Add(new Blogger
                        {
                            Title = blogger.Title,
                            Category = blogger.Category,
                            Post = blogger.Post,
                            Date = DateTime.Now,
                            PostedBy = blogger.PostedBy,
                            ImagePath = "~/Images/" + file.FileName
                        });
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ImagePath", "Please upload file. It will make your blog nice");
            }

            return View(blogger);
        }

        // GET: Bloggers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogger blogger = db.Bloggers.Find(id);
            if (blogger == null)
            {
                return HttpNotFound();
            }
            return View(blogger);
        }

        //POST: Bloggers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Category,Post,Date,PostedBy,ImagePath")] Blogger blogger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogger);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Blogger blogger, HttpPostedFileBase file)
        //{


        //        //if (ModelState.IsValid)
        //        //{
        //        //    if (!supportedTypes.Contains(fileExt))
        //        //    {
        //        //        ModelState.AddModelError("ImagePath", "File Extension Is InValid - Only Upload JPEG/PNG/GIF/TIFF File");
        //        //    }
        //        //    else if (file.ContentLength > (1 * 1024))
        //        //    {
        //        //        ModelState.AddModelError("ImagePath", "File size is too big");
        //        //    }
        //        //    if (file != null)
        //        //    {
        //        //        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
        //        //        file.SaveAs(path);

        //        //        db.Bloggers.Add(new Blogger
        //        //        {
        //        //            Title = blogger.Title,
        //        //            Category = blogger.Category,
        //        //            Post = blogger.Post,
        //        //            Date = DateTime.Now,
        //        //            PostedBy = blogger.PostedBy,
        //        //            ImagePath = "~/Images/" + file.FileName
        //        //        });
        //        //        db.Entry(blogger).State = EntityState.Modified;
        //        //        db.SaveChanges();
        //        //    }

        //        //    return RedirectToAction("Index");
        //        //}
        //        if (ModelState.IsValid)
        //        {
        //        db.Bloggers.Add(new Blogger
        //        {
        //            Title = blogger.Title,
        //            Category = blogger.Category,
        //            Post = blogger.Post,
        //            Date = DateTime.Now,
        //            PostedBy = blogger.PostedBy,
        //            ImagePath = "~/Images/" + file.FileName
        //        });
        //        db.Entry(blogger).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }


        //    return View(blogger);

        //}


        // GET: Bloggers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogger blogger = db.Bloggers.Find(id);
            if (blogger == null)
            {
                return HttpNotFound();
            }
            return View(blogger);
        }

        // POST: Bloggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blogger blogger = db.Bloggers.Find(id);
            db.Bloggers.Remove(blogger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
