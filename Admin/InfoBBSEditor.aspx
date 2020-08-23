<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InfoBBSEditor.aspx.vb" Inherits="KPPress.InfoBBSEditor"%>
<%@ Register TagPrefix="uc1" TagName="TOP" Src="/include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Left" Src="/Admin/AdminLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="/include/Bottom.ascx" %>
<HTML>
	<HEAD>
		<title>KPM - 조선언론 정보기지</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="/include/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#ffffff" leftMargin="0" background="/images/bg.gif" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="965" bgColor="#ffffff" border="0">
				<tr>
					<td colSpan="3">
						<!-- 상단메뉴 --><uc1:top id="TOP1" runat="server"></uc1:top></td>
					<td width="5" background="/images/bg_r.gif" rowSpan="4"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="150" height="100%">
						<!-- 왼쪽메뉴 --><uc1:left id="Left1" runat="server"></uc1:left></td>
					<td vAlign="top" align="center" width="580">
						<!-- 메인 시작 -->
						<table cellSpacing="0" cellPadding="0" width="568" border="0">
							<tr height="8">
								<td></td>
							</tr>
							<tr>
								<td bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></td>
							</tr>
							<tr>
								<td vAlign="bottom" height="20">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></td>
										</tr>
										<tr>
											<td vAlign="bottom" height="20">
												<table cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
													<tr>
														<td><IMG height="11" src="../images/icon_arrow_blue.gif" width="10">&nbsp; <font style="FONT-SIZE: 12pt" color="#003399">
																<strong>
																	<asp:label id="lblAdminMenuName" runat="server"></asp:label></font></STRONG></td>
														<td class="icon" vAlign="bottom">
															<div align="right">&nbsp;</div>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" bgColor="#ffffff" border="0">
							<tr>
								<td width="601" height="500"><iframe src='bbs_<%=iif(request("page")="","list",request("page"))%>.aspx?bbsid=notice&amp;no=<%=request("no")%>' scrolling=auto width="100%" height="100%" frameborder=0></iframe></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="568" bgColor="#ffffff" border="0">
							<tr>
								<td width="568" height="7"></td>
							</tr>
						</table>
						<!-- 메인 끝 --></td>
					<td vAlign="top" align="center" width="230"></td>
				</tr>
				<tr>
					<td width="960" colSpan="3" height="8"></td>
				</tr>
				<tr>
					<td colSpan="3">
						<!-- 하단 --><uc1:bottom id="Bottom1" runat="server"></uc1:bottom></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
