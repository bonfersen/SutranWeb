using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTOLayer;

namespace BusinessLayer
{
    public interface IViewBusiness
    {
        Dictionary<string, object> GetViewSutranReportEvent(string txtFechaEventoInicial, string txtFechaEventoFinal, string txtVin, int jtStartIndex, int jtPageSize, string jtSorting);
    }
}
