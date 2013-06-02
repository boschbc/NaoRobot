using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI
{
    interface IRealtimeField
    {
        void UpdateContent();

        void ResetContent();
    }
}
