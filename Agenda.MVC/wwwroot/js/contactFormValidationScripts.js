jQuery.validator.addMethod("telephone-number",
  function (value, element, param) {
    var currentRow = $(element).attr('name').split("[")[1][0];
    var telephoneType = $('#Telephones_' + currentRow + '__Type :selected').text();

    if (telephoneType.localeCompare('Landline') == 0)
      return /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) [2-8][0-9]{3}\-[0-9]{4}$/.test(value);

    if (telephoneType.localeCompare('Cellphone') == 0)
      return /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) 9[1-9][0-9]{3}\-[0-9]{4}$/.test(value);

    if (telephoneType.localeCompare('Commercial') == 0)
      return /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$/.test(value);
     
    return false;
  }
);

jQuery.validator.unobtrusive.adapters.addBool("telephone-number");
