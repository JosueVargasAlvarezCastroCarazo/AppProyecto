using AppProyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppProyecto.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        public User MyUser { get; set; }

        public UserViewModel()
        {
            MyUser = new User();
        }

        public async Task<User> ValidateLogin(string identification, string password)
        {

            if (IsBusy)
            {
                return new User();
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;
                MyUser.LoginPassword = password;

                User R = await MyUser.ValidateLogin();

                return R;
            }
            catch (Exception)
            {
                return new User();
                throw;
            }
            finally
            {
                IsBusy = false;
            }

            
        }

        public async Task<bool> CreateAccount(
            string identification, 
            string password,
            string name,
            string phone,
            string address
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;
                MyUser.LoginPassword = password;

                MyUser.Name = name;
                MyUser.PhoneNumber = phone;
                MyUser.Address = address;
                MyUser.Active = true;

                bool R = await MyUser.CreateAccount();

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
