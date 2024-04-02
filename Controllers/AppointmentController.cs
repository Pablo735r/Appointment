
using MVCClass.Models.DAO;
using MVCClass.Models.DTO;
using System;
using System.Web.Mvc;

namespace MVCClass.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        private AppointmentDAO dao = new AppointmentDAO();
        private ClientDAO clientDAO = new ClientDAO();
        private DoctorDAO doctorDAO = new DoctorDAO();
        public ActionResult Index()
        {
            return View(dao.ReadAppointments());
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            return View(dao.ReadAppointment(id));
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            var clientsList = clientDAO.ReadClients(); // Fetch the list of clients using the ClientDAO

            ViewBag.ClientsList = new SelectList(clientsList, "Id", "ClientName"); // Assuming Id and ClientName are properties of your Client entity


            var doctorsList = doctorDAO.ReadDoctors();

            ViewBag.DoctorsList = new SelectList(doctorsList, "Id", "DoctorName");


            //////////////////////

            var clientsList2 = clientDAO.ReadClients(); // Fetch the list of clients using the ClientDAO

            ViewBag.ClientsList2 = new SelectList(clientsList2, "ClientName", "ClientName"); // Assuming Id and ClientName are properties of your Client entity


            var doctorsList2 = doctorDAO.ReadDoctors();

            ViewBag.DoctorsList2 = new SelectList(doctorsList2, "DoctorName", "DoctorName");






            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(AppointmentDTO appointment)
        {
            try
            {
                // TODO: Add insert logic here

                // Check for overlapping appointments|| appointment.DoctorId != appointment.ClientId
                //bool isOverlapping = dao.CheckForOverlappingAppointments(appointment.DoctorId, appointment.StartTime, appointment.EndTime);
                //if (appointment.StartTime >= appointment.EndTime)
                //{
                    //ModelState.AddModelError("", "Selected time slot is not available. Please choose another time.");
                    //return View("ErrorTime");
                //}
                //else
                //{
                   
                    dao.CreateAppointment(appointment);

                    return RedirectToAction("Index");
                //}

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dao.ReadAppointment(id));
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AppointmentDTO appointment)
        {
            try
            {
                // TODO: Add update logic here
                dao.UpdateAppointment(id, appointment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dao.ReadAppointment(id));
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                dao.DeleteAppointment(id);
                /*if (HasAssociatedAppointments(id, "ClientId"))
                {

                    return View("ErrorAppointment");

                }*/

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*private bool HasAssociatedAppointments(int id, string columnName)
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
        }*/

    }
}
