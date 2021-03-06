<%@ Register TagPrefix="uc1" TagName="Right" Src="include/Right.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="include/Bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Left" Src="include/Left.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TOP" Src="include/Top.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="search_past.aspx.vb" Inherits="KPPress.search_past_jpn" %>
<HTML>
	<HEAD>
		<title>KPM - 조선언론 정보기지</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="/japanese/include/main.css" type="text/css" rel="stylesheet">
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
								<td width="170" bgColor="#e2e2e2" height="26">&nbsp;&nbsp;<b>過去の記事</b></td>
								<td align="right" width="398" bgColor="#e2e2e2"></td>
							</tr>
							<tr>
								<td width="568" colSpan="2" height="15"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="558" bgColor="#ffffff" border="0">
							<tr>
								<td align="center" width="558" height="20"><font color="#818181">찾으실 기사의 년도, 월, 일을 
										선택하시기 바랍니다.</font>
								</td>
							</tr>
							<tr>
								<td align="center" width="558" height="40"><asp:dropdownlist id="ddlMedia" runat="server" DataTextField="MediaName" DataValueField="MediaID"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="ddlYear" runat="server" Height="24px" Width="70px"></asp:dropdownlist>年
									<asp:dropdownlist id="ddlMonth" runat="server" Height="25px" Width="48px"></asp:dropdownlist>月
									<asp:dropdownlist id="ddlDay" runat="server" Height="19px" Width="50px"></asp:dropdownlist>日
									<asp:imagebutton id="imgbtnSearch" runat="server" align="absmiddle" ImageUrl="/images/btn_search.gif"></asp:imagebutton></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td><asp:panel id="msg_result" runat="server" Visible="False">※ 검색결과 : 총 <FONT color="#cc0000">
											<asp:Label id="lblCount" Runat="server"></asp:Label></FONT>건의 
            기사를 찾았습니다.</asp:panel></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td bgColor="#c6c6c6" height="1"></td>
							</tr>
							<tr>
								<td height="7"></td>
							</tr>
							<tr>
								<td><asp:datagrid id="DataGrid1" Width="100%" Visible="False" Runat="server" CssClass="box" AllowPaging="True"
										GridLines="None" PageSize="20" AutoGenerateColumns="False" OnPageIndexChanged="doPaging" ShowHeader="False">
										<Columns>
											<asp:TemplateColumn>
												<ItemTemplate>
													ㆍ <a href='/japanese/view_article.aspx?ArticleID=<%# DataBinder.Eval(Container.DataItem, "ArticleID") %>'>
														<%# DataBinder.Eval(Container.DataItem, "Title") %>
													</a>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="JunsongDateTime" DataFormatString="{0:yyyy-MM-dd HH:mm}">
												<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#003296" Mode="NumericPages" Height="30" VerticalAlign="Bottom"></PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="568" bgColor="#ffffff" border="0">
							<tr>
								<td width="568" colSpan="2" height="15"></td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;<A class="branch_a" href="javascript:history.back();">← Back</A></td>
								<td align="right"><A class="branch_a" href="#t">↑ Top</A>&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td width="568" colSpan="2" height="15"></td>
							</tr>
						</table>
						<!-- 메인 끝 --></td>
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
