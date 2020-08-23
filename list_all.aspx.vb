Public Class list_all
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lbl_bar As System.Web.UI.WebControls.Label
    Protected WithEvents imgbtnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ddlMedia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKeyword As System.Web.UI.WebControls.TextBox

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
        txtKeyword.Attributes.Add("onkeypress", "if (event.keyCode == 13) {" + Page.GetPostBackEventReference(imgbtnSearch, "") + "; return false;}")
        If Not IsPostBack Then
            GetTitle()
            InitDropDownList(ddlMedia, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=101 order by sequence", "전체")
            BindData()
            Selected()
        End If
    End Sub
    Private Sub GetTitle()
        If Request.QueryString("lvl") = "1" Then
            lbl_bar.Text = "주요기사"
            lbl_bar.ForeColor = Color.Red
        End If
    End Sub
    Public Sub InitDropDownList(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataTextField As String, ByVal DataValueField As String, ByVal SQLMun As String, Optional ByVal DefaultValue As String = "")
        Dim DT As DataTable
        Dim i As Integer
        DT = WebNewsDB.GetDataTableBySQL(SQLMun)
        With ddlTemp
            .Items.Clear()
            If DT.Rows.Count = 0 Then
                ddlTemp.Items.Add("없음")
                Exit Sub
            End If

            If DefaultValue <> "" Then
                Dim aItem1 As New System.Web.UI.WebControls.ListItem
                aItem1.Text = DefaultValue
                aItem1.Value = "0"
                ddlTemp.Items.Add(aItem1)
            End If

            For i = 0 To DT.Rows.Count - 1
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = DT.Rows(i).Item(DataTextField)
                aItem.Value = DT.Rows(i).Item(DataValueField)
                ddlTemp.Items.Add(aItem)
            Next
            '.DataTextField = DataTextField
            '.DataValueField = DataValueField
            '.DataSource = DT
            .DataBind()
        End With
        DT.Dispose()
    End Sub
    Private Sub BindData()
        Dim DT As DataTable
        Dim str_sql As String

        str_sql = " SELECT ArticleID, Title, LanguageID, SectionID, Articles.MediaID, MediaName, LocalID, JunsongDateTime, chkPhoto "
        str_sql += " FROM Articles "
        str_sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        str_sql += " where AuthID >= 303 "
        str_sql += " and LanguageID = 101 " '한국어
        If ddlMedia.SelectedValue <> 0 Then
            str_sql += " and Articles.MediaID = " + ddlMedia.SelectedValue
        End If
        str_sql += " 	and importance > " + Request.QueryString("lvl")
        str_sql += " and JunsongDateTime > DATEADD(day, -7,  "
        str_sql += " ( SELECT MAX(JunsongDateTime) FROM Articles where AuthID >= 303 and importance > " + Request.QueryString("lvl") + ") ) "
        str_sql += " and (" + splitKWD("Title") + " Or " + splitKWD("Nayong1") + " Or " + splitKWD("Nayong2") + ")"
        str_sql += " 	order by JunsongDateTime desc "

        DT = WebNewsDB.GetDataTableBySQL(str_sql)
        DataGrid1.DataSource = DT
        DataGrid1.DataBind()
        DT.Dispose()
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        GetTitle()
        BindData()
    End Sub
    Private Sub Selected()
        If Request.QueryString("lvl") = "1" Then
            CType(FindControl("Left1").FindControl("hl_key"), HyperLink).ForeColor = Color.Red
        Else
            CType(FindControl("Left1").FindControl("hl_all"), HyperLink).ForeColor = Color.Red
        End If
    End Sub
    Function splitKWD(ByVal FieldName As String) As String

        Dim KWD() As String = Split(txtKeyword.Text, " ")
        Dim result As String
        Dim i As Integer

        For i = 0 To KWD.Length - 1
            If i <> 0 Then result += " And "
            result += FieldName + " LIKE '%" + KWD(i) + "%'"
        Next

        Return "(" + result + ")"

    End Function
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function

    Private Sub imgbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub
End Class
