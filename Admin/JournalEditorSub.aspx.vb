Public Class JournalEditorSub
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlJournal As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtGanHengMulNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtISSN As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBalHengJi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBalHengCher As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAdd As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtJournalName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJournalID As System.Web.UI.WebControls.Label

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
        If Not IsPostBack Then
            WebNewsDB.InitDropDownList(ddlJournal, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=201")
            bindData()
        End If
    End Sub
    Private Function Save(ByVal IsJunSong As Boolean, Optional ByVal delete As String = "") As Boolean
        Save = False

        If ddlJournal.SelectedValue = 0 Then
            Response.Write("<script>alert('�м������� ������ �ֽʽÿ�');</script>")
            Exit Function
        End If

        Dim LoginUserID As String = Request.Cookies("LoginUserID").Value
        Dim EditMode As Integer

        Dim cmd, jn As String

        If lblJournalID.Text = "" Then
            cmd = "insert"
        Else
            cmd = "update"
        End If

        If delete = "delete" Then
            cmd = "delete"
        End If
        If ddlJournal.Visible = True Then
            jn = ddlJournal.SelectedItem.Text
        Else
            jn = txtJournalName.Text
        End If

        lblJournalID.Text = WebNewsDB.SetJournalName(cmd, Val(lblJournalID.Text), jn, txtBalHengCher.Text, _
                                                    txtBalHengJi.Text, txtGanHengMulNo.Text, txtISSN.Text)

        Response.Write("<script>opener.document.Form1.btnJournalRefresh.click();</script>")
        Response.Write("<script>window.location.href='JournalEditorSub.aspx';</script>")

        Save = True
    End Function


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save(True)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Save(True, "delete")

        WebNewsDB.InitDropDownList(ddlJournal, "MediaName", "MediaID", "SELECT MediaID, MediaName FROM Media where MediaParentID=201")
        bindData()

    End Sub

    Private Sub ddlJournal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJournal.SelectedIndexChanged
        bindData()
    End Sub
    Sub bindData()
        If ddlJournal.SelectedValue = 0 Then Exit Sub
        Dim DS As DataSet
        DS = WebNewsDB.GetDataSetBySQLMun("select * from journals where journalid='" + ddlJournal.SelectedValue.ToString + "'")

        With DS.Tables(0).Rows(0)
            lblJournalID.Text = ddlJournal.SelectedValue.ToString + ""
            txtGanHengMulNo.Text = .Item("JungGiGanHengMulNo") + ""
            txtISSN.Text = .Item("ISSN") + ""
            txtBalHengJi.Text = .Item("BalHengJi") + ""
            txtBalHengCher.Text = .Item("BalHengCher") + ""


        End With
        DS.Dispose()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        btnAdd.Visible = False
        ddlJournal.Visible = False
        txtJournalName.Visible = True

        lblJournalID.Text = ""
        txtJournalName.Text = ""
        txtBalHengCher.Text = ""
        txtBalHengJi.Text = ""
        txtGanHengMulNo.Text = ""
        txtISSN.Text = ""
    End Sub
End Class
