using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentApi.Ef;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests
    {
       /* [TestMethod]
        public void GetAllProjects()
        {
            Model model = new Model();
            var result = model.Projects.ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void AddNewProject()
        {
            Model model = new Model();
            Project p = new Project();
            p.Name = "something";
            p.Description = "A lot of something";
            model.Projects.Add(p);
            int count = model.Projects.ToList().Count;
            model.SaveChanges();
            int newCount = model.Projects.ToList().Count;
            Assert.AreEqual(newCount, count + 1);
        }

        [TestMethod]
        public void UpdateProject()
        {
            Model model = new Model();
            Project p = model.Projects.Find(1);
            string oldDescription = p.Description;
            p.Description = ((new Random()).Next(0,Int32.MaxValue)).ToString();
            model.SaveChanges();
            var newP = model.Projects.Find(1);
            Assert.AreNotEqual(oldDescription, newP.Description);
        }

        [TestMethod]
        public void CreateUnaffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "Awesome team";
            Model model = new Model();
            model.Teams.Add(t);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateAffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "A Team";
            Project p = new Project();
            p.Name = "The A Team Project";
            List<Team> demTeams = new List<Team>();
            demTeams.Add(t);
            p.Teams = demTeams;

            Model model = new Model();
            model.Projects.Add(p);
            model.SaveChanges();

            Team team = model.Teams.Where(someTeam => someTeam.Name == "A Team").FirstOrDefault();
            Project affiliatedProject = team.Project;
            bool projectHasTeam = team.Name == t.Name && affiliatedProject.Name == p.Name;

            Assert.IsNotNull(team);
            Assert.IsNotNull(affiliatedProject);
            Assert.IsTrue(projectHasTeam);
        }

        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        [TestMethod]
        public void InvalidOperationException_On_Create_Unaffiliated_ContactInfo()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "tere@mail.dk";
            contactInfo.Phone = "32323232";
            Model model = new Model();
            model.ContactInfos.Add(contactInfo);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateEmployeeWithOutContactInfo()
        {
            Employee employee = new Employee();
            employee.Name = "John";

            Model model = new Model();
            model.Employees.Add(employee);
            model.SaveChanges();
        }*/

        [TestMethod]
        public void CreateEmployeeWithContactInfo()
        {
            /*Employee employee = new Employee();
            employee.Name = "Tom";

            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "test@mail.dk";
            contactInfo.Phone = "55555555";
            employee.ContactInfo = contactInfo;

            Model model = new Model();*/

            // Arrange:
            /*Model model = new Model();
            string newName = "Jørgen";
            Employee newEmployee = new Employee { Name = newName };
            ContactInfo newContactInfo = new ContactInfo { Email = "mail@mail.com", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
            newEmployee.ContactInfo = newContactInfo;
            model.Employees.Add(newEmployee);

            // Act:
            model.SaveChanges();
            Employee existingEmployee = model.Employees.Single(e => e.Name == newName);
            ContactInfo existingContactInfo = existingEmployee.ContactInfo;

            // Assert:
            Assert.AreEqual(newEmployee.Name, existingEmployee.Name);
            Assert.AreEqual(newContactInfo.Phone, existingContactInfo.Phone);*/

        }
    }
}
