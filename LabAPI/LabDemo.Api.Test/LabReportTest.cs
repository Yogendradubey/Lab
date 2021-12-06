using LabDemo.Api.Controllers;
using LabDemo.Models;
using LabDemo.Services.LabReports;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabDemo.Api.Test
{
    public class LabReportTest
    {
        List<LabReport> labReports = new List<LabReport>();

        [SetUp]
        public void Setup()
        {
            labReports = new List<LabReport> {
                new LabReport(){ LRId=1,PId=1,EnteredTime=DateTime.Now,TestTime=DateTime.Now,Result="Pass" }
            };
        }

        [Test]
        public void LabReport_GetById_ValidId()
        {

            // mocking
            var logger = new Mock<ILogger<LabReportsController>>();
            var labReportService = new Mock<ILabReportService>();

            /// setup
            labReportService.Setup(x => x.GetLabReport(It.IsAny<int>()))
            .Returns((int i) => labReports.Single(lb => lb.LRId == i));
            //  labReportService.Setup(x => x.GetLabReport(1))
            //.Returns(rep);

            var labReportController = new LabReportsController(logger.Object, labReportService.Object);
            var reportExists = labReportController.Get(1);
            Assert.IsNotNull(reportExists);
            Assert.AreEqual(reportExists.LRId, 1);
            Assert.AreEqual(reportExists.Result, "Pass");
        }
        [Test]
        public void LabReport_GetById_InValidId()
        {
            // mocking
            var logger = new Mock<ILogger<LabReportsController>>();
            var labReportService = new Mock<ILabReportService>();

            /// setup
            labReportService.Setup(x => x.GetLabReport(It.IsAny<int>()))
           .Returns((int i) => labReports.FirstOrDefault(lb => lb.LRId == i));
            //labReportService.Setup(x => x.GetLabReport(1))
            //.Returns(rep);

            var labReportController = new LabReportsController(logger.Object, labReportService.Object);
            var reportExists = labReportController.Get(2);
            Assert.IsNull(reportExists);
            //Assert.That(() => labReportController.Get(5),
            //       Throws.Exception
            //             .TypeOf<InvalidOperationException>());
        }
    }
}