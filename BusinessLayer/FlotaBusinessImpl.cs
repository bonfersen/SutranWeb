using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelLayer;
using EntitiesLayer;
using DTOLayer;

namespace BusinessLayer
{
    public class FlotaBusinessImpl : IFlotaBusiness
    {
        public FlotaDTO GetByID(int id)
        {
            //Obtener flota por id
            GenTbFlotaDAOImpl flotaDAO = new GenTbFlotaDAOImpl();
            Gen_tb_Flota genTbFlota = flotaDAO.GetByID(id);

            //Mapear a DTO
            AutoMapper.Mapper.CreateMap<Gen_tb_Flota, FlotaDTO>();
            FlotaDTO flotaDTO = AutoMapper.Mapper.Map<FlotaDTO>(genTbFlota);

            return flotaDTO;
        }

        public List<FlotaDTO> getFlotasDTO(string strSortColumn, string strSortDirection)
        {
            //Obtener lista de flotas desde BD
            GenTbFlotaDAOImpl flotaDAO = new GenTbFlotaDAOImpl();
            //List<Gen_tb_Flota> lstGenTbFlota = flotaDAO.Get(orderBy: flotaIQ => flotaIQ.OrderBy(flota => flota.nombreFlota));

            Boolean sortDirection = (strSortDirection == "ASC") ? false : true;

            List<Gen_tb_Flota> lstGenTbFlota = flotaDAO.OrderByDynamic(strSortColumn, sortDirection);

            //Mapear desde Entidad a Vista
            AutoMapper.Mapper.CreateMap<Gen_tb_Flota, FlotaDTO>();
            List<FlotaDTO> lstFlotaDTO = AutoMapper.Mapper.Map<List<Gen_tb_Flota>, List<FlotaDTO>>(lstGenTbFlota);
            return lstFlotaDTO;
        }

        public void saveFlotas(FlotaDTO flotaDTO)
        {
            //Mapear desde Vista a Entidad
            AutoMapper.Mapper.CreateMap<FlotaDTO, Gen_tb_Flota>();
            Gen_tb_Flota genTbFlota = AutoMapper.Mapper.Map<Gen_tb_Flota>(flotaDTO);

            //Grabar flota
            GenTbFlotaDAOImpl flotaDAO = new GenTbFlotaDAOImpl();
            flotaDAO.InsertEntity(genTbFlota);
        }

        public void UpdateEntity(FlotaDTO flotaDTO)
        {
            //Mapear desde Vista a Entidad
            AutoMapper.Mapper.CreateMap<FlotaDTO, Gen_tb_Flota>();
            Gen_tb_Flota genTbFlota = AutoMapper.Mapper.Map<Gen_tb_Flota>(flotaDTO);

            //Grabar flota
            GenTbFlotaDAOImpl flotaDAO = new GenTbFlotaDAOImpl();
            flotaDAO.UpdateEntity(genTbFlota);
        }
    }
}
