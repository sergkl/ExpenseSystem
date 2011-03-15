using System;
using System.Web.Mvc;
using ExpenseSystem.Behaviour;
using ExpenseSystem.Common;
using ExpenseSystem.Repositories.Responses;
using Machine.Specifications;
using MoqLib = Moq;

namespace When
{
    /// <summary>
    /// Test class: When I add new expense record with all needed information for this target
    /// </summary>
    [Subject("When adding new expense record")]
    public class I_provide_not_all_info_for_new_expense_record : concern_for_expense_controller
    {
        private static string description;
        private static decimal price;
        private static int tagId;
        private static DateTime dateStamp;

        private Establish context = () =>
                                        {
                                            //Setup init values
                                            description = string.Empty;
                                            price = 0;
                                            tagId = 0;
                                            dateStamp = DateTime.Now.Date;

                                            //Init mock data
                                            var addResponse = new AddResponse();
                                            controller.SessionVars.UserId = 1;
                                            addResponse.IsError = true;
                                            addResponse.Errors.Add(Error.ExpenseRecordHasNotBeenSet);
                                            mockExpenseRecordRepository.Setup(
                                                p =>
                                                p.Add(controller.SessionVars.UserId, description, price, tagId,
                                                      dateStamp)).Returns(addResponse);
                                            controller.ExpenseRecordRepository = mockExpenseRecordRepository.Object;
                                        };

        private It expense_record_should_be_added =
            () => ((result as JsonResult).Data as AddResponse).Errors[0].ShouldEqual(Error.ExpenseRecordHasNotBeenSet);

        private Because of = () => { result = controller.AddExpenseRecord(description, price, tagId, dateStamp); };
    }
}