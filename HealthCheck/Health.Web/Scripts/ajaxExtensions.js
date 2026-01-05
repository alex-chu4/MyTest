(function ($) {
    $.extend({
        ajaxPost: function (url, data, callback) {
            return $.ajax({
                url: url,
                type: "post",
                async: true,
                cache: false,
                contentType: "application/json",
                data: JSON.stringify(data),
                dateType: "json",
                success: function (result) {
                    if (callback) {
                        callback(result);
                    }
                }
            });
        },
        ajaxGet: function (url, data, callback) {
            return $.ajax({
                url: url,
                type: "get",
                async: true,
                cache: false,
                data: data,
                dataType: "json",
                success: function (result) {
                    if (callback) {
                        callback(result);
                    }
                }
            });
        }
    });
})(jQuery);
