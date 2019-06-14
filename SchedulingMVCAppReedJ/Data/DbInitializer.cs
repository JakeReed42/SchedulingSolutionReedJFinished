using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchedulingMVCAppReedJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(IServiceProvider services)
        {
            //change from accessing just the database service
            //add user and role services. 
            //var refers to generic variable... can hold any datatype
            var database = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //List<DepartmentChair> departmentChairList = DepartmentChair.PopulateDepartmentChair();


            //Step 1: Avoid "Re-populating" tables with the same data

            if (!database.Departments.Any())
            {

                //List<DepartmentChair> departmentChairs = database.DepartmentChairs.ToList<DepartmentChair>();


                //Department department = new Department("Management Information Systems", departmentChairs[0].DepartmentChairID);
                //List<Department> departmentsList = new List<Department>();
                //departmentsList.Add(department);


                //department = new Department("Accounting", departmentChairs[1].DepartmentChairID);
                //departmentsList.Add(department);
                //Step 2: Get data for Departments table
                // Class method. Call using Class Department6


                //  department.PopulateDepartments(); this is an error because we made the static

                //List<DepartmentChair> departments = database.Departments.ToList<DepartmentChair>();

                List<Department> departmentList = Department.PopulateDepartments();
                 database.Departments.AddRange(departmentList);
                 database.SaveChanges();
            }



            // This is the data population for Course
            if (!database.Courses.Any())
            {
                List<Course> courseList = Course.PopulateCourses();

                database.Courses.AddRange(courseList);
                database.SaveChanges();
            }

            string roleCoordinator = "Coordinator";
            String roleDepartmentChair = "Chair";

            List<ApplicationUser> appUserList =
                ApplicationUser.PopulateUsers(); 

            if (!database.Roles.Any())
            {

                IdentityRole role = new IdentityRole(roleCoordinator);
                await roleManager.CreateAsync(role);


                role = new IdentityRole(roleDepartmentChair);
                await roleManager.CreateAsync(role);
            }

            


            if (!database.Users.Any())
            {
                //DBConn 1 (Synchronus - Method 1)
                //DBConn 1 (Sych - Method 2)
                // this means method 2 only goes after method 1 is done
                // Async means that both can go go at the same time 

                foreach (ApplicationUser eachCoordinator in appUserList)
                {
                    //userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await userManager.CreateAsync(eachCoordinator);

                    //userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await userManager.AddToRoleAsync(eachCoordinator, roleCoordinator);

                }
            }


            // Need to finish added department chairs to the list. 



            List<DepartmentChair> chairList =
                DepartmentChair.PopulateDepartmentChair();

            if (!database.DepartmentChairs.Any())
            {

                foreach (DepartmentChair eachChair in chairList)
                {
                    await userManager.CreateAsync(eachChair);

                    await userManager.AddToRoleAsync(eachChair, roleDepartmentChair);

                }
            }
            if (!database.ConflictCourses.Any())
            {
                List<ConflictCourse> conflictCourseList = ConflictCourse.PopulateConflictCourses();

                database.ConflictCourses.AddRange(conflictCourseList);
                database.SaveChanges();
            }



            if (!database.Instructors.Any())
            {
                List<Instructor> instructorList = Instructor.PopulateInstructors();
                database.Instructors.AddRange(instructorList);
                database.SaveChanges();
            }

            if (!database.CourseOfferings.Any())
            {
                List<CourseOffering> offeringList = CourseOffering.PopulateCourseOffering();
                database.CourseOfferings.AddRange(offeringList);
                database.SaveChanges();

            }

            if (!database.CoursesOfferingsChanges.Any())
            {
                
                List<CourseOffering> courseOffering = database.CourseOfferings.ToList();
                CourseOfferingChanges courseOfferingChanges =
                    new CourseOfferingChanges("Chair", "Add", /*new DateTime(2019, 01, 15, 9, 52, 03),*/ courseOffering[0].CRN, courseOffering[0].Days
                    , courseOffering[0].StartTime, courseOffering[0].EndTime, courseOffering[0].StartDate, courseOffering[0].EndDate, courseOffering[0].InstructorID ,chairList[0].LastName, courseOffering[0].CourseID);
            database.Add(courseOfferingChanges);
                database.SaveChanges();
            }



            List<Department> departments =
                database.Departments.ToList<Department>();

           
            
                List<DepartmentChair> departmentChairs =
                    database.DepartmentChairs.ToList<DepartmentChair>();

            // have to change each time you revert?
            
            foreach (Department eachDepartment in departments)
            {
                foreach (DepartmentChair eachChair in departmentChairs)
                {
                    if (eachDepartment.DepartmentID == eachChair.DepartmentID)
                    {
                        eachDepartment.DepartmentChairID = eachChair.DepartmentChairID;
                    }
                }
            }
            database.Departments.UpdateRange(departments);
            database.SaveChanges();



            if (!database.TeachingQualifications.Any())
            {
                List<TeachingQualification> teachingQualificationsList = TeachingQualification.PopulateTeachingQualifications();
                database.TeachingQualifications.AddRange(teachingQualificationsList);
                database.SaveChanges();
            }

        }// end of task

}// End of Class

}// End of Namespace
