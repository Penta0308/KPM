Public Class Login1
    Inherits System.Web.UI.UserControl

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtLoginUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLoginPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnLogin As System.Web.UI.WebControls.Button
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink

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
    Private WebNewsConstants As Constants
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim DS As DataSet

        DS = WebNewsDB.GetCountByLoginUserIDAndPassword(txtLoginUserID.Text, txtLoginPassword.Text)
        If DS.Tables(0).Rows.Count > 0 Then
            Dim ckLogin As New HttpCookie("LoginUserID")
            ckLogin.Value = txtLoginUserID.Text
            ckLogin.Expires = Now.AddHours(2)
            Response.Cookies.Add(ckLogin)

            Dim ckLang As New HttpCookie("LanguageID")
            ckLang.Value = WebNewsConstants.LanguageKorean
            ckLang.Expires = Now.AddHours(2)
            Response.Cookies.Add(ckLang)

            Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(DS.Tables(0).Rows(0).Item("LoginUserName") + " " + DS.Tables(0).Rows(0).Item("Jigchek") + " 환영합니다. 해당 관리권한에 따라 좌측의 메뉴를 사용할수 있습니다"))
        Else
            Response.Write("<script>alert('잘못된 아이디 혹은 패스워드 입니다');history.back(-1);</script>")
            Exit Sub
        End If
        DS.Dispose()
    End Sub
End Class
