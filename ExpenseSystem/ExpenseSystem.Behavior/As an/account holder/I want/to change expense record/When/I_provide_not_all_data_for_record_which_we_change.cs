using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Behaviour;
using Machine.Specifications;
using ExpenseSystem.Repositories.Responses;
using System.Web.Mvc;
using ExpenseSystem.Common;

namespace When
{
    public class I_provide_not_all_data_for_record_which_we_change : concern_for_expense_controller
    {
        Establish context = () =>
        {
            //Setup init values
            expenseRecordId = 0;
            description = string.Empty;
            price = 0;
            tagId = 0;
            dateStamp = DateTime.Now.Date;

            //Init mock data
            Response response = new Response();
            response.IsError = true;
            response.Errors.Add(Error.ExpenseRecordHasNotBeenSet);
            controller.SessionVars.UserId = 1;
            mockExpenseRecordRepository.Setup(p => p.Edit(controller.SessionVars.UserId, expenseRecordId, description, price, tagId, dateStamp)).Returns(response);
            controller.ExpenseRecordRepository = mockExpenseRecordRepository.Object;
        };

        Because of = () => { result = controller.EditExpenseRecord(expenseRecordId, description, price, tagId, dateStamp); };

        It expense_record_should_be_added = () => ((result as JsonResult).Data as Response).Errors[0].ShouldEqual(Error.ExpenseRecordHasNotBeenSet);

        static int expenseRecordId;
        static string description;
        static decimal price;
        static int tagId;
        static DateTime dateStamp;
    }
}
