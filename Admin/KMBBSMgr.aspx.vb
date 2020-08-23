Public Class KMBBSMgr
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents lblLanguage As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBBSSelector As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bbsFrame As System.Web.UI.HtmlControls.HtmlGenericControl
    Private WebNewsConstants As Constants
#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        If Request.Cookies("LoginUserID") Is Nothing Then
            Response.Redirect("AdminMain.aspx")
            Exit Sub
        End If
        If Not (Request.QueryString("EditMode") Is Nothing) Then
            If WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode")) = "" Then
                Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                Exit Sub
            Else
                Session("User_type") = 1
                bbsFrame.Attributes.Add("src", "bbs_list.aspx?bbsid=kmnotice")
            End If
        End If

    End Sub

    Private Sub ddlBBSSelector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBBSSelector.SelectedIndexChanged
        'Response.Write("<script>frames.item(1).location.href='';</script>")
        bbsFrame.Attributes.Add("src", "bbs_list.aspx?bbsid=" + ddlBBSSelector.SelectedValue)
    End Sub
End Class
