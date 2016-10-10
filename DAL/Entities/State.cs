using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Flags]
    public enum State
    {
        Aprobat = 1,
        PrimaAprobare = 2,
        AsteaptaAprobare = 4,
        MarcataFiindGresita = 8,
        Adaugataautomat = 16,

    }
}
