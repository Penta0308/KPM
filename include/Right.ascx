<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Right.ascx.vb" Inherits="KPPress.Right" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="JavaScript" src="/include/main.js"></script>
<table cellSpacing="0" cellPadding="0" width="212" border="0">
	<tr>
		<td width="212" height="37">
			<table cellSpacing="0" cellPadding="0" width="212" border="0">
				<tr>
					<td width="212" height="6"><IMG height="6" src="/images/bg_search_top.gif" width="212"></td>
				</tr>
				<tr>
					<td vAlign="middle" align="center" width="212" bgColor="#408080" height="25"><!--<input id="txtSearch" type="text" name="Text1" runat="server">-->
						<asp:imagebutton id="imgbtnSearch" runat="server" align="absmiddle" ImageUrl="/images/btn_search_big.gif"></asp:imagebutton></td>
				</tr>
				<tr>
					<td width="212" height="6"><IMG height="6" src="/images/bg_search_bottom.gif" width="212"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td height="10"></td>
	</tr>
</table>
<asp:panel id="pnl_photo" Runat="server" Visible="false" Width="212">
	<TABLE cellSpacing="0" cellPadding="0" width="212" align="center" border="0">
		<TR>
			<TD width="212"><A href="/list_photo.aspx"><IMG height="24" src="/images/bar_r_photo.gif" width="212" border="0"></A></TD>
		</TR>
		<TR>
			<TD bgColor="#c6c6c6" height="2"></TD>
		</TR>
		<TR>
			<TD align="center" bgColor="#c6c6c6">
				<TABLE cellSpacing="0" cellPadding="0" width="200" border="0">
					<asp:Repeater id="Repeater3" runat="server">
						<ItemTemplate>
							<tr>
								<td width="192">
									<a href="#" onclick='popup_photo(<%# Container.DataItem("mmFileID") %>)'><img src='<%# Container.DataItem("Location_Thumb") %>' border="1" align=left vspace="5" style="border-color:black"></a>
								</td>
							</tr>
							<tr>
								<td height="28">
									<font style="font-size:8pt;"><a href="#" onclick='popup_photo(<%# Container.DataItem("mmFileID") %>)' class="branch_a">
											▶
											<%# Container.DataItem("Title") %>
										</a></font>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater></TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="10"></TD>
		</TR>
	</TABLE>
</asp:panel><asp:panel id="pnl_video" Runat="server" Visible="true" Width="212">
	<TABLE cellSpacing="0" cellPadding="0" width="212" align="center" border="0">
		<TR>
			<TD width="212"><A href="/list_video.aspx"><IMG height="24" src="/images/bar_r_video.gif" width="212" border="0"></A></TD>
		</TR>
		<TR>
			<TD bgColor="#c6c6c6" height="2"></TD>
		</TR>
		<TR>
			<TD align="center" bgColor="#c6c6c6">
				<TABLE cellSpacing="0" cellPadding="0" width="200" border="0">
					<asp:Repeater id="Repeater4" runat="server">
						<ItemTemplate>
							<tr>
								<td width="192">
									<a href="#" onclick='popup_video(<%# Container.DataItem("mmFileID") %>, 300)'><img src='<%# DataBinder.Eval(Container.DataItem, "Location_Thumb") %>' border="1" align=left vspace="5" style="border-color:black"></a>
								</td>
							</tr>
							<tr>
								<td height="28">
									<font style="font-size:8pt;"><a href="#" onclick='popup_video(<%# Container.DataItem("mmFileID") %>, 300)' class="branch_a">
											▶
											<%# Container.DataItem("Title") %>
										</a></font>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater></TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="10"></TD>
		</TR>
	</TABLE>
