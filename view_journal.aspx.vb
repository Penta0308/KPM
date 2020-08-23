Public Class view_journal
    Inherits System.Web.UI.Page

#Region " Web Form 디자이너에서 생성한 코드 "

    '이 호출은 Web Form 디자이너에 필요합니다.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbl_bar As System.Web.UI.WebControls.Label
    Protected WithEvents lbx_Journals As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbl_Title As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Nayong As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Writers As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_TitleEng As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Media As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Year As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Ho As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Page As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_Rugye As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_BalHengJi As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_BalHengCher As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_JungGiGanHengMulNo As System.Web.UI.WebControls.Label
    Protected WithEvents lbl_ISSN As System.Web.UI.WebControls.Label

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
    Public MediaID As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'GetTitle()
            ViewListBox()
            ViewArticle()
            CType(FindControl("Left1").FindControl("lbx_l_Journals"), ListBox).SelectedValue = MediaID
            lbx_Journals.SelectedValue = MediaID
        End If
    End Sub
    Private Sub ViewArticle()
        Dim DS As DataSet
        Dim Str_Sql As String
        Str_Sql = "SELECT JournalsArticle.JournalID, MediaName, BalHengJi, BalHengCher, BalHengJugi, SeoJiInfo, JungGiGanHengMulNo, ISSN, Title, TitleEng, Writers, Nayong1, Nayong2, BalHengYear, GwonHo, Rugye, Page, JunsongCher, FileName, Status "
        Str_Sql += " FROM JournalsArticle "
        Str_Sql += " INNER JOIN Journals ON JournalsArticle.JournalID = Journals.JournalID "
        Str_Sql += " INNER JOIN Media ON JournalsArticle.JournalID = Media.MediaID "
        Str_Sql += " where JArticleID = " + Request.QueryString("JArticleID")
        DS = WebNewsDB.GetDataSetBySQLMun(Str_Sql)

        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)

                MediaID = .Item("JournalID")

                lbl_bar.Text = .Item("MediaName")
                lbl_Title.Text = .Item("Title")

                lbl_Writers.Text = "<b>저자</b> " + .Item("Writers")
                If .Item("TitleEng") <> "" Then
                    lbl_TitleEng.Text = "| <b>영문제목</b> " + .Item("TitleEng")
                End If

                lbl_Media.Text = "| <b>출처</b> <" + .Item("MediaName") + ">"

                If .Item("BalHengYear") <> "0" Then
                    lbl_Year.Text = CStr(.Item("BalHengYear")) + "년"
                    If .Item("GwonHo") <> "0" Then
                        lbl_Ho.Text = " " + CStr(.Item("GwonHo")) + "호"
                        If .Item("Rugye") <> "0" Then
                            lbl_Rugye.Text = "(루계" + CStr(.Item("Rugye")) + "호)"
                        End If
                        If .Item("Page") <> "-" Then
                            lbl_Page.Text = .Item("Page") + "쪽"
                        End If
                    End If
                End If

                If .Item("BalHengJi") <> "" Then
                    lbl_BalHengJi.Text = "| <b>발행지</b> " + .Item("BalHengJi")
                End If
                If .Item("BalHengCher") <> "" Then
                    lbl_BalHengCher.Text = "| <b>발행처</b> " + .Item("BalHengCher")
                End If
                If .Item("JungGiGanHengMulNo") <> "" Then
                    lbl_JungGiGanHengMulNo.Text = "| <b>정기간행물번호</b> 제" + .Item("JungGiGanHengMulNo") + "호"
                End If
                If .Item("ISSN") <> "" Then
                    lbl_ISSN.Text = "| <b>ISSN</b> " + .Item("ISSN")
                End If

                If .Item("FileName") = "" Then
                    lbl_Nayong.Text = .Item("Nayong1") + .Item("Nayong2") & ""
                    lbl_Nayong.Text = lbl_Nayong.Text.Replace(Chr(13), "<br>")
                    lbl_Nayong.Text = lbl_Nayong.Text.Replace("</div><br>", "</div>")
                    lbl_Nayong.Text = lbl_Nayong.Text.Replace("  ", "&nbsp;&nbsp;")
                Else
                    lbl_Nayong.Text = "<center><a href='" + .Item("FileName") + "' target='_blank'><img src='/images/btn_orginal.gif' border='0'></a></center>"

                End If

            End With
        End If

        DS.Dispose()

    End Sub
    Private Sub ViewListBox()
        Dim DT As DataTable
        Dim i As Integer
        Dim str_sql As String
        str_sql = "SELECT MediaID, MediaName FROM Media WHERE MediaParentID=201 ORDER BY sequence"
        DT = WebNewsDB.GetDataTableBySQL(str_sql)

        If DT.Rows.Count = 0 Then
            lbx_Journals.Items.Add("없음")
            Exit Sub
        Else
            lbx_Journals.DataSource = DT
            lbx_Journals.DataBind()
        End If
        DT.Dispose()
    End Sub
    Private Sub lbx_Journals_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbx_Journals.SelectedIndexChanged
        Response.Redirect("/list_journal.aspx?MediaID=" + lbx_Journals.SelectedItem.Value)
    End Sub
End Class
