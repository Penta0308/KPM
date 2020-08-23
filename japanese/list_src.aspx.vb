Public Class list_src_jpn
    Inherits System.Web.UI.Page

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
    Public image As String
    Public lim_date As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            GetTitle()
            BindData()
            If Not (CType(FindControl("TOP1").FindControl("hl_" + CStr(Request.QueryString("MediaID"))), HyperLink) Is Nothing) Then
                CType(FindControl("TOP1").FindControl("hl_" + Request.QueryString("MediaID")), HyperLink).ForeColor = Color.Yellow
            End If
        End If
    End Sub
    Private Sub GetTitle()
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("SELECT MediaName, lim_number, lim_date, image from Media WHERE MediaID = " + Request.QueryString("MediaID"))
        lim_date = DS.Tables(0).Rows(0).Item("lim_date")
        If Not IsDBNull(DS.Tables(0).Rows(0).Item("image")) Then
            image = DS.Tables(0).Rows(0).Item("image")
            image = "<IMG height=""26"" src=""/images/" + image + """ width=""170"">"
        Else
            image = DS.Tables(0).Rows(0).Item("MediaName")
            image = "&nbsp;&nbsp;&nbsp;<b>" + image + "</b>"
        End If
        DS.Dispose()
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        DS = WebNewsDB.GetArticlesByMedia(Request.QueryString("MediaID"), 0, lim_date, 201) '일본어
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
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        GetTitle()
        BindData()
    End Sub
End Class
