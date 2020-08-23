Public Class Right_jpn
    Inherits System.Web.UI.UserControl

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents Repeater2 As System.Web.UI.WebControls.Repeater
    Protected WithEvents Repeater3 As System.Web.UI.WebControls.Repeater
    Protected WithEvents Repeater4 As System.Web.UI.WebControls.Repeater
    Protected WithEvents pnl_photo As System.Web.UI.WebControls.Panel
    Protected WithEvents pnl_video As System.Web.UI.WebControls.Panel
    Protected WithEvents imgbtnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtSearch As System.Web.UI.HtmlControls.HtmlInputText

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
    Public cmd As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
        If Not IsPostBack Then
            If cmd <> "photo" Then
                ViewPhoto()
                pnl_photo.Visible = True
            End If
            If cmd <> "video" Then
                ViewVideo()
                pnl_video.Visible = True
            End If
            'ViewEditorial()
            ViewInterview()
        End If
        txtSearch.Attributes.Add("onkeypress", "if (event.keyCode == 13) {" + Page.GetPostBackEventReference(imgbtnSearch, "") + "; return false;}")
    End Sub
    Private Sub ViewEditorial()
        Dim DS As DataSet
        Dim Query As String

        Query = " select TOP 2 ArticleID, Title from Articles "
        Query += " where SectionID = 108 and importance > 1 "
        Query += " order by JunsongDateTime desc "

        DS = WebNewsDB.GetDataSetBySQLMun(Query)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
    Private Sub ViewInterview()
        Dim DS As DataSet
        Dim Query As String

        Query = " select TOP 2 ArticleID, Title from Articles "
        Query += " where SectionID = 109 and LanguageID=201"
        Query += " order by JunsongDateTime desc "

        DS = WebNewsDB.GetDataSetBySQLMun(Query)
        Repeater2.DataSource = DS.Tables(0)
        Repeater2.DataBind()
        DS.Dispose()
    End Sub
    Private Sub ViewPhoto()
        Dim DS As DataSet
        DS = WebNewsDB.FGetmmFilesByAuthID(201, 0, 1)
        Repeater3.DataSource = DS.Tables(0)
        Repeater3.DataBind()
        DS.Dispose()
    End Sub
    Private Sub ViewVideo()
        Dim DS As DataSet
        DS = WebNewsDB.FGetmmFilesByAuthID(202, 0, 1)
        Repeater4.DataSource = DS.Tables(0)
        Repeater4.DataBind()
        DS.Dispose()
    End Sub

    Private Sub imgbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        Response.Redirect("search.aspx?media=0&keyword=" + HttpUtility.UrlEncodeUnicode(txtSearch.Value))
    End Sub
End Class
