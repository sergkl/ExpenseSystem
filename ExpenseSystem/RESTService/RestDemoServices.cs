using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Common;

namespace ExpenseSystem.RESTService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                 ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RestDemoServices : IRESTDemoServices
    {
        public string GetUserFullName(string Id)
        {
            string result = string.Empty;
            int id;
            if (int.TryParse(Id, out id))
            {
                
                UserRepository userRepository = new UserRepository(new ExpenseSystemEntities());
                GetObjectResponse<Entities.User> response = userRepository.GetById(id, id);
                if (!response.IsError)
                {
                    result = string.Format("{0} {1} {2}", response.Object.FirstName, response.Object.MiddleName, response.Object.LastName).Replace("  ", ""); //Replace if middle name is empty
                }
                else
                {
                    foreach (string error in response.Errors)
                        result += error;
                }
            }
            else
            {
                result = Error.ValueMustBeInteger;
            }
            return result;
        }
    }
}
