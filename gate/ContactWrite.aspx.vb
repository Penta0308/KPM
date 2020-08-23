Public Class ContactWrite
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
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

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        Dim strSQL As String
        Dim DS As DataSet

        Dim strQuery, strFields, strValues As String
        Dim strBbsID, strSubject, strText, strNation, strTel, strEmail, strFileName, strWriteUserID, strWriteUserName, strWriteCustomerName, strWriterIP, strcfgSend, strcfgVoice, strREF, strPOS, strLEV As String
        Dim strTBName As String = "bbs"
        strFileName = ""

        If Trim(txtSubject.Text) = "" Then
            Response.Write("<script>alert('제목을 입력해주세요.');</script>")
            Exit Sub
        End If

        '마지막 글번호를 불러온다.
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
                Response.Write("<script>alert('파일전송에 실패했습니다.');</script>")
                'Response.Write("<script>alert('" + "파일전송에 실패했습니다. : " + sFullPath + Replace(Ex.Message, "'", "\'") + "');</script>")
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

        strText = "국가/소재도시 : " + strNation + vbCrLf
        strText += "기관명 : " + strWriteCustomerName + vbCrLf
        strText += "부서/담당자 : " + strWriteUserName + vbCrLf
        strText += "전화/팍스 : " + strTel + vbCrLf
        strText += "전자우편 : " + strEmail + vbCrLf
        strText += "내용 : " + vbCrLf + Replace(txtText.Value, "'", "''") + vbCrLf


        strWriterIP = Request.UserHostAddress

        strSubject = Replace(txtSubject.Text, "'", "''")


        WebNewsDB.GetDataSetBySQLMun(strQuery)
        WebNewsDB.SetBBS("insert", strBbsID, strNumber(0), strSubject, strText, strWriteUserID, strWriteUserName, strWriterIP, strFileName, Today, "0")
        lblScript.Text = "<script>PopupOpen('ContactWritePopup.htm','MenuEditorSub','395','164');window.location.href='" + Request.RawUrl + "';</script>"
    End Sub

End Class
