<!DOCTYPE html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecipeConverter.Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Recipe converter</title>
	
	<meta name="description" content="A simple tool to convert imperial units in your recipe to metric ones." />
	<meta name="viewport" content="width=device-width" />
	<link rel="icon" type="image/png" href="favicon.png" />
	<link rel="stylesheet" href="app.css" />
</head>
<body>
	<form method="post">
		<textarea ID="txtRecipe" placeholder="paste your recipe here" runat="server" />
		<input type="submit" value="Convert" />
	</form>
</body>
</html>
