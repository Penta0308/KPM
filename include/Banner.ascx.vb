Public Class Banner
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents pnlThis As System.Web.UI.WebControls.Panel

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
    End Sub
    Public Sub InitData(ByVal LanguageID As Integer, ByVal AdTypeID As Integer)
        Dim DS As DataSet
        DS = WebNewsDB.GetBannerByLanguageIDAndAdTypeID(LanguageID, AdTypeID)
        If DS.Tables(0).Rows.Count > 0 Then
            pnlThis.Visible = True
            DataList1.DataSource = DS.Tables(0)
            DataList1.DataBind()
        Else
            pnlThis.Visible = False
        End If

        DS.Dispose()

    End Sub
End Class
