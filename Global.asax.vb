Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Security

Public Class Global
    Inherits System.Web.HttpApplication

#Region " ���� ��� �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� ���� ��� �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.

    End Sub

    '���� ��� �����̳ʿ� �ʿ��մϴ�.
    Private components As System.ComponentModel.IContainer

    '����: ���� ���ν����� ���� ��� �����̳ʿ� �ʿ��մϴ�.
    '���� ��� �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' ���� ���α׷��� ���۵� �� �߻��մϴ�.
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' ������ ���۵� �� �߻��մϴ�.
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' �� ��û�� ������ �� �߻��մϴ�.
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' ����� �����Ϸ��� �� �� �߻��մϴ�.
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' ������ �Ͼ �� �߻��մϴ�.
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        On Error Resume Next
        FormsAuthentication.SignOut()
        WebNewsDB.LogOut()
        WebNewsDB.GetDataSetBySQLMun("delete from CustomersLoginCount where SessionID='" + Session.SessionID + "'")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' ���� ���α׷��� ���� �� �߻��մϴ�.
    End Sub

End Class
