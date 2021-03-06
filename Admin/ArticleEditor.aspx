<%@ Register TagPrefix="uc1" TagName="AdminLeft" Src="AdminLeft.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="../include/Bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="../include/Top.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ArticleEditor.aspx.vb"  ValidateRequest="false" Inherits="KPPress.ArticleEditor"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>조선신보사관리자</title>
		<LINK href="/include/style.css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
<!--
function LinkAdd()
{
	var LinkArticleNo =  document.Form1.txtLinkArticleID.value ;
	var LinkDescription = document.Form1.txtLinkDescription.value ;
	if (LinkArticleNo.length == 0) {
		alert('기사번호를 입력해주세요.');
		return false;
	}
	if (LinkDescription.length == 0) {
		alert('기사설명을 입력해주세요.');
		return false;
	}
	document.Form1.txtNayong.value += "<a href='/view_article.aspx?ArticleID=" + LinkArticleNo + "'>" + LinkDescription + "</a><br>"	;
}
function PopupOpen(page,name,width,height){
	var features = 'width=' + width + ',height=' + height + '';
    features    += 'diretories=no,location=no,menubar=no,scrollbars=yes,toolbar=no,resizable=yes,';
    features    += 'status=yes,';
    window_handle= window.open(page,name,features);
}
// -->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="780" border="0">
				<tr>
					<td><uc1:top id="Top1" runat="server"></uc1:top></td>
				</tr>
				<tr>
					<td background="../images/bg_table_big.gif">
						<table cellSpacing="0" cellPadding="0" width="779" border="0">
							<tr>
								<td vAlign="top" width="155" background="../images/bg_table_small.gif"><uc1:adminleft id="AdminLeft1" runat="server"></uc1:adminleft></td>
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" width="604" align="center" border="0">
										<tr>
											<td><IMG height="9" src="../images/00.gif" width="1"></td>
										</tr>
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0" bgcolor="<%response.write(LanguageColor)%>">
													<tr>
														<td bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></td>
													</tr>
													<tr>
														<td vAlign="bottom" height="20">
															<table cellSpacing="0" cellPadding="0" width="99%" align="center" border="0">
																<tr>
																	<td><IMG height="11" src="../images/icon_arrow_blue.gif" width="10">&nbsp; <font style="FONT-SIZE: 12pt" color="#003399">
																			<strong>
																				<asp:label id="lblAdminMenuName" runat="server"></asp:label></font>
																		<asp:label id="lblLanguage" runat="server"></asp:label><asp:checkbox id="chkIsOldArticle" runat="server" Visible="False" Font-Size="9pt" Font-Bold="True"
																			Text="지난 기사 입력"></asp:checkbox></STRONG></td>
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
											<td style="HEIGHT: 9px"><IMG height="9" src="../images/00.gif" width="1"></td>
										</tr>
										<TR>
											<td><asp:checkbox id="chkSeeArticleSelector" runat="server" Font-Size="9pt" Font-Bold="True" Text="기사 목록 보기"
													Checked="true" AutoPostBack="True"></asp:checkbox><asp:panel id="pnlArticleSelector" Runat="server">
													<TABLE cellSpacing="0" cellPadding="0" width="604" align="center" border="0">
														<TR>
															<TD>
																<TABLE width="100%">
																	<TR>
																		<TD class="text" style="WIDTH: 233px">검색어(제목)
																			<asp:textbox id="txtSearchWord" runat="server" Font-Size="9pt" Width="140px"></asp:textbox></TD>
																		<TD class="text">입력일
																			<asp:textbox id="txtGijunDateTime" runat="server" Font-Size="9pt" Width="208px"></asp:textbox>이후
																			<asp:Button id="btnSearch" Text="찾기" Font-Size="8pt" Runat="server"></asp:Button></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4">
																			<asp:Label id="lblErrorMsg" runat="server" Font-Size="9pt" Width="100%" ForeColor="Red"></asp:Label></TD>
																	</TR>
																</TABLE>
															</TD>
														<TR>
															<TD>
																<asp:datagrid id="DataGrid1" runat="server" Font-Size="9pt" Width="100%" PageSize="15" DataKeyField="ArticleID"
																	AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="doPaging" BorderColor="White"
																	BorderStyle="Ridge" CellSpacing="1" BorderWidth="2px" BackColor="White" CellPadding="3" GridLines="None">
																	<SelectedItemStyle Font-Bold="True" ForeColor="MidnightBlue" BackColor="Lavender"></SelectedItemStyle>
																	<ItemStyle HorizontalAlign="Center" ForeColor="Black" BackColor="WhiteSmoke"></ItemStyle>
																	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="GhostWhite" BackColor="DimGray"></HeaderStyle>
																	<FooterStyle ForeColor="Black" BackColor="#C6C3C6"></FooterStyle>
																	<Columns>
																		<asp:BoundColumn DataField="ArticleID" HeaderText="No.">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:ButtonColumn DataTextField="Title" HeaderText="제목" CommandName="Select">
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		</asp:ButtonColumn>
																		<asp:BoundColumn DataField="WriterName" HeaderText="작성자">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="SectionName" HeaderText="섹션">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="AuthName" HeaderText="단계">
																			<HeaderStyle Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="InputDateTime" HeaderText="입력일시">
																			<HeaderStyle Width="13%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PreviewBtn" HeaderText="보기">
																			<HeaderStyle Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#C6C3C6" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD>
														</TR>
													</TABLE>
												</asp:panel></td>
										</TR>
										<tr>
											<td><asp:panel id="pnlEntries" Visible="False" Runat="server">
													<TABLE cellSpacing="1" cellPadding="3" border="0">
														<TR>
															<TD class="text" width="15%" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			기사번호</FONT></STRONG></DIV>
															</TD>
															<TD width="85%">
																<TABLE cellSpacing="1" cellPadding="0" border="0">
																	<TR>
																		<TD class="text" align="center" width="30">
																			<asp:label id="lblArticleID" runat="server" Font-Bold="True" Font-Size="10pt"></asp:label></TD>
																		<TD class="text" align="center" width="30"><FONT face="굴림"></FONT></TD>
																		<TD class="text" style="WIDTH: 42px" align="center" width="42" bgColor="#f2f2f2">
																			<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;출처</FONT></STRONG></DIV>
																		</TD>
																		<TD class="text" align="center" width="30" bgColor="#f2f2f2"><FONT color="#003399">1차</FONT>
																		</TD>
																		<TD class="text">
																			<asp:dropdownlist id="ddlMedia1" runat="server" Font-Size="9pt" AutoPostBack="True" Height="20px"></asp:dropdownlist></TD>
																		<TD class="text" align="center" width="30" bgColor="#f2f2f2"><FONT color="#003399">2차</FONT>
																		</TD>
																		<TD class="text">
																			<asp:dropdownlist id="ddlMedia2" runat="server" Font-Size="9pt" Height="20px"></asp:dropdownlist></TD>
																		<TD>
																			<asp:Label id="lblChulcherEdit" runat="server" Visible="false">
																				<INPUT id="Button1" style="FONT-SIZE: 9pt" onclick="PopupOpen('MenuEditor.aspx?MediaParentID=301&EditMode=301','MenuEditor','300','320');"
																					type="button" value="메뉴변경" name="Button1" size=10></asp:Label>
																		</td>
																		<td>
																			<asp:Button id="btnChulcherReload" runat="server" Text="새로고침" Visible=false></asp:Button></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="text" width="15%" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			형태</FONT></STRONG></DIV>
															</TD>
															<TD width="85%">
																<TABLE cellSpacing="1" cellPadding="0" width="99%" border="0">
																	<TR>
																		<TD style="WIDTH: 81px">
																			<asp:dropdownlist id="ddlType" runat="server" Font-Size="9pt" Width="84px" Height="20px">
																				<asp:ListItem Value="0">Text</asp:ListItem>
																				<asp:ListItem Value="1">File</asp:ListItem>
																			</asp:dropdownlist></TD>
																		<TD width="30">
																			<asp:DropDownList id="ddlLanguage" runat="server" Visible="false"></asp:DropDownList></TD> <!--TD class="text" style="WIDTH: 63px" width="83" bgColor="#f2f2f2">
																			<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																						언어</FONT></STRONG></DIV>
																		</TD-->
																		<TD width="30"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="text" width="15%" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;섹션</FONT></STRONG></DIV>
															</TD>
															<TD width="85%">
																<TABLE cellSpacing="1" cellPadding="0" border="0">
																	<TR>
																		<TD style="WIDTH: 20px" width="27">
																			<asp:dropdownlist id="ddlSection" runat="server" Font-Size="9pt" Width="135px"></asp:dropdownlist></TD>
																		<TD style="WIDTH: 20px" width="27"></TD>
																		<TD class="text" style="WIDTH: 63px" width="83" bgColor="#f2f2f2">
																			<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																						지역</FONT></STRONG></DIV>
																		</TD>
																		<TD class="text">
																			<asp:dropdownlist id="ddlLocal" runat="server" Font-Size="9pt" Width="84px" Height="20px"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="text" width="15%" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			제목</FONT></STRONG></DIV>
															</TD>
															<TD width="85%">
																<asp:textbox id="txtTitle" runat="server" Font-Size="9pt" Width="491px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			부제목</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:textbox id="txtSubTitle" runat="server" Font-Size="9pt" Width="491px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			본문요약</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:textbox id="txtSubNayong" runat="server" Font-Size="9pt" Width="491px" Height="56px" TextMode="MultiLine"
																	MaxLength="200"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			본문</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:textbox id="txtNayong" runat="server" Font-Size="9pt" Width="491px" Height="361px" TextMode="MultiLine"></asp:textbox></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			사진</FONT></STRONG></DIV>
															</TD>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 351px"><INPUT language="vb" id="txtImageFile" style="FONT-SIZE: 9pt; WIDTH: 96.96%; HEIGHT: 22px"
																				type="file" size="37" name="txtImageFile" runat="server">
																		</TD>
																		<TD class="text" style="WIDTH: 60px" bgColor="#f2f2f2"><DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																						정렬</FONT></STRONG></DIV>
																		</TD>
																		<TD class="text">
																			<asp:radiobuttonlist id="rblImageAlign" Font-Size="9pt" Runat="server" Width="11px" RepeatDirection="Horizontal">
																				<asp:ListItem Value="left">좌</asp:ListItem>
																				<asp:ListItem Value="right" Selected="True">우</asp:ListItem>
																			</asp:radiobuttonlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			사진설명</FONT></STRONG></DIV>
															</TD>
															<TD><INPUT id="txtImageCaption" style="WIDTH: 77.07%; HEIGHT: 22px" type="text" size="59" name="Text1"
																	runat="server">
																<asp:button id="btnAttachImage" runat="server" Text="첨부" Font-Size="9pt"></asp:button>
																<asp:CheckBox id="chkSale" runat="server" Text="구매유도"></asp:CheckBox>
																<asp:label id="lblImageMsg" runat="server" Font-Size="9pt" Visible="False" ForeColor="Red"></asp:label><INPUT id="txtPhotoID_temp" type="hidden" size="12" name="txtLinkDescription" runat="server"><INPUT id="txtPhotoID" type="hidden" size="12" name="txtLinkDescription" runat="server"><INPUT id="txtImageFile_temp" type="hidden" size="12" name="txtLinkDescription" runat="server"></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;파일첨부</FONT></STRONG></DIV>
															</TD>
															<TD><INPUT id="txtEtcFile" style="FONT-SIZE: 9pt; WIDTH: 96.06%; HEIGHT: 22px" type="file"
																	size="62" name="txtEtcFile" runat="server">
															</TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			파일설명</FONT></STRONG></DIV>
															</TD>
															<TD><INPUT id="txtEtcCaption" style="WIDTH: 88%; HEIGHT: 22px" type="text" size="68" name="Text1"
																	runat="server">
																<asp:button id="btnAttachFile" runat="server" Text="첨부" Font-Size="9pt"></asp:button>
																<asp:label id="lblEtcMsg" runat="server" Font-Size="9pt" Visible="False" ForeColor="Red"></asp:label></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;기사링크</FONT></STRONG></DIV>
															</TD>
															<TD><INPUT id="txtLinkArticleID" title="WIDTH: 29.04%; HEIGHT: 22px" type="text" size="18"
																	name="txtLinkArticleID"> &lt; 기사번호입력 <INPUT style="FONT-SIZE: 9pt" onclick="LinkAdd();" type="button" value="첨부">&nbsp;&nbsp;&nbsp;&nbsp;
															</TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;<A name="#bottom">기사설명</A></FONT></STRONG></DIV>
															</TD>
															<TD><INPUT id="txtLinkDescription" style="WIDTH: 97.51%; HEIGHT: 22px" type="text" size="37"
																	name="txtLinkDescription">
															</TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2"></TD>
															<TD><INPUT id="LinkArticle" style="FONT-SIZE: 9pt" onclick="PopupOpen('ArticleLinker.aspx','aLinker','500','550');"
																	type="button" value="검색으로 링크하기">
																<asp:Button id="LinkSubmit" runat="server" Text="새로고침" Height="22px"></asp:Button><INPUT id="txtLinkArticleID2" style="WIDTH: 97.51%; HEIGHT: 22px" type="hidden" size="37"
																	name="txtLinkDescription" runat="server">
																<asp:datagrid id="Datagrid3" runat="server" Font-Size="9pt" Visible="true" Width="491px" DataKeyField="ArticleID"
																	AutoGenerateColumns="False">
																	<Columns>
																		<asp:BoundColumn DataField="ArticleID" HeaderText="기사번호">
																			<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Title" HeaderText="제목">
																			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="inputDateTime" HeaderText="입력일">
																			<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn>
																			<HeaderStyle Width="15px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<ItemTemplate>
																				<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="/images/icon_del.gif" CommandName="Delete"></asp:ImageButton>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																</asp:datagrid></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			기자이름</FONT></STRONG></DIV>
															</TD>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 196px">
																			<asp:textbox id="txtWriterName" runat="server" Font-Size="9pt" Width="185px"></asp:textbox></TD>
																		<TD class="text" style="WIDTH: 70px" bgColor="#f2f2f2"><DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																						이메일</FONT></STRONG></DIV>
																		</TD>
																		<TD><FONT face="굴림">&nbsp;</FONT>
																			<asp:textbox id="txtEmail" runat="server" Font-Size="9pt" Width="215px"></asp:textbox></TD>
																	</TR>
																</TABLE>
															</TD>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			작업기록</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:datagrid id="DataGrid2" runat="server" Font-Size="9pt" Visible="False" Width="491px" AutoGenerateColumns="False">
																	<Columns>
																		<asp:BoundColumn DataField="AuthName" HeaderText="작업명"></asp:BoundColumn>
																		<asp:BoundColumn DataField="LoginUserName" HeaderText="작업자"></asp:BoundColumn>
																		<asp:BoundColumn DataField="LogDateTime" HeaderText="입력일시"></asp:BoundColumn>
																	</Columns>
																</asp:datagrid></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			입력일</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:textbox id="txtInputDateTime" runat="server" Font-Size="9pt" Width="252px" Enabled="False"></asp:textbox>
																<asp:Label id="lblErrInput" runat="server" Font-Size="9pt" Width="242px" ForeColor="Red"></asp:Label></TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																			전송일</FONT></STRONG></DIV>
															</TD>
															<TD>
																<asp:textbox id="txtJunsongDateTime" runat="server" Font-Size="9pt" Width="250px"></asp:textbox>
																<asp:Label id="lblErrJunsong" runat="server" Font-Size="9pt" Width="242px" ForeColor="Red"></asp:Label></TD>
														</TR>
														<TR>
															<TD>
																<DIV align="right"><FONT color="#003399"><STRONG></STRONG></FONT></DIV>
															</TD>
															<TD>&nbsp;&nbsp;&nbsp;
																<asp:checkbox id="chkTopNews" runat="server" Text="주요뉴스로" Font-Size="9pt"></asp:checkbox>&nbsp; 
																&nbsp;
																<asp:button id="btnArticleSave" runat="server" Text="저장" Font-Size="8pt"></asp:button>&nbsp;&nbsp;
																<asp:button id="btnJunsong" runat="server" Text="웹출판" Font-Size="8pt"></asp:button>&nbsp;&nbsp;&nbsp;
																<asp:checkbox id="chkPreviewAfterSave" runat="server" Text="미리보기" Font-Size="9pt" Checked="true"></asp:checkbox>
																<asp:Button id="btnDelete" Text="기사삭제" Font-Size="8pt" Runat="server"></asp:Button></TD>
														</TR>
													</TABLE>
													<asp:ValidationSummary id="ValidationSummary1" runat="server" Font-Size="9pt" ShowSummary="False" ShowMessageBox="True"
														DisplayMode="List"></asp:ValidationSummary>
												</asp:panel></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td background="../images/bg_table_big.gif"><uc1:bottom id="Bottom1" runat="server"></uc1:bottom></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
