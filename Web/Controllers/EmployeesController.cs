using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Domain_Models;
using Repository;
using Service.Interface;
using Domain.Domain_Models.DTO;

namespace Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;
        private readonly IPolyclinicService _polyclinicService;
        private readonly IHealthExaminationService _healthExaminationService;

        public EmployeesController(ApplicationDbContext context, IEmployeeService employeeService, IHealthExaminationService healthExaminationService, IPolyclinicService polyclinicService)
        {
            _context = context;
            _employeeService = employeeService;
            _healthExaminationService = healthExaminationService;
            _polyclinicService = polyclinicService;
        }

        // GET: Employees
        public IActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        // GET: Employees/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.Get(id);
            HealthExamsForEmployee dto = new HealthExamsForEmployee()
            {
                Employee = employee,
                HealthExaminations = _healthExaminationService.GetExamsForEmployee(id)
            };
            if (employee == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FullName,DateOfBirth,Title,CompanyId,Id")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                _employeeService.Insert(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FullName,DateOfBirth,Title,CompanyId,Id")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.Update(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var employee = _employeeService.Get(id);
            if (employee != null)
            {
                _employeeService.Delete(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _employeeService.Get(id) != null;
        }

        public IActionResult ExamForm(Guid id)
        {
            ExaminationDTO dto = new ExaminationDTO()
            {
                employeeId = id,
                companyId = _employeeService.Get(id).CompanyId,
                Polyclinics = _polyclinicService.GetAll()
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamEmployee(ExaminationDTO dto)
        {
            if (ModelState.IsValid)
            {
                var polyclinic = _polyclinicService.Get(dto.polyclinicId);
                if(polyclinic.AvailableSlots <= 0)
                {
                    return View("NoCapacity");
                }
                _polyclinicService.LowerCapacity(polyclinic);
                _healthExaminationService.addExamination(dto);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
