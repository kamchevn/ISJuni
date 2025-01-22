using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models.DTO
{
    public class ExaminationDTO
    {
        public Guid employeeId {  get; set; }
        public Guid companyId { get; set; }
        public Guid? polyclinicId { get; set; }
        public List<Polyclinic>? Polyclinics { get; set; }
        public string? Description { get; set; }
        public DateTime DateTaken { get; set; }
    }
}
