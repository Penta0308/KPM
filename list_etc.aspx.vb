Public Class list_etc
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbl_key As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_bar As System.Web.UI.WebControls.Label
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
    Public lim_date As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            GetTitle()
            BindData()
            Selected()
        End If
    End Sub
    Private Sub GetTitle()
        Dim DS As DataSet
        If Request.QueryString("chk_etc") = "section" Then
            DS = WebNewsDB.GetDataSetBySQLMun("SELECT SectionName, lim_date from Sections WHERE SectionID = " + Request.QueryString("ID"))
            lbl_bar.Text = DS.Tables(0).Rows(0).Item("SectionName")
        Else
            DS = WebNewsDB.GetDataSetBySQLMun("SELECT LocalName, lim_date from Locals WHERE LocalID = " + Request.QueryString("ID"))
            lbl_bar.Text = DS.Tables(0).Rows(0).Item("LocalName")
        End If

        lim_date = DS.Tables(0).Rows(0).Item("lim_date")
        DS.Dispose()
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        Dim str_lvl As String
        If Request.QueryString("lvl") = "1" Then
            str_lvl = Request.QueryString("lvl")
            lbl_key.Visible = True
        Else
            str_lvl = "0"
        End If
        DS = WebNewsDB.GetArticlesByETC(Request.QueryString("ID"), Request.QueryString("chk_etc"), str_lvl, lim_date, 101) '한국어
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub
    Private Sub Selected()
        If Request.QueryString("chk_etc") = "section" Then
            CType(FindControl("Left1").FindControl("hl_s_" + Request.QueryString("ID")), HyperLink).ForeColor = Color.Red
        Else
            CType(FindControl("Left1").FindControl("hl_l_" + Request.QueryString("ID")), HyperLink).ForeColor = Color.Red
        End If
    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        GetTitle()
        BindData()
    End Sub
End Class
