Public Class ArticleEditor
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents chkIsOldArticle As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSeeArticleSelector As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtSearchWord As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGijunDateTime As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblErrorMsg As System.Web.UI.WebControls.Label
    Protected WithEvents pnlArticleSelector As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlSection As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubNayong As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNayong As System.Web.UI.WebControls.TextBox
    Protected WithEvents rblImageAlign As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents btnAttachImage As System.Web.UI.WebControls.Button
    Protected WithEvents lblImageMsg As System.Web.UI.WebControls.Label
    Protected WithEvents btnAttachFile As System.Web.UI.WebControls.Button
    Protected WithEvents lblEtcMsg As System.Web.UI.WebControls.Label
    Protected WithEvents txtWriterName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataGrid2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtInputDateTime As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblErrInput As System.Web.UI.WebControls.Label
    Protected WithEvents txtJunsongDateTime As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblErrJunsong As System.Web.UI.WebControls.Label
    Protected WithEvents btnArticleSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnJunsong As System.Web.UI.WebControls.Button
    Protected WithEvents chkPreviewAfterSave As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents pnlEntries As System.Web.UI.WebControls.Panel
    Protected WithEvents txtImageFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtImageCaption As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtEtcFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtEtcCaption As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLocal As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTopNews As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Datagrid3 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtLinkArticleID2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents LinkSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblArticleID As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMedia1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMedia2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLanguage As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkSale As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtPhotoID_temp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtPhotoID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtImageFile_temp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblLanguage As System.Web.UI.WebControls.Label
    Protected WithEvents lblChulcherEdit As System.Web.UI.WebControls.Label
    Protected WithEvents btnChulcherReload As System.Web.UI.WebControls.Button

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
    Private LanguageID As Integer = 101
    Public LanguageColor As String = "ffaaaa"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not (Request.Cookies("Language") Is Nothing) Then
            lblLanguage.Text = "(" + Request.Cookies("Language").Value + ")"
        End If

        If Not (Request.Cookies("LanguageID") Is Nothing) Then
            LanguageID = Request.Cookies("LanguageID").Value
            LanguageColor = Request.Cookies("LanguageColor").Value
        End If


        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If
            txtGijunDateTime.Text = Now.AddMonths(-1)
            txtInputDateTime.Text = Now
            txtJunsongDateTime.Text = Now



            If Not (Request.QueryString("EditMode") Is Nothing) Then

                lblAdminMenuName.Text = WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode"))
                If lblAdminMenuName.Text = "" Then
                    Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                    Exit Sub
                End If
                btnArticleSave.Text = lblAdminMenuName.Text + " 완료"

                ViewSet()
            End If



            WebNewsDB.InitDropDownList(ddlLanguage, "LanguageName", "LanguageID", "SELECT [LanguageID], [LanguageName] FROM Languages")
            WebNewsDB.InitDropDownList(ddlSection, "SectionName", "SectionID", "SELECT [SectionID], [SectionName] FROM [Sections]")
            WebNewsDB.InitDropDownList(ddlMedia1, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaID=101 or MediaID=301")
            WebNewsDB.InitDropDownList(ddlMedia2, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=" + ddlMedia1.SelectedValue + " and MediaID<>0")
            WebNewsDB.InitDropDownList(ddlLocal, "LocalName", "LocalID", "SELECT LocalID, LocalName FROM Locals")

            If Not IsPostBack Then
                WebNewsDB.SetDropDownListByValue(ddlLanguage, LanguageID)
            End If
        End If
    End Sub

    Private Sub btnArticleSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArticleSave.Click
        If ArticleSave(False) = False Then
            Exit Sub
        End If
        If chkPreviewAfterSave.Checked Then
            Response.Write("<script>window.open ('/view_article.aspx?ArticleID=" + Server.HtmlEncode(lblArticleID.Text) + "');</script>")
        Else
            Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(txtTitle.Text + "의 " + lblAdminMenuName.Text + " 작업이 완료되었습니다"))
        End If
    End Sub
    Private Sub btnJunsong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJunsong.Click
        If ArticleSave(True) = False Then
            Exit Sub
        End If
        If chkPreviewAfterSave.Checked Then
            Response.Write("<script>window.open ('/view_article.aspx?ArticleID=" + Server.HtmlEncode(lblArticleID.Text) + "');</script>")
        Else
            Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(txtTitle.Text + "의 전송이 완료되었습니다"))
        End If
    End Sub

    Private Function ArticleSave(ByVal IsJunSong As Boolean) As Boolean
        ArticleSave = False
        'If DateCheck(txtInputDateTime, lblErrInput, Now) = False Then
        '    Exit Function
        'End If
        If txtTitle.Text = "" Then
            Response.Write("<script>alert('제목을 입력하세요.');</script>")
            Exit Function
        End If
        If chkIsOldArticle.Checked = True Then
            If DateCheck(txtJunsongDateTime, lblErrJunsong, Now) = False Then
                Exit Function
            End If
        End If

        Dim LoginUserID As String = Request.Cookies("LoginUserID").Value
        Dim EditMode As Integer
        If IsJunSong Then
            EditMode = WebNewsConstants.AdminEditModeJunsong
        Else
            EditMode = Request.QueryString("EditMode")
        End If

        Dim MediaID, Importance As Integer

        If ddlMedia2.SelectedValue = "없음" Then
            MediaID = ddlMedia1.SelectedValue
        Else
            MediaID = ddlMedia2.SelectedValue
        End If

        If chkTopNews.Checked = True Then
            Importance = 2
        Else
            Importance = 1
        End If

        '사진유무 체크
        Dim chkPhoto As String
        If txtNayong.Text.IndexOf("<img border") > 0 Then
            chkPhoto = "1"
        Else
            chkPhoto = "0"
        End If
        txtTitle.Text = Replace(txtTitle.Text, "<", "&lt;")
        txtTitle.Text = Replace(txtTitle.Text, ">", "&gt;")
        lblArticleID.Text = WebNewsDB.SetArticle(Val(lblArticleID.Text), txtWriterName.Text, txtEmail.Text, _
                                                ddlLanguage.SelectedValue, Trim(txtTitle.Text), txtSubTitle.Text, _
                                                txtSubNayong.Text, txtNayong.Text, ddlSection.SelectedValue, _
                                                EditMode, LoginUserID, txtInputDateTime.Text, txtJunsongDateTime.Text, _
                                                MediaID, ddlLocal.SelectedValue, Importance, txtLinkArticleID2.Value, chkPhoto)
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

        pnlArticleSelector.Visible = True
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
        Select Case EditMode
            Case WebNewsConstants.AdminEditModeWriterNew
                btnDelete.Visible = False
                pnlArticleSelector.Visible = False
                chkSeeArticleSelector.Visible = False
                pnlEntries.Visible = True
            Case Else
                binddata()
        End Select

    End Sub
    Private Sub binddata()
        Dim DS As DataSet
        DS = WebNewsDB.GetArticlesByAuthID(LanguageID, Request.QueryString("EditMode"), True, CDate(txtGijunDateTime.Text), txtSearchWord.Text)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub

    'Private Sub btnAttachLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    txtNayong.Text += "<a href='../news/ViewArticle.aspx?ArticleID=" + txtLinkArticleID.Value + "'>" + txtLinkDescription.Value + "</a>"
    'End Sub

    Private Sub btnAttachImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachImage.Click
        If txtPhotoID_temp.Value = "" Then
            If chkSale.Checked = True Then
                Response.Write("<script>window.open('MediaEditor11.aspx?EditMode=201&mode=new&popup=true&path=" + txtImageFile_temp.Value + "','','width=779,height=500,top=30,left=300,diretories=no,location=no,menubar=no,scrollbars=yes,toolbar=no,resizable=no,status=no');</script>")
                txtImageFile_temp.Value = txtImageFile.PostedFile.FileName
            End If
        End If
        FileUpload(txtImageFile, txtImageCaption, lblImageMsg, WebNewsConstants.FileUploadModeImageFile)
    End Sub

    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click
        FileUpload(txtEtcFile, txtEtcCaption, lblEtcMsg, WebNewsConstants.FileUploadModeDocFile)
    End Sub
    Private Sub FileUpload(ByRef txtFile As System.Web.UI.HtmlControls.HtmlInputFile, ByRef txtCaption As System.Web.UI.HtmlControls.HtmlInputText, _
                            ByRef lblMsg As System.Web.UI.WebControls.Label, ByVal FileUploadMode As Integer)
        If Val(lblArticleID.Text) = 0 Then
            If ArticleSave(False) = False Then
                Exit Sub
            End If
        End If
        Dim sFile As String = ""
        Dim sFileHeader8c As String = ""
        Dim sPath As String
        Dim sPathDB As String
        Dim sFullPath As String
        Dim sSplit() As String
        Dim sPathFriendly As String

        sFile = txtFile.PostedFile.FileName
        sSplit = Split(sFile, "\")
        sFile = sSplit(UBound(sSplit))
        sFileHeader8c = Microsoft.VisualBasic.Left(sFile, 8)

        Select Case FileUploadMode
            Case WebNewsConstants.FileUploadModeImageFile
                sPath = Server.MapPath("\Uploaded\ImageFiles\" + sFileHeader8c)
                sPathDB = "/Uploaded/ImageFiles/" + sFileHeader8c
            Case WebNewsConstants.FileUploadModeDocFile
                sPath = Server.MapPath("\Uploaded\ETCFiles\" + sFileHeader8c)
                sPathDB = "/Uploaded/ETCFiles/" + sFileHeader8c
        End Select
        If Dir(sPath, FileAttribute.Directory) = "" Then
            MkDir(sPath)
        End If

        sFullPath = sPath & "\" & sFile
        'Try
        If txtImageFile_temp.Value = "" Or Mid(txtImageFile_temp.Value, 1, 3) = "/Up" Then
            Select Case FileUploadMode
                Case WebNewsConstants.FileUploadModeImageFile
                    txtNayong.Text += "<div align=""" + rblImageAlign.SelectedValue + """>"
                    txtNayong.Text += "<table border=""0"" width=""10%"" align=""" + rblImageAlign.SelectedValue + """>"
                    txtNayong.Text += "<tr>"
                    If txtImageFile_temp.Value = "" Then
                        txtNayong.Text += "<td width=""100%""><img border=""0"" src=""" + sPathDB + "/" + sFile + """></td>"
                    Else
                        txtNayong.Text += "<td width=""100%""><img border=""0"" src=""" + txtImageFile_temp.Value + """></td>"
                    End If
                    txtNayong.Text += "</tr>"
                    txtNayong.Text += "<tr>"
                    txtNayong.Text += "<td width=""100%"" style=""FONT-SIZE: 8pt""><font color=""#800000"">"
                    txtNayong.Text += txtCaption.Value
                    If txtPhotoID_temp.Value <> "" Then
                        txtNayong.Text += "&nbsp;<a href='javascript:MMPhotoView(" + txtPhotoID_temp.Value + ");'><img src='/images/icon_sale.gif' border=0></a>"
                    End If
                    txtNayong.Text += "</font></td>"
                    txtNayong.Text += "</tr>"
                    txtNayong.Text += "</table>"
                    txtNayong.Text += "</div>"
                Case WebNewsConstants.FileUploadModeDocFile
                    txtNayong.Text += "<p align=left><a href='" + sPathDB + "/" + sFile + "'><b>[첨부파일]</b> " + txtCaption.Value + "</a></p>"
            End Select
        End If

        txtImageFile_temp.Value = ""

        If txtPhotoID_temp.Value = "" Then
            txtImageFile_temp.Value = sPathDB + "/" + sFile
            txtFile.PostedFile.SaveAs(sFullPath)
            WebNewsDB.SetFiles((sPathDB + "/" + sFile), Val(lblArticleID.Text), txtCaption.Value, FileUploadMode)
        End If

        txtCaption.Value = ""
        txtPhotoID_temp.Value = ""
        chkSale.Checked = False

        'lblMsg.Text = sFile & "파일이 성공적으로 전송되었습니다"

        'Catch Ex As Exception
        'lblMsg.Text = sFile & "의 전송에 실패했습니다 : " & Ex.Message
        'lblMsg.Font.Bold = True
        'lblMsg.Visible = True
        'End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not (lblArticleID.Text = "" Or Val(lblArticleID.Text) = 0) Then
            Response.Redirect("AdminMessage.aspx" + _
            "?Message=" + Server.HtmlEncode("기사는 관련 작업과 더불어 영구적으로 삭제되며 복구될 수 없습니다.  해당 기사를 정말 삭제하시겠습니까?") + _
            "&cmd=" + CStr(WebNewsConstants.ConfirmCommandDeleteArticle) + _
            "&ArticleID=" + lblArticleID.Text + _
            "&NavigateUrlYes=" + Server.HtmlEncode("ArticleEditor.aspx?EditMode=" + Request.QueryString("EditMode")))
        End If
    End Sub

    Private Sub chkSeeArticleSelector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeeArticleSelector.CheckedChanged
        pnlArticleSelector.Visible = chkSeeArticleSelector.Checked
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        binddata()
    End Sub
    Private Sub DataGrid1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged
        Dim DS As DataSet
        DS = WebNewsDB.GetArticlesByArticleID(DataGrid1.DataKeys(DataGrid1.SelectedIndex))
        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                lblArticleID.Text = .Item("ArticleID")
                txtTitle.Text = .Item("Title") & ""
                txtSubTitle.Text = .Item("SubTitle") & ""
                txtSubNayong.Text = .Item("SubNayong") & ""
                txtNayong.Text = .Item("Nayong1") + .Item("Nayong2") & ""
                txtWriterName.Text = .Item("WriterName") & ""
                txtEmail.Text = .Item("Email") & ""
                txtJunsongDateTime.Text = .Item("JunsongDateTime") & ""

                WebNewsDB.SetDropDownListByValue(ddlLanguage, .Item("LanguageID"))
                WebNewsDB.SetDropDownListByValue(ddlSection, .Item("SectionID"))
                WebNewsDB.SetDropDownListByValue(ddlLocal, .Item("LocalID"))

                If Not IsDBNull(.Item("MediaID")) Then
                    Dim DS2 As DataSet
                    DS2 = WebNewsDB.GetDataSetBySQLMun("select MediaParentID from Media where MediaID=" + CStr(.Item("MediaID")))
                    WebNewsDB.SetDropDownListByValue(ddlMedia1, DS2.Tables(0).Rows(0).Item("MediaParentID"))
                    WebNewsDB.InitDropDownList(ddlMedia2, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=" + ddlMedia1.SelectedValue + " and MediaID<>0")
                    WebNewsDB.SetDropDownListByValue(ddlMedia2, .Item("MediaID"))
                    DS2.Dispose()
                End If


                If txtLinkArticleID2.Value = "" Then
                    txtLinkArticleID2.Value = .Item("LinkArticles") & ""
                End If

                If txtLinkArticleID2.Value <> "" Then
                    bindLinkArticle()
                End If

                If .Item("Importance") = 2 Then
                    chkTopNews.Checked = True
                End If
                DataGrid2.DataSource = DS.Tables(1)
                DataGrid2.DataBind()
            End With
        End If
        DS.Dispose()



        chkSeeArticleSelector.Checked = False
        pnlArticleSelector.Visible = False
        pnlEntries.Visible = True
        DataGrid2.Visible = True
    End Sub


    Public Sub ddlMedia1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMedia1.SelectedIndexChanged
        WebNewsDB.InitDropDownList(ddlMedia2, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=" + ddlMedia1.SelectedValue + " and MediaID<>0")
        If ddlMedia1.SelectedValue = 301 Then
            lblChulcherEdit.Visible = True
            btnChulcherReload.Visible = True
        Else
            lblChulcherEdit.Visible = False
            btnChulcherReload.Visible = False
        End If
    End Sub

    Private Sub LinkSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkSubmit.Click
        bindLinkArticle()
    End Sub

    Sub bindLinkArticle()

        Dim ArticleID() As String = Split(txtLinkArticleID2.Value, ";")

        Dim i As Integer
        Dim DS As DataSet
        Dim Query As String

        Query = "select ArticleID,Title,inputDateTime from Articles where ArticleID=0 "
        For i = 0 To ArticleID.Length - 2
            Query += "or ArticleID=" + CStr(ArticleID(i))
        Next
        'Response.Write(Query)
        'Exit Sub
        DS = WebNewsDB.GetDataSetBySQLMun(Query)
        Datagrid3.DataSource = DS.Tables(0)
        Datagrid3.DataBind()
        DS.Dispose()

        Response.Write("<script>window.location.href='#bottom';</script>")
    End Sub

    Private Sub btnSetDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtJunsongDateTime.Text = Now
    End Sub

    Private Sub Datagrid3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid3.ItemCommand
        If e.CommandName = "Delete" Then
            Dim ArticleID As String
            ArticleID = Datagrid3.DataKeys(e.Item.ItemIndex).ToString + ";"
            txtLinkArticleID2.Value = Replace(txtLinkArticleID2.Value, ArticleID, "")
            bindLinkArticle()
        End If
    End Sub
    Public Sub btnChulcherReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChulcherReload.Click
        WebNewsDB.InitDropDownList(ddlMedia2, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=301 and MediaID<>0")
    End Sub
End Class
