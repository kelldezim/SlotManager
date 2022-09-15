using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.Exceptions
{
    public sealed class EmptyLicencePlateException : CustomerException
    {
        public EmptyLicencePlateException() : base("Licence plate is empty")
        {
        }
    }
}
