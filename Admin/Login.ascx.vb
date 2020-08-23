Public Class Login1
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtLoginUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLoginPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnLogin As System.Web.UI.WebControls.Button
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink

    '����: ������ �ڸ� ǥ���� ������ Web Form �����̳��� �ʼ� �����Դϴ�.
    '�����ϰų� �ű��� ���ʽÿ�.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �� �޼��� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
        '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
        InitializeComponent()
    End Sub

#End Region
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���⿡ ����� �ڵ带 ��ġ�Ͽ� �������� �ʱ�ȭ�մϴ�.
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

            Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(DS.Tables(0).Rows(0).Item("LoginUserName") + " " + DS.Tables(0).Rows(0).Item("Jigchek") + " ȯ���մϴ�. �ش� �������ѿ� ���� ������ �޴��� ����Ҽ� �ֽ��ϴ�"))
        Else
            Response.Write("<script>alert('�߸��� ���̵� Ȥ�� �н����� �Դϴ�');history.back(-1);</script>")
            Exit Sub
        End If
        DS.Dispose()
    End Sub
End Class
