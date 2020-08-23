Public Class list_journal
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbl_bar As System.Web.UI.WebControls.Label
    Protected WithEvents lbx_Journals As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlGwonho As System.Web.UI.WebControls.DropDownList

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
        If Not IsPostBack Then
            BindYear()
            GetTitle()
            BindData()
            ViewListBox()
            CType(FindControl("Left1").FindControl("lbx_l_Journals"), ListBox).SelectedValue = Request.QueryString("MediaID")
            lbx_Journals.SelectedValue = Request.QueryString("MediaID")
        End If
    End Sub
    Private Sub GetTitle()
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("SELECT MediaName from Media WHERE MediaID = " + Request.QueryString("MediaID"))
        lbl_bar.Text = DS.Tables(0).Rows(0).Item("MediaName")
        DS.Dispose()
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT JArticleID, Title, Writers, BalHengYear, GwonHo, FileName, Status "
        Str_Sql += " FROM JournalsArticle "
        Str_Sql += " where JournalID = " + Request.QueryString("MediaID")
        Str_Sql += " and Status > 0 "
        If ddlYear.SelectedValue <> "전체" Then
            Str_Sql += " and BalHengYear= " + ddlYear.SelectedValue
        End If
        If ddlGwonho.SelectedValue <> "전체" Then
            Str_Sql += " and Gwonho= " + ddlGwonho.SelectedValue
        End If
        Str_Sql += " order by BalHengYear desc, GwonHo desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub
    Sub BindYear()
        Dim ds As DataSet
        Dim SQL As String
        Dim i As Integer
        ddlYear.Items.Clear()

        SQL = "select max(balHengYear)as MaxValue, min(balHengYear)as MinValue"
        SQL += " from JournalsArticle "
        SQL += " where JournalID = " + Request.QueryString("MediaID")
        ds = WebNewsDB.GetDataSetBySQLMun(SQL)

        If ds.Tables(0).Rows.Count = 0 Then
            ddlYear.Items.Add("없음")
        Else
            ddlYear.Items.Add("전체")
        End If
        With ds.Tables(0).Rows(0)
            For i = .Item("MinValue") To .Item("MaxValue")
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = i
                aItem.Value = i
                ddlYear.Items.Add(aItem)
            Next
        End With
    End Sub
    Sub bindGwonho()
        Dim ds As DataSet
        Dim SQL As String
        Dim i As Integer
        ddlGwonho.Items.Clear()

        If ddlYear.SelectedValue = "전체" Then
            ddlGwonho.Items.Add("전체")
            Exit Sub
        End If
        SQL = "select max(gwonho)as MaxValue, min(gwonho)as MinValue"
        SQL += " from JournalsArticle "
        SQL += " where JournalID = " + Request.QueryString("MediaID")
        SQL += " and BalHengYear = " + ddlYear.SelectedValue
        ds = WebNewsDB.GetDataSetBySQLMun(SQL)

        If ds.Tables(0).Rows.Count = 0 Then
            ddlGwonho.Items.Add("없음")
        Else
            ddlGwonho.Items.Add("전체")
        End If
        With ds.Tables(0).Rows(0)
            For i = .Item("MinValue") To .Item("MaxValue")
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = i
                aItem.Value = i
                ddlGwonho.Items.Add(aItem)
            Next
        End With
    End Sub
    Public Function Img_pdf(ByVal FileName As String) As String
        Dim Img_p As String
        If FileName <> "" Then
            Img_p = "<img src='/images/icon_pdf.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
    Public Function Name_Writers(ByVal Writers As String) As String
        Dim name_w As String
        If Writers <> "" Then
            name_w = " / " + Writers
        End If
        Return name_w
    End Function
    Private Sub ViewListBox()
        Dim DT As DataTable
        Dim i As Integer
        Dim str_sql As String
        str_sql = "SELECT MediaID, MediaName FROM Media WHERE MediaParentID=201 ORDER BY sequence"
        DT = WebNewsDB.GetDataTableBySQL(str_sql)

        If DT.Rows.Count = 0 Then
            lbx_Journals.Items.Add("없음")
            Exit Sub
        Else
            lbx_Journals.DataSource = DT
            lbx_Journals.DataBind()
        End If
        DT.Dispose()
    End Sub
    Private Sub lbx_Journals_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbx_Journals.SelectedIndexChanged
        Response.Redirect("/list_journal.aspx?MediaID=" + lbx_Journals.SelectedItem.Value.ToString)
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        GetTitle()
        BindData()
    End Sub

    Private Sub ddlYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        bindGwonho()
        BindData()
    End Sub

    Private Sub ddlGwonho_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlGwonho.SelectedIndexChanged
        BindData()
    End Sub
End Class
