// Get all the keys
var keys = document.querySelectorAll('#calculator span');
var operators = ['+', '-', 'x', '/'];
var decimalAdded = false;

// Add onclick event
for(var i = 0; i < keys.length; i++) {
  keys[i].onclick = function(e) {
    // Get the input and button values
    var input = document.querySelector('.screen');
    var inputVal = input.innerHTML;
    var btnVal = this.innerHTML;

    //Erase all,if C clicked
    if(btnVal == 'C') {
      input.innerHTML = '';
      decimalAdded = false;
    }

    // If eval key is pressed, calculate and display the result
    else if(btnVal == '=') {
      var equation = inputVal;
      var lastChar = equation[equation.length - 1];

    //Replace x with *
      equation = equation.replace(/x/g, '*')

      //check last char of eq
      if(operators.indexOf(lastChar) > -1 || lastChar == '.')
        equation = equation.replace(/.$/, '');

      if(equation)
        input.innerHTML = eval(equation);

      decimalAdded = false;
    }

    else if(operators.indexOf(btnVal) > -1) {
      // Get the last character from the equation
      var lastChar = inputVal[inputVal.length - 1];

      // only add if input is not empty and operator not last char
      if(inputVal != '' && operators.indexOf(lastChar) == -1)
        input.innerHTML += btnVal;

      // Allow minus if the string is empty
      else if(inputVal == '' && btnVal == '-')
        input.innerHTML += btnVal;

      // Replace last operator with new clicked operator
      if(operators.indexOf(lastChar) > -1 && inputVal.length > 1) {
        input.innerHTML = inputVal.replace(/.$/, btnVal);
      }
      decimalAdded =false;
    }

    else if(btnVal == '.') {
      if(!decimalAdded) {
        input.innerHTML += btnVal;
        decimalAdded = true;
      }
    }
    else {
      input.innerHTML += btnVal;
    }
    e.preventDefault();
  }
}