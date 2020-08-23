Public Class gate_popup_notice
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents lblText As System.Web.UI.WebControls.Label

    '참고: 다음의 자리 표시자 선언은 Web Form 디자이너의 필수 선언입니다.
    '삭제하거나 옮기지 마십시오.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 이 메서드 호출은 Web Form 디자이너에 필요합니다.
        '코드 편집기를 사용하여 수정하지 마십시오.
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
