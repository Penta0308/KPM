Public Class Top_jpn
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbl_LastEdit As System.Web.UI.WebControls.Label
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater

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
            BindLatestNews()
            ViewLastEdit()
        End If
    End Sub
    Private Sub ViewLastEdit()

        lbl_LastEdit.Text = WebNewsDB.GetLastUpdated.ToString("yyyyҴ MM�� dd��")

        '        Select Case Weekday(WebNewsDB.GetLastUpdated)
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Sunday
        '        lbl_LastEdit.Text += " (��)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Monday
        '        lbl_LastEdit.Text += " (��)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Tuesday
        '        lbl_LastEdit.Text += " (ȭ)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Wednesday
        '        lbl_LastEdit.Text += " (��)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Thursday
        '        lbl_LastEdit.Text += " (��)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Friday
        '        lbl_LastEdit.Text += " (��)"
        '            Case Microsoft.VisualBasic.FirstDayOfWeek.Saturday
        '        lbl_LastEdit.Text += " (��)"
        '        End Select

        lbl_LastEdit.Text += " " + WebNewsDB.GetLastUpdated.ToString("HH�� mm��") + " ����"

    End Sub
    Private Sub BindLatestNews()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT TOP 5 MediaName, ArticleID, Title, LanguageID, SectionID, Articles.MediaID, LocalID, JunsongDateTime "
        Str_Sql += " FROM Articles "
        Str_Sql += " INNER Join  Media ON Articles.MediaID = Media.MediaID "
        Str_Sql += " where AuthID>= 303 " '������
        Str_Sql += " and LanguageID = 201 " '�Ϻ���
        Str_Sql += " and importance > 0 "
        Str_Sql += " order by JunsongDateTime desc "
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
End Class
