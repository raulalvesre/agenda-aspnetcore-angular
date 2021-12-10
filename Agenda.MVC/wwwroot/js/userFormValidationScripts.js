//NAME NOT NULL OR EMPTY
jQuery.validator.addMethod("name-not-null-or-empty",
  function (value, element, param) {
    return value && value.trim();
  }
);

jQuery.validator.unobtrusive.adapters.addBool("name-not-null-or-empty");

//RIGHT MINIMUM LENGTH PASSWORD
jQuery.validator.addMethod("password-with-correct-minimum-length",
  function (value, element, param) {
    return value.length >= 6;
  }
);

jQuery.validator.unobtrusive.adapters.addBool("password-with-correct-minimum-length");

//PASSWORD WITH AT LEAST ONE NUMBER
jQuery.validator.addMethod("password-with-at-least-one-number",
  function (value, element, param) {
    return /\d/.test(value);
  }
);

jQuery.validator.unobtrusive.adapters.addBool("password-with-at-least-one-number");

//RIGHT MINIMUM LENGTH USERNAME
jQuery.validator.addMethod("username-with-correct-minimum-length",
  function (value, element, param) {
    return value.length >= 3;
  }
);

jQuery.validator.unobtrusive.adapters.addBool("username-with-correct-minimum-length");
