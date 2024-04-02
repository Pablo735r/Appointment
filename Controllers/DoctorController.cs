using MVCClass.Models;
using MVCClass.Models.DAO;
using MVCClass.Models.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Web.Mvc;

namespace MVCClass.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        private DoctorDAO dao = new DoctorDAO();
        public ActionResult Index()
        {
            return View(dao.ReadDoctors());
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ReadDoctor(id));
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(DoctorDTO doctor)
        {
            try
            {
                // TODO: Add insert logic here
                dao.CreateDoctor(doctor);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ReadDoctor(id));
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DoctorDTO doctor)
        {
            try
            {
                // TODO: Add update logic here
                dao.UpdateDoctor(id, doctor);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ReadDoctor(id));
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                if (HasAssociatedAppointments(id, "ClientId"))
                {

                    return View("DoctorError");

                }

                dao.DeleteDoctor(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private bool HasAssociatedAppointments(int id, string columnName)
        {
            using (MySqlConnection conn = SecurityConfig.GetConnection())
            {
                conn.Open();

                string checkQuery = $"SELECT COUNT(*) FROM AppointmentsPJD WHERE {columnName} = @Id";

                using (var command = new MySqlCommand(checkQuery, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }
}
