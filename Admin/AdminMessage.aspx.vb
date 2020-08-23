Public Class AdminMessage
    Inherits System.Web.UI.Page

#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnYes As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal

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
        Literal1.Text = "<span id=""lblMessage"" class=""text"" >" + Server.HtmlDecode(Request.QueryString("Message")) + "</span>"
        Select Case Request.QueryString("cmd")
            Case WebNewsConstants.ConfirmCommandDeleteArticle
                btnOn()
            Case WebNewsConstants.ConfirmCommandDeleteAd
                btnOn()
            Case WebNewsConstants.ConfirmCommandDeleteMMFile
                btnOn()
            Case WebNewsConstants.ConfirmCommandDeleteJournal
                btnOn()
            Case WebNewsConstants.ConfirmCommandDeleteCustomer
                btnOn()
            Case Else
                btnOff()
        End Select
    End Sub
    Private Sub btnOn()
        btnYes.Visible = True
        btnCancel.Visible = True
    End Sub
    Private Sub btnOff()
        btnYes.Visible = False
        btnCancel.Visible = False
    End Sub
    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Dim DS As DataSet
        Select Case Request.QueryString("cmd")
            Case WebNewsConstants.ConfirmCommandDeleteArticle
                WebNewsDB.DeleteArticle(Request.QueryString("ArticleID"))
            Case WebNewsConstants.ConfirmCommandDeleteAd
                WebNewsDB.DeleteAd(Request.QueryString("AdID"))
            Case WebNewsConstants.ConfirmCommandDeleteMMFile
                DS = WebNewsDB.GetmmFilesBymmFileID(Request.QueryString("mmFileID"))
                With DS.Tables(0).Rows(0)
                    If IO.File.Exists(Server.MapPath(.Item("Location_Thumb"))) Then
                        Kill(Server.MapPath(.Item("Location_Thumb")))
                    End If
                    If IO.File.Exists(Server.MapPath(.Item("Location_Small"))) Then
                        Kill(Server.MapPath(.Item("Location_Small")))
                    End If
                    If IO.File.Exists(Server.MapPath(.Item("Location_Large"))) Then
                        Kill(Server.MapPath(.Item("Location_Large")))
                    End If
                End With
                DS.Dispose()
                WebNewsDB.DeletemmFile(Request.QueryString("mmFileID"))
            Case WebNewsConstants.ConfirmCommandDeleteJournal
                WebNewsDB.DeleteJournal(Request.QueryString("JArticleID"))
            Case WebNewsConstants.ConfirmCommandDeleteCustomer
                WebNewsDB.GetDataSetBySQLMun("delete from CustomerIPRanges where CustomerID=" + Request.QueryString("CustomerID"))
                WebNewsDB.GetDataSetBySQLMun("delete from Customers where CustomerID=" + Request.QueryString("CustomerID"))
        End Select
        Response.Write("<script>window.location.href='" + Server.HtmlDecode(Request.QueryString("NavigateUrlYes")) + "';</script>")
    End Sub
End Class
