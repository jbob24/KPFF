// Function to scroll to top before sorting to fix an IE bug
// Which repositions the header off the top of the screen
// if you try to sort while scrolled to bottom.
function GoTop() {
    document.getElementById('hoursGridDiv').scrollTop = 0;
}

// For those browsers that fully support the CSS :hover pseudo class the "table.scrollTable tr:hover" definition above 
// will work fine, but Internet Explorer 6 only supports "hover" for "<a>" tag elements, so we need to use the following 
// JavaScript to mimic support (at least until IE7 comes out, which does support "hover" for all elements)

// Create a JavaScript function that dynamically assigns mouseover and mouseout events to all 
// rows in a table which is assigned the "scrollTable" class name,  in order to simulate a "hover" 
// effect on each of the tables rows
//HoverRow = function () {

//    // If an IE browser
//    if (document.all) {
//        var table_rows = 0;

//        // Find the table that uses the "scrollTable" classname
//        var allTableTags = document.getElementsByTagName("table");
//        for (i = 0; i < allTableTags.length; i++) {
//            // If this table uses the "scrollTable" class then get a reference to its body and first row
//            if (allTableTags[i].className == "scrollTable") {
//                table_body = allTableTags[i].getElementsByTagName("tbody");
//                table_rows = table_body[0].getElementsByTagName("tr");
//                i = allTableTags.length + 1; // Force an exit from the loop - only interested in first table match
//            }
//        }

//        // For each row add a onmouseover and onmouseout event that adds, then remove the "hoverMe" class
//        // (but leaving untouched all other class assignments) to the row in order to simulate a "hover"
//        // effect on the entire row
//        for (var i = 0; i < table_rows.length; i++) {
//            // ignore rows with the title and total class assigned to them
//            if (table_rows[i].className != "title" && table_rows[i].className != "total") {
//                table_rows[i].onmouseover = function () { this.className += " hoverMe"; }
//                table_rows[i].onmouseout = function () { this.className = this.className.replace(new RegExp(" hoverMe\\b"), ""); }
//            }
//        } // End of for loop

//    } // End of "If an IE browser"

//}
//// If this browser suports attaching events (IE) then make the HoverRow function run on page load
//// Hote: HoverRow has to be re-run each time the table gets sorted
//if (window.attachEvent) window.attachEvent("onload", HoverRow);