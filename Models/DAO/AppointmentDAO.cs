using MVCClass.Models.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace MVCClass.Models.DAO
{
    public class AppointmentDAO
    {

        public string CreateAppointment(AppointmentDTO appointment)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string createAppointmentQuery = "INSERT INTO AppointmentsPJD (ClientId, ClientName, DoctorId, DoctorName, Date, StartTime, EndTime, Reason) VALUES (@ClientId, @ClientName, @DoctorId, @DoctorName, @Date, @StartTime, @EndTime, @Reason)";

                    using (var command = new MySqlCommand(createAppointmentQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientId", appointment.ClientId);
                        command.Parameters.AddWithValue("@ClientName", appointment.ClientName);
                        command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                        command.Parameters.AddWithValue("@DoctorName", appointment.DoctorName);
                        command.Parameters.AddWithValue("@Date", appointment.Date);
                        command.Parameters.AddWithValue("@StartTime", appointment.StartTime);
                        command.Parameters.AddWithValue("@EndTime", appointment.EndTime);
                        command.Parameters.AddWithValue("@Reason", appointment.Reason);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Appointment has been created.";
                        }
                        else
                        {
                            return "There was something wrong creating the appointment.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error using a logging library or write to a log file
                Console.WriteLine($"Error trying to create an appointment: {ex.Message}");
                return "Error trying to create an appointment.";
            }
        }


        public List<AppointmentDTO> ReadAppointments()
        {
            List<AppointmentDTO> appointments = new List<AppointmentDTO>();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readAppointmentsQuery = "SELECT * FROM AppointmentsPJD";
                    using (var command = new MySqlCommand(readAppointmentsQuery, conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AppointmentDTO appointment = new AppointmentDTO();
                                MapAppointmentFromReader(reader, appointment);
                                appointments.Add(appointment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading appointments: {ex.Message}");
            }

            return appointments;
        }

        public AppointmentDTO ReadAppointment(int id)
        {
            AppointmentDTO appointment = new AppointmentDTO();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readAppointmentsQuery = "SELECT * FROM AppointmentsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(readAppointmentsQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MapAppointmentFromReader(reader, appointment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading appointments by Id: {ex.Message}");
            }
            return appointment;
        }

        public string UpdateAppointment(int id, AppointmentDTO appointment)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string updateQuery = "UPDATE AppointmentsPJD SET ClientId, = @ClientId, ClientName = @ClientName, DoctorId = @DoctorId, DoctorName = @DoctorName,Date = @Date, StartTime = @StartTime, EndTime = @EndTime, Reason = @Reason";

                    using (var command = new MySqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientId", appointment.ClientId);
                        command.Parameters.AddWithValue("@ClientName", appointment.ClientName);
                        command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                        command.Parameters.AddWithValue("@DoctorName", appointment.DoctorName);
                        command.Parameters.AddWithValue("@Date", appointment.Date);
                        command.Parameters.AddWithValue("@StartTime", appointment.StartTime);
                        command.Parameters.AddWithValue("@EndTime", appointment.EndTime);
                        command.Parameters.AddWithValue("@Reason", appointment.Reason);
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The appointment has been updated.";
                        }
                        else
                        {
                            return "Appointment id does not exist";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to update a appointment: {ex.Message}");
                return "Error trying to update a appointment.";
            }
        }

        public string DeleteAppointment(int id)
        {
            try
            {

                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM AppointmentsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(deleteQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The appointment has been deleted.";
                        }
                        else
                        {
                            return "No appointment found with the specified ID.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return "Error trying to delete a appointment.";
            }
        }




        private void MapAppointmentFromReader(MySqlDataReader reader, AppointmentDTO appointment)
        {
            appointment.Id = reader.GetInt32("Id");
            appointment.ClientId = reader.GetInt32("ClientId");
            appointment.ClientName = reader.GetString("ClientName");
            appointment.DoctorId = reader.GetInt32("DoctorId");
            appointment.DoctorName = reader.GetString("DoctorName");
            appointment.Date = reader.GetDateTime("Date");
            appointment.StartTime = reader.GetTimeSpan("StartTime");
            appointment.EndTime = reader.GetTimeSpan("EndTime");
            appointment.Reason = reader.GetString("Reason");
        }

    
    }
}
