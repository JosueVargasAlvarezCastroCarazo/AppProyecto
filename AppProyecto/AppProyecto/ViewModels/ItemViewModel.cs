using AppProyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppProyecto.ViewModels
{

    public class ItemViewModel : BaseViewModel
    {

        Item MyItem { get; set; }
        public ItemViewModel()
        {
            MyItem = new Item();
        }

        //crea un item nuevo
        public async Task<bool> Create(
            string name,
            string code,
            string description
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyItem.ItemName = name;
                MyItem.Code = code;
                MyItem.ItemDescription = description;
                MyItem.Active = true;

                bool R = await MyItem.Create();

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

        
        //trae un item segun un id
        public async Task<Item> GetItemById(int id)
        {

            if (IsBusy)
            {
                return new Item();
            }
            IsBusy = true;

            try
            {
                Item R = await MyItem.GetItemById(id);

                return R;
            }
            catch (Exception)
            {
                return new Item();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //actualiza un item
        public async Task<bool> Update(
            int id,
            string name,
            string code,
            string description,
            bool active
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyItem.ItemId = id;
                MyItem.ItemName = name;
                MyItem.Code = code;
                MyItem.ItemDescription = description;
                MyItem.Active = active;

                bool R = await MyItem.Update();

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

        //obtiene una lista de items
        public async Task<List<Item>> GetList(
           bool Active,
           string search
           )
        {

            if (IsBusy)
            {
                return new List<Item>();
            }
            IsBusy = true;

            try
            {


                List<Item> R = await MyItem.GetList(Active, search);

                return R;
            }
            catch (Exception)
            {
               return new List<Item>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

    }
}
