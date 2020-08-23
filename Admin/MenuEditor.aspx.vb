Public Class MenuEditor
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lstRight As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnUp As System.Web.UI.WebControls.Button
    Protected WithEvents btnDown As System.Web.UI.WebControls.Button
    Protected WithEvents lblCategoryCount As System.Web.UI.WebControls.Label
    Protected WithEvents txtCategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnInsCategory As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents txtCategoryName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEditCategory As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents lblTemp As System.Web.UI.WebControls.Label

    '참고: 다음의 자리 표시자 선언은 Web Form 디자이너의 필수 선언입니다.
    '삭제하거나 옮기지 마십시오.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 이 메서드 호출은 Web Form 디자이너에 필요합니다.
        '코드 편집기를 사용하여 수정하지 마십시오.
        InitializeComponent()
    End Sub

#End Region
    Dim DS As New DataSet
    Dim strQuery As String
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblTemp.Text = ""
        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If
            If Not (Request.QueryString("EditMode") Is Nothing) Then
                Dim AdminmenuName As String
                AdminmenuName = WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode"))
                If AdminmenuName = "" Then
                    Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                    Exit Sub
                End If

            End If
            bindData()
        End If
    End Sub

    Sub bindData()
        Dim i As Integer
        strQuery = "select MediaID,MediaName from Media  where MediaParentID=" + Request("MediaParentID")
        strQuery += " order by sequence"
        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)

        lstRight.Items.Clear()
        With DS.Tables(0)
            For i = 0 To .Rows.Count - 1
                Dim list As New System.Web.UI.WebControls.ListItem
                list.Text = .Rows(i).Item("MediaName")
                list.Value = .Rows(i).Item("MediaID")
                lstRight.Items.Add(list)
            Next
        End With
        DS.Dispose()
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        If IsPostBack Then
            'Dim local, up, del As Integer
            'local = lstRight.SelectedIndex
            'up = local - 1
            'del = local + 1
            'If local > 0 Then
            '    lstRight.Items.Insert(up, lstRight.Items(local))
            '    lstRight.Items.RemoveAt(del)
            '    lstRight.SelectedIndex = up
            'End If
            Dim Sequence, MediaID, MediaParentID As String
            sequence = CInt(lstRight.SelectedIndex - 1)
            MediaID = lstRight.SelectedValue
            MediaParentID = Request("MediaParentID")

            strQuery = "update media set sequence = Sequence+1 where sequence = " + Sequence + " and MediaParentID=" + MediaParentID
            WebNewsDB.GetDataSetBySQLMun(strQuery)

            strQuery = "update media set sequence=" + Sequence + " where mediaID=" + MediaID
            WebNewsDB.GetDataSetBySQLMun(strQuery)

            bindData()
            lstRight.SelectedIndex = CInt(Sequence)
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        If IsPostBack Then
            'Dim local, down, del, count As Integer
            'local = lstRight.SelectedIndex
            'down = local + 2
            'del = local

            'count = lstRight.Items.Count - 1
            'If count <> local And local >= 0 Then
            '    lstRight.Items.Insert(down, lstRight.Items(local))

            '    lstRight.Items.RemoveAt(local)
            '    lstRight.SelectedIndex = down - 1
            'End If
            Dim Sequence, MediaID, MediaParentID As String
            Sequence = CInt(lstRight.SelectedIndex + 1)
            MediaID = lstRight.SelectedValue
            MediaParentID = Request("MediaParentID")

            strQuery = "update media set sequence = Sequence-1 where sequence = " + Sequence + " and MediaParentID=" + MediaParentID
            WebNewsDB.GetDataSetBySQLMun(strQuery)

            strQuery = "update media set sequence=" + Sequence + " where mediaID=" + MediaID
            WebNewsDB.GetDataSetBySQLMun(strQuery)

            bindData()
            lstRight.SelectedIndex = CInt(Sequence)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lstIndex As Integer = lstRight.SelectedIndex
        If lstIndex < 0 Then
            Response.Write("<script>alert('분류를 선택해주십시오.');</script>")
            Exit Sub
        End If

        Dim lstValue As String
        lstValue = lstRight.Items(lstRight.SelectedIndex).Value

        strQuery = "select articleID from articles  where MediaID=" + lstValue
        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)
        If DS.Tables(0).Rows.Count > 0 Then
            'Response.Write("<script>PopupOpen('MenuEditorSub.aspx?MediaID=301&EditMode=301','MenuEditorSub','300','320');</script>")
            lblTemp.Text = "<script>PopupOpen('MenuEditorSub.aspx?MediaID=" + lstValue + "&EditMode=301','MenuEditorSub','370','200');</script>"
            Exit Sub
        End If
        DS.Dispose()

        strQuery = "delete from Media where MediaID=" + lstRight.SelectedValue
        WebNewsDB.GetDataSetBySQLMun(strQuery)

        bindData()
        txtCategoryName.Text = ""
    End Sub

    Private Sub btnInsCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsCategory.Click
        If Trim(txtCategory.Text) = "" Then
            Response.Write("<script>alert('분류명을 입력해주십시오.');</script>")
            Exit Sub
        End If

        strQuery = "select max(MediaID)as MaxMediaID,max(sequence)as MaxSequence from Media where MediaParentID=" + Request("MediaParentID")
        DS = WebNewsDB.GetDataSetBySQLMun(strQuery)

        Dim maxMediaID, maxSequence As Integer
        With DS.Tables(0).Rows(0)
            maxMediaID = .Item("MaxMediaID") + 1
            maxSequence = .Item("MaxSequence") + 1
        End With
        DS.Dispose()

        strQuery = "insert into Media(MediaID, MediaParentID, MediaName, Sequence, lim_date) values"
        strQuery += "('"
        strQuery += CStr(maxMediaID) + "','"
        strQuery += Request("MediaParentID") + "','"
        strQuery += txtCategory.Text + "','"
        strQuery += CStr(maxSequence) + "','7')"
        WebNewsDB.GetDataSetBySQLMun(strQuery)
        bindData()

        txtCategory.Text = ""
        lstRight.SelectedIndex = lstRight.Items.Count - 1
    End Sub

    Private Sub lstRight_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstRight.SelectedIndexChanged
        txtCategoryName.Text = lstRight.SelectedItem.Text
    End Sub

    Private Sub btnEditCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditCategory.Click
        Dim lstIndex As Integer = lstRight.SelectedIndex
        If lstIndex < 0 Then
            Response.Write("<script>alert('분류를 선택해주십시오.');</script>")
            Exit Sub
        End If
        'lstRight.SelectedItem.Text = txtCategoryName.Text
        strQuery = "update Media set MediaName='" + txtCategoryName.Text + "' where MediaID=" + lstRight.SelectedValue
        WebNewsDB.GetDataSetBySQLMun(strQuery)
        bindData()
        lstRight.SelectedIndex = lstIndex
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script>opener.document.Form1.btnChulcherReload.click();window.close();</script>")
    End Sub
End Class
