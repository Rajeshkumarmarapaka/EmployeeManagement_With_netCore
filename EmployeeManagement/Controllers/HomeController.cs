﻿using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
   [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeRepository employeeRepository,
                               IHostingEnvironment hostingEnvironment,
                               ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        // List All the Employees
        [AllowAnonymous]
        public ViewResult Index()
        {

            var model= _employeeRepository.GetAllEmloyee();

            return View(model);
        }
        //View the Specific Employee
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //throw new Exception("details exception");
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");


            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }

        
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        // Create or Add new Employee
        [HttpPost]  
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //upload singe files
                string uniquefilename = ProcessUploadedFile(model);
                Employee newemployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniquefilename
                };
                _employeeRepository.Add(newemployee);

                return RedirectToAction("Details", new { id = newemployee.Id });
            }
            return View();
        }
        [HttpGet]     
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id=employee.Id,
                Name=employee.Name,
                Email=employee.Email,
                Department=employee.Department,
                ExistingPhotoPath=employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]       
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo !=null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filepath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filepath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                _employeeRepository.Update(employee);

                return RedirectToAction("Index");
            }
            return View();
        }
          
        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniquefilename = null;
            if (model.Photo != null)
            {
                //upload single files
                string uploadsfolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filepath = Path.Combine(uploadsfolder, uniquefilename);

                using(var fileStream= new FileStream(filepath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                //upload multiple files
                //string uniquefilename = null;
                //if(model.Photo != null && model.Photo.Count>0)
                //{
                //    foreach (IFormFile photo in model.Photo)
                //    {
                //        string uploadsfolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //        uniquefilename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //        string filepath = Path.Combine(uploadsfolder, uniquefilename);
                //        photo.CopyTo(new FileStream(filepath, FileMode.Create));
                //    }
                //}
            }

            return uniquefilename;
        }
    }
}
