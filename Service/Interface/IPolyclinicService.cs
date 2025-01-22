using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPolyclinicService
    {
        List<Polyclinic> GetAll();
        Polyclinic Get(Guid? id);
        Polyclinic Insert(Polyclinic entity);
        List<Polyclinic> InsertMany(List<Polyclinic> entities);
        Polyclinic Update(Polyclinic entity);
        Polyclinic Delete(Polyclinic entity);
        void LowerCapacity(Polyclinic entity);
    }
}
