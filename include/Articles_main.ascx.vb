Public Class Articles_main
    Inherits System.Web.UI.UserControl

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid

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
    Public MediaID As String
    Public lim_number As String
    Public image As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetTitle()
        If Not IsPostBack Then
            BindData()
        End If
    End Sub
    Private Sub GetTitle()
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("SELECT MediaName, lim_number, lim_date, image from Media WHERE MediaID = " + MediaID)
        lim_number = DS.Tables(0).Rows(0).Item("lim_number")
        If Not IsDBNull(DS.Tables(0).Rows(0).Item("image")) Then
            image = DS.Tables(0).Rows(0).Item("image")
            image = "<IMG height=""26"" src=""/images/" + image + """ width=""170"">"
        Else
            image = DS.Tables(0).Rows(0).Item("MediaName")
            image = "<b>" + image + "</b>"
        End If
        DS.Dispose()
    End Sub

    Private Sub BindData()
        Dim DS As DataSet
        DS = WebNewsDB.GetArticlesByMedia(CInt(MediaID), 1, lim_number, 101)
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()
    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
End Class
