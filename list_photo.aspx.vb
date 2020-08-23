Public Class list_photo
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlSelectSection As System.Web.UI.WebControls.DropDownList

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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���⿡ ����� �ڵ带 ��ġ�Ͽ� �������� �ʱ�ȭ�մϴ�.
        If Not IsPostBack Then
            WebNewsDB.InitDropDownList2(ddlSelectSection, "SectionName", "SectionID", "SELECT [SectionID], [SectionName] FROM [Sections] where SectionID < 109 and SectionID > 0", "��ü")
            BindData()
            CType(FindControl("TOP1").FindControl("hl_photo"), HyperLink).ForeColor = Color.Yellow
        End If
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        DS = WebNewsDB.FGetmmFilesByAuthID(201, ddlSelectSection.SelectedValue, 0)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub ddlSelectSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSelectSection.SelectedIndexChanged
        BindData()
    End Sub
End Class
