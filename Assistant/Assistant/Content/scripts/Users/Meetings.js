$(function () {
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
            filterMeetings(filterMeetingUrl);
        }
    });

    $('#meetings-table').dataTable({
        "aaSorting": [],
        "columnDefs": [{
            "targets": [-1, -2],
            "orderable": false
        }]
    });

    $('#create-meeting').click(function () {
        var dialog = bootbox.dialog({
            title: 'Meeting',
            message: $('#meeting-create-update-form').html(),
            buttons: [
                {
                    label: "Cancel",
                    className: 'btn-default',
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                {
                    label: "Save",
                    className: 'btn-success',
                    callback: function () {
                        createMeeting(dialog, createUrl);
                    }
                }
            ]
        });

        dialog.init(function () {
            dialog.find('.apply-date-time-picker').datetimepicker({
                dateFormat: globals.formats.jquerUIDatePickerFormat
            });
        });

        return false;
    });

    $('.update-meeting').click(function () {
        var html = $('#meeting-create-update-form').html();
        var url = $(this).attr('href');
        var firstname = $(this).attr('data-person-firstname');
        var lastname = $(this).attr('data-person-lastname');
        var meetingTime = $(this).attr('data-meeting-time');
        var meetingStatusId = $(this).attr('data-status-id');

        var dialog = bootbox.dialog({
            title: 'Meeting',
            message: html,
            buttons: [
                {
                    label: "Cancel",
                    className: 'btn-default',
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                {
                    label: "Save",
                    className: 'btn-success',
                    callback: function () {
                        updateMeeting(dialog, url);
                    }
                }
            ]
        });

        dialog.init(function () {
            dialog.find('#firstname').val(firstname);
            dialog.find('#lastname').val(lastname);
            dialog.find('.meeting-time').val(meetingTime);
            dialog.find('#status').val(meetingStatusId);

            dialog.find('.apply-date-time-picker').datetimepicker({
                dateFormat: globals.formats.jquerUIDatePickerFormat
            });
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


    $('#from, #to, #filter-meeting-status-id').change(function () {
        filterMeetings(filterMeetingUrl);
    });

    $('#clear-filter').click(function () {
        $('#from').val('');
        $('#to').val('');
        $('#filter-meeting-status-id').val(null);
        filterMeetings(filterMeetingUrl);

    });
});

function createMeeting(dialog, url) {
    var personFirstname = dialog.find('#firstname').val();
    var personLastname = dialog.find('#lastname').val();
    var meetingTime = dialog.find('.meeting-time').val();
    var meetingStatusId = dialog.find('#status').val();

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            MeetingStatusId: meetingStatusId,
            PersonFirstName: personFirstname,
            PersonLastName: personLastname,
            MeetingTime: meetingTime
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
        }
    });
}

function updateMeeting(dialog, url) {
    var personFirstname = dialog.find('#firstname').val();
    var personLastname = dialog.find('#lastname').val();
    var meetingTime = dialog.find('.meeting-time').val();
    var meetingStatusId = dialog.find('#status').val();

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            MeetingStatusId: meetingStatusId,
            PersonFirstName: personFirstname,
            PersonLastName: personLastname,
            MeetingTime: meetingTime
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
        }
    });
}

function filterMeetings(url) {
    var from = $('#from').val();
    var to = $('#to').val();
    var meetingStatusId = $('#filter-meeting-status-id').val();

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            meetingStatusId: meetingStatusId,
            startDate: from,
            endDate: to
        },
        beforeSend: function () {
            $('#meetings-box').ShowLoader();
        },
        dataType: 'json',
        success: function (response) {
            if (response.IsSuccess) {
                var data = response.Data;
                initFilteredList(data.filteredMeetings.MeetingItems);
            } else {
                toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
            }
        },
        error: function (response) {
            toastrNotification.init(globals.textAbort, { notificationType: toastrNotification.types.error }).showMessage();
        },
        complete: function () {
            $('#meetings-box').HideLoader();
        }
    });

}

function initFilteredList(meetings) {
    var meetingsHtml = $('#meetings-template').html();
    var compiledMeetingsTemplate = Template7.compile(meetingsHtml);
    var html = compiledMeetingsTemplate({ MeetingItems: meetings });
    $('#meetings-body').empty();
    $('#meetings-body').append(html);
}
