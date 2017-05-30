using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptimization.TSP
{
    
      //Класс: TSPDistance - відповідальний за дистанцію між "містами" у графі для TSP.
     
    public abstract class TSPDistance : INotifyPropertyChanged
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

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        
        private decimal _Length;
        public double LengthDouble
        {
            get { return Convert.ToDouble(Length); }
        }

        public decimal Length
        {
            get { return _Length; }
            set
            {
                if (value > 0)
                {
                    _Length = value;
                }
                else
                {
                    _Length = Convert.ToDecimal(1);
                }
                OnPropertyChanged("Length");
            }
        }
    
        public TSPDistance(double length)
        {
            _IsSelected = false;
            this.Length = Convert.ToDecimal(length);
        }

        public static double CalculateLengthOfPath<C,D>(List<C> path) where C: TSPCity<D> where D:TSPDistance
        {
            double length = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                length += path[i].GetDistanceTo(path[i + 1]).LengthDouble;
            }
            return length;
        }
    }
}
