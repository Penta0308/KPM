Public Class Search
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents imgbtnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label
    Protected WithEvents msg_result As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlWhere As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKeyword As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgNews As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgJournal As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dlPhoto As System.Web.UI.WebControls.DataList
    Protected WithEvents dlVideo As System.Web.UI.WebControls.DataList
    Protected WithEvents pnlPhoto As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlVideo As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlJournalMore As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlJournal As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlNewsMore As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlNews As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPhotoMore As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlVideoMore As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlNone As System.Web.UI.WebControls.Panel

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
        txtKeyword.Attributes.Add("onkeypress", "if (event.keyCode == 13) {" + Page.GetPostBackEventReference(imgbtnSearch, "") + "; return false;}")
        If Not IsPostBack Then
            If Request("keyword") <> "" Then
                txtKeyword.Text = Request("keyword")
                WebNewsDB.SetDropDownListByValue(ddlWhere, Request("media"))
                Select Case Request("media")
                    Case "news"
                        bindDataGrid(dgNews, pnlNews, pnlNewsMore, "", " Articles INNER Join  Media ON Articles.MediaID = Media.MediaID", "MediaName", "ArticleID", "Title", "JunSongDateTime", "Nayong1", "junsongdateTime desc", "Nayong2")
                        dgNews.PagerStyle.Visible = True
                    Case "journal"
                        bindDataGrid(dgJournal, pnlJournal, pnlJournalMore, "", "JournalsArticle INNER Join  Media ON JournalsArticle.JournalID = Media.MediaID", "MediaName", "JArticleID", "Title", "CAST(BalHengYear AS varchar(5)) + '년 ' + CAST(GwonHo AS varchar(5)) + '호' AS balheng", "Nayong1", "BalHengYear desc, gwonho desc", "Nayong2")
                        dgJournal.PagerStyle.Visible = True
                    Case "photo"
                        bindDataList(dlPhoto, pnlPhoto, pnlPhotoMore, "", "mmFiles", "", "mmFileID", "Title", "InputDateTime", "Caption", "201")
                    Case "video"
                        bindDataList(dlVideo, pnlVideo, pnlVideoMore, "", "mmFiles", "", "mmFileID", "Title", "InputDateTime", "Caption", "202")
                    Case Else
                        bindDataGrid(dgNews, pnlNews, pnlNewsMore, "5", " Articles INNER Join  Media ON Articles.MediaID = Media.MediaID", "MediaName", "ArticleID", "Title", "JunSongDateTime", "Nayong1", "junsongdateTime desc", "Nayong2")
                        bindDataGrid(dgJournal, pnlJournal, pnlJournalMore, "5", "JournalsArticle INNER Join  Media ON JournalsArticle.JournalID = Media.MediaID", "MediaName", "JArticleID", "Title", "CAST(BalHengYear AS varchar(5)) + '년 ' + CAST(GwonHo AS varchar(5)) + '호' AS balheng", "Nayong1", "BalHengYear desc, gwonho desc", "Nayong2")
                        bindDataList(dlPhoto, pnlPhoto, pnlPhotoMore, "5", "mmFiles", "", "mmFileID", "Title", "InputDateTime", "Caption", "201")
                        bindDataList(dlVideo, pnlVideo, pnlVideoMore, "5", "mmFiles", "", "mmFileID", "Title", "InputDateTime", "Caption", "202")

                End Select

            End If

        End If
    End Sub


    Sub bindDataGrid(ByVal DataGrid As WebControls.DataGrid, ByVal Panel As WebControls.Panel, ByVal PanelMore As WebControls.Panel, ByVal PageSize As String, ByVal Media As String, _
                ByVal Field_MediaName As String, ByVal Field_ID As String, ByVal Field_Title As String, _
                ByVal Field_Date As String, ByVal Field_Nayong As String, ByVal Sort As String, Optional ByVal Field_Nayong2 As String = "")

        Dim Query As String
        Dim DS As DataSet

        Query = "Select "
        If PageSize <> "" Then Query += " Top " + PageSize
        If Field_MediaName <> "" Then Query += " " + Field_MediaName + ","
        Query += " " + Field_ID + "," + Field_Title + "," + Field_Date + " "

        Query += " From " + Media
        Query += " Where "
        If Field_Nayong2 <> "" Then Query += splitKWD(Field_Nayong2) + " or "
        Query += splitKWD(Field_Nayong) + " or "
        Query += splitKWD(Field_Title)

        If Sort <> "" Then
            Query += " order by " + Sort
        End If

        'Response.Write(Query)
        DS = WebNewsDB.GetDataSetBySQLMun(Query)

        If DS.Tables(0).Rows.Count = 0 Then
            Panel.Visible = False
            If Request("media") <> "0" And Request("media") <> "" Then pnlNone.Visible = True
            Exit Sub
        Else
            Panel.Visible = True
            If Request("media") = "0" Or Request("media") = "" Then
                If DS.Tables(0).Rows.Count = 5 Then PanelMore.Visible = True
            End If
        End If

        DataGrid.DataSource = DS.Tables(0)
        DataGrid.DataBind()

        DS.Dispose()


    End Sub
    Sub bindDataList(ByVal DataGrid As WebControls.DataList, ByVal Panel As WebControls.Panel, ByVal PanelMore As WebControls.Panel, ByVal PageSize As String, ByVal Media As String, _
                ByVal Field_MediaName As String, ByVal Field_ID As String, ByVal Field_Title As String, _
                ByVal Field_Date As String, ByVal Field_Nayong As String, Optional ByVal Field_Auth As String = "")

        Dim Query As String
        Dim DS As DataSet

        Query = "Select "
        If PageSize <> "" Then Query += " Top " + PageSize
        If Field_MediaName <> "" Then Query += Field_MediaName + ","
        Query += " Location_Thumb, "
        Query += " " + Field_ID + "," + Field_Title + "," + Field_Date + " "

        Query += " From " + Media
        Query += " Where "
        If Field_Auth <> "" Then Query += "authID=" + Field_Auth + " and "
        Query += "status > 0 and "
        Query += "(" + splitKWD(Field_Nayong) + " or "
        Query += splitKWD(Field_Title) + ")"

        If Request.QueryString("sort") <> "" Then
            Query += " order by " + Request.QueryString("sort")
        End If

        'Response.Write(Query)
        DS = WebNewsDB.GetDataSetBySQLMun(Query)

        If DS.Tables(0).Rows.Count = 0 Then
            Panel.Visible = False
            If Request("media") <> "0" Or Request("media") = "" Then pnlNone.Visible = True
            Exit Sub
        Else
            If Request("media") <> "0" Or Request("media") = "" Then
                If DS.Tables(0).Rows.Count > 5 Then PanelMore.Visible = True
            End If
            Panel.Visible = True
        End If

        DataGrid.DataSource = DS.Tables(0)
        DataGrid.DataBind()

        DS.Dispose()


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

    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        dgNews.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dgNews, pnlNews, pnlNewsMore, "", " Articles INNER Join  Media ON Articles.MediaID = Media.MediaID", "MediaName", "ArticleID", "Title", "JunSongDateTime", "Nayong1", "junsongdateTime desc", "Nayong2")
    End Sub
    Sub doPaging2(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        dgJournal.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dgJournal, pnlJournal, pnlJournalMore, "", "JournalsArticle INNER Join  Media ON JournalsArticle.JournalID = Media.MediaID", "MediaName", "JArticleID", "Title", "CAST(BalHengYear AS varchar(5)) + '년 ' + CAST(GwonHo AS varchar(5)) + '호' AS balheng", "Nayong1", "BalHengYear desc, gwonho desc", "Nayong2")
    End Sub


    Private Sub imgbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        Response.Write("<script>window.location='search.aspx?media=" + ddlWhere.SelectedValue + "&keyword=" + HttpUtility.UrlEncodeUnicode(txtKeyword.Text) + "';</script>")
    End Sub
End Class

