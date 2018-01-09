using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelLayer;
using EntitiesLayer;

namespace DataModelLayer
{
    public class GenTbFlotaDAOImpl : IGenTbFlotaDAOService
    {
        private GenericRepository<Gen_tb_Flota> genericRepository = new GenericRepository<Gen_tb_Flota>(new SUTRANEntities());
        
        public List<Gen_tb_Flota> Get(System.Linq.Expressions.Expression<Func<Gen_tb_Flota, bool>> filter = null, Func<IQueryable<Gen_tb_Flota>, IOrderedQueryable<Gen_tb_Flota>> orderBy = null, string includeProperties = "")
        {
            return genericRepository.Get(filter, orderBy, includeProperties);
        }

        public Gen_tb_Flota GetByID(object id)
        {
            return genericRepository.GetByID(id);
        }

        public void InsertEntity(Gen_tb_Flota entity)
        {
            genericRepository.InsertEntity(entity);
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(Gen_tb_Flota entityToDelete) 
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Gen_tb_Flota entityToUpdate)
        {
            genericRepository.UpdateEntity(entityToUpdate);
        }
    }
}
