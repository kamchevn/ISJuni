using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee Get(Guid? id);
        Employee Insert(Employee entity);
        List<Employee> InsertMany(List<Employee> entities);
        Employee Update(Employee entity);
        Employee Delete(Employee entity);
    }
}
