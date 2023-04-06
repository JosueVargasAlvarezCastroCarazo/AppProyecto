using AppProyecto.Models;
using AutoAppo_JosueVa.Models;
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

        //verifica el login ademas de retornar un usuario
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

        //verifica que la identificacion no exista en la base de datos y devuelve un usuario si existiera
        public async Task<User> CheckIdentification(string identification)
        {

            if (IsBusy)
            {
                return new User();
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;

                User R = await MyUser.CheckIdentification();

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

        //devuelve un usuario segun un id
        public async Task<User> GetUserById(int id)
        {

            if (IsBusy)
            {
                return new User();
            }
            IsBusy = true;

            try
            {
                User R = await MyUser.GetUserById(id);

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

        //crea una una nueva cuenta
        public async Task<bool> CreateAccount(
            string identification, 
            string password,
            string name,
            string phone,
            string address,
            string email
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
                MyUser.Email = email;

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

        //actualiza el perfil de el usuario
        public async Task<bool> UpdateProfile(
            int id,
            string identification,
            string password,
            string name,
            string phone,
            string address,
            bool? active,
            bool? isAdmin,
            string email
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.UserId = id;
                MyUser.Identification = identification;
                MyUser.LoginPassword = password;
                MyUser.Name = name;
                MyUser.PhoneNumber = phone;
                MyUser.Address = address;
                MyUser.Active = active;
                MyUser.IsAdmin = isAdmin;
                MyUser.Email = email;

                bool R = await MyUser.UpdateProfile();

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

        //actualiza el perfil del usaurio ademas de la contraseña
        public async Task<bool> UpdateProfilePassword(
            int id,
            string identification,
            string password,
            string name,
            string phone,
            string address,
            bool? active,
            bool? isAdmin,
            string email
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.UserId = id;
                MyUser.Identification = identification;
                MyUser.LoginPassword = password;
                MyUser.Name = name;
                MyUser.PhoneNumber = phone;
                MyUser.Address = address;
                MyUser.Active = active;
                MyUser.IsAdmin = isAdmin;
                MyUser.Email = email;

                bool R = await MyUser.UpdateProfilePassword();

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

        //envia un correo electronico para conseguir un codigo de contraseña
        public bool SendEmail(
            string email,
           string code)
        {

            try
            {
                var emailInstance = new Email();
                emailInstance.SendTo = email;
                emailInstance.Subeject = "Gabelo Conejo Código de recuperación";
                emailInstance.Message = "Su código es: " + code;
                bool R = emailInstance.SendEmail();

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


        //retorna una lista de usuarios
        public async Task<List<User>> GetList(
         bool Active,
         string search
         )
        {

            if (IsBusy)
            {
                return new List<User>();
            }
            IsBusy = true;

            try
            {


                List<User> R = await MyUser.GetList(Active, search);

                return R;
            }
            catch (Exception)
            {
                return new List<User>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


    }
}
