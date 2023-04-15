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


        //crea una reserva
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

        //update current reservation
        public async Task<bool> Update(
           int ReservationId,
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
                MyReservation.ReservationId = ReservationId;
                MyReservation.UserId = UserId;
                MyReservation.ItemId = ItemId;
                MyReservation.StartDate = StartDate;
                MyReservation.EndDate = EndDate;
                MyReservation.Notes = Notes;

                bool R = await MyReservation.Update();

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

        //Elimina una reserva
        public async Task<bool> Delete(
           int ReservationId

           )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyReservation.ReservationId = ReservationId;

                bool R = await MyReservation.Delete();

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


        //obtiene una lista de reservas
        public async Task<List<Reservation>> GetList(
           int UserId, DateTime Start, DateTime End
           )
        {

            if (IsBusy)
            {
                return new List<Reservation>();
            }
            IsBusy = true;

            try
            {


                List<Reservation> R = await MyReservation.GetList(UserId, Start, End);

                return R;
            }
            catch (Exception)
            {
                return new List<Reservation>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }



        //obtiene una lista de reservas
        public async Task<List<Reservation>> SearchByItemId(
           int ItemId
           )
        {

            if (IsBusy)
            {
                return new List<Reservation>();
            }
            IsBusy = true;

            try
            {


                List<Reservation> R = await MyReservation.SearchByItemId(ItemId);

                return R;
            }
            catch (Exception)
            {
                return new List<Reservation>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }





    }
}
