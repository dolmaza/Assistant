$(function () {
    if (hasAvatar == 'True') {
        Popover.init({ imageName: 'avatar picture', imageFile: avatarBase64 }).show();
    }

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
                        toastrNotification.init(globals.textSuccess).showMessage();
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