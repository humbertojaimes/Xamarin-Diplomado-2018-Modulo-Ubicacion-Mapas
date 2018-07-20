using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UbicacionMapas.Views
{
    public partial class StoresPage : ContentPage
    {
        public StoresPage()
        {
            InitializeComponent();
            lvStores.ItemsSource = Classes.StoresCatalog.Stores;
        }

        public async void tbMap_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.StoresMapPage());
        } 

    }
}
