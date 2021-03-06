<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Articles2_main.ascx.vb" Inherits="KPPress.Articles2_main" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="DataGrid1" ShowHeader="False" Font-Size="9pt"
	Width="558px" AutoGenerateColumns="False" AllowPaging="False" BorderStyle="None" BackColor="White"
	CellPadding="1" GridLines="None" runat="server">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<a href="/view_article.aspx?ArticleID=<%# Container.DataItem("ArticleID") %>">[<%= MediaName %>] <%# Container.DataItem("Title") %></a>
				<%# Img_photo(Container.DataItem("chkPhoto")) %>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" Height="20"></ItemStyle>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="JunsongDateTime" DataFormatString="{0:MM-dd HH:mm}">
			<ItemStyle HorizontalAlign="Right" Width="80" VerticalAlign="Top"></ItemStyle>
		</asp:BoundColumn>
	</Columns>
</asp:datagrid>
