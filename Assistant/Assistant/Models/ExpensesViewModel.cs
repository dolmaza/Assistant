using System.Collections.Generic;

namespace Assistant.Models
{
    public class ExpensesViewModel
    {
        #region Properties
        public string UserFullName { get; set; }
        public string UserBudget { get; set; }
        public string ExpensesSum { get; set; }
        public string MoneyLeft { get; set; }

        public List<ExpenseItem> ExpenseItems { get; set; }

        public string UsersUrl { get; set; }
        public string CreateExpenseUrl { get; set; }
        public string FilterExpenseUrl { get; set; }
        public string ExportToExcelUrl { get; set; }
        #endregion

        #region Sub Classes
        public class ExpenseItem
        {
            public int? Id { get; set; }
            public string ExpenseCaption { get; set; }
            public string Amount { get; set; }

            public string UpdateUrl { get; set; }
            public string DeleteUrl { get; set; }
        }
        #endregion

    }
}