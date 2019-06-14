using Microsoft.AspNetCore.Mvc;
using Moq;
using SchedulingMVCAppReedJ.Controllers;
using SchedulingMVCAppReedJ.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SchedulingTests
{
    public class InstructorControllerShould
    {
        [Fact]
        public async Task AddNewInstructor()
        {
            Mock<IInstructorRepository> mockInstructorRepository =
               new Mock<IInstructorRepository>();

            Instructor addedInstructor = null;

            mockInstructorRepository.Setup(m => m.AddInstructor(It.IsAny<Instructor>()))
            .Returns(Task.CompletedTask).Callback<Instructor>(m => addedInstructor = m);

           InstructorController instructorController =
                new InstructorController(mockInstructorRepository.Object);

            Instructor instructor = new Instructor("Nanda", "Surendra", "NaSurendra@Mix", 1);
            instructor.InstructorID = 14;
            //Act
           var result = await instructorController.AddInstructor(instructor);

            Assert.Equal(instructor.InstructorLastName, addedInstructor.InstructorLastName);
            Assert.Equal(instructor, addedInstructor);
        }

        [Fact]
        public async Task EditInstructor()
        {
            Mock<IInstructorRepository> mockInstructorRepository =
               new Mock<IInstructorRepository>();

            Instructor editedInstructor = null;

            mockInstructorRepository.Setup(m => m.AddInstructor(It.IsAny<Instructor>()))
            .Returns(Task.CompletedTask).Callback<Instructor>(repo => editedInstructor = repo);

            InstructorController instructorController =
                 new InstructorController(mockInstructorRepository.Object);

            Instructor instructor = new Instructor("Nanda", "Surendra", "NaSurendra@Mix", 1);
            instructor.InstructorID = 14;
            //Act
            var result = await instructorController.EditInstructor(instructor);

            Assert.IsType<RedirectToActionResult>(result);

            //Assert.Equal(instructor.InstructorLastName, editedInstructor.InstructorLastName);
            //Assert.Equal(instructor, editedInstructor);

        }

        [Fact]
        public void ReturnViewForListAllInstructors()
        {
            Mock<IInstructorRepository> mockInstructorRepository =
                new Mock<IInstructorRepository>();

            List<Instructor> mockInstructorList = PopulateInstructors();

            mockInstructorRepository.Setup(m => m.ListAllInstructors())
                .Returns(mockInstructorList);

            

            InstructorController instructorController =
                new InstructorController(mockInstructorRepository.Object);

           var result = instructorController.ListAllInstructors();
                        
         ViewResult viewResult = Assert.IsType<ViewResult>(result);
            List<Instructor> viewResultModel = (List<Instructor>) viewResult.Model;
            // ^ this is called "casting" tells the 
            //program to accept that it will be what we say it is

            int expectedNumberOfInstructors = 4;

            Assert.Equal(expectedNumberOfInstructors, viewResultModel.Count);

            string expectedEmail = "NaSurendra@Mix";

            string actualEmail = viewResultModel.
                Find(m => m.InstructorID == 1).InstructorEmail;

            Assert.Equal(expectedEmail, actualEmail);


            Assert.Equal(mockInstructorList, viewResultModel);


        }// end of Return ListAllInstructors

        private List<Instructor> PopulateInstructors()
        {
            List<Instructor> instructorList = new List<Instructor>();

            Instructor instructor = new Instructor("Nanda", "Surendra", "NaSurendra@Mix", 1);
            instructor.InstructorID = 1;
            instructorList.Add(instructor);

            instructor = new Instructor("Virginia", "Kliest", "VKliest@Mix", 1);
            instructor.InstructorID = 2;
            instructorList.Add(instructor);

            instructor = new Instructor("Ludwig", "Schuapp", "LudSchuapp@Mix", 2);
            instructor.InstructorID = 3;
            instructorList.Add(instructor);

            instructor = new Instructor("Leeroy", "Jenkins", "TimesUp@Mix", 3);
            instructor.InstructorID = 4;
            instructorList.Add(instructor);

            return instructorList;
        }

    }
}
