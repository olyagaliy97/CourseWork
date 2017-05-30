using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptimization.TSP
{
    public interface TSPSerializable<C, D> where C : TSPCity<D> where D : TSPDistance
    {
        void Serialize(string fileName);
        TSPGraph<C, D> Deserialize(string fileName);
    }
}
