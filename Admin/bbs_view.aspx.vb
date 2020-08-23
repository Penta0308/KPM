Public Class bbs_view
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents Imagebutton2 As System.Web.UI.WebControls.ImageButton
    Private WebNewsConstants As Constants
#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblText As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents lblNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblWriter As System.Web.UI.WebControls.Label
    Protected WithEvents lblHit As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegDate As System.Web.UI.WebControls.Label
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblAdmin As System.Web.UI.WebControls.Label

    '참고: 다음의 자리 표시자 선언은 Web Form 디자이너의 필수 선언입니다.
    '삭제하거나 옮기지 마십시오.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 이 메서드 호출은 Web Form 디자이너에 필요합니다.
        '코드 편집기를 사용하여 수정하지 마십시오.
        InitializeComponent()
    End Sub

#End Region
    Public strStyle As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("User_type") <> "1" Then
            lblAdmin.Visible = False
        Else
            lblAdmin.Visible = True
        End If

        If Not IsPostBack Then
            If Microsoft.VisualBasic.Right(Request("bbsID"), 3) = "jpn" Then
                strStyle = "/japanese/include/main.css"
            Else
                strStyle = "/include/main.css"
            End If
            bindbbs()
            If Request("bbsID") = "contact" Then
                Imagebutton2.Visible = True
                lblAdmin.Visible = False
            End If
        End If
    End Sub
    Sub bindbbs()
        Dim DS As New DataSet
        Dim strQuery, strBbsID, strNumber As String

        strBbsID = Request("bbsid")
        strNumber = Request("no")

        Dim strTBName As String = "bbs"

        WebNewsDB.GetDataSetBySQLMun("Update " + strTBName + " set hit=hit+1 where bbsid='" + strBbsID + "' and number='" + strNumber + "'")

        strQuery = "select subject,text,writeuserid,writeusername,regdate,hit, filename from " + strTBName + " where bbsID='" + strBbsID + "' and number='" + strNumber + "'"

        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)

        lblNo.Text = Request("no")

        With DS.Tables(0).Rows(0)
            lblSubject.Text = .Item("subject")
            lblWriter.Text = .Item("WriteUserName")
            lblHit.Text = .Item("hit")
            lblRegDate.Text = .Item("RegDate")
            lblText.Text = .Item("text")
            If Not IsDBNull(.Item("filename")) Then
                If .Item("filename") <> "" Then
                    lblFileName.Text = "첨부파일 : <a href=" + .Item("filename") + ">" + .Item("FileName") + "</a>"
                End If
            End If
        End With

        DS.Dispose()

        lblText.Text = Replace(lblText.Text, vbCrLf, "<br>")

    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Response.Redirect("bbs_list.aspx?bbsid=" + Request("bbsID") + "&keyword=" + Request("keyword") + "&keywordkind=" + Request("keywordkind") + "&pg=" + Request("pg"))
    End Sub

    Private Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Delete()
    End Sub

    Private Sub Imagebutton2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebutton2.Click
        Delete()
    End Sub
    Sub Delete()
        Dim strQuery As String
        Dim DS As DataSet
        strQuery = "select filename from bbs where bbsID='" + Request("bbsid") + "' and number='" + Request("no") + "'"
        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)
        With DS.Tables(0).Rows(0)
            If Not IsDBNull(.Item("filename")) Then
                If IO.File.Exists(Server.MapPath(.Item("FileName"))) Then
                    Kill(Server.MapPath(.Item("FileName")))
                End If
            End If
        End With
        WebNewsDB.GetDataSetBySQLMun("delete from bbs where bbsid='" + Request("bbsID") + "' and number='" + Request("no") + "'")
        Response.Write("<script>alert('삭제되었습니다.');window.location.href='bbs_list.aspx?bbsid=" + Request("bbsID") + "';</script>")
    End Sub
End Class
