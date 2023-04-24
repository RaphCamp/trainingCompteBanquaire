using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBancaireTest.Doubles
{
    internal class FournisseurStubDateTime : IFournisseurDateTime
    {
        public DateTime GetNow()
        {
            return new DateTime(2023, 03, 16,16,30,45);
        }
    }
}
