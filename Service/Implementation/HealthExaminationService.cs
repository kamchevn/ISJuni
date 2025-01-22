using Domain.Domain_Models;
using Domain.Domain_Models.DTO;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class HealthExaminationService : IHealthExaminationService
    {
        private readonly IRepository<HealthExamination> _repository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Polyclinic> _polyclinicRepository;

        public HealthExaminationService(IRepository<HealthExamination> repository, IRepository<Employee> employeeRepository, IRepository<Polyclinic> polyclinicRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _polyclinicRepository = polyclinicRepository;
        }

        public void addExamination(ExaminationDTO examinationDTO)
        {
            HealthExamination healthExamination = new HealthExamination()
            {
                Description = examinationDTO.Description,
                DateTaken = examinationDTO.DateTaken,
                Employee = _employeeRepository.Get(examinationDTO.employeeId),
                EmployeeId = examinationDTO.employeeId,
                Polyclinic = _polyclinicRepository.Get(examinationDTO.polyclinicId),
                PolyclinicId = (Guid) examinationDTO.polyclinicId
            };
            _repository.Insert(healthExamination);
        }

        public List<HealthExamination> GetExamsForEmployee(Guid? employeeId)
        {
            return _repository.GetAll().Where(x => x.EmployeeId == employeeId).ToList();
        }

        public List<HealthExamination> GetExamsForPolyclinic(Guid? polyclinicId)
        {
            return _repository.GetAll().Where(x => x.PolyclinicId == polyclinicId).ToList();
        }
    }
}
