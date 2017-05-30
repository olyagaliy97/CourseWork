using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptimization.TSP
{
    public interface TSPAlgorithm<C,D> where C : TSPCity<D> where D:TSPDistance
    {
        List<C> GetOptimalPath(TSPGraph<C,D> graph);
    }
}
