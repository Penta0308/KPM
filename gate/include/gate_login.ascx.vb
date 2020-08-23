Imports System.Web.Security
Public Class gate_login
    Inherits System.Web.UI.UserControl


#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_login As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txt_userid As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt_pwd As System.Web.UI.WebControls.TextBox

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
    Dim DS As New DataSet
    Dim SQL As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tmpJogakArr() As String
        Dim tmpJogak As String
        Dim tmpFormattedIP As String = ""
        Dim tmpJogakCount As Integer
        Dim tmpSessionIP As String = Request.UserHostAddress()

        'ip�� �����������㰡 ���� �˻�
        tmpJogakArr = Split(tmpSessionIP, ".")
        tmpJogakCount = UBound(tmpJogakArr) - LBound(tmpJogakArr) + 1

        Dim i As Integer = 0
        Dim arrstartnum As Integer
        For arrstartnum = 0 To tmpJogakCount - 1
            tmpJogak = tmpJogakArr(i)
            If tmpFormattedIP = "" Then
                If tmpJogak.Length() = 1 Then
                    tmpFormattedIP += "00" + tmpJogak
                ElseIf tmpJogak.Length() = 2 Then
                    tmpFormattedIP += "0" + tmpJogak
                Else
                    tmpFormattedIP += tmpJogak
                End If
            Else
                If tmpJogak.Length() = 1 Then
                    tmpFormattedIP += ".00" + tmpJogak
                ElseIf tmpJogak.Length() = 2 Then
                    tmpFormattedIP += ".0" + tmpJogak
                Else
                    tmpFormattedIP += "." + tmpJogak
                End If
            End If
            i = i + 1
        Next

        SQL = "SELECT CustomerID FROM CustomerIPRanges WHERE ('" + tmpFormattedIP + "' between IPRangeBegin  AND IPRangeEnd )"
        'Response.Write(SQL)
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        If DS.Tables(0).Rows.Count <> 0 Then
            FormsAuthentication.SetAuthCookie("CustomerIPLogin", False)
            Session("CustomerLoginID") = "CustomerIPLogin"
            If IsNothing(Request("ReturnUrl")) Then
                Response.Redirect("/")
            Else
                Response.Redirect(Request("ReturnUrl"))
            End If
        End If
        DS.Dispose()
    End Sub

    Private Sub btn_login_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_login.Click
        Dim userid As String
        Dim pwd As String

        userid = Trim(txt_userid.Text)
        pwd = Trim(txt_pwd.Text)
        If userid = "" Then
            Response.Write("<script>alert('UserName�� �Է��� �ֽʽÿ�.');history.back(-1);</script>")
            Exit Sub
        End If
        'ID�� �����������㰡 ���� �˻�
        SQL = "select CustomerName, License from customers where CustomerLoginID='" + userid + "' and CustomerLoginPWD='" + pwd + "'"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        If DS.Tables(0).Rows.Count <> 0 Then

            '���������ο� üũ
            Dim intLicense As Integer
            intLicense = DS.Tables(0).Rows(0).Item("License")
            If intLicense > 0 Then
                DS = WebNewsDB.GetDataSetBySQLMun("select count(SessionID) as Cnt from CustomersLoginCount where CustomerLoginID='" & userid & "'")
                If DS.Tables.Item(0).Rows(0).Item("Cnt") >= intLicense Then
                    Response.Write("<script>alert('Sorry, you can\'t log in this data anymore. \n\nAuthorized users are limited.');</script>")
                    Exit Sub
                Else
                    WebNewsDB.GetDataSetBySQLMun("insert into CustomersLoginCount(SessionID,CustomerLoginID) Values('" + Session.SessionID + "','" + userid + "')")
                End If
            End If


            '�����Ϸ�
            FormsAuthentication.SetAuthCookie(userid, False)
            Session("CustomerLoginID") = userid

            If IsNothing(Request("ReturnUrl")) Then
                Response.Redirect("/")
            Else
                Response.Redirect(Request("ReturnUrl"))
            End If
        Else
            Response.Write("<script>alert('�α��ο� �����Ͽ����ϴ�.');history.back(-1);</script>")
        End If
        DS.Dispose()
    End Sub
End Class