</asp:panel>
<table cellSpacing="0" cellPadding="0" width="212" border="0">
	<tr>
		<td><A href="/list_etc.aspx?ID=108&amp;chk_etc=section&amp;lvl=1"><IMG height="23" src="/images/bar_r_editorial.gif" width="212" border="0"></A></td>
	</tr>
	<tr>
		<td height="4"></td>
	</tr>
	<tr>
		<td align="center">
			<table cellSpacing="0" cellPadding="0" width="212" border="0">
				<tr>
					<td bgColor="#c6c6c6" colSpan="6" height="1"></td>
				</tr>
				<tr>
					<td width="1" bgColor="#c6c6c6" height="8"></td>
					<td width="210" colSpan="4"></td>
					<td width="1" bgColor="#c6c6c6"></td>
				</tr>
				<asp:repeater id="Repeater1" runat="server">
					<ItemTemplate>
						<tr>
							<td width="1" bgColor="#C6C6C6"></td>
							<td width="3"></td>
							<td width="10" valign="top">ㆍ</td>
							<td width="190" valign="top"><a href='/view_article.aspx?ArticleID=<%# DataBinder.Eval(Container.DataItem, "ArticleID")  %>'><%# DataBinder.Eval(Container.DataItem, "Title")  %></a></td>
							<td width="7"></td>
							<td width="1" bgColor="#C6C6C6"></td>
						</tr>
					</ItemTemplate>
					<SeparatorTemplate>
						<tr>
							<td width="1" bgColor="#C6C6C6" height="3"></td>
							<td width="210" colspan="4"></td>
							<td width="1" bgColor="#C6C6C6"></td>
						</tr>
					</SeparatorTemplate>
				</asp:repeater>
				<tr>
					<td width="1" bgColor="#c6c6c6" height="5"></td>
					<td width="210" colSpan="4"></td>
					<td width="1" bgColor="#c6c6c6"></td>
				</tr>
				<tr>
					<td bgColor="#c6c6c6" colSpan="6" height="1"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td height="8"></td>
	</tr>
	<tr>
		<td><b><A href="/list_etc.aspx?ID=109&amp;chk_etc=section&amp;lvl=1"><IMG height="23" src="/images/bar_r_interview.gif" width="212" border="0"></A></b></td>
	</tr>
	<tr>
		<td height="4"></td>
	</tr>
	<tr>
		<td align="center">
			<table cellSpacing="0" cellPadding="0" width="212" border="0">
				<tr>
					<td width="212" bgColor="#c6c6c6" colSpan="6" height="1"></td>
				</tr>
				<tr>
					<td width="1" bgColor="#c6c6c6" height="8"></td>
					<td width="210" colSpan="4"></td>
					<td width="1" bgColor="#c6c6c6"></td>
				</tr>
				<asp:repeater id="Repeater2" runat="server">
					<ItemTemplate>
						<tr>
							<td width="1" bgColor="#C6C6C6"></td>
							<td width="3"></td>
							<td width="10" valign="top">ㆍ</td>
							<td width="190" valign="top"><a href='/view_article.aspx?ArticleID=<%# DataBinder.Eval(Container.DataItem, "ArticleID")  %>'><%# DataBinder.Eval(Container.DataItem, "Title")  %></a></td>
							<td width="7"></td>
							<td width="1" bgColor="#C6C6C6"></td>
						</tr>
					</ItemTemplate>
					<SeparatorTemplate>
						<tr>
							<td width="1" bgColor="#C6C6C6" height="3"></td>
							<td width="210" colspan="4"></td>
							<td width="1" bgColor="#C6C6C6"></td>
						</tr>
					</SeparatorTemplate>
				</asp:repeater>
				<tr>
					<td width="1" bgColor="#c6c6c6" height="5"></td>
					<td width="210" colSpan="4"></td>
					<td width="1" bgColor="#c6c6c6"></td>
				</tr>
				<tr>
					<td bgColor="#c6c6c6" colSpan="6" height="1"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td height="8">
		</td>
	</tr>
	<asp:Panel id="pnlKigo" runat="server">
		<TR>
			<TD><IMG height="23" src="/images/bar_r_kigo.gif" width="212" border="0"></TD>
		</TR>
		<TR>
			<TD height="4"></TD>
		</TR>
		<TR>
			<TD align="center">
				<TABLE cellSpacing="0" cellPadding="0" width="212" border="0">
					<TR>
						<TD width="212" bgColor="#c6c6c6" colSpan="6" height="1"></TD>
					</TR>
					<TR>
						<TD width="1" bgColor="#c6c6c6" height="8"></TD>
						<TD width="210" colSpan="4"></TD>
						<TD width="1" bgColor="#c6c6c6"></TD>
					</TR>
					<asp:repeater id="Repeater5" runat="server">
						<ItemTemplate>
							<tr>
								<td width="1" bgColor="#C6C6C6"></td>
								<td width="3"></td>
								<td width="10" valign="top">ㆍ</td>
								<td width="190" valign="top"><a href='/view_article.aspx?ArticleID=<%# DataBinder.Eval(Container.DataItem, "ArticleID")  %>'><%# replace(DataBinder.Eval(Container.DataItem, "Title"),"&lt;기고&gt;","")  %></a></td>
								<td width="7"></td>
								<td width="1" bgColor="#C6C6C6"></td>
							</tr>
						</ItemTemplate>
						<SeparatorTemplate>
							<tr>
								<td width="1" bgColor="#C6C6C6" height="3"></td>
								<td width="210" colspan="4"></td>
								<td width="1" bgColor="#C6C6C6"></td>
							</tr>
						</SeparatorTemplate>
					</asp:repeater>
					<TR>
						<TD width="1" bgColor="#c6c6c6" height="5"></TD>
						<TD width="210" colSpan="4"></TD>
						<TD width="1" bgColor="#c6c6c6"></TD>
					</TR>
					<TR>
						<TD bgColor="#c6c6c6" colSpan="6" height="1"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="8"></TD>
		</TR>
	</asp:Panel>
	<asp:Panel id="pnlKPMToday" runat="server" Visible="false">
		<TR>
			<TD><IMG height="23" src="/images/bar_r_today.gif" width="212" border="0"></TD>
		</TR>
		<TR>
			<TD width="212" height="4"></TD>
		</TR>
		<TR>
			<TD align="center" bgColor="#e6e6e6" height="46">
				<TABLE cellSpacing="0" cellPadding="0" width="186" border="0">
					<TR>
						<TD>
							<asp:panel id="pnlInfoService" runat="server" CssClass="branch_a"></asp:panel></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="8"></TD>
		</TR>
	</asp:Panel>
</table>
