Public Class gate_main_jpn
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents Repeater2 As System.Web.UI.WebControls.Repeater

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
        BindLatestNews()
        BindLatestNotice()
    End Sub
    Private Sub BindLatestNews()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 10 MediaNameJpn, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID>= 303 "
        Str_Sql += " and importance > 0 "
        Str_Sql += " and LanguageId = 201 "
        Str_Sql += " order by JunsongDateTime desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
    Private Sub BindLatestNotice()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 5 * "
        Str_Sql += " FROM bbs "
        Str_Sql += " where bbsid='noticejpn' "
        Str_Sql += " order by number desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater2.DataSource = DS.Tables(0)
        Repeater2.DataBind()
        DS.Dispose()
    End Sub
End Class