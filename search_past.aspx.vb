Public Class search_past
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDay As System.Web.UI.WebControls.DropDownList
    Protected WithEvents imgbtnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label
    Protected WithEvents msg_result As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlMedia As System.Web.UI.WebControls.DropDownList

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
        If Not IsPostBack Then
            Dim i As Integer
            For i = Year(Now) To 1953 Step -1
                ddlYear.Items.Add(i.ToString)
            Next
            For i = 1 To 12
                ddlMonth.Items.Add(i.ToString)
            Next
            For i = 1 To 31
                ddlDay.Items.Add(i.ToString)
            Next

            ddlMonth.SelectedValue = Month(Now)
            ddlDay.SelectedValue = Day(Now)

            InitDropDownList(ddlMedia, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=101 order by sequence", "��ü")

            CType(FindControl("Left1").FindControl("hl_past"), HyperLink).ForeColor = Color.Red
        End If
    End Sub
    Sub bindData()
        Dim DT As DataTable
        DT = ViewState("DT")
        If DT.Rows.Count > 0 Then
            DataGrid1.Visible = True
        Else
            DataGrid1.Visible = False
        End If
        msg_result.Visible = True
        DataGrid1.DataSource = DT
        DataGrid1.DataBind()
        lblCount.Text = DT.Rows.Count
    End Sub
    Public Sub InitDB(ByVal DT As DataTable)
        DataGrid1.CurrentPageIndex = 0
        ViewState("DT") = DT
        bindData()
    End Sub

    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        bindData()
    End Sub
    Public Sub InitDropDownList(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataTextField As String, ByVal DataValueField As String, ByVal SQLMun As String, Optional ByVal DefaultValue As String = "")
        Dim DT As DataTable
        Dim i As Integer
        DT = WebNewsDB.GetDataTableBySQL(SQLMun)
        With ddlTemp
            .Items.Clear()
            If DT.Rows.Count = 0 Then
                ddlTemp.Items.Add("����")
                Exit Sub
            End If

            If DefaultValue <> "" Then
                Dim aItem1 As New System.Web.UI.WebControls.ListItem
                aItem1.Text = DefaultValue
                aItem1.Value = "0"
                ddlTemp.Items.Add(aItem1)
            End If

            For i = 0 To DT.Rows.Count - 1
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = DT.Rows(i).Item(DataTextField)
                aItem.Value = DT.Rows(i).Item(DataValueField)
                ddlTemp.Items.Add(aItem)
            Next
            '.DataTextField = DataTextField
            '.DataValueField = DataValueField
            '.DataSource = DT
            .DataBind()
        End With
        DT.Dispose()
    End Sub
    Private Sub imgbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim SelectedDate As String
        SelectedDate = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + ddlDay.SelectedValue
        If IsDate(SelectedDate) Then
            Dim DS As DataSet
            DS = WebNewsDB.GetArticlesByJunsongDateTime(CDate(SelectedDate), DateAdd(DateInterval.Day, 1, CDate(SelectedDate)), ddlMedia.SelectedValue, 101)
            InitDB(DS.Tables(0))
            DS.Dispose()
        Else
            Response.Write("<script>alert('" + SelectedDate + "�� �߸��� ��¥�Դϴ�. �ٽ� �����Ͻʽÿ�');history.back(-1);</script>")
            Exit Sub
        End If
    End Sub
    Public Function Img_photo(ByVal chkPhoto As String) As String
        Dim Img_p As String
        If chkPhoto = "1" Then
            Img_p = "<img src='/images/icon_photo.gif' align='absmiddle'>"
        End If
        Return Img_p
    End Function
End Class
