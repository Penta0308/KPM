Public Class Articles3_main
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
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
    Public cmd As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub
    Private Sub BindData()
        Dim DS As DataSet
        Dim Str_Sql As String

        Str_Sql = "SELECT TOP 15 MediaName, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime, chkphoto "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID >= 303 "
        Str_Sql += " and LanguageID = 101 "
        If cmd = "15" Then
            Str_Sql += " and ( Media.MediaID = '1004' or Media.MediaID = '1005' or Media.MediaID = '1006' ) "
        Else
            Str_Sql += " and importance > 1 "
        End If
        Str_Sql += " order by JunsongDateTime desc "

        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        DataList1.DataSource = DS.Tables(0)
        DataList1.DataBind()
        DS.Dispose()
        Dim i As Integer

        For i = 4 To DataList1.Items.Count - 2 Step 5
            CType(DataList1.Items(i).FindControl("Label2"), Label).Text = "<img src=/images/line_main.gif>"
        Next

    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
End Class

