Public Class KMBBSMgr
    Inherits System.Web.UI.Page
    Private WebNewsDB As New db
    Protected WithEvents lblAdminMenuName As System.Web.UI.WebControls.Label
    Protected WithEvents lblLanguage As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBBSSelector As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bbsFrame As System.Web.UI.HtmlControls.HtmlGenericControl
    Private WebNewsConstants As Constants
#Region " Web Form �����̳ʿ��� ������ �ڵ� "

    '�� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '����: ������ �ڸ� ǥ���� ������ Web Form �����̳��� �ʼ� �����Դϴ�.
    '�����ϰų� �ű��� ���ʽÿ�.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �� �޼��� ȣ���� Web Form �����̳ʿ� �ʿ��մϴ�.
        '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.Cookies("LoginUserID") Is Nothing Then
            Response.Redirect("AdminMain.aspx")
            Exit Sub
        End If
        If Not (Request.QueryString("EditMode") Is Nothing) Then
            If WebNewsDB.GetAuthByLoginUserIDAndAuthID(Request.Cookies("LoginUserID").Value, Request.QueryString("EditMode")) = "" Then
                Response.Write("<script>alert('�� �޴��� �������� �����ϴ�. �����ڿ��� ������ ��û�Ͻʽÿ�.');history.back(-1);</script>")
                Exit Sub
            Else
                Session("User_type") = 1
                bbsFrame.Attributes.Add("src", "bbs_list.aspx?bbsid=kmnotice")
            End If
        End If

    End Sub

    Private Sub ddlBBSSelector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBBSSelector.SelectedIndexChanged
        'Response.Write("<script>frames.item(1).location.href='';</script>")
        bbsFrame.Attributes.Add("src", "bbs_list.aspx?bbsid=" + ddlBBSSelector.SelectedValue)
    End Sub
End Class
