using System;
using System.Collections.Generic;
using System.Text;

namespace AppProyecto.Models
{
    public class Reservation
    {


        public Reservation()
        {
            
        }

        public int ReservationId { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

    }
}
