using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTOLayer;

namespace BusinessLayer
{
    public interface IViewBusiness
    {
        object GetViewSutranReportEvent(int startIndex, int count, string sorting);
    }
}
