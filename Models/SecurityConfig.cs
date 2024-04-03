using MySql.Data.MySqlClient;

/******/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*****/

namespace MVCClass.Models
{
    public static class SecurityConfig
    {
        private static string connection = "server=saacapps.com;UserID=saacapps_ucatolica;" +
                "Database=saacapps_training;Password=Ucat0lica";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connection);
        }
    }
}