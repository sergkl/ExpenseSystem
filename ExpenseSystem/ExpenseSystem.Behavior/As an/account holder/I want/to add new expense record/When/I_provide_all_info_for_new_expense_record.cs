using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories.Responses;
using MoqLib = Moq;
using System.Web.Mvc;
using ExpenseSystem.Behaviour;

namespace When
{
    /// <summary>
    /// Test class: When I add new expense record with all needed information for this target
    /// </summary>
    [Subject("When adding new expense record")]
    public class I_provide_all_info_for_new_expense_record : concern_for_expense_controller
    {
        Establish context = () =>
        {
            //Setup init values
            description = "new expense record";
            price = 12;
            tagId = 1;
            dateStamp = DateTime.Now.Date;

            //Init mock data
            AddResponse addResponse = new AddResponse();
            controller.SessionVars.UserId = 1;
            addResponse.Id = 1;
            mockExpenseRecordRepository.Setup(p => p.Add(controller.SessionVars.UserId, description, price, tagId, dateStamp)).Returns(addResponse);
            controller.ExpenseRecordRepository = mockExpenseRecordRepository.Object;
        };

        Because of = () => { result = controller.AddExpenseRecord(description, price, tagId, dateStamp); };

        It expense_record_should_be_added = () => ((result as JsonResult).Data as AddResponse).Id.ShouldBeGreaterThan(0);

        static string description;
        static decimal price;
        static int tagId;
        static DateTime dateStamp;
    }
}
