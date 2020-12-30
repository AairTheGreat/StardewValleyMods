using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterTrainLoot.Interfaces
{
    public interface IJsonAssetsAPI
    {
        IDictionary<string, int> GetAllCropIds();
        IDictionary<string, int> GetAllFruitTreeIds();
    }
}
