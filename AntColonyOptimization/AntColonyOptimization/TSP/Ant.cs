using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptimization.ACO
{
   
     // Класс: Ant - представлення сутності "мураха" для ACO.
   
    public class Ant
    {
        
        private List<ACOCity> _TabuList;
        private ACOCity _CurrentCity;      
        private List<ACOCity> _CitiesToVisit;
        private ACOProperties _Props;

        public Ant(ACOProperties props, List<ACOCity> cities)
        {
            _Props = props;
            _CitiesToVisit = new List<ACOCity>(cities);
            _TabuList = new List<ACOCity>();
            SelectFirstCity();
        }

        private void SelectFirstCity()
        {
            Random rand = new Random();
            _CurrentCity = _CitiesToVisit[rand.Next(0,_CitiesToVisit.Count - 1)];
            _CurrentCity.Receive(this, _CitiesToVisit);
        }


        public ACOCity GoNext()
        {
            if (!HasFinished())
            {
                ACOCity cityToGo = MakeDecision();
                _CurrentCity.Release(this, _TabuList);
                _CurrentCity = cityToGo;
                _CurrentCity.Receive(this, _CitiesToVisit);
            }
            
            return _CurrentCity;
        }


        public List<ACOCity> GetPath()
        {
            if (HasFinished() )
            {
                if (!_TabuList.Contains(_CurrentCity))
                {
                    _TabuList.Add(_CurrentCity);
                }
               
                return _TabuList;
            }
            return null;
        }

        public bool HasFinished()
        {
            return _CitiesToVisit.Count == 0;
        }

        protected double CalculateProbability(ACOCity to)
        {
            return CalculateVariables(to) / CalculateVariables();
        }

        private double CalculateVariables(ACOCity to)
        {
            if (_CitiesToVisit.Contains(to))
            {
                ACODistance distance = (ACODistance)_CurrentCity.GetDistanceTo(to);
                double length = distance.LengthDouble;
                double pheromone = distance.Pheromone;
                return Math.Pow(1/length, Convert.ToDouble(_Props.Beta)) * Math.Pow(pheromone, Convert.ToDouble(_Props.Alpha));
            }
            return 0;
        }


        private double CalculateVariables()
        {
            double result = 0;
            foreach (ACOCity to in _CitiesToVisit)
            {
                result += CalculateVariables(to);
            }
            return result;
        }


        private Dictionary<ACOCity, double> GetProbabilityTable()
        {
            Dictionary<ACOCity, double> table = new Dictionary<ACOCity, double>();
            foreach (ACOCity city in _CitiesToVisit)
            {
                table[city] = CalculateProbability(city);
            }
            return table;
        }

        private ACOCity MakeDecision()
        {
            ACOCity cityToGo = null;
            Random random = new Random();
            double randomValue = random.NextDouble();
            double currentProb = 0;
            foreach (KeyValuePair<ACOCity, double> entry in GetProbabilityTable())
            {
                currentProb += entry.Value;
                if (randomValue <= currentProb)
                {
                    cityToGo = entry.Key;
                    break;
                }
            }
            return cityToGo;
        }

        public void ClearMemory()
        {
            _CitiesToVisit = new List<ACOCity>(_TabuList);
            _CitiesToVisit.Remove(_CurrentCity);
            _TabuList.Clear();
        }
    }
}
