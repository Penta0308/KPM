Public Class bbs_write
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtText As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlVoiceList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSelectCfg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkSend As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblScript As System.Web.UI.WebControls.Label
    Protected WithEvents btnCfgEdit As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblCfg As System.Web.UI.WebControls.Label
    Protected WithEvents lblCfgNone As System.Web.UI.WebControls.Label
    Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblfile As System.Web.UI.WebControls.Label
    Protected WithEvents txtFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents pnlFile As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlFileLink As System.Web.UI.WebControls.Panel
    Protected WithEvents btnDeleteFile As System.Web.UI.WebControls.Button
    Dim DS As New DataSet
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
    Public strStyle As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("User_type") <> "1" Then
            Response.Write("<script>alert('������ �����ϴ�.');window.location.href='bbs_list.aspx?bbsid=notice';</script>")
        End If

        lblScript.Text = ""
        If Microsoft.VisualBasic.Right(Request("bbsID"), 3) = "jpn" Then
            'strStyle = "/japanese/include/main.css"
            strStyle = "/include/style.css"
        Else
            strStyle = "/include/main.css"
        End If

        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If
            If Not (Request.QueryString("EditMode") Is Nothing) Then
                If WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode")) = "" Then
                    Response.Write("<script>alert('�� �޴��� �������� �����ϴ�. �����ڿ��� ������ ��û�Ͻʽÿ�.');history.back(-1);</script>")
                    Exit Sub
                End If
            End If

            pnlFileLink.Visible = False

            bindData()
        End If
    End Sub
    Sub bindData()
        If Request("mode") = "update" Then
            Dim strQuery, strBbsID, strNumber As String

            strBbsID = Request("bbsid")
            strNumber = Request("no")

            Dim strTBName As String = "bbs"

            strQuery = "select subject,text,FileName from " + strTBName + " where bbsID='" + strBbsID + "' and number='" + strNumber + "'"
            DS = WebNewsDB.GetDataSetBySQLMun(strQuery)


            With DS.Tables(0).Rows(0)
                txtSubject.Text = .Item("subject")
                txtText.Text = .Item("text")
                If .Item("FileName") = "" Then
                    pnlFile.Visible = True
                    pnlFileLink.Visible = False
                Else
                    pnlFile.Visible = False
                    pnlFileLink.Visible = True
                    lblfile.Text = .Item("FileName")
                End If
            End With

            DS.Dispose()
        End If
    End Sub

    Private Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Request("mode") <> "update" Then
            Dim strQuery, strFields, strValues As String
            Dim strBbsID, strSubject, strText, strWriteUserID, strWriteUserName, strWriterIP, strcfgSend, strcfgVoice, strREF, strPOS, strLEV As String

            'Dim strTBName As String() = db.GetStringBySQLMun("select tablename from bbsTableList where bbsID='" + Request("bbsID") + "'")
            Dim strTBName As String = "bbs"
            'On Error GoTo err


            '������ �۹�ȣ�� �ҷ��´�.
            strQuery = "select max(number) as MaxNo from " + strTBName + " where bbsid='" + Request("bbsid") + "'"
            Dim strNumber As String() = WebNewsDB.GetStringBySQLMun(strQuery)
            If strNumber(0) = "" Then
                strNumber(0) = "1"
            Else
                strNumber(0) = CStr(CInt(strNumber(0)) + 1)
            End If


            Dim sFile As String = ""
            Dim sPath, sFullPath, strFileName As String
            Dim sSplit() As String

            sFile = txtFile.PostedFile.FileName
            If sFile <> "" Then
                sFile = txtFile.PostedFile.FileName
                sSplit = Split(sFile, "\")
                sFile = sSplit(UBound(sSplit))

                strFileName = "/uploaded/BBSFiles/" + Request("bbsid") + "/" + strNumber(0) + "/" + sFile

                sPath = Server.MapPath("..\Uploaded\BBSFiles\" + Request("bbsid") + "\" + strNumber(0))
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
            Else
                strfilename = ""
            End If

            strBbsID = Request("bbsid")
            strWriteUserID = ""
            strWriteUserName = "������"
            strWriterIP = Request.UserHostAddress

            strText = Replace(txtText.Text, "'", "''")
            strSubject = Replace(txtSubject.Text, "'", "''")



            WebNewsDB.SetBBS("insert", strbbsid, strnumber(0), strsubject, strtext, strwriteuserid, strwriteusername, strwriterip, strfilename, Today, "0")

            Response.Redirect("bbs_list.aspx?bbsid=" + strBbsID)
        Else
            Dim strQuery, strFields, strValues As String
            Dim strBbsID, strSubject, strText, strWriteUserID, strWriteUserName, strWriterIP, strcfgSend, strcfgVoice, strREF, strPOS, strLEV As String

            'Dim strTBName As String() = db.GetStringBySQLMun("select tablename from bbsTableList where bbsID='" + Request("bbsID") + "'")
            Dim strTBName As String = "bbs"
            'On Error GoTo err


            '������ �۹�ȣ�� �ҷ��´�.
            Dim strNumber As String
            strNumber = Request("no")

            Dim sFile As String = ""
            Dim sPath, sFullPath, strFileName As String
            Dim sSplit() As String

            strfilename = ""
            If lblfile.Text = "" Then
                sFile = txtFile.PostedFile.FileName
                If sFile <> "" Then
                    sFile = txtFile.PostedFile.FileName
                    sSplit = Split(sFile, "\")
                    sFile = sSplit(UBound(sSplit))

                    strFileName = "/uploaded/BBSFiles/" + Request("bbsid") + "/" + strNumber + "/" + sFile

                    sPath = Server.MapPath("..\Uploaded\BBSFiles\" + Request("bbsid") + "\" + strNumber)
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
            End If

            strBbsID = Request("bbsid")
            strWriteUserID = ""
            strWriteUserName = "������"
            strWriterIP = Request.UserHostAddress

            strText = Replace(txtText.Text, "'", "''")
            strSubject = Replace(txtSubject.Text, "'", "''")


            If lblfile.Text <> "" Then
                strfilename = lblfile.Text
            End If
            WebNewsDB.SetBBS("update", strbbsid, strnumber, strsubject, strtext, "", "", "", strfilename, Today, "0")

            Response.Write("<script>alert('�����Ǿ����ϴ�.');window.location.href='bbs_list.aspx?bbsid=" + Request("bbsid") + "';</script>")

            End If


err:
            'Dim errMsg As SqlClient.SqlError
            'errmsg=
            'response.Write(debug.
    End Sub

    Private Sub btnDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFile.Click
        Dim FileName As String
        FileName = lblfile.Text
        If FileName <> "" Then
            If IO.File.Exists(Server.MapPath(FileName)) Then
                Kill(Server.MapPath(FileName))
            End If
        End If

        Dim strQuery As String
        strQuery = "update bbs set FileName='' where bbsID='" + Request("bbsid") + "' and number='" + Request("no") + "'"
        WebNewsDB.GetDataSetBySQLMun(strQuery)
        lblfile.Text = ""
        bindData()
    End Sub
End Class
