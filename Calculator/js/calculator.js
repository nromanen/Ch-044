

    var firstDigit = '0';
    var secondDigit = '0';
    var operation = '';
    var inputField = document.getElementById('inputField');
    var additionField = document.getElementById('additionField')
    var buttons = document.getElementsByTagName('button');
    var valueStr;
      /*function MakeEvents()
      {
        for (var i=0; i<buttons.length;i++)
          {
            if ( !isNaN(buttons[i].innerHTML) )
            {
              valueStr = buttons[i].innerHTML;
              buttons[i].addEventListener("click", function(valueStr2){ PutDigit(valueStr2); }, false);
            }
          }
      }*/
      function SetOperation(op)
      {
        operation = op;
        DisplayData();
      }

      function ChangeSign()
      {
        if (operation != '')
          {
            if (secondDigit[0]=='-')
              secondDigit = secondDigit.substr(1, secondDigit.length);
            else {
              secondDigit = '-' + secondDigit;
            }
          }
          else {
            if (firstDigit[0]=='-')
              firstDigit = firstDigit.substr(1, firstDigit.length);
            else {
              firstDigit = '-' + firstDigit;
            }

          }
          DisplayData();
      }
      function DisplayData()
      {
        var mainStr = "";
        var addStr = ""
        if (operation != '')
          {
            mainStr = parseFloat(secondDigit);
            if (secondDigit.indexOf('.') != -1 && (parseFloat(mainStr) - Math.trunc(parseFloat(mainStr)) ) == 0)
            {
              mainStr += secondDigit.substr(secondDigit.indexOf('.'), secondDigit.length);
            }

            if (secondDigit.indexOf('.') != -1 && ( secondDigit[secondDigit.length -1]) == '0')
            {
              var endPointer = secondDigit.length - 1;
              var flag = true;
              while (flag)
              {
                if (secondDigit[endPointer]=='0')
                {
                  mainStr += '0';
                  endPointer--;
                }
                else {
                  flag = false;
                }
              }
            }

            addStr = parseFloat(firstDigit);
            addStr += operation;

          }
        else
        {
          addStr = "";
          mainStr = parseFloat(firstDigit);
          if (firstDigit.indexOf('.') != -1 && (parseFloat(mainStr) - Math.trunc(parseFloat(mainStr)) ) == 0)
          {
            mainStr += firstDigit.substr(firstDigit.indexOf('.'), firstDigit.length);
          }
          else
          if (firstDigit.indexOf('.') != -1 && ( firstDigit[firstDigit.length -1]) == '0')
          {
            console.log("!!!");
            var endPointer = firstDigit.length - 1;
            var flag = true;
            while (flag)
            {
              if (firstDigit[endPointer]=='0')
              {
                mainStr += '0';
                endPointer--;
              }
              else {
                flag = false;
              }
            }
          }
        }

        NormalizeField(mainStr.toString());
        inputField.innerHTML = mainStr;
        additionField.innerHTML = addStr;
      }
      function Clear()
      {
          operation = '';
          firstDigit='0';
          secondDigit='0';
          DisplayData();
      }
      function ClearLastNumber()
      {
        if (operation!='')
          secondDigit = '0';
        else
          firstDigit = '0';
        DisplayData();
      }
      function PutZero()
      {
        if (operation != '')
        {
          //if (secondDigit[1] != '0' || (secondDigit[0]))
            secondDigit += '0';
          /*else
            if (secondDigit.indexOf(".") != -1)
              secondDigit += '0';*/
        }
        else
        {
          //if (firstDigit[1] != '0')
            firstDigit += '0';
          /*else
            if (firstDigit.indexOf(".") != -1)
              firstDigit += '0';*/
        }
        DisplayData();
      }
      function PutPoint()
      {
        if (operation != '')
        {
          if (secondDigit.indexOf('.') == -1)
            secondDigit += '.';
        }
        else
        {
          if (firstDigit.indexOf('.') == -1)
            firstDigit += '.';
        }
        DisplayData();
      }
      function DeleteCharacter()
      {
        if (operation != '')
        {
            if (secondDigit!='0')
              secondDigit = secondDigit.substring(0, secondDigit.length - 1);
        }
        else {
          if (firstDigit!='0')
          {
            firstDigit = firstDigit.substring(0, firstDigit.length - 1);
          }
        }
        DisplayData();
      }


      function PutDigit(digit)
      {
        if (operation == '')
          {
            firstDigit += digit;
          }
        else
          {
            secondDigit += digit;
          }
          DisplayData(digit);
      }

      function CalculateResult()
      {
       if (operation == '')
        return;
       var firstNumber = parseFloat(firstDigit);
       var secondNumber = parseFloat(secondDigit);
       var result = 0;
       switch (operation) {
         case '+':
            result = firstNumber + secondNumber;
            break;
         case '-':
            result = firstNumber - secondNumber;
            break;
         case '*':
            result = firstNumber * secondNumber;
            break;
         case '/':
            result = firstNumber / secondNumber;
            break;
         default:
            console.log('Some error happened');
       }

       firstDigit = result.toString();

       secondDigit = '0';
       operation = '';
       DisplayData();
       additionField.innerHTML = "";
       if (firstDigit == "Infinity")
        firstDigit = '0';
      }
      function NormalizeField(str){
        if (str.length >= 17)
        {
          var resultSize = 30 - str.length * 2 / 4;
        }
        else {
          resultSize = 30;
        }
         inputField.style.fontSize = resultSize.toString() + "px";
      }
