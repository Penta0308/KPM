Public Class view_article
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Section As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Local As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Title As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Nayong As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_SubTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_JunsongDateTime As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_WriterName As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Email As System.Web.UI.WebControls.Label
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents lbl_sep As System.Web.UI.WebControls.Label

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
    Public image As String
    Public str_LinkArticles As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���⿡ ����� �ڵ带 ��ġ�Ͽ� �������� �ʱ�ȭ�մϴ�.
        If Not IsPostBack Then
            ViewData()
            If str_LinkArticles <> "" Then
                bindLinkArticle()
            End If
        End If
    End Sub
    Private Sub ViewData()
        Dim DS As DataSet
        DS = WebNewsDB.GetArticleByArticleIDJoin(Request.QueryString("ArticleID"))

        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                If Not (CType(FindControl("TOP1").FindControl("hl_" + CStr(.Item("MediaID"))), HyperLink) Is Nothing) Then
                    CType(FindControl("TOP1").FindControl("hl_" + CStr(.Item("MediaID"))), HyperLink).ForeColor = Color.Yellow
                End If

                If .Item("SectionName") <> "����" Then
                    lbl_Section.Text = .Item("SectionName")
                    CType(FindControl("Left1").FindControl("hl_s_" + CStr(.Item("SectionID"))), HyperLink).ForeColor = Color.Red
                End If

                If .Item("LocalName") <> "����" Then
                    If .Item("SectionName") <> "����" Then
                        lbl_sep.Text = " | "
                    End If
                    lbl_Local.Text = .Item("LocalName")
                    CType(FindControl("Left1").FindControl("hl_l_" + CStr(.Item("LocalID"))), HyperLink).ForeColor = Color.Red
                End If

                If Not IsDBNull(DS.Tables(0).Rows(0).Item("image")) Then
                    image = DS.Tables(0).Rows(0).Item("image")
                    image = "<IMG height=""26"" src=""/images/" + image + """ width=""170"">"
                Else
                    image = DS.Tables(0).Rows(0).Item("MediaName")
                    image = "&nbsp;&nbsp;&nbsp;<b>" + image + "</b>"
                End If
                lbl_Title.Text = .Item("Title")
                lbl_SubTitle.Text = .Item("SubTitle")
                lbl_Nayong.Text = .Item("Nayong1") + .Item("Nayong2") & ""
                lbl_Nayong.Text = lbl_Nayong.Text.Replace(Chr(13), "<br>")
                lbl_Nayong.Text = lbl_Nayong.Text.Replace("</div><br>", "</div>")
                lbl_Nayong.Text = lbl_Nayong.Text.Replace("  ", "&nbsp;&nbsp;")
                If .Item("WriterName") <> "" Then
                    lbl_WriterName.Text = .Item("WriterName") + " ����"
                End If
                If .Item("Email") <> "" Then
                    lbl_Email.Text = "(" + .Item("Email") + ")"
                End If
                lbl_JunsongDateTime.Text = .Item("JunsongDateTime")

                str_LinkArticles = .Item("LinkArticles")

            End With
        End If

        DS.Dispose()
    End Sub
    Private Sub bindLinkArticle()

        Dim ArticleID() As String = Split(str_LinkArticles, ";")

        Dim i As Integer
        Dim DS As DataSet
        Dim Query As String

        Query = "select ArticleID, Title from Articles where "
        For i = 0 To ArticleID.Length - 2
            Query += "ArticleID=" + CStr(ArticleID(i))
            If i <> ArticleID.Length - 2 Then
                Query += " or "
            End If
        Next
        Query += " order by JunsongDateTime desc "

        DS = WebNewsDB.GetDataSetBySQLMun(Query)
        Repeater1.DataSource = DS.Tables(0)
        Repeater1.DataBind()
        DS.Dispose()
    End Sub
End Class
