using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Behaviour;
using System.Web.Mvc;

namespace When
{
    /// <summary>
    /// Test class: When I provide all data for record which we are going to change
    /// </summary>
    [Subject("When changing expense record")]
    public class I_provide_all_data_for_record_which_we_change : concern_for_expense_controller
    {
        Establish context = () =>
        {
            //Setup init values
            expenseRecordId = 1;
            description = "changed record";
            price = 12;
            tagId = 1;
            dateStamp = DateTime.Now.Date;

            //Init mock data
            Response response = new Response();
            controller.SessionVars.UserId = 1;
            mockExpenseRecordRepository.Setup(p => p.Edit(controller.SessionVars.UserId, expenseRecordId, description, price, tagId, dateStamp)).Returns(response);
            controller.ExpenseRecordRepository = mockExpenseRecordRepository.Object;
        };

        Because of = () => { result = controller.EditExpenseRecord(expenseRecordId, description, price, tagId, dateStamp); };

        It expense_record_should_be_added = () => ((result as JsonResult).Data as Response).IsError.ShouldEqual(false);

        static int expenseRecordId;
        static string description;
        static decimal price;
        static int tagId;
        static DateTime dateStamp;
    }
}
