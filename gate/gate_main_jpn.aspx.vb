Public Class gate_main_jpn
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents Repeater2 As System.Web.UI.WebControls.Repeater

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
        BindLatestNews()
        BindLatestNotice()
    End Sub
    Private Sub BindLatestNews()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 10 MediaNameJpn, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID>= 303 "
        Str_Sql += " and importance > 0 "
        Str_Sql += " and LanguageId = 201 "
        Str_Sql += " order by JunsongDateTime desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
    Private Sub BindLatestNotice()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 5 * "
        Str_Sql += " FROM bbs "
        Str_Sql += " where bbsid='noticejpn' "
        Str_Sql += " order by number desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater2.DataSource = DS.Tables(0)
        Repeater2.DataBind()
        DS.Dispose()
    End Sub
End Class