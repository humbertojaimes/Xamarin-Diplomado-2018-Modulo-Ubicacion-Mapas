using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Linq;
using UbicacionMapas.Controls;

namespace UbicacionMapas.Views
{
    public partial class StoresMapPage : ContentPage
    {
        public StoresMapPage()
        {
            InitializeComponent();
            InitMap();
        }

        async Task<Position> GetUserPosition()
        {
            try
            {
                GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
                var userPosition = await Geolocation.GetLocationAsync(geolocationRequest);
                return new Position(userPosition.Latitude, userPosition.Longitude);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Error", "El dispositivo no soporta el uso de la ubicación", "Aceptar");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Error", "Es necesario tener permisos de uso de la ubicación", "Aceptar");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error desconocido", "Aceptar");
            }

            return default(Position);
        }

        async Task<Position> GetAddressPosition(string address)
        {
            var location = (await Geocoding.GetLocationsAsync(address))?.First();
            if (location != null)
            {
                return new Position(location.Latitude, location.Longitude);
            }

            return default(Position);
        }

        async Task InitMap()
        {

            var userPosition = await GetUserPosition();

            if (userPosition != default(Position))
            {
                mapStore.Pins.Add(new ColorPin()
                {
                    Position = userPosition,
                    Label = "Mi ubicación",
                    PinColor = Color.Red
                }); 

            }

            mapStore.MoveToRegion(MapSpan.FromCenterAndRadius(userPosition, Distance.FromKilometers(1000)));

            foreach (var store in Classes.StoresCatalog.Stores)
            {
                var storePosition = await GetAddressPosition(store.Address);

                if (storePosition != default(Position))
                {
                    mapStore.Pins.Add(new ColorPin()
                    {
                        Position = storePosition,
                        Label = store.Name,
                        PinColor = Color.Green
                    }); 

                }
            }


        }
    }
}