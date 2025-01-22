using Domain.Domain_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPolyclinicService _polyclinicService;
        private readonly IHealthExaminationService _healthExaminationService;

        public AdminController(IPolyclinicService polyclinicService, IHealthExaminationService healthExaminationService)
        {
            _polyclinicService = polyclinicService;
            _healthExaminationService = healthExaminationService;
        }

        [HttpGet("[action]")]
        public List<Polyclinic> GetAllPolyclinics()
        {
            return this._polyclinicService.GetAll();
        }
        [HttpGet("[action]")]
        public List<HealthExamination> GetAllHealthExaminations()
        {
            return this._healthExaminationService.GetAll();
        }
        [HttpPost("[action]")]
        public HealthExamination GetDetails(BaseEntity id)
        {
            return this._healthExaminationService.GetDetailsForExam(id);
        }
    }
}
