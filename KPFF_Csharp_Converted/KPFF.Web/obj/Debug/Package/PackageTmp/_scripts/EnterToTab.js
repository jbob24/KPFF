$(document).ready(function () {
    // get only input tags with class textBox
    textboxes = $("input.textBox");
    // now we check to see which browser is being used
    if ($.browser.mozilla) {
        $(textboxes).keypress(checkForEnter);
        //$("body").keypress(function () { return false; });
    } else {
        $(textboxes).keydown(checkForEnter);
        //$("body").keydown(function () { return false; });
    }
});

function checkForEnter(event) {
    if (event.keyCode == 13) {
        currentBoxNumber = textboxes.index(this);

        if (event.shiftKey) {
            if (currentBoxNumber == 0) {
                currentBoxNumber = textboxes.length;
            }

            if (textboxes[currentBoxNumber - 1] != null) {
                nextBox = textboxes[currentBoxNumber - 1];
            }
        }
        else {
            if (currentBoxNumber == textboxes.length - 1) {
                currentBoxNumber = -1;
            }
            if (textboxes[currentBoxNumber + 1] != null) {
                nextBox = textboxes[currentBoxNumber + 1]
            }
        }

        if (nextBox) {
            nextBox.focus();
            nextBox.select();
            event.preventDefault();
            return false;
        }
    }
} 