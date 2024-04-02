
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace MVCClass.Models.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Reason { get; set; }
    }
}


//INSERT INTO AppointmentsPJD (Id, ClientId, DoctorId, Date, StartTime, EndTime, Reason)
//VALUES(6, 6, 6, '2024-04-01', '10:00:00', '11:00:00', 'Cardiac checkup');