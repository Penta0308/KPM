Public Class ContactWrite
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtNation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWriteCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWriteUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtText As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents txtFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblScript As System.Web.UI.WebControls.Label

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

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        Dim strSQL As String
        Dim DS As DataSet

        Dim strQuery, strFields, strValues As String
        Dim strBbsID, strSubject, strText, strNation, strTel, strEmail, strFileName, strWriteUserID, strWriteUserName, strWriteCustomerName, strWriterIP, strcfgSend, strcfgVoice, strREF, strPOS, strLEV As String
        Dim strTBName As String = "bbs"
        strFileName = ""

        If Trim(txtSubject.Text) = "" Then
            Response.Write("<script>alert('������ �Է����ּ���.');</script>")
            Exit Sub
        End If

        '������ �۹�ȣ�� �ҷ��´�.
        strQuery = "select max(number) as MaxNo from " + strTBName + " where bbsid='contact'"
        Dim strNumber As String() = WebNewsDB.GetStringBySQLMun(strQuery)
        If strNumber(0) = "" Then
            strNumber(0) = "1"
        Else
            strNumber(0) = CStr(CInt(strNumber(0)) + 1)
        End If

        Dim sFile As String = ""
        Dim sPath, sFullPath As String
        Dim sSplit() As String

        sFile = txtFile.PostedFile.FileName
        If sFile <> "" Then
            sFile = txtFile.PostedFile.FileName
            sSplit = Split(sFile, "\")
            sFile = sSplit(UBound(sSplit))

            strFileName = "/uploaded/ContactFiles/" + strNumber(0) + "/" + sFile

            sPath = Server.MapPath("..\Uploaded\ContactFiles\" + strNumber(0))
            sFullPath = sPath & "\" & sFile
            If Dir(sPath, FileAttribute.Directory) = "" Then
                MkDir(sPath)
            End If

            Try
                txtFile.PostedFile.SaveAs(sFullPath)
            Catch Ex As Exception
                Response.Write("<script>alert('�������ۿ� �����߽��ϴ�.');</script>")
                'Response.Write("<script>alert('" + "�������ۿ� �����߽��ϴ�. : " + sFullPath + Replace(Ex.Message, "'", "\'") + "');</script>")
                Exit Sub
            End Try
        End If

        strBbsID = "contact"
        strWriteUserID = ""
        strWriteCustomerName = Replace(txtWriteCustomerName.Text, "'", "''") + ""
        strWriteUserName = Replace(txtWriteUserName.Text, "'", "''") + ""

        strNation = Replace(txtNation.Text, "'", "''")
        strTel = Replace(txtTel.Text, "'", "''")
        strEmail = Replace(txtEmail.Text, "'", "''")

        strText = "����/���絵�� : " + strNation + vbCrLf
        strText += "����� : " + strWriteCustomerName + vbCrLf
        strText += "�μ�/����� : " + strWriteUserName + vbCrLf
        strText += "��ȭ/�Ž� : " + strTel + vbCrLf
        strText += "���ڿ��� : " + strEmail + vbCrLf
        strText += "���� : " + vbCrLf + Replace(txtText.Value, "'", "''") + vbCrLf


        strWriterIP = Request.UserHostAddress

        strSubject = Replace(txtSubject.Text, "'", "''")


        WebNewsDB.GetDataSetBySQLMun(strQuery)
        WebNewsDB.SetBBS("insert", strBbsID, strNumber(0), strSubject, strText, strWriteUserID, strWriteUserName, strWriterIP, strFileName, Today, "0")
        lblScript.Text = "<script>PopupOpen('ContactWritePopup.htm','MenuEditorSub','395','164');window.location.href='" + Request.RawUrl + "';</script>"
    End Sub

End Class
