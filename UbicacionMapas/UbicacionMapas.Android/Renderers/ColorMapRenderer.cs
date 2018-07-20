using System;
using Android.Content;
using Android.Gms.Maps.Model;
using UbicacionMapas.Controls;
using UbicacionMapas.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ColorMap), typeof(ColorMapRenderer))] 

namespace UbicacionMapas.Droid.Renderers
{
    public class ColorMapRenderer : MapRenderer
    {

        public ColorMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null) 
                Control.GetMapAsync(this);
        }

        //Este método se invoca cada que un marcador se agrega al mapa
        //Nosotros interceptamos la acción y modificamos el color
        //Las demás propiedades del Pin se respetan
        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var colorPin = pin as ColorPin;
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(GetMarkerIcon(ColorExtensions.ToAndroid(colorPin.PinColor)));
            return marker;
        }

        public BitmapDescriptor GetMarkerIcon(global::Android.Graphics.Color color)
        {
            float[] hsv = new float[3];
            global::Android.Graphics.Color.ColorToHSV(color, hsv);
            return BitmapDescriptorFactory.DefaultMarker(hsv[0]);
        }
    } 

}
