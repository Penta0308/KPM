Public Class gate_popup_notice
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents lblText As System.Web.UI.WebControls.Label

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
            bindbbs()
        End If
    End Sub
    Sub bindbbs()
        Dim DS As New DataSet
        Dim strQuery, strBbsID, strNumber As String

        strBbsID = "notice"
        strNumber = Request("no")

        Dim strTBName As String = "bbs"

        WebNewsDB.GetDataSetBySQLMun("Update " + strTBName + " set hit=hit+1 where bbsid='" + strBbsID + "' and number='" + strNumber + "'")

        strQuery = "select subject,text,writeuserid,writeusername,regdate,hit from " + strTBName + " where bbsID='" + strBbsID + "' and number='" + strNumber + "'"

        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)

        'lblNo.Text = Request("no")

        With DS.Tables(0).Rows(0)
            lblSubject.Text = .Item("subject")
            'lblWriter.Text = .Item("WriteUserName")
            'lblHit.Text = .Item("hit")
            'lblRegDate.Text = .Item("RegDate")
            lblText.Text = .Item("text")
        End With

        DS.Dispose()

        lblText.Text = Replace(lblText.Text, vbCrLf, "<br>")

    End Sub
End Class
