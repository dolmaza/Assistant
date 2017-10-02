$(function () {
    $('.apply-numeric-input').numericInput({ allowFloat: true });

    $('#create-expense').click(function () {
        var dialog = bootbox.dialog({
            title: 'Expense',
            message: $('#expense-create-update-form .row').html(),
            buttons: [
                {
                    label: "Cancel",
                    className: 'btn-default',
                    callback: function () {
                        bootbox.hideAll();
                        setDefaultValues();
                    }
                },
                {
                    label: "Save",
                    className: 'btn-success',
                    callback: function () {
                        
                        createExpense(dialog, createUrl);
                    }
                }
            ]
        });

        dialog.init(function () {
            $('.apply-numeric-input').numericInput({ allowFloat: true });

        });
    });

    $('.update-expense').click(function () {
        var url = $(this).attr('href');
        var expenseCaption = $(this).attr('data-expense-caption');
        var amount = $(this).attr('data-amount');
        var html = $('#expense-create-update-form .row').html();
        
        var dialog = bootbox.dialog({
            title: 'Expense',
            message: html,
            buttons: [
                {
                    label: "Cancel",
                    className: 'btn-default',
                    callback: function () {
                        bootbox.hideAll();
                        setDefaultValues();
                    }
                },
                {
                    label: "Save",
                    className: 'btn-success',
                    callback: function () {
                        updateExpense(dialog, url);
                    }
                }
            ]
        });

        dialog.init(function () {
            dialog.find('#expense-caption').val(expenseCaption);
            dialog.find('#amount').val(amount);
            $('.apply-numeric-input').numericInput({ allowFloat: true });

        });

        return false;
    });

    $(".delete-btn").click(function () {
        var url = $(this).attr("href");

        bootbox.confirm(globals.textConfirmDelete,
            function (result) {
                if (result) {
                    window.location = url;
                }
            });

        return false;
    });

    $("#from, #to").datepicker({
        dateFormat: globals.formats.jquerUIDatePickerFormat,
        changeMonth: false,
        numberOfMonths: 1,
        onSelect: function (selectedDate) {
            if (this.id == 'from') {
                var dateMin = $('#from').datepicker("getDate");
                var rMin = new Date(dateMin.getFullYear(), dateMin.getMonth(), dateMin.getDate() + 1); 
                var rMax = new Date(dateMin.getFullYear(), dateMin.getMonth(), dateMin.getDate() + 31);
                $('#to').datepicker("option", "minDate", rMin);
                $('#to').datepicker("option", "maxDate", rMax);
            }
            filterExpenses(filterExpenseUrl);
        }
    });

    $('#from, #to').change(function () {
        filterExpenses(filterExpenseUrl);
    });

});

function createExpense(dialog, url, expenseId) {
    var expenseCaption = dialog.find('#expense-caption').val();
    var amount = dialog.find('#amount').val();
    
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            ExpenseCaption: expenseCaption,
            Amount: amount
        },
        dataType: 'json',
        success: function (response) {
            if (response.IsSuccess) {
                window.location = document.URL;
                toastrNotification.init(globals.textSuccess).showMessage();
            } else {
                toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
            }
        },
        error: function (response) {
            toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
        },
        complete: function () {
            setDefaultValues();
        }
    });
}

function updateExpense(dialog, url) {
    var expenseCaption = dialog.find('#expense-caption').val();
    var amount = dialog.find('#amount').val();

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            ExpenseCaption: expenseCaption,
            Amount: amount
        },
        dataType: 'json',
        success: function (response) {
            if (response.IsSuccess) {
                window.location = document.URL;
                toastrNotification.init(globals.textSuccess).showMessage();
            } else {
                toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
            }
        },
        error: function (response) {
            toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
        },
        complete: function () {
            setDefaultValues();
        }
    });
}

function filterExpenses(url) {
    var from = $('#from').val();
    var to = $('#to').val();

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            startDate: from,
            endDate: to
        },
        beforeSend: function () {
            $('#expneses-box').ShowLoader();
        },
        dataType: 'json',
        success: function (response) {
            if (response.IsSuccess) {
                var data = response.Data;
                
                initFilteredList(data.filteredExpenses.ExpenseItems);
                $('#budget').text(data.filteredExpenses.UserBudget+' ₾');
                $('#expense-sum').text(data.filteredExpenses.ExpensesSum + ' ₾');
                $('#money-left').text(data.filteredExpenses.MoneyLeft + ' ₾');
                
            } else {
                toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
            }
        },
        error: function (response) {
            toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
        },
        complete: function () {
            $('#expneses-box').HideLoader();
        }
    });

}


function initFilteredList(expenses) {
    var expensesHtml = $('#expenses-template').html();
    var compiledExpensesTemplate = Template7.compile(expensesHtml);
    var html = compiledExpensesTemplate({ ExpenseItems: expenses });
    $('#expenses-body').empty();
    $('#expenses-body').append(html);
}

function setDefaultValues() {
    $('#expense-caption').val('');
    $('#amount').val('');
}