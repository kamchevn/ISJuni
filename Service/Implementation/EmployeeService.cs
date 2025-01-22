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
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Delete(Employee entity)
        {
            return _employeeRepository.Delete(entity);
        }

        public Employee Get(Guid? id)
        {
            return _employeeRepository.Get(id);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll().ToList();
        }

        public Employee Insert(Employee entity)
        {
            return _employeeRepository.Insert(entity);
        }

        public List<Employee> InsertMany(List<Employee> entities)
        {
            return _employeeRepository.InsertMany(entities);
        }

        public Employee Update(Employee entity)
        {
            return _employeeRepository.Update(entity);
        }
    }
}
