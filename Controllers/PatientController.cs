using System;
using Microsoft.AspNetCore.Mvc;
using NewLifeHospital.Models;

namespace NewLifeHospital.Controllers
{
    public class PatientController : Controller
    {
        private readonly IRepository _repository;

        public PatientController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult RegisterForMembership()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterForMembership(PatientInfoDetail pObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pObj);
                }

                if (_repository.RegisterForMembership(pObj))
                {
                    ViewBag.Message = "Registration successful. Your RegistrationID is " + pObj.RegistrationID + ".";
                    ModelState.Clear();
                    return View(new PatientInfoDetail());
                }

                ViewBag.Message = "Registration failed. Please try again.";
                return View(pObj);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while registering: " + ex.Message;
                return View(pObj);
            }
        }

        [HttpGet]
        public ActionResult CancelMembership()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelMembership(int registrationId)
        {
            try
            {
                if (registrationId <= 0)
                {
                    ViewBag.Message = "Please enter a valid RegistrationID.";
                    return View();
                }

                ViewBag.Message = _repository.CancelMembership(registrationId)
                    ? "Membership cancelled successfully for RegistrationID " + registrationId + "."
                    : "No record found for RegistrationID " + registrationId + ".";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while cancelling the membership: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmail(int registrationId, string email)
        {
            try
            {
                if (registrationId <= 0)
                {
                    ViewBag.Message = "Please enter a valid RegistrationID.";
                    return View();
                }

                if (string.IsNullOrWhiteSpace(email) || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email))
                {
                    ViewBag.Message = "Please enter a valid EmailID.";
                    return View();
                }

                ViewBag.Message = _repository.UpdateEmail(registrationId, email)
                    ? "EmailID updated successfully for RegistrationID " + registrationId + "."
                    : "No record found for RegistrationID " + registrationId + ".";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred while updating the EmailID: " + ex.Message;
                return View();
            }
        }
    }
}
