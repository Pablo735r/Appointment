using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClass.Models.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        public string DoctorName { get; set; }

        public string DoctorLastName { get; set; }

        public string Specialization { get; set; }

        public string WorkSchedule { get; set; }

    }
}