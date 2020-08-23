Public Class bbs_list
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Private WebNewsConstants As Constants
    Protected WithEvents ddlSelectMenu As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSearchKeyWord As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnSearch As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblBbsTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageList As System.Web.UI.WebControls.Label
    Protected WithEvents lblNone As System.Web.UI.WebControls.Label
    Protected WithEvents lblPagePre As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageNext As System.Web.UI.WebControls.Label
    Protected WithEvents lblAdmin As System.Web.UI.WebControls.Label
    Dim DS As New DataSet
#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgBbsList As System.Web.UI.WebControls.DataGrid

    '����: ������ �ڸ� ǥ���� ������ Web Form �����̳��� �ʼ� �����Դϴ�.
    '�����ϰų� �ű��� ���ʽÿ�.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �� �޼��� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
        '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
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

        If Microsoft.VisualBasic.Right(Request("bbsID"), 3) = "jpn" Then
            strStyle = "/japanese/include/main.css"
        Else
            strStyle = "/include/main.css"
        End If

        If Not IsPostBack Then
            If Request("pg") <> "" Then
                dgBbsList.CurrentPageIndex = CInt(Request("pg")) - 1
            End If
            If Request("keywordkind") <> "" Then
                ddlSelectMenu.SelectedIndex = Request("keywordkind")
            End If
            bindbbslist(HttpUtility.UrlDecode(Request("keyword")))

            If Request("bbsID") = "contact" Then
                dgBbsList.Columns(1).Visible = True
                dgBbsList.Columns(3).Visible = True
                lblAdmin.Visible = False
            End If
        End If
    End Sub
    Sub bindbbslist(Optional ByVal keyword As String = "")
        Dim query As String

        On Error GoTo err
        'Dim strTBName As String() = db.GetStringBySQLMun("select tablename from bbsTableList where bbsID='" + Request("bbsID") + "'")
        Dim strTBName As String = "bbs"

        query = "select * from " + strTBName + " where bbsid='" + Request("bbsID") + "'"

        If keyword <> "" Then
            query += " and "
            Select Case ddlSelectMenu.SelectedItem.Text
                Case "����"
                    query += "subject like '%" + keyword + "%'"
                Case "����+����"
                    query += "(subject like '%" + keyword + "%' or "
                    query += "text like '%" + keyword + "%')"
                Case "�۾���"
                    query += "(WriteUserID like '%" + keyword + "%' or "
                    query += "WriteUserName like '%" + keyword + "%')"
                Case "�۹�ȣ"
                    query += "Number=" + keyword
            End Select
        End If

        query += " order by number desc"

        DS = WebNewsDB.GetDataSetBySQLMun(query)
        lblBbsTotal.Text = DS.Tables(0).Rows.Count
        If DS.Tables(0).Rows.Count = 0 Then GoTo err

        dgBbsList.DataSource = DS.Tables(0)
        dgBbsList.DataBind()

        DS.Dispose()

        bindPageList()

        Exit Sub

err:
        lblNone.Visible = True
        DS.Dispose()
        'Response.Write("������ �ֽ��ϴ�" + vbCrLf + vbCrLf + query)


    End Sub
    Sub bindPageList()
        Dim limit As Integer = 5
        Dim temp, nPage, sPage, i, nLast As Integer
        Dim strLink As String

        strLink = "<A href='bbs_list.aspx?bbsid=" + Request("bbsID") + "&keyword=" + Request("keyword") + "&keywordkind=" + CStr(ddlSelectMenu.SelectedIndex) + "&pg="

        nLast = dgBbsList.PageCount

        If Request("pg") <> "" Then
            nPage = CInt(Request("pg"))
        Else
            nPage = 1
        End If

        temp = (nPage - 1) Mod limit
        sPage = nPage - temp

        If sPage - limit > 0 Then
            lblPagePre.Visible = True
            lblPagePre.Text = strLink + CStr(sPage - 1) + "'>" + lblPagePre.Text + "</A>"
        End If

        For i = sPage To sPage + limit - 1
            If i = nPage Then
                lblPageList.Text += "<font class=redbold>&nbsp;" + CStr(i) + "&nbsp;</font>"
            Else
                lblPageList.Text += strLink + CStr(i) + "'><font class=E4>[" + CStr(i) + "]</font></A>"
            End If

            If i >= sPage + limit - 1 Then Exit For
            If i >= nLast Then Exit For
            lblPageList.Text += "��"
        Next

        If sPage + limit <= nLast Then
            lblPageNext.Visible = True
            lblPageNext.Text = strLink + CStr(sPage + limit) + "'>" + lblPageNext.Text + "</A>"
        End If



    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        Response.Redirect("bbs_list.aspx?bbsid=" + Request("bbsID") + "&keyword=" + HttpUtility.UrlEncodeUnicode(txtSearchKeyWord.Value) + "&keywordkind=" + CStr(ddlSelectMenu.SelectedIndex))
    End Sub
End Class
