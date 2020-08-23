Imports System.IO
Public Class popup_view_photo_jpn
    Inherits System.Web.UI.Page



#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents imgPreview As System.Web.UI.WebControls.Image
    Protected WithEvents lblSizeW As System.Web.UI.WebControls.Label
    Protected WithEvents lblSizeH As System.Web.UI.WebControls.Label
    Protected WithEvents lblRev As System.Web.UI.WebControls.Label
    Protected WithEvents lblByte As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaption As System.Web.UI.WebControls.Label

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
    Public url_photo As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���⿡ ����� �ڵ带 ��ġ�Ͽ� �������� �ʱ�ȭ�մϴ�.
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
