Imports System.Web.Security
Public Class Left
    Inherits System.Web.UI.UserControl
#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hl_s_103 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_102 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_101 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_104 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_105 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_106 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_107 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_101 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_102 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_103 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_104 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_105 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_106 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_107 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_108 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_109 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_l_110 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_all As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_past As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_108 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_s_109 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_about As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_usage As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_media As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_key As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_ad As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_copyright As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lbx_l_Journals As System.Web.UI.WebControls.ListBox
    Protected WithEvents btn_logout As System.Web.UI.WebControls.ImageButton
    Protected WithEvents hl_qna As System.Web.UI.WebControls.HyperLink
    Protected WithEvents pnlLogout As System.Web.UI.WebControls.Panel
    Protected WithEvents hl_terms As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hl_contact As System.Web.UI.WebControls.HyperLink

    '����: ������ �ڸ� ǥ���� ������ Web Form �����̳��� �ʼ� �����Դϴ�.
    '�����ϰų� �ű��� ���ʽÿ�.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �� �޼��� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
        '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
        InitializeComponent()
    End Sub

#End Region
    Private WebNewsDB As New db
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���⿡ ����� �ڵ带 ��ġ�Ͽ� �������� �ʱ�ȭ�մϴ�.
        If Not IsPostBack Then
            ViewListBox()
            If context.User.Identity.Name = "CustomerIPLogin" Then
                pnlLogout.Visible = False
            End If

            If IsNothing(Session("CustomerLoginID")) Then
                FormsAuthentication.SignOut()
                Response.Redirect(Request.Url.ToString)
            End If

        End If
    End Sub
    Private Sub lbx_Journals_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbx_l_Journals.SelectedIndexChanged
        Response.Redirect("/list_journal.aspx?MediaID=" + lbx_l_Journals.SelectedItem.Value.ToString)
    End Sub
    Private Sub ViewListBox()
        Dim DT As DataTable
        Dim i As Integer
        Dim str_sql As String
        str_sql = "SELECT MediaID, MediaName FROM Media WHERE MediaParentID=201 ORDER BY sequence"
        DT = WebNewsDB.GetDataTableBySQL(str_sql)

        If DT.Rows.Count = 0 Then
            lbx_l_Journals.Items.Add("����")
            Exit Sub
        Else
            lbx_l_Journals.DataSource = DT
            lbx_l_Journals.DataBind()
        End If

        DT.Dispose()
    End Sub

    Private Sub btn_logout_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_logout.Click
        FormsAuthentication.SignOut()
        WebNewsDB.GetDataSetBySQLMun("delete from CustomersLoginCount where SessionID='" + Session.SessionID + "'")
        Response.Redirect("/gate/gate_main.aspx")
    End Sub
End Class
