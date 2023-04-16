using Acr.UserDialogs;
using AppProyecto.Models;
using AppProyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProyecto.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecoverPassword : ContentPage
	{
        private string CurrentCode;
        UserViewModel UserViewModel;
        User MyUser;

        public RecoverPassword ()
		{
			InitializeComponent ();
            this.BindingContext = UserViewModel = new UserViewModel();
        }

        //genera un codigo unico para cambio de contrasena
        public void GenerateCode()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(6)
                .ToList().ForEach(e => builder.Append(e));
            CurrentCode = builder.ToString();
        }

        //envia el codigo al correo de restauracion
        private async void BtnSendCode_Clicked(object sender, EventArgs e)
        {

            if (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim()))
            {
                GenerateCode();

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = UserViewModel.SendEmail(TxtEmail.Text.Trim(), CurrentCode);
                    R = await UserViewModel.SaveCode(CurrentCode, TxtEmail.Text.Trim(),DateTime.Now);

                    if (R)
                    {
                        await DisplayAlert("Atención", "El código de recuperación fue enviado al correo", "Aceptar");
                        FirstStack.IsVisible = false;
                        SecondStack.IsVisible = true;
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Problemas al ejecutar el proceso", "Aceptar");
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
               
            }
            else
            {
                await DisplayAlert("Atención", "El espacio esta en blanco", "Aceptar");
            }
            
            
        }

        //verifica que el codigo sea el correcto
        private async void BtnCheckCode_Clicked(object sender, EventArgs e)
        {
            if (TxtCode.Text != null && !string.IsNullOrEmpty(TxtCode.Text.Trim()))
            {

                try
                {
                  
                    if (TxtCode.Text.Trim().Equals(CurrentCode))
                    {
                        SecondStack.IsVisible = false;
                        ThirdStack.IsVisible = true;
                    }
                    else
                    {
                        await DisplayAlert("Atención", "El código no es el correcto", "Aceptar");
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                   
                }

            }
            else
            {
                await DisplayAlert("Atención", "El espacio esta en blanco", "Aceptar");
            }
        }

        //verifica la identificacion del usuario para traerlo
        private async void BtnCheckIdentification_Clicked(object sender, EventArgs e)
        {
            if (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim()))
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    User R = await UserViewModel.CheckIdentification(TxtIdentification.Text.Trim());

                    if (R != null && R.UserId > 0)
                    {

                        if (R.Email.Equals(TxtEmail.Text.Trim()))
                        {
                            MyUser = R;
                            ThirdStack.IsVisible = false;
                            FourStack.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Atención", "El usuario no coincide con el correo de recuperación", "Aceptar");
                        }


                        
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Problemas al ejecutar el proceso", "Aceptar");
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }

            }
            else
            {
                await DisplayAlert("Atención", "El espacio esta en blanco", "Aceptar");
            }
        }

        //cambia la contraseña del usuario
        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            if (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = await UserViewModel.UpdateProfilePassword(
                            MyUser.UserId,
                            MyUser.Identification,
                            TxtPassword.Text.Trim(),
                            MyUser.Name,
                            MyUser.PhoneNumber,
                            MyUser.Address,
                            MyUser.Active,
                            MyUser.IsAdmin,
                            MyUser.Email
                            );

                    if (R)
                    {

                        await DisplayAlert("Atención", "Contraseña actualizada", "Aceptar");
                        await this.Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Problemas al ejecutar el proceso", "Aceptar");
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }

            }
            else
            {
                await DisplayAlert("Atención", "El espacio esta en blanco", "Aceptar");
            }
        }
    }
}