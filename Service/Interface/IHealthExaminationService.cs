using Domain.Domain_Models;
using Domain.Domain_Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IHealthExaminationService
    {
        List<HealthExamination> GetExamsForEmployee(Guid? employeeId);
        List<HealthExamination> GetExamsForPolyclinic(Guid? polyclinicId);
        void addExamination(ExaminationDTO examinationDTO);
    }
}
