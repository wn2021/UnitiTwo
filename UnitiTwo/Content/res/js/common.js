
$(function () {
    document.body.style.visibility = "visible";
});


function saveGrid(grid, options) {

    if (typeof options == "string") options = { url: options };
    options = $.extend({
        callback: function (success, result) {
        }
    }, options);

    var data = grid.getChanges();
    var json = mini.encode(data);
    grid.loading("保存中，请稍后......");
    $.ajax({
        url: options.url,
        data: { data: json },
        type: "post",
        success: function (text) {
            var result = mini.decode(text),
                success = result.success !== false; // { success: true, message: "" }

            if (options.callback(success, result) !== false) {
                if (success) {
                    alert("保存成功");
                    grid.reload();
                } else {
                    alert("Server Error\n\n" + result.message + "\n\n" + result.stackTrace);
                }
            }

            if (options.success) options.success();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (options.callback(false) !== false) {
                alert("HTTP Error\n\n" + jqXHR.responseText);
            }
        },
        complete: function () {
            grid.unmask();
        }
    });
}

function saveForm(form, options) {
    if (typeof form == "string") form = new mini.Form(form);

    if (typeof options == "string") options = { url: options };
    options = $.extend({
        callback: function (success) {
        }
    }, options);

    form.validate();
    if (form.isValid() == false) return;

    var values = form.getData();
    var json = mini.encode([values]);
    $.ajax({
        url: options.url,
        type: 'post',
        data: { data: json },
        cache: false,
        success: function (text) {
            options.callback(true);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert(jqXHR.responseText);
            options.callback(false);
        }
    });
}


(function () {
    
    var proto = mini.ComboBox.prototype,
        set = proto.set;
    
    proto.set = function(values){
        if(!this._inited){
            this._inited = true;

            //values.ajaxOptions = $.extend({cache: true}, values.ajaxOptions);            
            init(this);
        }
        set.apply(this, arguments);
    }

    function init(me){
        
    }
    
})();

function onEnglishValidation(e) {
    if (e.isValid) {
        if (isEnglish(e.value) == false) {
            e.errorText = "必须输入英文";
            e.isValid = false;
        }
    }
}
function onEnglishAndNumberValidation(e) {
    if (e.isValid) {
        if (isEnglishAndNumber(e.value) == false) {
            e.errorText = "必须输入英文+数字";
            e.isValid = false;
        }
    }
}
function onChineseValidation(e) {
    if (e.isValid) {
        if (isChinese(e.value) == false) {
            e.errorText = "必须输入中文";
            e.isValid = false;
        }
    }
}
function onIDCardsValidation(e) {
    if (e.isValid) {
        var pattern = /\d*/;
        if (e.value.length < 15 || e.value.length > 18 || pattern.test(e.value) == false) {
            e.errorText = "必须输入15~18位数字";
            e.isValid = false;
        }
    }
}
////////////////////////////////////
/* 是否英文 */
function isEnglish(v) {
    var re = new RegExp("^[a-zA-Z\_]+$");
    if (re.test(v)) return true;
    return false;
}
/* 是否英文+数字 */
function isEnglishAndNumber(v) {

    var re = new RegExp("^[0-9a-zA-Z\_]+$");
    if (re.test(v)) return true;
    return false;
}
/* 是否汉字 */
function isChinese(v) {
    var re = new RegExp("^[\u4e00-\u9fa5]+$");
    if (re.test(v)) return true;
    return false;
}
/*自定义vtype*/
mini.VTypes["englishErrorText"] = "请输入英文";
mini.VTypes["english"] = function (v) {
    var re = new RegExp("^[a-zA-Z\_]+$");
    if (re.test(v)) return true;
    return false;
}



