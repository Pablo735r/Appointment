
using MVCClass.Models;
using MVCClass.Models.DAO;
using MVCClass.Models.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Web.Mvc;

namespace MVCClass.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        private ClientDAO dao = new ClientDAO();
        
        public ActionResult Index()
        {
            return View(dao.ReadClients());
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ReadClient(id));
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(ClientDTO client)
        {
            try
            {
                // TODO: Add insert logic here
                dao.CreateClient(client);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ReadClient(id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClientDTO client)
        {
            try
            {
                // TODO: Add update logic here
                dao.UpdateClient(id, client);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ReadClient(id));
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                if (HasAssociatedAppointments(id, "ClientId"))
                {

                    return View("Error");

                }

                dao.DeleteClient(id);

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
