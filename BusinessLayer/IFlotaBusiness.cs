using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesLayer;
using DTOLayer;

namespace BusinessLayer
{
    public interface IFlotaBusiness
    {
        List<FlotaDTO> getFlotasDTO(string strSortColumn, string strSortDirection);
        void saveFlotas(FlotaDTO flotaDTO);
        FlotaDTO GetByID(int id);
        void UpdateEntity(FlotaDTO flotaDTO);
    }
}
