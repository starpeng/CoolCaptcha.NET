<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CaptchaTest._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cool Captcha test</title>
    <style type="text/css">
        body {
            font-family: sans-serif;
            font-size: 0.8em;
            padding: 20px;
        }

        #result {
            border: 1px solid green;
            width: 300px;
            margin: 0 0 35px 0;
            padding: 10px 20px;
            font-weight: bold;
        }

        #change-image {
            font-size: 0.8em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="result">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <p><strong>Write the following word:</strong></p>
        <img src="captcha.aspx" id="captcha" alt="" /><br />
        <a href="#" onclick="
    document.getElementById('captcha').src='captcha.aspx?'+Math.random();
    document.getElementById('captcha-form').focus();"
            id="change-image">Not readable? Change text.</a><br />
        <br />
        <input type="text" name="captcha" id="captcha-text" autocomplete="off" /><br />
        <input type="submit" />
        <br />
    </form>
</body>
</html>
