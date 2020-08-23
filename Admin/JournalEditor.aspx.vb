Public Class JournalEditor
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents txtSearchWord As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGijunDateTime As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblErrorMsg As System.Web.UI.WebControls.Label
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInputDateTime As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblErrInput As System.Web.UI.WebControls.Label
    Protected WithEvents btnJunsong As System.Web.UI.WebControls.Button
    Protected WithEvents chkPreviewAfterSave As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents pnlEntries As System.Web.UI.WebControls.Panel
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents chkSeeListSelector As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblJArticleID As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJournal As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtGanHengMulNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtISSN As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBalHengJi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBalHengCher As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBalHengYear As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGwonHo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRugye As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBeginPage As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJunSongCher As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJunSongDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFormat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnPDFUpload As System.Web.UI.WebControls.Button
    Protected WithEvents txtNayong As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtEndPage As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileSize As System.Web.UI.WebControls.Label
    Protected WithEvents txtTitleEng As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWriters As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAddJournal As System.Web.UI.WebControls.Button
    Protected WithEvents pnlSetFile As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlSetText As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlSelectJournal As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlSelector2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnJournalRefresh As System.Web.UI.WebControls.Button

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
    Public base_sPath As String
    Public base_sPathDB As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        base_sPath = "\Uploaded\Journal\"
        base_sPathDB = "/Uploaded/Journal/"

        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If
            txtGijunDateTime.Text = Now.AddMonths(-1)
            txtInputDateTime.Text = Now
            If Not (Request.QueryString("EditMode") Is Nothing) Then

                lblAdminMenuName.Text = WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode"))
                If lblAdminMenuName.Text = "" Then
                    Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                    Exit Sub
                End If
                ViewSet()
            End If
            InitDropDownList(ddlJournal, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=201", "없음")
            InitDropDownList(ddlSelectJournal, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=201", "전체")
            WebNewsDB.SetDropDownListByValue(ddlSelectJournal, Request.QueryString("JournalID"))

            If Request.QueryString("mode") <> "" Then
                ViewSet(Request.QueryString("mode"))
            End If

        End If
    End Sub

    Public Sub InitDropDownList(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataTextField As String, ByVal DataValueField As String, ByVal SQLMun As String, Optional ByVal DefaultValue As String = "")
        Dim DT As DataTable
        Dim i As Integer
        DT = WebNewsDB.GetDataTableBySQL(SQLMun)
        With ddlTemp
            .Items.Clear()
            If DT.Rows.Count = 0 Then
                ddlTemp.Items.Add("없음")
                Exit Sub
            End If

            If DefaultValue <> "" Then
                Dim aItem1 As New System.Web.UI.WebControls.ListItem
                aItem1.Text = DefaultValue
                aItem1.Value = "0"
                ddlTemp.Items.Add(aItem1)
            End If

            For i = 0 To DT.Rows.Count - 1
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = DT.Rows(i).Item(DataTextField)
                aItem.Value = DT.Rows(i).Item(DataValueField)
                ddlTemp.Items.Add(aItem)
            Next
            '.DataTextField = DataTextField
            '.DataValueField = DataValueField
            '.DataSource = DT
            .DataBind()
        End With
        DT.Dispose()
    End Sub

    Private Sub btnJunsong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJunsong.Click
        If ArticleSave(True) = False Then
            Exit Sub
        End If
        If chkPreviewAfterSave.Checked Then
            Response.Write("<script>window.open('/view_journal.aspx?JArticleID=" + Server.HtmlEncode(lblJArticleID.Text) + "');</script>")
        Else
            'Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(txtTitle.Text + "의 전송이 완료되었습니다"))
        End If
        Response.Write("<script>window.location.href='JournalEditor.aspx?EditMode=" + Request.QueryString("EditMode") + "';</script>")
    End Sub

    Private Function ArticleSave(ByVal IsJunSong As Boolean) As Boolean
        ArticleSave = False

        If txtTitle.Text = "" Then
            Response.Write("<script>alert('제목을 입력해 주십시요');</script>")
            Exit Function
        End If
        If ddlJournal.SelectedValue = 0 Then
            Response.Write("<script>alert('학술지명을 선택해 주십시요');</script>")
            Exit Function
        End If

        Dim LoginUserID As String = Request.Cookies("LoginUserID").Value
        Dim EditMode As Integer

        Dim status, cmd As String

        If lblJArticleID.Text = "" Then
            cmd = "insert"
        Else
            cmd = "update"
        End If
        If IsJunSong Then
            status = "1"
        Else
            status = "0"
        End If
        If lblFileName.Text = "없음" Then
            lblFileName.Text = ""
        End If

        lblJArticleID.Text = WebNewsDB.SetJournalArticle(cmd, Val(lblJArticleID.Text), txtTitle.Text, txtTitleEng.Text, _
                            txtWriters.Text, txtNayong.Text, ddlJournal.SelectedValue, IIf(txtBalHengYear.Text = "", 0, txtBalHengYear.Text), _
                            IIf(txtGwonHo.Text = "", 0, txtGwonHo.Text), IIf(txtRugye.Text = "", 0, txtRugye.Text), txtBeginPage.Text + "-" + txtEndPage.Text, txtJunSongCher.Text, txtJunSongDate.Text, _
                            lblFileName.Text, lblFileSize.Text, status, txtInputDateTime.Text)
        ArticleSave = True
    End Function
    Private Function DateCheck(ByRef txtDate As System.Web.UI.WebControls.TextBox, ByRef lblError As System.Web.UI.WebControls.Label, ByVal DefVal As DateTime) As Boolean
        If IsDate(txtDate.Text) Then
            lblError.Visible = False
            Return True
        Else
            lblError.Text = "올바르지 않은 날짜형식입니다"
            lblError.Visible = True
            txtDate.Text = DefVal
            Return False
        End If
    End Function
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If DateCheck(txtGijunDateTime, lblErrorMsg, Now.AddDays(-10)) = True Then
            DataGrid1.CurrentPageIndex = 0
            ViewSet()
        End If
    End Sub
    Private Sub ViewSet()

        Dim EditMode As Integer = Request.QueryString("EditMode")

        Panel1.Visible = True
        btnJunsong.Visible = False
        If WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, WebNewsConstants.AdminEditModeArticleDelete) <> "" Then
            btnDelete.Visible = True
        Else
            btnDelete.Visible = False
        End If
        If (EditMode <> WebNewsConstants.AdminEditModeJunsong) And (EditMode <> WebNewsConstants.AdminEditModeEditAfterJunsong) Then
            If WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, WebNewsConstants.AdminEditModeJunsong) <> "" Then
                btnJunsong.Visible = True
            End If
        End If

        binddata()

    End Sub
    Private Sub binddata()
        Dim DS As DataSet
        DS = WebNewsDB.GetJournalsArticlesByJournalID(Request.QueryString("JournalID") + 0, 1, CDate(txtGijunDateTime.Text), txtSearchWord.Text)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()

        Dim pdfLink As String
        Dim i As Integer

        'With DS.Tables(0)
        '    For i = 0 To DataGrid1.Items.Count - 1
        '        If .Rows(i).Item("FileName") <> "" Then
        '            pdfLink = "<a href=" + .Rows(i).Item("FileName") + " target='_blank'><img src='/images/icon_pdf.gif' border='0'></a>"
        '            CType(DataGrid1.Items(i).FindControl("Label2"), Label).Text = pdfLink
        '        End If
        '    Next
        'End With
        With DataGrid1
            For i = 0 To .Items.Count - 1
                If Trim(.Items(i).Cells(8).Text) <> "&nbsp;" Then
                    pdfLink = "<a href=" + .Items(i).Cells(8).Text + " target='_blank'><img src='/images/icon_pdf.gif' border='0'></a>"
                    CType(DataGrid1.Items(i).FindControl("Label2"), Label).Text = pdfLink
                    'Response.Write(CStr(i) + "=" + Trim(.Items(i).Cells(8).Text) + "<br>")
                End If
            Next
        End With

        DS.Dispose()

    End Sub

    Private Function FileUpload(ByRef txtFile As System.Web.UI.HtmlControls.HtmlInputFile, _
                    ByVal lblPath As WebControls.Label, Optional ByVal FileName As String = "") As String


        Dim sFile As String = ""
        Dim sPath As String
        Dim sPathDB As String
        Dim sFullPath As String
        Dim sSplit() As String

        sFile = txtFile.PostedFile.FileName
        sSplit = Split(sFile, "\")
        sFile = sSplit(UBound(sSplit))
        If txtFile.PostedFile.FileName = "" Then
            Response.Write("<script>alert('먼저 파일을 선택해주십시요');</script>")
            Exit Function
        End If
        If Microsoft.VisualBasic.Right(sFile, 3) <> "pdf" Then
            Response.Write("<script>alert('오류 : PDF파일이 아닙니다.');</script>")
            Exit Function
        End If
        If ddlJournal.SelectedValue = 0 Then
            Response.Write("<script>alert('먼저 학술지명을 선택해 주십시요');</script>")
            Exit Function
        End If
        If txtBalHengYear.Text = "" Then
            'Response.Write("<script>alert('먼저 발행년도를 입력해주십시요');</script>")
            Exit Function
        End If
        If txtGwonHo.Text = "" Then
            'Response.Write("<script>alert('먼저 권호를 입력해주십시요');</script>")
            Exit Function
        End If

        If Val(lblJArticleID.Text) = 0 Then
            If ArticleSave(False) = False Then
                Exit Function
            End If
        End If

        'sFile = Microsoft.VisualBasic.Right(txtBalHengYear.Text, 2)
        'sFile += IIf(Len(txtGwonHo.Text) = 1, "0" + txtGwonHo.Text, txtGwonHo.Text)
        'sFile += "_" + lblJArticleID.Text + ".pdf"
        sFile = "KPJ_" + lblJArticleID.Text + ".pdf"

        sPath = Server.MapPath(base_sPath + ddlJournal.SelectedValue)
        sPathDB = base_sPathDB + ddlJournal.SelectedValue

        If Dir(sPath, FileAttribute.Directory) = "" Then
            MkDir(sPath)
        End If

        'Dim img As System.Drawing.Image
        'img = Drawing.Image.FromFile(sFullPath)

        sFullPath = sPath & "\" & sFile
        'Try
        txtFile.PostedFile.SaveAs(sFullPath)
        'lblFileSize.Text = CStr(Format(Microsoft.VisualBasic.FileLen(sFullPath) / 1024 / 1024, "#######0.##")) + "Kbyte"
        lblFileSize.Text = CStr(Microsoft.VisualBasic.FileLen(sFullPath))

        WebNewsDB.GetDataSetBySQLMun("update JournalsArticle set FileName='" + (sPathDB + "/" + sFile) + "' where JArticleID=" + lblJArticleID.Text)
        lblPath.Text = (sPathDB + "/" + sFile)
        Return sFullPath

        'txtCaption.Value = ""
        'lblMsg.Text = sFile & "파일이 성공적으로 전송되었습니다"

        'Catch Ex As Exception
        'lblMsg.Text = sFile & "의 전송에 실패했습니다 : " & Ex.Message
        'lblMsg.Font.Bold = True
        'lblMsg.Visible = True
        'End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not (lblJArticleID.Text = "" Or Val(lblJArticleID.Text) = 0) Then
            Response.Redirect("AdminMessage.aspx" + _
            "?Message=" + Server.HtmlEncode("자료는 관련 작업과 더불어 영구적으로 삭제되며 복구될 수 없습니다.  해당 자료를 정말 삭제하시겠습니까?") + _
            "&cmd=" + CStr(WebNewsConstants.ConfirmCommandDeleteJournal) + _
            "&JArticleID=" + lblJArticleID.Text + _
            "&NavigateUrlYes=" + Server.HtmlEncode("JournalEditor.aspx?EditMode=" + Request.QueryString("EditMode")))
        End If
    End Sub

    Private Sub chkSeelistSelector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeeListSelector.CheckedChanged
        Panel1.Visible = chkSeeListSelector.Checked
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        binddata()
    End Sub
    Private Sub DataGrid1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged
        Dim DS As DataSet
        DS = WebNewsDB.GetJournalsArticlesByJArticleID(DataGrid1.DataKeys(DataGrid1.SelectedIndex))
        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                lblJArticleID.Text = .Item("JArticleID").ToString + ""
                txtTitle.Text = .Item("Title") + ""
                txtTitleEng.Text = .Item("TitleEng") + ""
                txtWriters.Text = .Item("Writers") + ""
                txtNayong.Text = .Item("Nayong1") + .Item("Nayong2") + ""
                WebNewsDB.SetDropDownListByValue(ddlJournal, .Item("JournalID"))
                txtBalHengYear.Text = IIf(.Item("BalHengYear").ToString = 0, "", .Item("BalHengYear").ToString)
                txtGwonHo.Text = IIf(.Item("GwonHo").ToString = 0, "", .Item("GwonHo").ToString)
                txtRugye.Text = IIf(.Item("Rugye").ToString = 0, "", .Item("Rugye").ToString)

                If Not IsDBNull(.Item("Page")) Then
                    Dim str As String() = Split(.Item("Page"), "-")
                    txtBeginPage.Text = str(0) + ""
                    If str.Length > 1 Then
                        txtEndPage.Text = str(1) + ""
                    End If
                End If
                txtJunSongCher.Text = .Item("JunSongCher") + ""
                txtJunSongDate.Text = .Item("JunSongDate") + ""
                lblFileName.Text = .Item("FileName") + ""
                lblFileSize.Text = .Item("FileSize") + ""
                txtInputDateTime.Text = .Item("InputDateTime").ToString + ""

                If lblFileName.Text <> "" Then
                    WebNewsDB.SetDropDownListByValue(ddlFormat, "1")
                End If

                If ddlFormat.SelectedValue = 0 Then
                    pnlSetFile.Visible = True
                    pnlSetText.Visible = False
                Else
                    pnlSetFile.Visible = False
                    pnlSetText.Visible = True
                End If

                bindJournalInfo()
            End With
        End If
        DS.Dispose()



        chkSeeListSelector.Checked = False
        Panel1.Visible = False
        pnlEntries.Visible = True
    End Sub


    Private Sub ddlMedia1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FileUpload(txtFile, lblFileName)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect(Request.Url.ToString + "&mode=new")
    End Sub
    Sub Viewset(ByVal cmd As String)
        Select Case cmd
            Case "new"
                btnNew.Visible = False
                chkSeeListSelector.Visible = False
                chkSeeListSelector.Checked = False
                'pnlSelector.Visible = False
                Panel1.Visible = False
                pnlEntries.Visible = True
        End Select
    End Sub
    Private Sub btnPDFUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFUpload.Click
        FileUpload(txtFile, lblFileName)
    End Sub

    Private Sub ddlJournal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJournal.SelectedIndexChanged
        If ddlJournal.SelectedValue = 0 Then Exit Sub
        bindJournalInfo()
    End Sub

    Sub bindJournalInfo()
        If ddlJournal.SelectedValue = 0 Then Exit Sub
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("select * from journals where journalid='" + ddlJournal.SelectedValue.ToString + "'")

        With DS.Tables(0).Rows(0)
            txtGanHengMulNo.Text = .Item("JungGiGanHengMulNo") + ""
            txtISSN.Text = .Item("ISSN") + ""
            txtBalHengJi.Text = .Item("BalHengJi") + ""
            txtBalHengCher.Text = .Item("BalHengCher") + ""


        End With
        DS.Dispose()
    End Sub

    Private Sub btnAddJournal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddJournal.Click
        If btnAddJournal.Text = "학술지 추가" Then
            txtGanHengMulNo.Text = ""
            txtISSN.Text = ""
            txtBalHengCher.Text = ""
            txtBalHengJi.Text = ""

            txtGanHengMulNo.Enabled = True
            txtISSN.Enabled = True
            txtBalHengCher.Enabled = True
            txtBalHengJi.Enabled = True

        End If
    End Sub

    Private Sub ddlFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFormat.SelectedIndexChanged
        If ddlFormat.SelectedValue = 0 Then
            pnlSetFile.Visible = True
            pnlSetText.Visible = False
        Else
            pnlSetFile.Visible = False
            pnlSetText.Visible = True
        End If
    End Sub

    Private Sub ddlSelectJournal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSelectJournal.SelectedIndexChanged
        Response.Redirect("JournalEditor.aspx?EditMode=250&JournalID=" + ddlSelectJournal.SelectedValue)
    End Sub

    Private Sub btnJournalRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJournalRefresh.Click
        InitDropDownList(ddlJournal, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=201", "없음")
        WebNewsDB.SetDropDownListByValue(ddlSelectJournal, "0")

        txtGanHengMulNo.Text = ""
        txtISSN.Text = ""
        txtBalHengJi.Text = ""
        txtBalHengCher.Text = ""
    End Sub
End Class
