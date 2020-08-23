Public Class ArticleListEditor_main_uc
    Inherits System.Web.UI.UserControl

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid

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
    Public MediaID As String
    Public lim_number As String
    Public image As String
    Private LanguageID As Integer = 101
    Public LanguageColor As String = "ffaaaa"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetTitle()
        If Not (Request.Cookies("LanguageID") Is Nothing) Then
            LanguageID = Request.Cookies("LanguageID").Value
            LanguageColor = Request.Cookies("LanguageColor").Value
        End If

        If Not IsPostBack Then
            BindData()
        End If
    End Sub
    Private Sub GetTitle()
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("SELECT MediaName, lim_number, lim_date, image from Media WHERE MediaID = " + MediaID)
        lim_number = DS.Tables(0).Rows(0).Item("lim_number")
        image = DS.Tables(0).Rows(0).Item("image")
        DS.Dispose()
    End Sub

    Private Sub BindData()
        Dim DS As DataSet
        DS = WebNewsDB.GetArticlesByMedia(CInt(MediaID), 1, lim_number, LanguageID)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
End Class
