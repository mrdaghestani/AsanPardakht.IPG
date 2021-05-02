var common = {
    toPersianNumber: function (input) {
        /* ۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹ */
        if (!input) return "";
        var str = $.trim(input.toString());
        if (!str) return "";
        return str.replace(/0/g, "۰")
            .replace(/1/g, "۱")
            .replace(/2/g, "۲")
            .replace(/3/g, "۳")
            .replace(/4/g, "۴")
            .replace(/5/g, "۵")
            .replace(/6/g, "۶")
            .replace(/7/g, "۷")
            .replace(/8/g, "۸")
            .replace(/9/g, "۹");
    },
    toEnglishNumber: function (input) {
        if (!input) return "";
        var str = $.trim(input.toString());
        if (!str) return "";
        return str.replace(/\u0660/g, "0").replace(/\u06F0/g, "0")
            .replace(/\u0661/g, "1").replace(/\u06F1/g, "1")
            .replace(/\u0662/g, "2").replace(/\u06F2/g, "2")
            .replace(/\u0663/g, "3").replace(/\u06F3/g, "3")
            .replace(/\u0664/g, "4").replace(/\u06F4/g, "4")
            .replace(/\u0665/g, "5").replace(/\u06F5/g, "5")
            .replace(/\u0666/g, "6").replace(/\u06F6/g, "6")
            .replace(/\u0667/g, "7").replace(/\u06F7/g, "7")
            .replace(/\u0668/g, "8").replace(/\u06F8/g, "8")
            .replace(/\u0669/g, "9").replace(/\u06F9/g, "9");
    },
    toCommaSeprated: function (input) {
        return input.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    },
    fromCommaSeprated: function (input) {
        return Number(input.replace(/,/g, ""));
    },
    toFormattedAmount: function (input) {
        return common.toPersianNumber(common.toCommaSeprated(input));
    },
    fromFormattedAmount: function (input) {
        return common.fromCommaSeprated(common.toEnglishNumber(input));
    },
    formatter: {
        init: function () {
            setInterval(function () {
                $('input.amount')
                    .not('.input-amount-inited')
                    .addClass('input-amount-inited')
                    .each(function () {
                        var item = $(this);
                        var cloned = item.clone().prop('id', '').prop('name', '').prop('type', 'text');

                        item.after(cloned);

                        item.prop('type', 'hidden');

                        cloned.keydown(function (e) {
                            var permittedKeys = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
                            permittedKeys.push(8); //BS
                            permittedKeys.push(46); //DEL
                            permittedKeys.push(35); //HOME
                            permittedKeys.push(36); //END
                            permittedKeys.push(116); //F5
                            permittedKeys.push(110); //DOT
                            permittedKeys.push(190); //DOT
                            permittedKeys.push(37); //LEFT
                            permittedKeys.push(38); //UP
                            permittedKeys.push(39); //RIGHT
                            permittedKeys.push(40); //DOWN
                            permittedKeys.push(9); //TAB
                            if (permittedKeys.indexOf(e.which) < 0 && !e.ctrlKey) {
                                console.log(e.which);
                                e.preventDefault();
                                return;
                            }

                            setTimeout(function () {
                                item.val(common.fromFormattedAmount(cloned.val()));
                                cloned.val(common.toFormattedAmount(item.val()));
                            }, 1);
                        });
                    });

                $('span.amount')
                    .each(function () {
                        var item = $(this);
                        var currentText = item.text();

                        var number = Number(currentText);

                        if (!isNaN(number)) {
                            if (number > 999)
                                item.text(common.toCommaSeprated(currentText));
                        }
                    });
            }, 75);
        }
    },
    ajaxForm: {
        init: function () {
            jQuery.validator.setDefaults({
                errorClass: 'invalid text-danger',
                errorElement: 'li',
                errorLabelContainer: '.error-place'
            });

            $('form.ajax-form:not(.inited)')
                .addClass('inited')
                .append($('<div/>').addClass('loader').html($('<div/>').addClass('spinner')))
                .submit(function (e) {
                    e.preventDefault();
                    var form = $(this);

                    common.ajaxForm.callEvent(form, 'prepareData');
                    if (!form.valid()) return;

                    var isSubmitting = form.data('isSubmitting');
                    if (isSubmitting) return;
                    form.data('isSubmitting', true);

                    var loaderIntervalId = setInterval(function (form) { form.find('div.loader .spinner').toggleClass('out'); }, 500, form);

                    common.ajaxForm.replaceButtonsTextToLoading(form);

                    common.ajaxForm.clearErrors(form);

                    var data = form.serialize();

                    var hasUpload = false;
                    if (form.attr('enctype') == 'multipart/form-data') {
                        hasUpload = true;
                        data = new FormData(this);
                    }

                    var progressListener = function (e) {
                        var percent = (100 * e.loaded) / e.total;
                        //var progressBar = form.find('.progress-bar');
                        //progressBar.html(percent.toFixed(2) + '%');
                        //progressBar.css('width', percent.toFixed(2) + '%');
                        common.ajaxForm.replaceButtonsTextToLoading(form, (' ' + percent.toFixed(2) + '%'));
                    };

                    $.ajax({
                        url: form.attr('action'),
                        data: data,
                        type: form.attr('method'),
                        beforeSend: function () {
                            //console.log(data);
                        },
                        success: function (data) {
                            common.ajaxForm.showErrors(form, data);
                            common.ajaxForm.callEvent(form, 'success', data);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            common.ajaxForm.showErrors(form, { error: 'خطای نامشخص، لطفاً با پشتیبانی تماس بگیرید' });
                            common.ajaxForm.callEvent(form, 'error');
                            console.log('error');
                        },
                        complete: function (jqXHR, textStatus) {
                            form.data('isSubmitting', false);
                            common.ajaxForm.replaceButtonsTextToPrevTexts(form);
                            clearInterval(loaderIntervalId);
                            common.ajaxForm.callEvent(form, 'complete');
                            console.log('complete');
                        },
                        xhr: function () {
                            var xhr = $.ajaxSettings.xhr();
                            if (hasUpload && xhr.upload) {
                                xhr.upload.addEventListener('progress', progressListener, false);
                            }
                            return xhr;
                        },
                        cache: false,
                        contentType: (hasUpload ? false : undefined),
                        processData: false
                    });
                });
        },
        callEvent: function (form, funcName) {
            var func = form.data(funcName);

            if (typeof func == 'string') {
                eval(func);
                return;
            }

            var args = [];
            for (var i = 2; i < arguments.length; i++) {
                args.push(arguments[i]);
            }
            if (func) func.apply(null, args);
        },
        replaceButtonsTextToLoading: function (form, moreInfo) {
            form.find('button[type="submit"]').each(function () {
                console.log('caaaa');
                var btn = $(this);
                if (!btn.data('innerHtml'))
                    btn.data('innerHtml', btn.html());
                btn.html('در حال ارسال' + (moreInfo ? moreInfo : '')).addClass('disabled').prop('disabled', true);
            });
        },
        replaceButtonsTextToPrevTexts: function (form) {
            form.find('button[type="submit"]').each(function () {
                var btn = $(this);
                btn.html(btn.data('innerHtml')).removeClass('disabled').prop('disabled', false);
                btn.data('innerHtml', false);
            });
        },
        clearErrors: function (form) {
            var errorPlace = form.find('.server-errors');
            if (errorPlace.length < 1) errorPlace = $('.server-errors');
            if (errorPlace.length > 0)
                errorPlace.html('');
        },
        showErrors: function (form, data) {
            var errorPlace = form.find('.server-errors');
            if (errorPlace.length < 1) errorPlace = $('.server-errors');
            if (errorPlace.length > 0 && data) {
                if (data.error) {
                    errorPlace.append($('<li />').addClass('invalid text-danger').text(data.error));
                }
                if (data.errors) {
                    for (var i = 0; i < data.errors.length; i++) {
                        errorPlace.append($('<li />').addClass('invalid text-danger').text(data.errors[i]));
                    }
                }
            }
        }
    },
    form: {
        init: function () {
            $('input[type="password"],input[type="text"]')
                .not('.form-inited')
                .addClass('form-inited')
                .each(function () {
                    var input = $(this);
                    var formGroup = input.closest('.form-group');
                    var controls = $('<div/>').addClass('controls');

                    if (input.hasClass('ltr'))
                        formGroup.addClass('input-ltr');

                    var setClearState = function () {
                        if (input.val() == '' || input.prop('readonly'))
                            formGroup.find('.fa-times-circle').css('display', 'none');
                        else
                            formGroup.find('.fa-times-circle').css('display', 'inline-block');
                    };

                    if (input.attr('type') == 'password')
                        controls.append($('<i/>').addClass('fas fa-eye-slash')
                            .click(function () {
                                if (input.attr('type') == 'password')
                                    input.attr('type', 'text');
                                else
                                    input.attr('type', 'password');
                                $(this).toggleClass('fa-eye-slash');
                                $(this).toggleClass('fa-eye');
                            }));

                    controls.append($('<i/>').addClass('far fa-times-circle')
                        .click(function () {
                            input.val('');
                            setClearState();
                            input.focus();
                        }));
                    formGroup.append(controls);
                    input.keypress(setClearState).keydown(setClearState).keyup(setClearState).blur(setClearState).focus(setClearState);
                });
        }
    }
};
$(document).ready(function () {
    common.form.init();
});