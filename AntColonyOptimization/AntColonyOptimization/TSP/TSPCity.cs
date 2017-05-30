using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptimization.TSP
{
  // Класс: TSPCity - представлення сутності "місто"(вершина) у графі для TSP.
     
    public abstract class TSPCity<D> : INotifyPropertyChanged where D: TSPDistance 
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }


        private string _CityName;
        public string СityName
        {
            get { return _CityName; }
            set
            {
                _CityName = value;
                OnPropertyChanged("CityName");
            }
        }
        private Dictionary<TSPCity<D>, D> linkMap;
        
        public TSPCity(string cityName)
        {
            this.СityName = cityName;
            linkMap = new Dictionary<TSPCity<D>,D>();
        }

        public Dictionary<TSPCity<D>, D> GetLinkMap()
        {
            return this.linkMap;
        }

        public bool Connect(TSPCity<D> city, D distance)
        {
            if (!linkMap.ContainsKey(city))
            {
                linkMap[city] = distance;
                city.Connect(this,distance);
                return true;
            }
            return false;
        }

        public void Disconnect(TSPCity<D> city)
        {
            if (linkMap.ContainsKey(city))
            {
                linkMap.Remove(city);
                city.Disconnect(this);
            }
        }

        public TSPDistance GetDistanceTo(TSPCity<D> city)
        {
            if (linkMap.ContainsKey(city))
            {
                return this.linkMap[city];
            }
            return null; 
        }

        public bool IsConnectedTo(TSPCity<D> city)
        {
            return this.linkMap.ContainsKey(city);
        }
 
        public bool IsConnectedTo(List<TSPCity<D>> cities)
        {
            foreach (TSPCity<D> city in cities)
            {
                if (!IsConnectedTo(city))
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasConnections()
        {
            return this.linkMap.Count > 0;
        }

        public int GetConnectionsCount()
        {
            return this.linkMap.Count;
        }
    }
}
