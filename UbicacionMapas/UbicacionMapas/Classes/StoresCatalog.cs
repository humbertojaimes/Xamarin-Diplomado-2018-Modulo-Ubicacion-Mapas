using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UbicacionMapas.Classes
{
    public class StoresCatalog 
    {
        private static ReadOnlyCollection<Store> stores;

        public static ReadOnlyCollection<Store> Stores
        { 
        
            get
            {
                if(stores == null)
                {
                    stores = new ReadOnlyCollection<Store>(
                        new List<Store>
                    {
                        new Store("Tienda Querétaro","Av. del Sol, 1,El Sol, 76113 Santiago de Querétaro, Qro., México","https://placeimg.com/640/800/any"),
                        new Store("Tienda Sonora","Av. Nainari 112, Norte, Urb. No. 4, 85040 Cd Obregón, Sonora","https://placeimg.com/640/800/architecture"),
                        new Store("Tienda CDMX","Av. Prolongación Paseo De La Reforma No.400, Alvaro Obregon, Distrito Federal, CP 01210, México","https://placeimg.com/640/800/nature"),
                        new Store("Tienda Jalisco","Av Lapizlázuli 3390, Victoria, 44560 Zapopan, Jalisco","https://placeimg.com/640/800/tech"),
                    } 
                    );
                }

                return stores;
            }
        }
    } 

}
