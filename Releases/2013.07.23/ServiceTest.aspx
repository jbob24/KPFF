<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceTest.aspx.cs" Inherits="KPFF.PMP.ServiceTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link rel="stylesheet" type="text/css" href="Style.css">
    <script src="_scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            restGetTest();

            $('#submitbtn').click(function () { restPostTest(); } );
        });

        function restGetTest() {
            $.ajax
            (
                {
                    type: 'GET',
                    url: 'Service/Service1.svc/GetData',
                    dataType: 'json',
                    data: 'id=21',
                    success: function (response, type, xhr) {
                        //window.alert('Name: ' + response.Name + ' & HoursPerWeek: ' + response.HoursPerWeek);
                        //var markup = '<span>' + response.Name + '</span><br /><span>' + response.HoursPerWeek + '</span>'
                        //$('#employeeData').html(markup);
                        buildEmployeeData(response);
                    },
                    error: function (xhr) {
                        window.alert('error: ' + xhr.statusText);
                    }
                }
            );
        };

        function restPostTest() {
            $.ajax
            (
                {
                    type: 'POST',
                    url: 'Service/Service1.svc/DoWork',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: '{ "request": { "Id": "21"} }',
                    success: function (response, type, xhr) {
                        //window.alert('Name: ' + response.DoWorkResult.Name + ' & HoursPerWeek: ' + response.DoWorkResult.HoursPerWeek);
                        thankyou(response.DoWorkResult);
                    },
                    error: function (xhr) {
                        window.alert('error: ' + xhr.statusText);
                    }
                }
            );
        }

        function buildEmployeeData(data) {
            var markup = '<span>' + data.Engineer.EmployeeName + '</span><br /><span>' + data.Engineer.HoursPerWeek + '</span>'
            $('#employeeData').html(markup);
        }

        function thankyou(data) {
            $('#employeeData').html(data.Engineer.EmployeeName + ' says thanks!');
        }
    </script>

    <form id="form1" runat="server">
    <div class="engineerData">
        <hr />
        <div  id="employeeData"></div>
    </div>
    <input type="button" value="submit" id="submitbtn" />
    </form>
</body>
</html>


// get data from service
