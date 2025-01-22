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
    public class PolyclinicsController : Controller
    {
        private readonly IPolyclinicService _polyclinicService;
        private readonly IHealthExaminationService _healthExaminationService;

        public PolyclinicsController(IPolyclinicService polyclinicService, IHealthExaminationService healthExaminationService)
        {
            _polyclinicService = polyclinicService;
            _healthExaminationService = healthExaminationService;
        }

        // GET: Polyclinics
        public IActionResult Index()
        {
            return View(_polyclinicService.GetAll());
        }

        // GET: Polyclinics/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.Get(id);
            HealthExamsForPolyclinic dto = new HealthExamsForPolyclinic()
            {
                Polyclinic = polyclinic,
                HealthExaminations = _healthExaminationService.GetExamsForPolyclinic(id)
            };
            if (polyclinic == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // GET: Polyclinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Polyclinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PolyclinicName,PolyclinicAddress,Reputation,AvailableSlots,Id")] Polyclinic polyclinic)
        {
            if (ModelState.IsValid)
            {
                polyclinic.Id = Guid.NewGuid();
                _polyclinicService.Insert(polyclinic);
                return RedirectToAction(nameof(Index));
            }
            return View(polyclinic);
        }

        // GET: Polyclinics/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.Get(id);
            if (polyclinic == null)
            {
                return NotFound();
            }
            return View(polyclinic);
        }

        // POST: Polyclinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("PolyclinicName,PolyclinicAddress,Reputation,AvailableSlots,Id")] Polyclinic polyclinic)
        {
            if (id != polyclinic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _polyclinicService.Update(polyclinic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolyclinicExists(polyclinic.Id))
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
            return View(polyclinic);
        }

        // GET: Polyclinics/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polyclinic = _polyclinicService.Get(id);
            if (polyclinic == null)
            {
                return NotFound();
            }

            return View(polyclinic);
        }

        // POST: Polyclinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var polyclinic = _polyclinicService.Get(id);
            if (polyclinic != null)
            {
                _polyclinicService.Delete(polyclinic);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PolyclinicExists(Guid id)
        {
            return _polyclinicService.Get(id) != null;
        }
    }
}
