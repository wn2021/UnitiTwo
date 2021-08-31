function onIDCardsValidation(e) {
    if (e.isValid) {
        var pattern = /\d*/;
        if (e.value.length < 15 || e.value.length > 18 || pattern.test(e.value) == false) {
            e.errorText = "必须输入15~18位数字";
            e.isValid = false;
        }
    }
}