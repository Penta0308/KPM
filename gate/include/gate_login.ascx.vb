Imports System.Web.Security
Public Class gate_login
    Inherits System.Web.UI.UserControl


#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btn_login As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txt_userid As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt_pwd As System.Web.UI.WebControls.TextBox

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
    Dim DS As New DataSet
    Dim SQL As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tmpJogakArr() As String
        Dim tmpJogak As String
        Dim tmpFormattedIP As String = ""
        Dim tmpJogakCount As Integer
        Dim tmpSessionIP As String = Request.UserHostAddress()

        'ip로 콘텐츠열람허가 여부 검색
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
            Response.Write("<script>alert('UserName을 입력해 주십시오.');history.back(-1);</script>")
            Exit Sub
        End If
        'ID로 콘텐츠열람허가 여부 검색
        SQL = "select CustomerName, License from customers where CustomerLoginID='" + userid + "' and CustomerLoginPWD='" + pwd + "'"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        If DS.Tables(0).Rows.Count <> 0 Then

            '동시접속인원 체크
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


            '인증완료
            FormsAuthentication.SetAuthCookie(userid, False)
            Session("CustomerLoginID") = userid

            If IsNothing(Request("ReturnUrl")) Then
                Response.Redirect("/")
            Else
                Response.Redirect(Request("ReturnUrl"))
            End If
        Else
            Response.Write("<script>alert('로그인에 실패하였습니다.');history.back(-1);</script>")
        End If
        DS.Dispose()
    End Sub
End Class
