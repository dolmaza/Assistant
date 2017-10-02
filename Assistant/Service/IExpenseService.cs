using Service.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service
{
    [ServiceContract]
    public interface IExpenseService
    {
        [OperationContract]
        IEnumerable<ExpenseServiceModel> GetAllExpenses(int? userId, DateTime? startDate, DateTime? endDate);


        [OperationContract]
        int? CreateExpense(ExpenseServiceModel model);


        [OperationContract]
        void UpdateExpense(ExpenseServiceModel model);


        [OperationContract]
        void DeleteExpense(int? Id);
    }
}
