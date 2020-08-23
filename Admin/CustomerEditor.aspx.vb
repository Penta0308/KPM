Public Class CustomerEditor
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents txtSearchWord As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlEntries As System.Web.UI.WebControls.Panel
    Protected WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerLoginID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerLoginPWD As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerLoginPWD2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnIPRange As System.Web.UI.WebControls.Button
    Protected WithEvents dgIPRange As System.Web.UI.WebControls.DataGrid
    Protected WithEvents IPRangeBegin As System.Web.UI.WebControls.TextBox
    Protected WithEvents IPRangeEnd As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCustomerID As System.Web.UI.WebControls.Label
    Protected WithEvents chkSeeSelector As System.Web.UI.WebControls.CheckBox
    Protected WithEvents pnlSelector As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlLicense As System.Web.UI.WebControls.DropDownList

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
    Private WebNewsConstants As Constants
    Private SQL As String
    Dim DS As New DataSet
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Request.Cookies("LoginUserID") Is Nothing Then
                Response.Redirect("AdminMain.aspx")
                Exit Sub
            End If

            If Not (Request.QueryString("EditMode") Is Nothing) Then

                lblAdminMenuName.Text = WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode"))
                If lblAdminMenuName.Text = "" Then
                    Response.Write("<script>alert('이 메뉴의 사용권한이 없습니다. 관리자에게 권한을 요청하십시오.');history.back(-1);</script>")
                    Exit Sub
                End If

                If Request.QueryString("mode") <> "" Then
                    ViewSet(Request.QueryString("mode"))
                End If

                binddata()

                Dim i As Integer
                For i = 1 To 100
                    ddlLicense.Items.Add(i)
                Next
            End If
        End If

    End Sub
    Private Sub ViewSet(ByVal cmd As String)

        pnlSelector.Visible = True
        Select Case cmd
            Case "new"
                btnNew.Visible = False
                btnDelete.Visible = False
                pnlSelector.Visible = False
                chkSeeSelector.Visible = False
                pnlEntries.Visible = True
            Case Else
                binddata()
        End Select

    End Sub
    Private Sub binddata()
        SQL = "select *, (SELECT COUNT(*) FROM customeripranges WHERE   customerid = customers.customerid) AS ipCount"
        SQL = SQL + " from Customers"
        SQL = SQL + " where customername like '%" + txtSearchWord.Text + "%'"
        SQL = SQL + " order by customerid desc"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        'DS = WebNewsDB.GetArticlesByAuthID(LanguageID, Request.QueryString("EditMode"), True, CDate(txtGijunDateTime.Text), txtSearchWord.Text)
        DataGrid1.DataKeyField = "customerID"
        DataGrid1.DataSource = DS.Tables(0)
        DataGrid1.DataBind()
        DS.Dispose()

        Dim i As Integer
        With DataGrid1
            For i = 0 To .Items.Count - 1
                If .Items(i).Cells(4).Text > 0 Then
                    .Items(i).Cells(4).Text = "있음"
                Else
                    .Items(i).Cells(4).Text = ""
                End If
                If .Items(i).Cells(3).Text = 0 Then
                    .Items(i).Cells(3).Text = "무제한"
                End If
            Next
        End With

    End Sub
    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not (lblArticleID.Text = "" Or Val(lblArticleID.Text) = 0) Then
    '        Response.Redirect("AdminMessage.aspx" + _
    '        "?Message=" + Server.HtmlEncode("기사는 관련 작업과 더불어 영구적으로 삭제되며 복구될 수 없습니다.  해당 기사를 정말 삭제하시겠습니까?") + _
    '        "&cmd=" + CStr(WebNewsConstants.ConfirmCommandDeleteArticle) + _
    '        "&ArticleID=" + lblArticleID.Text + _
    '        "&NavigateUrlYes=" + Server.HtmlEncode("ArticleEditor.aspx?EditMode=" + Request.QueryString("EditMode")))
    '    End If
    'End Sub

    Private Sub chkSeeSelector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeeSelector.CheckedChanged
        pnlSelector.Visible = chkSeeSelector.Checked
    End Sub
    Sub doPaging(ByVal s As Object, ByVal e As DataGridPageChangedEventArgs)
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        binddata()
    End Sub
    Private Sub DataGrid1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

        SQL = "select * from Customers where customerid=" + CStr(DataGrid1.DataKeys(DataGrid1.SelectedIndex)) + " order by regDate"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)

        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                txtCustomerName.Text = .Item("CustomerName")
                txtCustomerLoginID.Text = .Item("CustomerLoginID") & ""
                txtCustomerLoginPWD.Text = .Item("CustomerLoginPWD") & ""
                lblCustomerID.Text = .Item("CustomerID")
                WebNewsDB.SetDropDownListByValue(ddlLicense, .Item("license"))
            End With
        End If


        DS.Dispose()
        binddgIPRange()


        chkSeeSelector.Checked = False
        pnlSelector.Visible = False
        pnlEntries.Visible = True
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect(Request.Url.ToString + "&mode=new")
    End Sub
    Sub setEditMode(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        dgIPRange.EditItemIndex = e.Item.ItemIndex
        binddgIPRange()
    End Sub
    Sub binddgIPRange()
        SQL = "select * from CustomerIPRanges where CustomerID=" + lblCustomerID.Text
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        dgIPRange.DataSource = DS.Tables.Item(0)
        dgIPRange.DataKeyField = DS.Tables.Item(0).Columns(0).ToString
        dgIPRange.DataBind()
    End Sub
    Sub IPRangeDelete(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        Dim intCustomerIPRangeID As Int16
        Select Case (CType(e.CommandSource, LinkButton)).CommandName
            Case "IPRangeDelete"
                intCustomerIPRangeID = dgIPRange.DataKeys.Item(e.Item.ItemIndex)
                SQL = "delete from CustomerIPRanges where CustomerIPRangeID = '" & intCustomerIPRangeID & "'"
                WebNewsDB.GetDataSetBySQLMun(SQL)
                dgIPRange.EditItemIndex = -1
                binddgIPRange()
            Case Else
        End Select
    End Sub
    Sub cancelEdit(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        dgIPRange.EditItemIndex = -1
        binddgIPRange()
    End Sub

    Sub updateDatabase(ByVal s As Object, ByVal e As DataGridCommandEventArgs)
        Dim intCustomerIPRangeID As Int16
        Dim strIPRangeBegin As String
        Dim strIPRangeEnd As String
        intCustomerIPRangeID = dgIPRange.DataKeys.Item(e.Item.ItemIndex)
        strIPRangeBegin = (CType(e.Item.Cells(0).Controls(0), TextBox)).Text
        strIPRangeEnd = (CType(e.Item.Cells(1).Controls(0), TextBox)).Text

        SQL = "update Customers set " & _
              "ServiceBeginDate='" & strIPRangeBegin & "', " & _
              "ServiceEndDate='" & strIPRangeEnd & "' " & _
              "where CustomerID='" & lblCustomerID.Text & "'"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)

        dgIPRange.EditItemIndex = -1

        binddgIPRange()


    End Sub

    Private Sub btnIPRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIPRange.Click
        Dim strIPRangeBegin, strIPRangeEnd As String

        strIPRangeBegin = IPRangeBegin.Text
        strIPRangeEnd = IPRangeEnd.Text

        If Trim(txtCustomerName.Text) = "" Then
            Response.Write("<script>alert('기관/개인명을 입력하세요');</script>")
            Exit Sub
        End If
        If lblCustomerID.Text = "" Then
            SQL = "select max(CustomerID)+1 from Customers"
            DS = WebNewsDB.GetDataSetBySQLMun(SQL)
            lblCustomerID.Text = DS.Tables(0).Rows(0).Item(0)

            SQL = "Insert into Customers(CustomerID,CustomerName,CustomerLoginID,CustomerLoginPWD) values('" & _
                   lblCustomerID.Text & " ','" & txtCustomerName.Text & " ','" & txtCustomerLoginID.Text & "','" & txtCustomerLoginPWD.Text & "')"
            DS = WebNewsDB.GetDataSetBySQLMun(SQL)

        End If

        SQL = "Insert into CustomerIPRanges(CustomerID,IPRangeBegin,IPRangeEnd) values('" & _
               lblCustomerID.Text & " ','" & strIPRangeBegin & "','" & strIPRangeEnd & "')"
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        binddgIPRange()
        IPRangeBegin.Text = ""
        IPRangeEnd.Text = ""
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtCustomerName.Text) = "" Then
            Response.Write("<script>alert('기관/개인명을 입력하세요');</script>")
            Exit Sub
        End If
        If lblCustomerID.Text = "" Then
            SQL = "select max(CustomerID)+1 from Customers"
            DS = WebNewsDB.GetDataSetBySQLMun(SQL)
            lblCustomerID.Text = DS.Tables(0).Rows(0).Item(0)

            SQL = "Insert into Customers(CustomerID,CustomerName,CustomerLoginID,CustomerLoginPWD,License) values('" & _
                   lblCustomerID.Text & " ','" & txtCustomerName.Text & " ','" & txtCustomerLoginID.Text & "','" & txtCustomerLoginPWD.Text & "','" & ddlLicense.SelectedValue & "')"
        Else
            SQL = "Update Customers set CustomerName='" & txtCustomerName.Text & " ',CustomerLoginID='" & txtCustomerLoginID.Text & "',CustomerLoginPWD='" & txtCustomerLoginPWD.Text & "',License='" & ddlLicense.SelectedValue & "' where CustomerID=" & lblCustomerID.Text
        End If
        DS = WebNewsDB.GetDataSetBySQLMun(SQL)
        Response.Redirect("CustomerEditor.aspx?EditMode=998")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not (lblCustomerID.Text = "" Or Val(lblCustomerID.Text) = 0) Then
            Response.Redirect("AdminMessage.aspx" + _
            "?Message=" + Server.HtmlEncode("이용자정보와 허용IP범위가 영구적으로 삭제되며 복구될 수 없습니다.  해당 이용자를 정말 삭제하시겠습니까?") + _
            "&cmd=" + CStr(WebNewsConstants.ConfirmCommandDeleteCustomer) + _
            "&CustomerID=" + lblCustomerID.Text + _
            "&NavigateUrlYes=" + Server.HtmlEncode("CustomerEditor.aspx?EditMode=" + Request.QueryString("EditMode")))
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        binddata()
    End Sub
End Class
