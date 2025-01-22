using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models.DTO
{
    public class HealthExamsForEmployee
    {
        public Employee? Employee { get; set; }
        public List<HealthExamination>? HealthExaminations { get; set; }
    }
}
