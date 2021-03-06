<%@ Register TagPrefix="uc1" TagName="TOP" Src="include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Left" Src="include/Left.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="include/Bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Right" Src="include/Right.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="view_article.aspx.vb" Inherits="KPPress.view_article_jpn" %>
<HTML>
	<HEAD>
		<title>KPM - Japanese</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="/japanese/include/main.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="/include/admin.js"></script>
	</HEAD>
	<body bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="963" bgColor="#ffffff" border="0" height="100%">
				<tr>
					<td colSpan="5">
						<!-- 상단메뉴 --><uc1:top id="TOP1" runat="server"></uc1:top></td>
					<td width="1" rowspan="4" bgcolor="#929292"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="148" height="100%">
						<!-- 왼쪽메뉴 --><uc1:left id="Left1" runat="server"></uc1:left></td>
					<td width="1" rowspan="3" bgcolor="#929292"></td>
					<td vAlign="top" align="center" width="584">
						<!-- 메인 시작 -->
						<table cellSpacing="0" cellPadding="0" width="568" bgColor="#ffffff" border="0">
							<tr>
								<td width="170" height="26" bgColor="#e2e2e2"><%= image %></td>
								<td align="right" width="398" bgColor="#e2e2e2">
									<asp:Label id="lbl_Section" runat="server" Font-Bold="True"></asp:Label>
									<asp:Label id="lbl_sep" runat="server" Font-Bold="True"></asp:Label>
									<asp:Label id="lbl_Local" runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td colspan="2" align="right" height="26"><a href='/print_article.aspx?ArticleID=<%= Request.QueryString("ArticleID") %>' target="_blank"><img src="/images/icon_print.gif" align="absmiddle" border="0">
										記事プリント</a></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="530" bgColor="#ffffff" border="0">
							<tr>
								<td width="530" height="10"></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lbl_Title" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="#003296"></asp:Label>
								</td>
							</tr>
							<tr>
								<td height="5"></td>
							</tr>
							<tr>
								<td>
									<asp:Label id="lbl_SubTitle" runat="server" Font-Bold="True"></asp:Label>
								</td>
							</tr>
							<tr>
								<td height="15"></td>
							</tr>
							<tr>
								<td class="text">
									<asp:Label id="lbl_Nayong" runat="server"></asp:Label>
								</td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td>
									<asp:Repeater id="Repeater1" runat="server">
										<ItemTemplate>
											<a href='/view_article.aspx?ArticleID=<%# DataBinder.Eval(Container.DataItem, "ArticleID") %>'>
												<b>[관련기사]</b>
												<%# DataBinder.Eval(Container.DataItem, "Title")  %>
											</a>
										</ItemTemplate>
										<SeparatorTemplate>
											<br>
										</SeparatorTemplate>
									</asp:Repeater>
								</td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td align="right" height="17">
									<asp:Label id="lbl_WriterName" runat="server"></asp:Label>
									<asp:Label id="lbl_Email" runat="server"></asp:Label>
								</td>
							</tr>
							<tr>
								<td align="right">
									<asp:Label id="lbl_JunsongDateTime" runat="server"></asp:Label>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="568" bgColor="#ffffff" border="0">
							<tr>
								<td colSpan="2" width="568" height="15"></td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;<a href="javascript:history.back();" class="branch_a">← Back</a></td>
								<td align="right"><a href="#t" class="branch_a">↑ Top</a>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td colSpan="2" width="568" height="15"></td>
							</tr>
						</table>
						<!-- 메인 끝 -->
					</td>
					<td width="1" rowspan="3" bgcolor="#929292"></td>
					<td vAlign="top" align="center" width="228">
						<!-- 오늘쪽 메뉴 --><uc1:right id="Right1" runat="server"></uc1:right></td>
				</tr>
				<tr>
					<td width="960" colSpan="5" height="8"></td>
				</tr>
				<tr>
					<td colSpan="5">
						<!-- 하단 --><uc1:bottom id="Bottom1" runat="server"></uc1:bottom></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
