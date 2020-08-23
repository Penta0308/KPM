Imports System.IO
Public Class popup_view_photo_jpn
    Inherits System.Web.UI.Page



#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents imgPreview As System.Web.UI.WebControls.Image
    Protected WithEvents lblSizeW As System.Web.UI.WebControls.Label
    Protected WithEvents lblSizeH As System.Web.UI.WebControls.Label
    Protected WithEvents lblRev As System.Web.UI.WebControls.Label
    Protected WithEvents lblByte As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaption As System.Web.UI.WebControls.Label

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
    Public url_photo As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
        If Not IsPostBack Then
            ViewImage()
        End If
    End Sub
    Sub ViewImage()
        Dim DS As DataSet
        DS = WebNewsDB.GetmmFilesBymmFileID(Request.QueryString("mmFileID"))
        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                Dim imgFile As Drawing.Image
                imgFile = Drawing.Image.FromFile(Server.MapPath(.Item("Location_Large")))

                lblSizeW.Text = imgFile.Width.ToString
                lblSizeH.Text = imgFile.Height.ToString
                lblRev.Text = imgFile.VerticalResolution.ToString

                imgPreview.ImageUrl = .Item("Location_small")

                lblTitle.Text = .Item("TitleJpn") + ""

                lblCaption.Text = Replace(.Item("CaptionJpn") + "", Chr(13), "<br>")

                url_photo = .Item("Location_Large")

                If .Item("FileSize") < 1024 Then
                    lblByte.Text = Format(.Item("FileSize"), "0 B")
                ElseIf .Item("FileSize") < 1024 * 1024 Then
                    lblByte.Text = Format(.Item("FileSize") / 1024, "0 KB")
                Else
                    lblByte.Text = Format(.Item("FileSize") / (1024 * 1024), "0.# MB")
                End If

            End With
        End If
        DS.Dispose()
    End Sub
End Class
