using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterTrashCans.Framework
{
    public interface IWeighted
    {
        double GetWeight();
        bool GetEnabled();
    }
}
