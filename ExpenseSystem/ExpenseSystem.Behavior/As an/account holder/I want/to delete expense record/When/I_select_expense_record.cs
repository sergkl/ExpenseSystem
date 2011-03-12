using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.Behaviour;
using Machine.Specifications;
using System.Web.Mvc;
using ExpenseSystem.Repositories.Responses;

namespace When
{
    /// <summary>
    /// Test class: When I select expense record
    /// </summary>
    [Subject("When deleting an expense record")]
    public class I_select_expense_record : concern_for_expense_controller
    {
        Establish context = () =>
        {
            //Setup init values
            expenseRecordId = 1;

            //Init mock data
            Response response = new Response();
            controller.SessionVars.UserId = 1;
            mockExpenseRecordRepository.Setup(p => p.Delete(controller.SessionVars.UserId, expenseRecordId)).Returns(response);
            controller.ExpenseRecordRepository = mockExpenseRecordRepository.Object;
        };

        Because of = () => { result = controller.DeleteExpenseRecord(expenseRecordId); };

        It tag_should_be_deleted = () => ((result as JsonResult).Data as Response).IsError.ShouldEqual(false);

        static int expenseRecordId;
    }
}
