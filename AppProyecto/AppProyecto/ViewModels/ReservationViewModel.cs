using AppProyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppProyecto.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {



        Reservation MyReservation { get; set; }
        public ReservationViewModel()
        {
            MyReservation = new Reservation();
        }



        public async Task<bool> Create(
            int UserId,
            int ItemId,
            DateTime StartDate,
            DateTime EndDate,
            string Notes

            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyReservation.UserId = UserId;
                MyReservation.ItemId = ItemId;
                MyReservation.StartDate = StartDate;
                MyReservation.EndDate = EndDate;
                MyReservation.Notes = Notes;

                bool R = await MyReservation.Create();

                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

    }
}
