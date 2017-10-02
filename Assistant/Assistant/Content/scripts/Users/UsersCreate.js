$(function () {
    var avatarBase64;

    $('.apply-numeric-input').numericInput({ allowFloat: true });

    $('.apply-date-picker').datepicker({ dateFormat: globals.formats.jquerUIDatePickerFormat });

    $('#avatar')
        .change(function () {
            AjaxLoader.ShowLoader();
            globals.readFileAsBase64(this).then(function (base64) {
                new Promise(function (resolve, reject) {
                    avatarBase64 = base64;
                    resolve();
                }).then(function () {
                    AjaxLoader.HideLoader();
                });
            });
        });

    $('#save-new-user').click(function () {
            var firstName = $('#first-name').val();
            var lastName = $('#last-name').val();
            var birthDate = $('#birth-date').val();
            var budget = $('#budget').val();

            $.ajax({
                type: 'POST',
                url: saveUrl,
                data: {
                    FirstName: firstName,
                    LastName: lastName,
                    BirthDate: birthDate,
                    Budget: budget,
                    AvatarBase64: avatarBase64
                },
                beforeSend: function () {
                    AjaxLoader.ShowLoader();
                },
                dataType: 'json',
                success: function (response) {
                    if (response.IsSuccess) {
                        $('#first-name').val('');
                        $('#last-name').val('');
                        $('#birth-date').val('');
                        $('#budget').val('');
                        $('#avatar').val('');
                        $('#clear-avatar').trigger('click');
                        avatarBase64 = null;


                        toastrNotification.init(globals.textSuccess).showMessage();
                        toastrNotification.init('You can create new user or back to <a href="' + usersUrl + '"><b>Users Page</b></a>', { notificationType: toastrNotification.types.info, timeOut: '10000' }).showMessage();

                    } else {
                        toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
                    }
                },
                error: function (response) {
                    toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
                },
                complete: function () {
                    AjaxLoader.HideLoader();
                }
            });
            return false;
        });


});