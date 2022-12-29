using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibreriaSumativa3;

namespace LibreriaSumativa3.Controllers
{
    public class LibrosController : Controller
    {
        private LibreriaEntities db = new LibreriaEntities();

        // GET: Libros
        public ActionResult Index()
        {
            return View(db.Libro.ToList());
        }

        // GET: Libros/Details/5
        public ActionResult Detalles(int? id)
        {
            if (false) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Libro libro = db.Libro.Find(id);
                if (libro == null)
                {
                    return HttpNotFound();
                }
                return View(libro);
            }

            if (true) {
                Libro libro=new Libro();
                string cnstr ="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Libreria;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection conn = new SqlConnection(cnstr);
                DataSet ds =new DataSet();
                using (conn) {
                    conn.Open();

                    string query = "select * from Libreria.dbo.Libro where Id="+id.ToString();
                    SqlDataAdapter da =new SqlDataAdapter(query, conn);
                    da.Fill(ds);

                    conn.Close();
                    conn.Dispose();
                }
                if (ds.Tables.Count > 0) {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[0];
                    libro= new Libro();
                    libro.Id = int.Parse(dr[0].ToString());
                    libro.Nombre = dr[1].ToString();
                    libro.Autor = dr[2].ToString();
                    libro.Editorial = dr[3].ToString();
                    libro.FechaPublicado = DateTime.Parse(dr[4].ToString());
                    return View(libro);
                }
                else {
                    return HttpNotFound();
                }
            }
        }

        // GET: Libros/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Libros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Id,Nombre,Autor,Editorial,FechaPublicado")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        // GET: Libros/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nombre,Autor,Editorial,FechaPublicado")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(libro);
        }

        // GET: Libros/Delete/5
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libro.Find(id);
            db.Libro.Remove(libro);
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
