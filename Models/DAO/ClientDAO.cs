using global::MVCClass.Models.DTO;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;


namespace MVCClass.Models.DAO
{
    public class ClientDAO
    {
  
        public string CreateClient(ClientDTO client)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                 
                    string createClientQuery = "INSERT INTO ClientsPJD (ClientName, ClientLastName, Address, PhoneNumber,Email) VALUES (@ClientName, @ClientLastName, @Address, @PhoneNumber, @Email)";
                    using (var command = new MySqlCommand(createClientQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientName", client.ClientName);
                        command.Parameters.AddWithValue("@ClientLastName", client.ClientLastName);
                        command.Parameters.AddWithValue("@Address", client.Address);
                        command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", client.Email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Client has been created.";
                        }
                        else
                        {
                            return "There was something wrong creating a client.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to create a client: {ex.Message}");
                return "Error trying to create a client.";
            }
        }

        public List<ClientDTO> ReadClients()
        {
            List<ClientDTO> clients = new List<ClientDTO>();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readUsersQuery = "SELECT * FROM ClientsPJD";

                    using (var command = new MySqlCommand(readUsersQuery, conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientDTO client = new ClientDTO();
                                MapClientFromReader(reader, client);
                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading clients: {ex.Message}");
            }

            return clients;
        }

        public ClientDTO ReadClient(int id)
        {
            ClientDTO client = new ClientDTO();

            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();
                    string readUsersQuery = "SELECT * FROM ClientsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(readUsersQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MapClientFromReader(reader, client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading client by Id: {ex.Message}");
            }
            return client;
        }

        public string UpdateClient(int id, ClientDTO client)
        {
            try
            {
                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string updateQuery = "UPDATE ClientsPJD SET ClientName = @ClientName, ClientLastName = @ClientLastName, Address = @Address, PhoneNumber = @PhoneNumber, Email = @Email WHERE id = @Id";

                    using (var command = new MySqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientName", client.ClientName);
                        command.Parameters.AddWithValue("@ClientLastName", client.ClientLastName);
                        command.Parameters.AddWithValue("@Address", client.Address);
                        command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", client.Email);
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The client has been updated.";
                        }
                        else
                        {
                            return "Client id does not exist";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to update a client: {ex.Message}");
                return "Error trying to update a client.";
            }
        }

        public string DeleteClient(int id)
        {
            try
            {
               

                using (MySqlConnection conn = SecurityConfig.GetConnection())
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM ClientsPJD WHERE id = @Id";

                    using (var command = new MySqlCommand(deleteQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "The client has been deleted.";
                        }
                        else
                        {
                            return "No client found with the specified ID.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting client: {ex.Message}");
                return "Error trying to delete a client.";
            }
        }

     


        private void MapClientFromReader(MySqlDataReader reader, ClientDTO client)
        {
            client.Id = reader.GetInt32("Id");
            client.ClientName = reader.GetString("ClientName");
            client.ClientLastName = reader.GetString("ClientLastName");
            client.Address = reader.GetString("Address");
            client.PhoneNumber = reader.GetString("PhoneNumber");
            client.Email = reader.GetString("Email");
        }
    }
}
