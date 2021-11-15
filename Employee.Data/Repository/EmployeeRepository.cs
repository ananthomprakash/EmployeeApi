using Employees.Data.Classes;
using Employees.Data.Interface;
using Employees.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        TutorialsContext db;

        public EmployeeRepository(TutorialsContext _db)
        {
            db = _db;
        }
        public async Task<int> AddEmployee(EmployeeRequest employee)
        {

            if (db != null)
            {
                Employee e = new Employee()
                {

                    EmployeeName = employee.EmployeeName,
                    EmployeeCity = employee.EmployeeCity,
                    EmployeeSalary = Convert.ToDecimal(employee.EmployeeSalary)

                };
                await db.Employees.AddAsync(e);
                await db.SaveChangesAsync();
                return e.EmployeeId;

            }
            return 0;

        }

        public async Task<int> DeleteEmployee(int? employeeId)
        {
            int result = 0;
            if (db != null)
            {
                //Find the employee for the specific employeeId
                var empId = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
                if (empId != null)
                {
                    //delete the employee
                    db.Employees.Remove(empId);

                    //commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        /// <summary>
        /// Method to get Departments
        /// </summary>
        /// <returns> List of Departments</returns>
        public async Task<List<Department>> GetDepartments()
        {
            if (db != null)
            {
                return await db.Departments.ToListAsync();
            }
            return null;
        }

        public async Task<EmployeeDetails> GetEmployeeDetail(int? employeeId)
        {
            if (db != null)
            {
                return await (from p in db.Departments
                              from c in db.Employees
                              where p.EmployeeId == employeeId
                              select new EmployeeDetails
                              {
                                  DepartmentID = p.DepartmentId,
                                  DepartmentName = p.DepartmentName,
                                  EmployeeId = c.EmployeeId,
                                  EmployeeName = c.EmployeeName,
                                  EmployeeSalary = Convert.ToInt32(c.EmployeeSalary),
                                  EmployeeCity = c.EmployeeCity
                              }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<EmployeeDetails>> GetEmployeeDetails()
        {
            if (db != null)
            {
                return await (from p in db.Departments
                              from c in db.Employees
                              where p.EmployeeId == c.EmployeeId
                              select new EmployeeDetails
                              {
                                  DepartmentID = p.DepartmentId,
                                  DepartmentName = p.DepartmentName,
                                  EmployeeId = c.EmployeeId,
                                  EmployeeName = c.EmployeeName,
                                  EmployeeSalary = Convert.ToInt32(c.EmployeeSalary),
                                  EmployeeCity = c.EmployeeCity
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            if (db != null)
            {
                return await db.Employees.ToListAsync();
            }
            return null;
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            int result = 0;
            if (db != null)
            {
                
                //update the post
                db.Employees.Update(employee);
                //commit the transaction
                result = await db.SaveChangesAsync();
                return result;

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<bool> CheckUser(string userName,string passWord)
        {
            
            if(db!= null)
            {
               return await db.Users.AnyAsync(x => x.Name == userName && x.Password == passWord);
            }
            return false;
        }
    }
}
