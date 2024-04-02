using global::MVCClass.Models.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MVCClass.Models.DAO
{
    public class DoctorDAO
    {
        public string CreateDoctor(DoctorDTO doctor)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string createUserQuery = "INSERT INTO DoctorsPJD (DoctorName, DoctorLastName, Specialization, WorkSchedule) VALUES (@DoctorName, @DoctorLastName, @Specialization, @WorkSchedule)";

                    using (var command = new MySqlCommand(createUserQuery, conn))
                    {
                        command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                        command.Parameters.AddWithValue("@DoctorLastName", doctor.DoctorLastName);
                        command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                        command.Parameters.AddWithValue("@WorkSchedule", doctor.WorkSchedule);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Doctor has been created.";
                        }
                        else
                        {
                            return "There was something wrong creating a doctor.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to create a doctor: {ex.Message}");
                return "Error trying to create a doctor.";
            }
        }

        public List<DoctorDTO> ReadDoctors()
        {
            List<DoctorDTO> doctors = new List<DoctorDTO>();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readUsersQuery = "SELECT * FROM DoctorsPJD";

                    using (var command = new MySqlCommand(readUsersQuery, conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DoctorDTO doctor = new DoctorDTO();
                                MapDoctorFromReader(reader, doctor);
                                doctors.Add(doctor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading doctor: {ex.Message}");
            }

            return doctors;
        }

        public DoctorDTO ReadDoctor(int id)
        {
            DoctorDTO doctor = new DoctorDTO();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readUsersQuery = "SELECT * FROM DoctorsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(readUsersQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MapDoctorFromReader(reader, doctor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading doctor by Id: {ex.Message}");
            }
            return doctor;
        }

        public string UpdateDoctor(int id, DoctorDTO doctor)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string updateQuery = "UPDATE DoctorsPJD SET DoctorName = @DoctorName, DoctorLastName = @DoctorLastName, Specialization = @Specialization, WorkSchedule = @WorkSchedule WHERE id = @Id";

                    using (var command = new MySqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                        command.Parameters.AddWithValue("@DoctorLastName", doctor.DoctorLastName);
                        command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                        command.Parameters.AddWithValue("@WorkSchedule", doctor.WorkSchedule);
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The doctor has been updated.";
                        }
                        else
                        {
                            return "Doctor id does not exist";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to update a doctor: {ex.Message}");
                return "Error trying to update a doctor.";
            }
        }

        public string DeleteDoctor(int id)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM DoctorsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(deleteQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The doctor has been deleted.";
                        }
                        else
                        {
                            return "Error trying to delete a doctor.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting doctor: {ex.Message}");
                return "Error trying to delete a doctor.";
            }
        }

        private void MapDoctorFromReader(MySqlDataReader reader, DoctorDTO doctor)
        {
            doctor.Id = reader.GetInt32("id");
            doctor.DoctorName = reader.GetString("DoctorName");
            doctor.DoctorLastName = reader.GetString("DoctorLastName");
            doctor.Specialization = reader.GetString("Specialization");
            doctor.WorkSchedule = reader.GetString("WorkSchedule");
            
        }
    }
}
