Public Class Articles2_main_jpn
    Inherits System.Web.UI.UserControl

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
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
            BindData()
        End If
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        Dim Str_Sql As String

        Str_Sql = "SELECT TOP 20 MediaName, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime, chkphoto "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID >= 303 "
        Str_Sql += " and LanguageID = 201 "
        Str_Sql += " order by JunsongDateTime desc "

        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        DataList1.DataSource = DS.Tables(0)
        DataList1.DataBind()
        DS.Dispose()
        Dim i As Integer

        For i = 4 To DataList1.Items.Count - 2 Step 5
            CType(DataList1.Items(i).FindControl("Label2"), Label).Text = "<img src=/images/line_main.gif>"
        Next

    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
End Class

