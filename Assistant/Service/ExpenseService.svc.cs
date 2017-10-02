using Core.Entities;
using Core.Repository;
using Service.Models;
using Service.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ExpenseService : BaseService, IExpenseService
    {
        private IRepository<Expense> _expenseRepository;

        public ExpenseService()
        {
            _expenseRepository = GetRepository<Expense>();
        }

        public int? CreateExpense(ExpenseServiceModel model)
        {
            var expense = new Expense
            {
                UserId = model.UserId,
                ExpenseCaption = model.ExpenseCaption,
                Amount = model.Amount
            };

            _expenseRepository.Add(expense);
            _expenseRepository.Complete();

            if (_expenseRepository.IsError)
            {
                throw new Exception(Resources.TextAbort);
            }

            return expense.Id;
        }

        public void DeleteExpense(int? Id)
        {
            var expense = _expenseRepository.GetByID(Id);

            if (expense == null)
            {
                throw new Exception(Resources.ValidationExpenseNotFound);
            }
            else
            {
                _expenseRepository.Remove(expense);
                _expenseRepository.Complete();

                if (_expenseRepository.IsError)
                {
                    throw new Exception(Resources.TextAbort);
                }
            }
        }

        public IEnumerable<ExpenseServiceModel> GetAllExpenses(int? userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _expenseRepository.Get(filter: e => e.UserId == userId
                                                && ((startDate.Value == null && endDate.Value == null)
                                                || (startDate.Value != null && endDate.Value == null && e.CreateTime.Value >= startDate.Value)
                                                || (startDate.Value == null && endDate.Value != null && e.CreateTime.Value <= endDate.Value)
                                                || (startDate.Value != null && endDate.Value != null && e.CreateTime.Value >= startDate.Value && e.CreateTime.Value <= endDate.Value))
                                                ,
                                            orderBy: ob => ob.OrderByDescending(e => e.CreateTime))
                                    .Select(e => new ExpenseServiceModel
                                    {
                                        Id = e.Id,
                                        UserId = e.UserId,
                                        ExpenseCaption = e.ExpenseCaption,
                                        Amount = e.Amount,
                                        CreateTime = e.CreateTime
                                    }).ToList();
        }

        public void UpdateExpense(ExpenseServiceModel model)
        {
            var expense = _expenseRepository.GetByID(model.Id);

            if (expense == null)
            {
                throw new Exception(Resources.ValidationExpenseNotFound);
            }
            else
            {
                expense.ExpenseCaption = model.ExpenseCaption;
                expense.Amount = model.Amount;

                _expenseRepository.Update(expense);
                _expenseRepository.Complete();

                if (_expenseRepository.IsError)
                {
                    throw new Exception(Resources.TextAbort);
                }
            }
        }
    }
}
