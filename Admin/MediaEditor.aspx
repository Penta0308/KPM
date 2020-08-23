<%@ Register TagPrefix="uc1" TagName="Top" Src="../include/Top.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Bottom" Src="../include/Bottom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdminLeft" Src="AdminLeft.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MediaEditor.aspx.vb" Inherits="KPPress.MediaEditor"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>조선신보사관리자</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="/include/style.css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="/include/admin.js"></script>
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
	document.Form1.txtNayong.value += "<a href='../news/ViewArticle.aspx?ArticleID=" + LinkArticleNo + "'>" + LinkDescription + "</a>"	;
}
function PopupOpen(page,name,width,height){
	var features = 'width=' + width + ',height=' + height + '';
    features    += 'diretories=no,location=no,menubar=no,scrollbars=yes,toolbar=no,resizable=yes,';
    features    += 'status=yes,';
    window_handle= window.open(page,name,features);
}
	
function ChkLenth(obj){
	var data =  obj.value;
	
	if(data.length >= 5)
	{
		obj.value=data;
		return;
	}
}
// -->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="780" border="0">
				<tr>
					<td><asp:panel id="Panel1" Runat="server">
							<uc1:top id="Top1" runat="server"></uc1:top>
						</asp:panel></td>
				</tr>
				<tr>
					<td background="../images/bg_table_big.gif">
						<table cellSpacing="0" cellPadding="0" width="779" border="0">
							<tr>
								<asp:panel id="Panel2" Runat="server">
									<TD vAlign="top" width="155" background="../images/bg_table_small.gif">
										<uc1:adminleft id="AdminLeft1" runat="server"></uc1:adminleft></TD>
								</asp:panel>
								<td vAlign="top">
									<table cellSpacing="0" cellPadding="0" width="604" align="center" border="0">
										<tr>
											<td><IMG height="9" src="../images/00.gif" width="1"></td>
										</tr>
										<tr>
											<td>
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
																				<asp:label id="lblAdminMenuName" runat="server"></asp:label></strong></font></td>
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
											<td><asp:checkbox id="chkSeeListSelector" runat="server" Checked="true" AutoPostBack="True" Text="목록 보기"
													Font-Bold="True" Font-Size="9pt"></asp:checkbox>&nbsp;&nbsp;
												<asp:button id="btnNew" runat="server" Text="새로입력"></asp:button><asp:panel id="pnlSelector" Runat="server">
													<TABLE cellSpacing="0" cellPadding="0" width="604" align="center" border="0">
														<TR>
															<TD>
																<TABLE width="100%">
																	<TR>
																		<TD class="text" style="WIDTH: 233px">검색어(제목)
																			<asp:textbox id="txtSearchWord" runat="server" Font-Size="9pt" Width="140px"></asp:textbox></TD>
																		<TD class="text">입력일
																			<asp:textbox id="txtGijunDateTime" runat="server" Font-Size="9pt" Width="208px"></asp:textbox>이후
																			<asp:Button id="btnSearch" Runat="server" Font-Size="8pt" Text="찾기"></asp:Button></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4">
																			<asp:Label id="lblErrorMsg" runat="server" Font-Size="9pt" Width="100%" ForeColor="Red"></asp:Label></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD bgColor="#eeeeee" height="20">
																<DIV align="left"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;메인&nbsp;목록</FONT></STRONG></DIV>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD>
																<asp:DataList id="DataList1" runat="server" CellSpacing="10" DataKeyField="mmFileID" CssClass="N1"
																	RepeatDirection="Horizontal" RepeatColumns="2">
																	<SelectedItemStyle BackColor="#B2B2B2"></SelectedItemStyle>
																	<ItemStyle CssClass="N1"></ItemStyle>
																	<ItemTemplate>
																		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
																					vAlign="middle" align="center" width="100" height="100"><%# Container.DataItem("Previewbtn")%></TD>
																				<TD>
																					<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																						<TR>
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200"><%# Container.DataItem("title")%></TD>
																						</TR>
																						<TR height="10">
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200">
																								<asp:Button id="btnUpdate" runat="server" Text="수정" CommandName="update"></asp:Button></TD>
																						</TR>
																					</TABLE>
																				</TD>
																			</TR>
																		</TABLE>
																	</ItemTemplate>
																</asp:DataList></TD>
														</TR>
														<TR>
															<TD bgColor="#eeeeee" height="20">
																<DIV align="left"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;일반&nbsp;목록</FONT></STRONG></DIV>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD>
																<asp:DataList id="Datalist2" runat="server" CellSpacing="10" DataKeyField="mmFileID" CssClass="N1"
																	RepeatDirection="Horizontal" RepeatColumns="2">
																	<SelectedItemStyle BackColor="#B2B2B2"></SelectedItemStyle>
																	<ItemStyle CssClass="N1"></ItemStyle>
																	<ItemTemplate>
																		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
																					vAlign="middle" align="center" width="100" height="100"><%# Container.DataItem("Previewbtn")%></TD>
																				<TD>
																					<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																						<TR>
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200"><%# Container.DataItem("title")%></TD>
																						</TR>
																						<TR height="10">
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200">
																								<asp:Button id="Button2" runat="server" Text="수정" CommandName="update"></asp:Button></TD>
																						</TR>
																					</TABLE>
																				</TD>
																			</TR>
																		</TABLE>
																	</ItemTemplate>
																</asp:DataList></TD>
														</TR>
														<TR>
															<TD bgColor="#eeeeee" height="20">
																<DIV align="left"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;대기중인&nbsp;목록</FONT></STRONG></DIV>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#c6c6c6"><IMG height="1" src="../images/00.gif" width="1"></TD>
														</TR>
														<TR>
															<TD>
																<asp:DataList id="Datalist3" runat="server" CellSpacing="10" DataKeyField="mmFileID" CssClass="N1"
																	RepeatDirection="Horizontal" RepeatColumns="2">
																	<SelectedItemStyle BackColor="#b2b2b2"></SelectedItemStyle>
																	<ItemStyle CssClass="N1"></ItemStyle>
																	<ItemTemplate>
																		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																			<TR>
																				<TD style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
																					vAlign="middle" align="center" width="100" height="100"><%# Container.DataItem("Previewbtn")%></TD>
																				<TD>
																					<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																						<TR>
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200"><%# Container.DataItem("title")%></TD>
																						</TR>
																						<TR height="10">
																							<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" width="200">
																								<asp:Button id="Button3" runat="server" Text="수정" CommandName="update"></asp:Button></TD>
																						</TR>
																					</TABLE>
																				</TD>
																			</TR>
																		</TABLE>
																	</ItemTemplate>
																</asp:DataList></TD>
														</TR>
													</TABLE>
												</asp:panel></td>
										</TR>
										<tr>
											<td><asp:panel id="pnlEntries" Runat="server" Visible="false">
													<TABLE cellSpacing="1" cellPadding="3" border="0">
														<TR>
															<TD class="text" width="15%" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;자료번호</FONT></STRONG></DIV>
															</TD>
															<TD width="85%">
																<asp:label id="lblmmFileID" runat="server" Font-Size="10pt" Font-Bold="True"></asp:label></TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<asp:panel id="pnlImgUpLoad" Runat="server" Visible="false">
															<TR>
																<TD class="text" bgColor="#f2f2f2">
																	<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																				사진</FONT></STRONG></DIV>
																</TD>
																<TD>
																	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD style="WIDTH: 351px"><INPUT language="vb" id="txtImageFile" style="FONT-SIZE: 9pt; WIDTH: 96.96%; HEIGHT: 22px"
																					type="file" size="37" name="txtImageFile" runat="server"></TD>
																			<TD style="WIDTH: 351px">&nbsp;
																				<asp:button id="btnAttachImage" runat="server" Font-Size="9pt" Text="첨부"></asp:button></TD>
																		</TR>
																	</TABLE>
																	<FONT style="FONT-SIZE: 9pt" face="굴림">현재 올려진 사진 :&nbsp;
																		<asp:Label id="lblFilePath_Large" runat="server" Font-Size="9pt" ForeColor="Red">없음</asp:Label>
																		<asp:Label id="lblFilePath_Thumb" runat="server" Font-Size="9pt" ForeColor="Red" Visible="False"></asp:Label>
																		<asp:Label id="lblFilePath_Small" runat="server" Font-Size="9pt" ForeColor="Red" Visible="False"></asp:Label></FONT></TD>
															</TR>
															<TR>
																<TD class="text"></TD>
																<TD>
																	<asp:Panel id="pnlFileInfo" runat="server" Visible="false">
																		<TABLE id="Table1" height="20" cellSpacing="1" cellPadding="0" border="0">
																			<TR>
																				<TD class="text" align="center" width="80" bgColor="#f2f2f2"><FONT color="#003399">사이즈</FONT>
																				</TD>
																				<TD class="text">
																					<asp:Label id="lblSizeW" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>&nbsp;x&nbsp;
																					<asp:Label id="lblSizeH" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>&nbsp;pixels</TD>
																				<TD class="text" align="center" width="80" bgColor="#f2f2f2"><FONT color="#003399">해상도</FONT>&nbsp;</TD>
																				<TD class="text">
																					<asp:Label id="lblRev" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>&nbsp;dpi</TD>
																			</TR>
																		</TABLE>
																	</asp:Panel></TD>
															</TR>
															<TR>
																<TD class="text"><FONT face="굴림">
																		<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;미리보기</FONT></STRONG></DIV>
																	</FONT>
																</TD>
																<TD>
																	<asp:Image id="imgThumb" runat="server" Visible="False"></asp:Image>
																	<asp:Image id="imgPreview" runat="server" Visible="False"></asp:Image></TD>
															</TR>
														</asp:panel>
														<asp:panel id="pnlVideoUpLoad" Runat="server" Visible="false">
															<TR>
																<TD class="text" bgColor="#f2f2f2">
																	<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																				56K동영상</FONT></STRONG></DIV>
																</TD>
																<TD>
																	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD style="WIDTH: 351px"><INPUT language="vb" id="txt56kFile" style="FONT-SIZE: 9pt; WIDTH: 96.96%; HEIGHT: 22px"
																					type="file" size="37" name="txt56kFile" runat="server"></TD>
																			<TD style="WIDTH: 351px">&nbsp;
																				<asp:button id="btn56kUpLoad" runat="server" Font-Size="9pt" Text="첨부"></asp:button></TD>
																		</TR>
																	</TABLE>
																	<FONT style="FONT-SIZE: 9pt" face="굴림">현재 올려진 파일 :&nbsp;
																		<asp:Label id="lbl56k" runat="server" Font-Size="9pt" ForeColor="Red">없음</asp:Label></FONT></TD>
															</TR>
															<TR>
																<TD class="text" bgColor="#f2f2f2">
																	<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																				300K동영상</FONT></STRONG></DIV>
																</TD>
																<TD>
																	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD style="WIDTH: 351px"><INPUT language="vb" id="txt300kFile" style="FONT-SIZE: 9pt; WIDTH: 96.96%; HEIGHT: 22px"
																					type="file" size="37" name="txt300kFile" runat="server"></TD>
																			<TD style="WIDTH: 351px">&nbsp;
																				<asp:button id="btn300kUpLoad" runat="server" Font-Size="9pt" Text="첨부"></asp:button></TD>
																		</TR>
																	</TABLE>
																	<FONT style="FONT-SIZE: 9pt" face="굴림">현재 올려진 파일 :&nbsp;
																		<asp:Label id="lbl300k" runat="server" Font-Size="9pt" ForeColor="Red">없음</asp:Label></FONT></TD>
															</TR>
															<TR>
																<TD class="text" bgColor="#f2f2f2">
																	<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;캡쳐화면</FONT></STRONG></DIV>
																</TD>
																<TD>
																	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																		<TR>
																			<TD style="WIDTH: 351px"><INPUT language="vb" id="txtThumbFile" style="FONT-SIZE: 9pt; WIDTH: 96.96%; HEIGHT: 22px"
																					type="file" size="37" name="txtThumbFile" runat="server"></TD>
																			<TD style="WIDTH: 351px">&nbsp;
																				<asp:button id="lblThumbUpLoad" runat="server" Font-Size="9pt" Text="첨부"></asp:button></TD>
																		</TR>
																	</TABLE>
																	<FONT style="FONT-SIZE: 9pt" face="굴림">현재 올려진 파일 :&nbsp;
																		<asp:Label id="lblThumb" runat="server" Font-Size="9pt" ForeColor="Red">없음</asp:Label></FONT></TD>
															</TR>
															<TR>
																<TD class="text"><FONT face="굴림"></FONT></TD>
																<TD></TD>
															</TR>
														</asp:panel>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;제목</FONT></STRONG></DIV>
															</TD>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD>조선어</TD>
																		<TD>
																			<asp:textbox id="txtTitle" runat="server" Width="455"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD>일본어</TD>
																		<TD>
																			<asp:textbox id="txtTitleJpn" runat="server" Width="455"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD>영어</TD>
																		<TD>
																			<asp:textbox id="txtTitleEng" runat="server" Width="455"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD></TD>
																		<TD><FONT color="olive">제목은 25자내로 입력하시기 바랍니다.</FONT></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD class="text" bgColor="#f2f2f2">
																<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">&nbsp;설명</FONT></STRONG></DIV>
															</TD>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD>조선어</TD>
																		<TD>
																			<asp:TextBox id="txtCaption" runat="server" Width="460px" Height="64px" TextMode="MultiLine"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD>일본어</TD>
																		<TD>
																			<asp:TextBox id="txtCaptionJpn" runat="server" Width="460px" Height="64px" TextMode="MultiLine"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD>영어</TD>
																		<TD>
																			<asp:TextBox id="txtCaptionEng" runat="server" Width="460px" Height="64px" TextMode="MultiLine"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD></TD>
																		<TD>
																			<asp:label id="lblcaptionmsg" runat="server" Font-Size="9pt" ForeColor="Olive" Visible="False">설명은 200자내로 입력하시기 바랍니다.</asp:label></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD><IMG height="4" src="../images/00.gif" width="1"></TD>
														</TR>
														<asp:panel id="Panel4" Runat="server">
															<TR>
																<TD class="text" bgColor="#f2f2f2">
																	<DIV align="right"><STRONG><FONT color="#003399"><IMG height="3" src="../images/point_blue.gif" width="3" align="absMiddle">
																				입력일</FONT></STRONG></DIV>
																</TD>
																<TD>
																	<asp:Label id="lblInputDateTime" runat="server" Font-Size="9pt"></asp:Label>
																	<asp:Label id="lblFileSize" runat="server">0</asp:Label></TD>
															</TR>
															<TR>
																<TD>
																	<DIV align="right"><FONT color="#003399"><STRONG></STRONG></FONT></DIV>
																</TD>
																<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:CheckBox id="chkShowMain" runat="server" Font-Size="9pt" Text="메인화면 등록"></asp:CheckBox>&nbsp;
																	<asp:button id="btnSave" runat="server" Font-Size="8pt" Text="저장"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:checkbox id="chkPreviewAfterSave" runat="server" Font-Size="9pt" Text="미리보기" Checked="true"></asp:checkbox>
																	<asp:Button id="btnDelete" Runat="server" Font-Size="8pt" Text="삭제"></asp:Button></TD>
															</TR>
														</asp:panel></TABLE>
													<asp:ValidationSummary id="ValidationSummary1" runat="server" Font-Size="9pt" DisplayMode="List" ShowMessageBox="True"
														ShowSummary="False"></asp:ValidationSummary>
												</asp:panel></td>
										</tr>
										<tr>
											<td align="center"><asp:button id="btnPhoto2Article" runat="server" Text="첨부" Font-Size="9pt" Visible="False"></asp:button></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<asp:panel id="Panel3" Runat="server">
					<TR>
						<TD background="../images/bg_table_big.gif">
							<uc1:bottom id="Bottom1" runat="server"></uc1:bottom></TD>
					</TR>
				</asp:panel></table>
		</form>
	</body>
</HTML>
