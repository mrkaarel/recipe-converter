<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" EnableViewState="false" Inherits="RecipeConverter.Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Recipe converter</title>
	<style>
		html, body {
			margin: 0; padding: 0;
			width: 100%; height: 100%;
		}
		textarea {
			width: 99vw;
			height: 94vh;
		}
		input[type=submit] {
			display: inline-block;
			margin-top: -60px;
		}
	</style>
	<meta name="viewport" content="width=device-width" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:TextBox ID="txtRecipe" TextMode="MultiLine" runat="server" />
		<asp:Button ID="btnConvert" Text="Convert" OnClick="btnConvert_Click" runat="server" />
	</form>
</body>
</html>
