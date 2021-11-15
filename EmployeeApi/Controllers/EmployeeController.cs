using Employees.Auth.Interface;
using Employees.Data.Interface;
using Employees.Data.Models;
using Employees.Validate.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    #region Employee Controller
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Variables
        private readonly IEmployeeRepository employeeRepository;

        private readonly EmployeeValidation _employeeValidation;

        private readonly EmployeeModelValidation _employeeModelValidation;

        private readonly IJwtAuth _jwtAuth;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_employeeRepository"></param>
        public EmployeeController(IEmployeeRepository _employeeRepository, IJwtAuth jwtAuth)
        {
            employeeRepository = _employeeRepository;
            _employeeValidation = new EmployeeValidation();
            _employeeModelValidation = new EmployeeModelValidation();

            _jwtAuth = jwtAuth;
        }
        #endregion

        #region Departments
        /// <summary>
        /// GetList of Departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await employeeRepository.GetDepartments();
                if (departments == null)
                {
                    return NotFound();
                }
                return Ok(departments);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region EmployeeDetails
        /// <summary>
        /// Get list of EmployeeDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            try
            {
                var emp = await employeeRepository.GetEmployeeDetails();
                if (emp == null)
                {
                    return NotFound();
                }
                return Ok(emp);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region GetEmployees
        /// <summary>
        /// Get list of Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var emp = await employeeRepository.GetEmployees();
                if (emp == null)
                {
                    return NotFound();
                }
                return Ok(emp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region GetEmployeeDetailsById
        /// <summary>
        /// Get List of EmployeeDetailById
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/GetEmployeeDetailById")]
        public async Task<IActionResult> GetEmployeeDetailById(int? employeeId)
        {
            try
            {
                var empDetail = await employeeRepository.GetEmployeeDetail(employeeId);
                return Ok(empDetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Add Employee
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeRequest employeeRequest)
        {
            try
            {
                //Checking for validation

                var result = await _employeeValidation.ValidateAsync(employeeRequest);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var emp = await employeeRepository.AddEmployee(employeeRequest);
                if (emp > 0)
                {
                    return Ok(emp);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Delete Employee
        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int? employeeId)
        {
            int result = 0;
            if (employeeId == null)
            {
                return BadRequest();
            }
            try
            {
                result = await employeeRepository.DeleteEmployee(employeeId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Deleted Succesfully");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Update Employee
        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employeeRequest)
        {
            try
            {
                //Check for validation
                var res = await _employeeModelValidation.ValidateAsync(employeeRequest);
                if (!res.IsValid)
                {
                    return BadRequest(res.Errors);
                }
                int result = await employeeRepository.UpdateEmployee(employeeRequest);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Updated Successfully");

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Authentication
        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost]
        [Route("v1/authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            bool checkUser = employeeRepository.CheckUser(userCredential.UserName, userCredential.Password).Result;
            if (checkUser == false)
            {
                return Unauthorized();
            }
            else
            {
                var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
                if (token == null)
                    return Unauthorized();
                return Ok(token);

            }
        }
        #endregion

    }
    #endregion
}

