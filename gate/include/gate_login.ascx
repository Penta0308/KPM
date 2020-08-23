<%@ Control Language="vb" AutoEventWireup="false" Codebehind="gate_login.ascx.vb" Inherits="KPPress.gate_login" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="0" cellPadding="0" width="180" bgColor="#e2e2e2" border="0">
	<tr>
		<td height="10" colspan="2"></td>
	</tr>
	<tr>
		<td width="70" height="25" align="right"><font color="#093842" style="FONT-SIZE:8pt">UserName</font>&nbsp;</td>
		<td width="100" align="right">
			<asp:TextBox id="txt_userid" runat="server" Width="95px" Font-Size="9pt" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="#7F9DB9"></asp:TextBox></td>
		<td width="10" rowspan="3"></td>
	</tr>
	<tr>
		<td width="70" height="25" align="right"><font color="#093842" style="FONT-SIZE:8pt">Password</font>&nbsp;</td>
		<td width="100" align="right">
			<asp:TextBox id="txt_pwd" runat="server" Width="95px" Font-Size="9pt" TextMode="Password" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="#7F9DB9"></asp:TextBox></td>
	</tr>
	<tr>
		<td width="70" height="40" align="right"></td>
		<td width="100" align="right">
			<asp:ImageButton id="btn_login" runat="server" ImageUrl="/images/btn_login_gate.gif"></asp:ImageButton></td>
	</tr>
</table>
