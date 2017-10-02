var globals = {
    textAbort: null,
    textSuccess: null,
    textConfirmDelete: "Do you really want to delete?",

    formats: {
        jquerUIDatePickerFormat: 'dd M yy'
    },

    slugify: function (text) {
        return text.toString().toLowerCase()
            .replace(/\s+/g, '-')           // Replace spaces with -
            .replace(/[^\w\-]+/g, '')       // Remove all non-word chars
            .replace(/\-\-+/g, '-')         // Replace multiple - with single -
            .replace(/^-+/, '')             // Trim - from start of text
            .replace(/-+$/, '');            // Trim - from end of text
    },

    readFileAsBase64: function (_this) {
        return new Promise(function (resolve, reject) {
            if (_this.files && _this.files[0]) {
                var fileReader = new FileReader();
                fileReader.onload = function (e) {
                    if (resolve) {
                        resolve(e.target.result);
                    }
                };

                fileReader.onerror = function (errorText) {
                    reject(errorText);
                };

                fileReader.readAsDataURL(_this.files[0]);
            }
        });
    }
};

