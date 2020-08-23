Public Class AdminLeft
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblJunsongArticleCount As System.Web.UI.WebControls.Label
    Protected WithEvents lblJunsongCount As System.Web.UI.WebControls.Label
    Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblID As System.Web.UI.WebControls.Label
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hlLogInOut As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hlInfoEdit As System.Web.UI.WebControls.HyperLink

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
    Public LanguageColor As String = "ffaaaa"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim LanguageID As Integer = 101
        If Not (Request.Cookies("LanguageID") Is Nothing) Then
            LanguageID = Request.Cookies("LanguageID").Value
        Else
            Dim cLanguageID As New HttpCookie("LanguageID")
            cLanguageID.Value = ddlLanguage.SelectedValue
            Response.Cookies.Add(cLanguageID)

            Dim Language As New HttpCookie("Language")
            Language.Value = ddlLanguage.SelectedItem.Text
            Response.Cookies.Add(Language)
        End If
        If Not (Request.Cookies("LanguageColor") Is Nothing) Then
            LanguageColor = Request.Cookies("LanguageColor").Value
        Else
            Dim cLanguageColor As New HttpCookie("LanguageColor")
            cLanguageColor.Value = LanguageColor
            Response.Cookies.Add(cLanguageColor)
        End If

        If Not IsPostBack Then
            WebNewsDB.SetDropDownListByValue(ddlLanguage, LanguageID)

        End If
        If (Request.Cookies("LoginUserID") Is Nothing) Then
            hlLogInOut.Text = "�α���"
            hlLogInOut.NavigateUrl = "../Admin/AdminMain.aspx"
            hlInfoEdit.Visible = False
        Else
            hlLogInOut.Text = "�α׾ƿ�" + " - " + Request.Cookies("LoginUserID").Value
            hlLogInOut.NavigateUrl = "../Admin/AdminMain.aspx?Logout=True"
            hlInfoEdit.Visible = True
            '���� ��Ȳ�� ������ �ҷ��´�
            lblJunsongArticleCount.Text = "(" + CStr(WebNewsDB.GetArticleCountByAuthID(LanguageID, WebNewsConstants.AdminEditModeWriterNew)) + ")"
        End If

    End Sub
    Private Sub ddlLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlLanguage.SelectedIndexChanged
        If Not (Request.Cookies("LoginUserID") Is Nothing) Then
            Dim LanguageID As New HttpCookie("LanguageID")
            LanguageID.Value = ddlLanguage.SelectedValue
            Response.Cookies.Add(LanguageID)

            Dim Language As New HttpCookie("Language")
            Language.Value = ddlLanguage.SelectedItem.Text
            Response.Cookies.Add(Language)

            If ddlLanguage.SelectedValue = 101 Then
                LanguageColor = "ffaaaa"
            ElseIf ddlLanguage.SelectedValue = 201 Then
                LanguageColor = "D4EAFF"
            ElseIf ddlLanguage.SelectedValue = 301 Then
                LanguageColor = "ffffaa"
            End If

            Dim cLanguageColor As New HttpCookie("LanguageColor")
            cLanguageColor.Value = LanguageColor
            Response.Cookies.Add(cLanguageColor)
        End If
        Response.Redirect(Request.Url.ToString)
        'Response.Redirect("AdminMain.aspx")
    End Sub
End Class
