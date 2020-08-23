Public Class ArticleLinker
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants
    Dim DS As DataSet
    Protected WithEvents datagrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMedia As System.Web.UI.WebControls.DropDownList

    Dim strQuery As String
#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlSelectMenu As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtSearchKeyWord As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNone As System.Web.UI.WebControls.Label
    Protected WithEvents ddlGroupM As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnMove As System.Web.UI.WebControls.Button
    Protected WithEvents btnDel As System.Web.UI.WebControls.Button
    Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtUserID As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    '참고: 다음의 자리 표시자 선언은 Web Form 디자이너의 필수 선언입니다.
    '삭제하거나 옮기지 마십시오.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 이 메서드 호출은 Web Form 디자이너에 필요합니다.
        '코드 편집기를 사용하여 수정하지 마십시오.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearchKeyWord.Attributes.Add("onkeypress", "if (event.keyCode == 13) {" + Page.GetPostBackEventReference(btnSearch, "") + ";document.all['btnSearch'].focus(); return false;}")

        If Not IsPostBack Then
            InitDropDownList(ddlMedia, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=101 order by sequence", "- 신문선택 -")
            'bindList()
        End If
    End Sub

    Private Sub bindList()
        lblNone.Visible = False

        strQuery = "select ArticleID, title "
        strQuery += " FROM Articles "

        strQuery += " where " + splitKWD("Title")

        If ddlMedia.SelectedValue <> 0 Then
            strQuery += " and Articles.MediaID = " + ddlMedia.SelectedValue
        End If

        strQuery += " order by inputdatetime desc"

        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)
        'lblTotal.Text = DS.Tables(0).Rows.Count
        If DS.Tables(0).Rows.Count = 0 Then
            lblNone.Visible = True
            DS.Dispose()
            'Exit Sub
        End If

        datagrid1.DataSource = DS.Tables(0)
        datagrid1.DataBind()

        DS.Dispose()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        datagrid1.CurrentPageIndex = 0
        bindList()
    End Sub
    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim i, sum As Integer
        Dim strArticleID As String
        sum = 0

        For i = 0 To datagrid1.Items.Count - 1
            If (CType(datagrid1.Items(i).FindControl("CheckBox1"), UI.WebControls.CheckBox)).Checked = True Then
                strArticleID += datagrid1.Items(i).Cells(1).Text + ";"
                sum = sum + 1
            End If
        Next

        Response.Write("<script>opener.document.all.txtLinkArticleID2.value+='" + strArticleID + "';</script>")
        Response.Write("<script>opener.document.Form1.LinkSubmit.click();</script>")
        Response.Write("<script>alert('총" & sum & "건의 기사링크가 저장되었습니다');</script>")
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
    Function splitKWD(ByVal FieldName As String) As String

        Dim KWD() As String = Split(txtSearchKeyWord.Text, " ")
        Dim result As String
        Dim i As Integer

        For i = 0 To KWD.Length - 1
            If i <> 0 Then result += " And "
            result += FieldName + " LIKE '%" + KWD(i) + "%'"
        Next

        Return "(" + result + ")"

    End Function
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        datagrid1.CurrentPageIndex = e.NewPageIndex
        bindList()
    End Sub
End Class
