using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EaseFunctions
{
    public static float Easeout(float time)
    {
        return -(time - 1) * (time - 1) + 1;
    }
}
