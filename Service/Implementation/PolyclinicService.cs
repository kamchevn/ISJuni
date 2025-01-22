using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class PolyclinicService : IPolyclinicService
    {
        private readonly IRepository<Polyclinic> _polyclinicRepository;

        public PolyclinicService(IRepository<Polyclinic> polyclinicRepository)
        {
            _polyclinicRepository = polyclinicRepository;
        }

        public Polyclinic Delete(Polyclinic entity)
        {
            return _polyclinicRepository.Delete(entity);
        }

        public Polyclinic Get(Guid? id)
        {
            return _polyclinicRepository.Get(id);
        }

        public List<Polyclinic> GetAll()
        {
            return _polyclinicRepository.GetAll().ToList();
        }

        public Polyclinic Insert(Polyclinic entity)
        {
            return _polyclinicRepository.Insert(entity);
        }

        public List<Polyclinic> InsertMany(List<Polyclinic> entities)
        {
            return _polyclinicRepository.InsertMany(entities);
        }

        public void LowerCapacity(Polyclinic entity)
        {
            --entity.AvailableSlots;
        }

        public Polyclinic Update(Polyclinic entity)
        {
            return _polyclinicRepository.Update(entity);
        }
    }
}
