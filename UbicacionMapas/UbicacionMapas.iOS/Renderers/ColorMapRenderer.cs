using System;
using System.Linq;
using CoreLocation;
using MapKit;
using UbicacionMapas.Controls;
using UbicacionMapas.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(ColorMap), typeof(ColorMapRenderer))] 
namespace UbicacionMapas.iOS.Renderers
{
    //Esta clase de apoyo sirve para convertir nuestro Pin de Xamarin Forms a una native con soporte a un color
    public class ColorPointAnnotation : MKPointAnnotation
    {
        public UIColor Color
        {
            get;
            private set;
        }

        public ColorPointAnnotation(UIColor color)
        {
            Color = color;
        }
    }

    public class ColorMapRenderer : MapRenderer
    {
        public ColorMap ColorMap => Element as ColorMap;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (ColorMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                //La siguiente línea indica el método encargado de crear los marcadores 
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
            }
        }

        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            var colorPin = ((ColorMap)Element).Pins.Where(x => x.Position == position).First() as ColorPin;
            //Las siguientes líneas son las encargadas de reemplazar el Pin actual por uno de nuestra clase nativa con soporte para el color
            var colorAnnotation = new ColorPointAnnotation(colorPin.PinColor.ToUIColor())
            {
                Title = colorPin.Label,
                Subtitle = colorPin.Address,
                Coordinate = new CLLocationCoordinate2D(colorPin.Position.Latitude, colorPin.Position.Longitude)
            };

            MKPinAnnotationView view = null;
            if (colorAnnotation != null)
            {
                var identifier = "colorAnnotation";
                view = mapView.DequeueReusableAnnotation(identifier) as MKPinAnnotationView;
                if (view == null)
                {
                    view = new MKPinAnnotationView(colorAnnotation, identifier);
                }

                view.Annotation = colorAnnotation;
                view.CanShowCallout = true;
                view.PinTintColor = colorAnnotation.Color;

            }
            return view;
        }
    }

}
