<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipeConverter.Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Recipe converter</title>
	
	<meta name="description" content="A simple tool to convert imperial units in your recipe to metric ones." />
	<meta name="viewport" content="width=device-width" />
	<link rel="icon" type="image/png" href="favicon.png" />
	<style>
		* { 
			box-sizing: border-box; 
		}
		html, body {
			margin: 0; padding: 0;
			width: 100%; height: 100%;
		}
		textarea {
			width: 99vw;
			height: 99vh;

			padding: 1em;

			border: none;
			overflow: auto;
			outline: none;
			resize: none;

			-webkit-box-shadow: none;
			-moz-box-shadow: none;
			box-shadow: none;
		}
		input[type=submit] {
			position: absolute;
			bottom: 1em;
			right: 1em;

			cursor: pointer;
			-webkit-appearance: none;
			
			font-family: RobotoDraft, 'Helvetica Neue', Helvetica, Arial;
			text-transform: uppercase;

			border: none;
			border-radius: 2px;
			background-color: rgba(66, 133, 244, 1);
			color: #fff;
			padding: 0.8em 1em;
		}
			input[type=submit]:hover {
				background-color: rgba(66, 133, 244, 0.9);
			}
	</style>
</head>
<body>
	<form runat="server">
		<asp:TextBox ID="txtRecipe" TextMode="MultiLine" placeholder="paste your recipe here" runat="server" />
		<asp:Button ID="btnConvert" Text="Convert" OnClick="btnConvert_Click" runat="server" />
	</form>
</body>
</html>
