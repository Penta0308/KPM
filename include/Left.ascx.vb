Imports System.Web.Security
Public Class Left
    Inherits System.Web.UI.UserControl
#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
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

    '참고: 다음의 자리 표시자 선언은 Web Form 디자이너의 필수 선언입니다.
    '삭제하거나 옮기지 마십시오.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 이 메서드 호출은 Web Form 디자이너에 필요합니다.
        '코드 편집기를 사용하여 수정하지 마십시오.
        InitializeComponent()
    End Sub

#End Region
    Private WebNewsDB As New db
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
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
            lbx_l_Journals.Items.Add("없음")
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
