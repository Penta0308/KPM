Public Class MediaEditor11
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
    Protected WithEvents pnlArticleSelector As System.Web.UI.WebControls.Panel
    Protected WithEvents btnAttachImage As System.Web.UI.WebControls.Button
    Protected WithEvents chkPreviewAfterSave As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents txtImageFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents chkSeeListSelector As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblInputDateTime As System.Web.UI.WebControls.Label
    Protected WithEvents imgThumb As System.Web.UI.WebControls.Image
    Protected WithEvents lblFilePath_Thumb As System.Web.UI.WebControls.Label
    Protected WithEvents imgPreview As System.Web.UI.WebControls.Image
    Protected WithEvents lblSizeW As System.Web.UI.WebControls.Label
    Protected WithEvents lblSizeH As System.Web.UI.WebControls.Label
    Protected WithEvents lblRev As System.Web.UI.WebControls.Label
    Protected WithEvents pnlFileInfo As System.Web.UI.WebControls.Panel
    Protected WithEvents lblFilePath_Large As System.Web.UI.WebControls.Label
    Protected WithEvents lblFilePath_Small As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents chkShowMain As System.Web.UI.WebControls.CheckBox
    Protected WithEvents pnlEntries As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlSelector As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlImgUpLoad As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlVideoUpLoad As System.Web.UI.WebControls.Panel
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents Datalist3 As System.Web.UI.WebControls.DataList
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents Datalist2 As System.Web.UI.WebControls.DataList
    Protected WithEvents lbl56k As System.Web.UI.WebControls.Label
    Protected WithEvents lbl300k As System.Web.UI.WebControls.Label
    Protected WithEvents lblThumb As System.Web.UI.WebControls.Label
    Protected WithEvents btn56kUpLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btn300kUpLoad As System.Web.UI.WebControls.Button
    Protected WithEvents lblThumbUpLoad As System.Web.UI.WebControls.Button
    Protected WithEvents txt56kFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txt300kFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtThumbFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblcaptionmsg As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileSize As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel4 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnPhoto2Article As System.Web.UI.WebControls.Button
    Protected WithEvents txtTitleJpn As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTitleEng As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaption As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaptionJpn As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaptionEng As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblmmFileID As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSection As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSection As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSelectSection As System.Web.UI.WebControls.DropDownList

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
        Response.AddHeader("Cache-Control", "no-cache")
        Response.AddHeader("Pragma", "no-cache")

        txtTitle.Attributes.Add("onkeyup", "cal_byteS('txtTitle', 50)")
        txtTitleJpn.Attributes.Add("onkeyup", "cal_byteS('txtTitleJpn', 50)")
        txtTitleEng.Attributes.Add("onkeyup", "cal_byteS('txtTitleEng', 50)")

        If Request.QueryString("popup") = "true" Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            btnAttachImage.Visible = False
            btnPhoto2Article.Visible = True
        End If
        Select Case Request.QueryString("EditMode")
            Case 201
                base_sPath = "\Uploaded\ImageCenter\"
                base_sPathDB = "/Uploaded/ImageCenter/"
            Case 202
                txtCaption.Attributes.Add("onkeyup", "cal_byteS('txtCaption', 400)")
                txtCaptionJpn.Attributes.Add("onkeyup", "cal_byteS('txtCaptionJpn', 400)")
                txtCaptionEng.Attributes.Add("onkeyup", "cal_byteS('txtCaptionEng', 400)")
                lblcaptionmsg.Visible = True
                ddlSelectSection.Visible = False

                base_sPath = "\Uploaded\VideoCenter\"
                base_sPathDB = "/Uploaded/VideoCenter/"
        End Select

        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If
            txtGijunDateTime.Text = Now.AddMonths(-1)
            lblInputDateTime.Text = Now
            If Not (Request.QueryString("EditMode") Is Nothing) Then

                lblAdminMenuName.Text = WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode"))
                If lblAdminMenuName.Text = "" Then
                    Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                    Exit Sub
                End If
            End If
            If Request.QueryString("mode") <> "" Then
                Viewset(Request.QueryString("mode"))
            End If
            WebNewsDB.InitDropDownList2(ddlSection, "SectionName", "SectionID", "SELECT [SectionID], [SectionName] FROM [Sections] where SectionID < 108 or sectionid=110")
            WebNewsDB.InitDropDownList2(ddlSelectSection, "SectionName", "SectionID", "SELECT [SectionID], [SectionName] FROM [Sections] where (SectionID < 108 and SectionID > 0) or sectionid=110", "전체")
            binddata()
        End If
    End Sub

    Private Sub binddata()
        Dim DS As DataSet
        DS = WebNewsDB.GetmmFilesByAuthID(Request.QueryString("EditMode"), ddlSelectSection.SelectedValue, CDate(txtGijunDateTime.Text), txtSearchWord.Text, 2)

        DataList1.DataSource = DS.Tables(0)
        DataList1.DataBind()


        DS = WebNewsDB.GetmmFilesByAuthID(Request.QueryString("EditMode"), ddlSelectSection.SelectedValue, CDate(txtGijunDateTime.Text), txtSearchWord.Text, 1)

        Datalist2.DataSource = DS.Tables(0)
        Datalist2.DataBind()


        DS = WebNewsDB.GetmmFilesByAuthID(Request.QueryString("EditMode"), ddlSelectSection.SelectedValue, CDate(txtGijunDateTime.Text), txtSearchWord.Text, 0)

        Datalist3.DataSource = DS.Tables(0)
        Datalist3.DataBind()

        DS.Dispose()


    End Sub
    Private Function mmFileSave(ByVal IsJunSong As Boolean) As Boolean
        mmFileSave = False
        'If DateCheck(txtInputDateTime, lblErrInput, Now) = False Then
        '    Exit Function
        'End If

        Dim LoginUserID As String = Request.Cookies("LoginUserID").Value
        Dim cmd, status As String


        If lblmmFileID.Text = "" Then
            cmd = "insert"
        Else
            cmd = "update"
        End If

        If chkShowMain.Checked = True Then
            status = "2"
        Else
            If IsJunSong Then
                status = "1"
            Else
                status = "0"
            End If
        End If

        Dim SectionValue As Integer
        If Request("EditMode") = 201 Then
            SectionValue = ddlSection.SelectedValue
        Else
            SectionValue = 0
        End If

        lblmmFileID.Text = WebNewsDB.SetmmFile(cmd, Val(lblmmFileID.Text), Request.QueryString("EditMode"), SectionValue, txtTitle.Text, txtTitleJpn.Text, txtTitleEng.Text, txtCaption.Text, txtCaptionJpn.Text, txtCaptionEng.Text, _
                                                 lblFilePath_Thumb.Text, lblFilePath_Small.Text, lblFilePath_Large.Text, _
                                                 lblFileSize.Text, status, lblInputDateTime.Text)

        mmFileSave = True
    End Function


    Private Sub btnAttachImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachImage.Click
        uploadIMG()
    End Sub
    Sub uploadIMG()
        Dim imgFile As Drawing.Image
        Dim imgPath As String

        imgPath = FileUpload(txtImageFile, "Large", lblFilePath_Large, WebNewsConstants.FileUploadModeImageFile)
        imgFile = Drawing.Image.FromFile(imgPath)

        lblSizeW.Text = imgFile.Width.ToString
        lblSizeH.Text = imgFile.Height.ToString
        lblRev.Text = imgFile.VerticalResolution.ToString

        imgFile.Dispose()

        pnlFileInfo.Visible = True

        lblFilePath_Small.Text = imgResizing(imgPath, 320, "Small")
        lblFilePath_Thumb.Text = imgResizing(imgPath, 190, "Thumb")


        imgThumb.ImageUrl = lblFilePath_Thumb.Text
        imgPreview.ImageUrl = lblFilePath_Small.Text

        imgPreview.Visible = True
        imgThumb.Visible = True
    End Sub
    Function imgResizing(ByVal imgPath As String, ByVal maxSize As Integer, ByVal type As String) As String
        Dim sizeW, sizeH, Rev As Integer
        'Dim img As Drawing.Image

        Dim Img As Drawing.Image
        Img = Drawing.Image.FromFile(imgPath)
        sizeW = Img.Width
        sizeH = Img.Height
        Rev = Img.VerticalResolution
        Img.Dispose()

        'Dim pThumbnail As Bitmap
        'Dim tempFile As New DEXTUpload.NET.FileUpload
        Dim pThumbnail As New DEXTUpload.NET.ImageProc
        pThumbnail.SetSourceFile(imgPath)
        'If sizeH > sizeW Then
        '    pThumbnail = img.GetThumbnailImage(CInt((sizeW * maxSize) / sizeH), maxSize, Nothing, New IntPtr)
        'ElseIf sizeW > sizeH Then
        '    pThumbnail = img.GetThumbnailImage(maxSize, CInt((sizeH * maxSize) / sizeW), Nothing, New IntPtr)
        'Else
        '    pThumbnail = img.GetThumbnailImage(maxSize, maxSize, Nothing, New IntPtr)
        'End If

        Dim sFile, sPath, sPathDB As String

        sFile = "KMP_" + Microsoft.VisualBasic.Left(type, 1) + lblmmFileID.Text + ".jpg"
        sPath = Server.MapPath(base_sPath + type)
        sPathDB = base_sPathDB + type
        If Dir(sPath, FileAttribute.Directory) = "" Then
            MkDir(sPath)
        End If

        WebNewsDB.GetDataSetBySQLMun("update mmFiles set Location_" + type + "='" + (sPathDB + "/" + sFile) + "' where mmFileID=" + lblmmFileID.Text)

        If sizeH > sizeW Then
            pThumbnail.SaveAsThumbnail(imgPath, sPath & "\" & sFile, CInt((sizeW * maxSize) / sizeH), maxSize, True)
        ElseIf sizeW > sizeH Then
            pThumbnail.SaveAsThumbnail(imgPath, sPath & "\" & sFile, maxSize, CInt((sizeH * maxSize) / sizeW), True)
        Else
            pThumbnail.SaveAsThumbnail(imgPath, sPath & "\" & sFile, maxSize, maxSize, True)
        End If

        'pThumbnail.Save(sPath & "\" & sFile, Drawing.Imaging.ImageFormat.Jpeg)

        pThumbnail.Dispose()

        Return (sPathDB + "/" + sFile)


    End Function
    Private Function FileUpload(ByRef txtFile As System.Web.UI.HtmlControls.HtmlInputFile, _
                        ByRef type As String, ByVal lblPath As WebControls.Label, ByVal FileUploadMode As Integer, Optional ByVal FileName As String = "") As String
        If txtFile.PostedFile.FileName = "" Then
            Response.Write("<script>alert('먼저 파일을 선택해주십시요');</script>")
            Exit Function
        End If
        If Val(lblmmFileID.Text) = 0 Then
            If mmFileSave(False) = False Then
                Exit Function
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
        sFile = "KPM_" + Microsoft.VisualBasic.Left(type, 1) + lblmmFileID.Text + Microsoft.VisualBasic.Right(sFile, 4)
        'sFileHeader8c = Microsoft.VisualBasic.Left(sFile, 8)
        If FileName <> "" Then
            sFile = FileName + Microsoft.VisualBasic.Right(sFile, 4)
        End If

        Select Case FileUploadMode
            Case WebNewsConstants.FileUploadModeImageFile
                sPath = Server.MapPath(base_sPath + type)
                sPathDB = base_sPathDB + type
            Case WebNewsConstants.FileUploadModeDocFile
                sPath = Server.MapPath(base_sPath + type)
                sPathDB = base_sPathDB + type
        End Select

        If Dir(sPath, FileAttribute.Directory) = "" Then
            MkDir(sPath)
        End If

        'Dim img As System.Drawing.Image
        'img = Drawing.Image.FromFile(sFullPath)

        sFullPath = sPath & "\" & sFile
        'Try
        txtFile.PostedFile.SaveAs(sFullPath)
        txtFile.Dispose()
        If type = "Large" Then
            lblFileSize.Text = CStr(Microsoft.VisualBasic.FileLen(sFullPath))
        End If

        WebNewsDB.GetDataSetBySQLMun("update mmFiles set Location_" + type + "='" + (sPathDB + "/" + sFile) + "',FileSize='" + lblFileSize.Text + "' where mmFileID=" + lblmmFileID.Text)
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If lblFilePath_Thumb.Text = "" Or lblFilePath_Small.Text = "" Or lblFilePath_Large.Text = "" Then
            Response.Write("<script>alert('파일첨부가 안되어있습니다.');</script>")
            Exit Sub
        End If
        If mmFileSave(True) = False Then
            Exit Sub
        End If
        If chkPreviewAfterSave.Checked Then
            Select Case Request.QueryString("EditMode")
                Case 201
                    Response.Write("<script>window.open('/popup_view_photo.aspx?mmFileID=" + lblmmFileID.Text + "','pop','toolbar=0, directories=0, status=0, menubar=no, scrollbars=yes, resizable=no,width=400,height=500,top=30,left=300');</script>")
                Case 202
                    Response.Write("<script>window.open('/popup_view_video.aspx?mmFileID=" + lblmmFileID.Text + "','pop','toolbar=0, directories=0, status=0, menubar=no, scrollbars=yes, resizable=no,width=400,height=500,top=30,left=300');</script>")
            End Select
            'Response.Write("<script>window.open ('/view_article.aspx?ArticleID=" + Server.HtmlEncode(lblmmFileID.Text) + "');</script>")
        Else
            'Response.Redirect("AdminMessage.aspx?Message=" + Server.HtmlEncode(txtTitle.Text + "의 " + lblAdminMenuName.Text + " 작업이 완료되었습니다"))
        End If
        Response.Write("<script>window.location.href='MediaEditor11.aspx?EditMode=" + Request.QueryString("EditMode") + "';</script>")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not (lblmmFileID.Text = "" Or Val(lblmmFileID.Text) = 0) Then
            Response.Redirect("AdminMessage.aspx" + _
            "?Message=" + Server.HtmlEncode("자료는 관련 작업과 더불어 영구적으로 삭제되며 복구될 수 없습니다.  해당 자료를 정말 삭제하시겠습니까?") + _
            "&cmd=" + CStr(WebNewsConstants.ConfirmCommandDeleteMMFile) + _
            "&mmFileID=" + lblmmFileID.Text + _
            "&NavigateUrlYes=" + Server.HtmlEncode("MediaEditor11.aspx?EditMode=" + Request.QueryString("EditMode")))
        End If
    End Sub

    Private Sub DataList1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        DataList1.SelectedIndex = e.Item.ItemIndex
        If e.CommandName = "update" Then
            Viewset_Update(DataList1, e.Item.ItemIndex)
        End If
    End Sub

    Private Sub Datalist2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles Datalist2.ItemCommand
        Datalist2.SelectedIndex = e.Item.ItemIndex
        If e.CommandName = "update" Then
            Viewset_Update(Datalist2, e.Item.ItemIndex)
        End If
    End Sub

    Private Sub Datalist3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles Datalist3.ItemCommand
        Datalist3.SelectedIndex = e.Item.ItemIndex
        If e.CommandName = "update" Then
            Viewset_Update(Datalist3, e.Item.ItemIndex)
        End If
    End Sub
    Private Sub chkSeeListSelector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeeListSelector.CheckedChanged
        pnlSelector.Visible = chkSeeListSelector.Checked
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect(Request.Url.ToString + "&mode=new")
    End Sub
    Sub Viewset(ByVal cmd As String)
        Select Case cmd
            Case "new"
                Select Case Request.QueryString("EditMode")
                    Case 201
                        pnlImgUpLoad.Visible = True
                    Case 202
                        pnlVideoUpLoad.Visible = True
                        lblSection.Visible = False
                End Select
                btnNew.Visible = False
                chkSeeListSelector.Visible = False
                chkSeeListSelector.Checked = False
                pnlSelector.Visible = False
                pnlEntries.Visible = True

        End Select
    End Sub

    Sub Viewset_Update(ByVal dList As WebControls.DataList, ByVal ItemIndex As Integer)
        Dim DS As DataSet
        DS = WebNewsDB.GetmmFilesBymmFileID(dList.DataKeys(ItemIndex))
        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                Select Case Request.QueryString("EditMode")
                    Case 201
                        WebNewsDB.SetDropDownListByValue(ddlSection, .Item("SectionID"))
                        Dim imgFile As Drawing.Image

                        imgFile = Drawing.Image.FromFile(Server.MapPath(.Item("Location_Large")))

                        lblSizeW.Text = imgFile.Width.ToString
                        lblSizeH.Text = imgFile.Height.ToString
                        lblRev.Text = imgFile.VerticalResolution.ToString

                        imgFile.Dispose()

                        pnlFileInfo.Visible = True

                        lblFilePath_Thumb.Text = .Item("Location_Thumb")
                        lblFilePath_Small.Text = .Item("Location_small")
                        lblFilePath_Large.Text = .Item("Location_large")

                        imgThumb.ImageUrl = lblFilePath_Thumb.Text
                        imgPreview.ImageUrl = lblFilePath_Small.Text

                        imgPreview.Visible = True
                        imgThumb.Visible = True

                        pnlImgUpLoad.Visible = True
                    Case 202

                        lblThumb.Text = .Item("Location_Thumb")
                        lbl56k.Text = .Item("Location_small")
                        lbl300k.Text = .Item("Location_large")

                        lblFilePath_Thumb.Text = .Item("Location_Thumb")
                        lblFilePath_Small.Text = .Item("Location_small")
                        lblFilePath_Large.Text = .Item("Location_large")

                        pnlVideoUpLoad.Visible = True
                End Select
                lblFileSize.Text = .Item("FileSize")
                lblmmFileID.Text = .Item("mmFileID")
                txtTitle.Text = .Item("Title") & ""
                txtTitleJpn.Text = .Item("TitleJpn") & ""
                txtTitleEng.Text = .Item("TitleEng") & ""
                txtCaption.Text = .Item("Caption") & ""
                txtCaptionJpn.Text = .Item("CaptionJpn") & ""
                txtCaptionEng.Text = .Item("CaptionEng") & ""
                lblInputDateTime.Text = .Item("inputDateTime") & ""

                If .Item("Status") = 2 Then
                    chkShowMain.Checked = True
                End If
            End With
        End If
        DS.Dispose()

        chkSeeListSelector.Checked = False
        pnlSelector.Visible = False
        pnlEntries.Visible = True
        DataList1.Visible = True
    End Sub

    Private Sub btn56kUpLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn56kUpLoad.Click
        FileUpload(txt56kFile, "Small", lbl56k, WebNewsConstants.FileUploadModeImageFile)
        lblFilePath_Small.Text = lbl56k.Text
    End Sub

    Private Sub btn300kUpLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn300kUpLoad.Click
        FileUpload(txt300kFile, "Large", lbl300k, WebNewsConstants.FileUploadModeImageFile)
        lblFilePath_Large.Text = lbl300k.Text
    End Sub

    Private Sub lblThumbUpLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblThumbUpLoad.Click
        Dim sFile As String
        sFile = FileUpload(txtThumbFile, "Thumb", lblThumb, WebNewsConstants.FileUploadModeImageFile, "Temp")

        lblThumb.Text = imgResizing(sFile, 190, "Thumb")

        lblFilePath_Thumb.Text = lblThumb.Text
    End Sub
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
            'DataGrid1.CurrentPageIndex = 0
            binddata()
        End If
    End Sub

    Private Sub btnPhoto2Article_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto2Article.Click
        If btnPhoto2Article.Text = "첨부" Then
            If txtTitle.Text = "" Then
                Response.Write("<script>alert('제목을 입력하세요.');</script>")
                Exit Sub
            End If
            If txtCaption.Text = "" Then
                Response.Write("<script>alert('설명을 입력하세요.');</script>")
                Exit Sub
            End If
            uploadIMG()
            mmFileSave(True)
            Response.Write("<script>alert('저장되었습니다..');</script>")
            Response.Write("<script>opener.document.all.txtPhotoID_temp.value+='" + lblmmFileID.Text + "';</script>")
            Response.Write("<script>opener.document.Form1.btnAttachImage.click();</script>")
            btnPhoto2Article.Text = "닫기"

        Else
            Response.Write("<script>window.close();</script>")
        End If
    End Sub

    Private Sub ddlSelectSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSelectSection.SelectedIndexChanged
        binddata()
    End Sub
End Class
