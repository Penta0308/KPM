Public Class Top_jpn
    Inherits System.Web.UI.UserControl

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbl_LastEdit As System.Web.UI.WebControls.Label
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater

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
            BindLatestNews()
            ViewLastEdit()
        End If
    End Sub
    Private Sub ViewLastEdit()

        lbl_LastEdit.Text = WebNewsDB.GetLastUpdated.ToString("yyyy年 MM月 dd日")

        '        Select Case Weekday(WebNewsDB.GetLastUpdated)
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Sunday
        '        lbl_LastEdit.Text += " (일)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Monday
        '        lbl_LastEdit.Text += " (월)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Tuesday
        '        lbl_LastEdit.Text += " (화)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Wednesday
        '        lbl_LastEdit.Text += " (수)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Thursday
        '        lbl_LastEdit.Text += " (목)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Friday
        '        lbl_LastEdit.Text += " (금)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Saturday
        '        lbl_LastEdit.Text += " (토)"
        '        End Select

        lbl_LastEdit.Text += " " + WebNewsDB.GetLastUpdated.ToString("HH時 mm分") + " 更新"

    End Sub
    Private Sub BindLatestNews()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 5 MediaName, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID>= 303 " '웹출판
        Str_Sql += " and LanguageID = 201 " '일본어
        Str_Sql += " and importance > 0 "
        Str_Sql += " order by JunsongDateTime desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
End Class
