function PopulateSelect(controlId, items) {
    $(controlId).empty();
    items.forEach(function (item) {
        const template = `<option value=${item.Id}>${item.Name}</option>`;
        $(controlId).append(template);
    });
}

function GetDateFromJson(value) {
    const date = new Date(parseInt(value.substr(6)));
    return moment(date).format('DD.MM.YYYY');
}

function ValidateForm(formId) {
    const errItems = [];
    $(formId).find('.required').each(function (i, item) {
        const itemToValidate = $(item).attr('for');
        const itemName = $(item).find('label').text();
        switch (itemToValidate) {
            case 'input':
                const inputId = $(item).find('input').attr('id');
                if (!ValidateInput(inputId))
                    errItems.push(itemName);
                break;
            case 'date':
                if (!ValidateDate(item))
                    errItems.push(itemName);
                break;
        }
    });
    if (errItems.length > 0)
    {
        const res = errItems.join('<br/>');
        swal.fire(`Обязательно к заполнению:<br/>${res}`,'', 'error');
    }
    return errItems.length == 0;
}

function ValidateInput(controlId) {
    const $control = $(`#${controlId}`);
    let result;
    // проверим, что это не typeahead
    if ($control.hasClass('typeahead')) {
        const id = $control.attr('data-id');
        if (!id)
            $control.addClass('is-invalid');
        result = id != undefined;
    }
    else {
        const len = $control.val().length;
        if (len == 0)
            $control.addClass('is-invalid');
        result = len != 0;
    }
    $control.unbind('change');
    $control.on('change', function () {
        if ($control.hasClass('is-invalid'))
            $control.removeClass('is-invalid');
    });
    return result;
}

function ValidateDate(control) {
    const $control = $(control).find('input');
    const len = $control.val().length;
    if (len == 0) {
        $control.addClass('is-invalid');
    }
    $control.unbind('change');
    $control.on('change', function () {
        $control.removeClass('is-invalid');
    });
    return len != 0;
}