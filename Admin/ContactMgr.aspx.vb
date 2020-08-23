Public Class ContactMgr
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents lblLanguage As System.Web.UI.WebControls.Label
    Private WebNewsConstants As Constants
#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '����: ������ �ڸ� ǥ���� ������ Web Form �����̳��� �ʼ� �����Դϴ�.
    '�����ϰų� �ű��� ���ʽÿ�.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �� �޼��� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
        '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
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
                Response.Write("<script>alert('�� �޴��� �������� �����ϴ�. �����ڿ��� ������ ��û�Ͻʽÿ�.');history.back(-1);</script>")
                Exit Sub
            Else
                Session("User_type") = 1
            End If
        End If

    End Sub

End Class
