using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOLayer
{
    public class FlotaDTO
    {
        private string _activo;

        public virtual int idFlota
        {
            get;
            set;
        }
    
        public virtual string nombreFlota
        {
            get;
            set;
        }
    
        public virtual string usuario
        {
            get;
            set;
        }
    
        public virtual string password
        {
            get;
            set;
        }

        public string activo
        {
            get;
            set;
        }
    }
}
