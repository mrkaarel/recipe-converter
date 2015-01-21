﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipeConverter.Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Recipe converter</title>
	<style>
		* { box-sizing: border-box; }
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
		}
	</style>
	<meta name="viewport" content="width=device-width" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:TextBox ID="txtRecipe" TextMode="MultiLine" placeholder="paste your recipe here and press Convert" runat="server" />
		<asp:Button ID="btnConvert" Text="Convert" OnClick="btnConvert_Click" runat="server" />
	</form>
</body>
</html>