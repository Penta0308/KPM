Imports System.Data.SqlClient
Imports System.Text
Imports System.Configuration
Imports System.Web.Security
Public Class db
    Private Connection1 As SqlConnection
    Private strConnection1 As String

    Private WebNewsConstants As Constants

    '----------------------------------------------------------------------------------
    '연결 정보 초기화
    '작성자 : 정종영
    '작성일 : 2003년 8월 13일
    '----------------------------------------------------------------------------------
    Public Sub New()
        With WebNewsConstants
            Select Case Val(ConfigurationSettings.AppSettings("SelectedDBServer"))
                Case .WebNewsDevServer
                    strConnection1 = ConfigurationSettings.AppSettings("WebNewsDevServer")
                Case .WebNewsTestServer
                    strConnection1 = ConfigurationSettings.AppSettings("WebNewsTestServer")
                Case .WebNewsGoLiveServer
                    strConnection1 = ConfigurationSettings.AppSettings("WebNewsGoLiveServer")
            End Select
        End With

        Connection1 = New SqlConnection(strConnection1)
    End Sub

    Public Sub LogOut()
        FormsAuthentication.SignOut()
    End Sub

    '----------------------------------------------------------------------------------
    '유틸
    '작성자 : 정종영
    '작성일 : 2003년 8월 13일
    '----------------------------------------------------------------------------------
    Public Function GetDataSetBySQLMun(ByVal SQLMun As String) As DataSet
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter(SQLMun, Connection1)

        DA.Fill(DS)
        Return DS

    End Function
    Public Function GetStringBySQLMun(ByVal SQLMun As String) As String()
        Dim i As Int16
        Dim RD As SqlDataReader
        Dim strValue() As String

        Connection1.Open()
        Dim CMD As New SqlCommand(SQLMun, Connection1)

        RD = CMD.ExecuteReader()

        RD.Read()

        ReDim strValue(RD.FieldCount - 1)

        For i = 0 To RD.FieldCount - 1
            strValue(i) = RD.GetValue(i).ToString()
        Next

        Connection1.Close()

        Return strValue

    End Function
    Public Sub InitDropDownList(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataTextField As String, ByVal DataValueField As String, ByVal SQLMun As String, Optional ByVal InitFirst As Boolean = True)
        Dim DT As DataTable
        Dim i As Integer
        DT = GetDataTableBySQL(SQLMun)
        With ddlTemp
            If InitFirst Then
                .Items.Clear()
            End If
            If DT.Rows.Count = 0 Then
                ddlTemp.Items.Add("없음")
            End If
            For i = 0 To DT.Rows.Count - 1
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = DT.Rows(i).Item(DataTextField)
                aItem.Value = DT.Rows(i).Item(DataValueField)
                ddlTemp.Items.Add(aItem)
            Next
            '.DataTextField = DataTextField
            '.DataValueField = DataValueField
            '.DataSource = DT
            .DataBind()
        End With
        DT.Dispose()
    End Sub
    Public Sub InitDropDownList2(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataTextField As String, ByVal DataValueField As String, ByVal SQLMun As String, Optional ByVal DefaultValue As String = "")
        Dim DT As DataTable
        Dim i As Integer
        DT = GetDataTableBySQL(SQLMun)
        With ddlTemp
            .Items.Clear()
            If DT.Rows.Count = 0 Then
                ddlTemp.Items.Add("없음")
                Exit Sub
            End If

            If DefaultValue <> "" Then
                Dim aItem1 As New System.Web.UI.WebControls.ListItem
                aItem1.Text = DefaultValue
                aItem1.Value = "0"
                ddlTemp.Items.Add(aItem1)
            End If

            For i = 0 To DT.Rows.Count - 1
                Dim aItem As New System.Web.UI.WebControls.ListItem
                aItem.Text = DT.Rows(i).Item(DataTextField)
                aItem.Value = DT.Rows(i).Item(DataValueField)
                ddlTemp.Items.Add(aItem)
            Next
            '.DataTextField = DataTextField
            '.DataValueField = DataValueField
            '.DataSource = DT
            .DataBind()
        End With
        DT.Dispose()
    End Sub
    Public Sub SetDropDownListByValue(ByRef ddlTemp As System.Web.UI.WebControls.DropDownList, ByVal DataValue As String)
        Dim i As Integer
        With ddlTemp
            .SelectedItem.Selected = False
            For i = 0 To .Items.Count - 1
                If .Items(i).Value = DataValue Then
                    .Items(i).Selected = True
                    Exit For
                End If
            Next
        End With
    End Sub
    Public Sub SetRadioButtonListByValue(ByRef rblTemp As System.Web.UI.WebControls.RadioButtonList, ByVal DataValue As String)
        Dim i As Integer
        With rblTemp
            .SelectedItem.Selected = False
            For i = 0 To .Items.Count - 1
                If .Items(i).Value = DataValue Then
                    .Items(i).Selected = True
                    Exit For
                End If
            Next
        End With
    End Sub
    Public Sub ExecuteNonQueryBySQL(ByVal SQLMun As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.Text
            .CommandText = SQLMun
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub

    Public Function GetDataTableBySQL(ByVal SQLMun As String) As DataTable
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter(SQLMun, Connection1)
        DA.Fill(DS)
        Connection1.Close()

        Return DS.Tables(0)
    End Function

    Public Function GetAuthByLoginUserID(ByVal LoginUserID As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If

        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetAuthByLoginUserID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 20))
            .Parameters("@LoginUserID").Value = LoginUserID
        End With

        DA.Fill(DS)
        Connection1.Close()
        Return DS
    End Function
    Public Function GetAuthByLoginUserIDAndAuthID(ByVal LoginUserID As String, ByVal AuthID As Integer) As String
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spGetAuthByLoginUserIDAndAuthID"
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 20))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            GetAuthByLoginUserIDAndAuthID = .ExecuteScalar
        End With

        Connection1.Close()
    End Function

    Public Sub SiteLinksDelete(ByVal Url As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSiteLinksDelete"
            .Parameters.Add(New SqlParameter("@Url", SqlDbType.NVarChar, 200))
            .Parameters("@Url").Value = Url
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SiteLinksAdd(ByVal Url As String, ByVal SiteDescription As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSiteLinksAdd"
            .Parameters.Add(New SqlParameter("@Url", SqlDbType.NVarChar, 200))
            .Parameters("@Url").Value = Url
            .Parameters.Add(New SqlParameter("@SiteDescription", SqlDbType.NVarChar, 200))
            .Parameters("@SiteDescription").Value = SiteDescription
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub AuthDelete(ByVal LoginUserID As String, ByVal AuthID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spAuthDelete"
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 20))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub
    Public Sub AuthAdd(ByVal LoginUserID As String, ByVal AuthID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spAuthAdd"
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 20))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Function DupCheckLoginID(ByVal LoginUserID As String) As Integer
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spDupCheckLoginID"
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 14))
            .Parameters("@LoginUserID").Value = LoginUserID
            DupCheckLoginID = .ExecuteScalar
        End With
        Connection1.Close()

    End Function
    Public Function GetLoginUser(ByVal LoginUserID As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetLoginUser", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserID").Value = LoginUserID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Sub SetBBS(ByVal Command As String, ByVal BbsID As String, ByVal Number As String, ByVal Subject As String, ByVal Text As String, _
                    ByVal WriteUserID As String, ByVal WriteUserName As String, ByVal WriterIP As String, _
                    ByVal FileName As String, ByVal regdate As String, ByVal hit As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetBBS"
            .Parameters.Add(New SqlParameter("@Command", SqlDbType.NVarChar, 10))
            .Parameters("@Command").Value = Command
            .Parameters.Add(New SqlParameter("@BBSID", SqlDbType.NVarChar, 50))
            .Parameters("@BBSID").Value = BbsID
            .Parameters.Add(New SqlParameter("@Number", SqlDbType.Int))
            .Parameters("@Number").Value = Number
            .Parameters.Add(New SqlParameter("@Subject", SqlDbType.NVarChar, 50))
            .Parameters("@Subject").Value = Subject
            .Parameters.Add(New SqlParameter("@Text", SqlDbType.NText))
            .Parameters("@Text").Value = Text
            .Parameters.Add(New SqlParameter("@WriteUserID", SqlDbType.NVarChar, 50))
            .Parameters("@WriteUserID").Value = WriteUserID
            .Parameters.Add(New SqlParameter("@WriteUserName", SqlDbType.NVarChar, 50))
            .Parameters("@WriteUserName").Value = WriteUserName
            .Parameters.Add(New SqlParameter("@WriterIP", SqlDbType.Char, 15))
            .Parameters("@WriterIP").Value = WriterIP
            .Parameters.Add(New SqlParameter("@FileName", SqlDbType.NVarChar, 200))
            .Parameters("@FileName").Value = FileName
            .Parameters.Add(New SqlParameter("@regdate", SqlDbType.SmallDateTime))
            .Parameters("@regdate").Value = regdate
            .Parameters.Add(New SqlParameter("@hit", SqlDbType.Int))
            .Parameters("@hit").Value = hit
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SetLoginUser(ByVal LoginUserID As String, ByVal LoginUserName As String, ByVal LoginPassword As String, _
                            ByVal Sosok As String, ByVal Jigchek As String, ByVal Email As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetLoginUser"
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@LoginUserName", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserName").Value = LoginUserName
            .Parameters.Add(New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 10))
            .Parameters("@LoginPassword").Value = LoginPassword
            .Parameters.Add(New SqlParameter("@Sosok", SqlDbType.NVarChar, 50))
            .Parameters("@Sosok").Value = Sosok
            .Parameters.Add(New SqlParameter("@Jigchek", SqlDbType.NVarChar, 50))
            .Parameters("@Jigchek").Value = Jigchek
            .Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100))
            .Parameters("@Email").Value = Email
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub
    Public Function GetCountByLoginUserIDAndPassword(ByVal LoginUserID, ByVal LoginPassword) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If

        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetCountByLoginUserIDAndPassword", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 10))
            .Parameters("@LoginPassword").Value = LoginPassword
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetArticleCountByAuthID(ByVal LanguageID As Integer, ByVal AuthID As Integer) As Integer
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spGetArticleCountByAuthID"
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            GetArticleCountByAuthID = .ExecuteScalar
        End With
        Connection1.Close()
    End Function
    Public Function GetArticlesByAuthID(ByVal LanguageID As Integer, ByVal AuthID As Integer, ByVal DoesAuthIDRightBefore As Boolean, _
                                        ByVal InputDateTime As DateTime, ByVal SearchWord As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByAuthID", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .Parameters.Add(New SqlParameter("@DoesAuthIDRightBefore", SqlDbType.Bit))
            .Parameters("@DoesAuthIDRightBefore").Value = DoesAuthIDRightBefore
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.SmallDateTime))
            .Parameters("@InputDateTime").Value = InputDateTime
            .Parameters.Add(New SqlParameter("@SearchWord", SqlDbType.NVarChar, 100))
            .Parameters("@SearchWord").Value = SearchWord
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS

    End Function
    Public Function GetmmFilesByAuthID(ByVal AuthID As Integer, ByVal SectionID As Integer, ByVal InputDateTime As DateTime, ByVal SearchWord As String, ByVal Status As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetmmFilesByAuthID", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.SmallDateTime))
            .Parameters("@InputDateTime").Value = InputDateTime
            .Parameters.Add(New SqlParameter("@SearchWord", SqlDbType.NVarChar, 100))
            .Parameters("@SearchWord").Value = SearchWord
            .Parameters.Add(New SqlParameter("@Status", SqlDbType.Int))
            .Parameters("@Status").Value = Status
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS

    End Function
    Public Function GetJournalsArticlesByJournalID(ByVal JournalID As Integer, ByVal Status As Integer, _
                                        ByVal InputDateTime As DateTime, ByVal SearchWord As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetJournalsArticlesByJournalID", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@JournalID", SqlDbType.Int))
            .Parameters("@JournalID").Value = JournalID
            .Parameters.Add(New SqlParameter("@Status", SqlDbType.Int))
            .Parameters("@Status").Value = Status
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.SmallDateTime))
            .Parameters("@InputDateTime").Value = InputDateTime
            .Parameters.Add(New SqlParameter("@SearchWord", SqlDbType.NVarChar, 100))
            .Parameters("@SearchWord").Value = SearchWord
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS

    End Function

    Public Function GetArticlesLayout(ByVal LanguageID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesLayout", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetArticlesByArticleID(ByVal ArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByArticleID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetmmFilesBymmFileID(ByVal mmFileID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetmmFilesBymmFileID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@mmFileID", SqlDbType.Int))
            .Parameters("@mmFileID").Value = mmFileID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetJournalsArticlesByJArticleID(ByVal JArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetJournalsArticlesByJArticleID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@JArticleID", SqlDbType.Int))
            .Parameters("@JArticleID").Value = JArticleID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Sub SwitchAdRow(ByVal srcAdID As String, ByVal DestAdID As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSwitchAdRow"
            .Parameters.Add(New SqlParameter("@srcAdID", SqlDbType.Int))
            .Parameters("@srcAdID").Value = srcAdID
            .Parameters.Add(New SqlParameter("@DestAdID", SqlDbType.Int))
            .Parameters("@DestAdID").Value = DestAdID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SetAd(ByVal AdName As String, ByVal AdTypeID As Integer, ByVal LanguageID As Integer, ByVal ImageURL As String, ByVal LinkURL As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetAd"
            .Parameters.Add(New SqlParameter("@AdName", SqlDbType.NVarChar, 50))
            .Parameters("@AdName").Value = AdName
            .Parameters.Add(New SqlParameter("@AdTypeID", SqlDbType.Int))
            .Parameters("@AdTypeID").Value = AdTypeID
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@ImageURL", SqlDbType.NVarChar, 50))
            .Parameters("@ImageURL").Value = ImageURL
            .Parameters.Add(New SqlParameter("@LinkURL", SqlDbType.NVarChar, 50))
            .Parameters("@LinkURL").Value = LinkURL
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Function GetBannerByLanguageIDAndAdTypeID(ByVal LanguageID As Integer, ByVal AdTypeID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetBannerByLanguageIDAndAdTypeID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@AdTypeID", SqlDbType.Int))
            .Parameters("@AdTypeID").Value = AdTypeID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetArticleByLanguageIDAndLayoutID(ByVal LanguageID As Integer, ByVal LayoutID As Integer, ByVal IsPreviewMode As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticleByLanguageIDAndLayoutID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@LayoutID", SqlDbType.Int))
            .Parameters("@LayoutID").Value = LayoutID
            .Parameters.Add(New SqlParameter("@IsPreviewMode", SqlDbType.NVarChar, 10))
            .Parameters("@IsPreviewMode").Value = IsPreviewMode
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetArticlesByArticleIDForService(ByVal ArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByArticleIDForService", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetArticlesBySectionID(ByVal SectionID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesBySectionID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetArticlesByJunsongDateTime2(ByVal BeginDateTime As DateTime, ByVal EndDateTime As DateTime) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetSearchResult", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@BeginDateTime", SqlDbType.SmallDateTime))
            .Parameters("@BeginDateTime").Value = BeginDateTime
            .Parameters.Add(New SqlParameter("@EndDateTime", SqlDbType.SmallDateTime))
            .Parameters("@EndDateTime").Value = EndDateTime
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetLastUpdated() As DateTime
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spGetLastUpdated"

            GetLastUpdated = .ExecuteScalar

        End With
        Connection1.Close()
    End Function
    Public Sub DeleteAd(ByVal AdID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spDeleteAd"
            .Parameters.Add(New SqlParameter("@AdID", SqlDbType.Int))
            .Parameters("@AdID").Value = AdID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub DeleteArticle(ByVal ArticleID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spDeleteArticle"
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub DeletemmFile(ByVal mmFileID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spDeleteMMFile"
            .Parameters.Add(New SqlParameter("@mmFileID", SqlDbType.Int))
            .Parameters("@mmFileID").Value = mmFileID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub DeleteJournal(ByVal JArticleID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spDeleteJournal"
            .Parameters.Add(New SqlParameter("@JArticleID", SqlDbType.Int))
            .Parameters("@JArticleID").Value = JArticleID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SetAsks(ByVal AskID As Integer, ByVal AskerName As String, ByVal Sosok As String, ByVal Email As String, ByVal Nayong As String, ByVal ETCStatusID As Integer, Optional ByVal LoginUserID As String = "")
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetAsks"
            .Parameters.Add(New SqlParameter("@AskID", SqlDbType.Int))
            .Parameters("@AskID").Value = AskID
            .Parameters.Add(New SqlParameter("@AskerName", SqlDbType.NVarChar, 50))
            .Parameters("@AskerName").Value = AskerName
            .Parameters.Add(New SqlParameter("@Sosok", SqlDbType.NVarChar, 50))
            .Parameters("@Sosok").Value = Sosok
            .Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 50))
            .Parameters("@Email").Value = Email
            .Parameters.Add(New SqlParameter("@Nayong", SqlDbType.NText))
            .Parameters("@Nayong").Value = Nayong
            .Parameters.Add(New SqlParameter("@ETCStatusID", SqlDbType.Int))
            .Parameters("@ETCStatusID").Value = ETCStatusID
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserID").Value = LoginUserID
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub
    Public Sub SetSubscriptions(ByVal SubscriptionID As Integer, ByVal IsPersonal As Boolean, ByVal SubscriberName As String, ByVal SubscriberAddress As String, ByVal SubscriberTelNo As String, ByVal Nayong As String, ByVal ETCStatusID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetSubscriptions"
            .Parameters.Add(New SqlParameter("@SubscriptionID", SqlDbType.Int))
            .Parameters("@SubscriptionID").Value = SubscriptionID
            .Parameters.Add(New SqlParameter("@IsPersonal", SqlDbType.Bit))
            .Parameters("@IsPersonal").Value = IsPersonal
            .Parameters.Add(New SqlParameter("@SubscriberName", SqlDbType.NVarChar, 50))
            .Parameters("@SubscriberName").Value = SubscriberName
            .Parameters.Add(New SqlParameter("@SubscriberAddress", SqlDbType.NVarChar, 50))
            .Parameters("@SubscriberAddress").Value = SubscriberAddress
            .Parameters.Add(New SqlParameter("@SubscriberTelNo", SqlDbType.NVarChar, 50))
            .Parameters("@SubscriberTelNo").Value = SubscriberTelNo
            .Parameters.Add(New SqlParameter("@Nayong", SqlDbType.NText))
            .Parameters("@Nayong").Value = Nayong
            .Parameters.Add(New SqlParameter("@ETCStatusID", SqlDbType.Int))
            .Parameters("@ETCStatusID").Value = ETCStatusID
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub
    Public Sub SetPDFs(ByVal BalhengDate As DateTime, ByVal Myun As Integer, ByVal SubNayong As String, ByVal FileLocation As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetPDFs"
            .Parameters.Add(New SqlParameter("@BalhengDate", SqlDbType.SmallDateTime))
            .Parameters("@BalhengDate").Value = BalhengDate
            .Parameters.Add(New SqlParameter("@Myun", SqlDbType.NVarChar, 50))
            .Parameters("@Myun").Value = Myun
            .Parameters.Add(New SqlParameter("@SubNayong", SqlDbType.NVarChar, 200))
            .Parameters("@SubNayong").Value = SubNayong
            .Parameters.Add(New SqlParameter("@FileLocation", SqlDbType.NVarChar, 200))
            .Parameters("@FileLocation").Value = FileLocation
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub
    Public Function GetPDFsByBalhengDate(ByVal BalhengDate As DateTime) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetPDFsByBalhengDate", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@BalhengDate", SqlDbType.SmallDateTime))
            .Parameters("@BalhengDate").Value = BalhengDate
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Function GetTopArticleImageSet(ByVal ArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetTopArticleImageSet", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetSections() As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetSections", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function

    Public Sub SetIsTheme(ByVal SectionID As Integer, ByVal IsTheme As Boolean)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetIsTheme"
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
            .Parameters.Add(New SqlParameter("@IsTheme", SqlDbType.Bit))
            .Parameters("@IsTheme").Value = IsTheme
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub

    Public Sub SetSections(ByVal SectionName As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetSections"
            .Parameters.Add(New SqlParameter("@SectionName", SqlDbType.NVarChar, 50))
            .Parameters("@SectionName").Value = SectionName
            .ExecuteNonQuery()
        End With
        Connection1.Close()

    End Sub

    Public Function GetFilesByFileLocation(ByVal FileLocation As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetFilesByFileLocation", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@FileLocation", SqlDbType.NVarChar, 200))
            .Parameters("@FileLocation").Value = FileLocation
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
    Public Function GetFilesByArticleID(ByVal ArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetFilesByArticleID", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function


    Public Sub SetFiles(ByVal FileLocation As String, ByVal ArticleID As Integer, ByVal FileCaption As String, ByVal FileTypeID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetFiles"
            .Parameters.Add(New SqlParameter("@FileLocation", SqlDbType.NVarChar, 200))
            .Parameters("@FileLocation").Value = FileLocation
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
            .Parameters.Add(New SqlParameter("@FileCaption", SqlDbType.NVarChar, 100))
            .Parameters("@FileCaption").Value = FileCaption
            .Parameters.Add(New SqlParameter("@FileTypeID", SqlDbType.Int))
            .Parameters("@FileTypeID").Value = FileTypeID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SetArticleSub(ByVal ArticleID As Integer, ByVal FirstImageFileLocation As String, ByVal FirstImageFileCaption As String)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetArticleSub"
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
            .Parameters.Add(New SqlParameter("@FirstImageFileCaption", SqlDbType.NVarChar, 100))
            .Parameters("@FirstImageFileCaption").Value = FirstImageFileCaption
            .Parameters.Add(New SqlParameter("@FirstImageFileLocation", SqlDbType.NVarChar, 200))
            .Parameters("@FirstImageFileLocation").Value = FirstImageFileLocation
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Function SetArticle(ByVal ArticleID As Integer, ByVal WriterName As String, ByVal Email As String, ByVal LanguageID As Integer, ByVal Title As String, ByVal SubTitle As String, ByVal SubNayong As String, _
                               ByVal Nayong As String, ByVal SectionID As Integer, ByVal AuthID As Integer, ByVal LoginUserID As String, ByVal InputDateTime As DateTime, ByVal JunsongDateTime As DateTime, ByVal MediaID As Integer, ByVal LocalID As Integer, ByVal Importance As Integer, ByVal LinkArticles As String, ByVal chkPhoto As String) As Integer
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetArticle"
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
            .Parameters.Add(New SqlParameter("@WriterName", SqlDbType.NVarChar, 200))
            .Parameters("@WriterName").Value = WriterName
            .Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 200))
            .Parameters("@Email").Value = Email
            .Parameters.Add(New SqlParameter("@LoginUserID", SqlDbType.NVarChar, 50))
            .Parameters("@LoginUserID").Value = LoginUserID
            .Parameters.Add(New SqlParameter("@Title", SqlDbType.NVarChar, 200))
            .Parameters("@Title").Value = Title
            .Parameters.Add(New SqlParameter("@SubTitle", SqlDbType.NVarChar, 200))
            .Parameters("@SubTitle").Value = SubTitle
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
            .Parameters.Add(New SqlParameter("@SubNayong", SqlDbType.NVarChar, 200))
            .Parameters("@SubNayong").Value = SubNayong
            If Len(Nayong) > WebNewsConstants.Nayong1MaxLength Then
                .Parameters.Add(New SqlParameter("@Nayong1", SqlDbType.NVarChar, WebNewsConstants.Nayong1MaxLength))
                .Parameters("@Nayong1").Value = Microsoft.VisualBasic.Left(Nayong, WebNewsConstants.Nayong1MaxLength)
                .Parameters.Add(New SqlParameter("@Nayong2", SqlDbType.NText))
                .Parameters("@Nayong2").Value = Microsoft.VisualBasic.Right(Nayong, Len(Nayong) - WebNewsConstants.Nayong1MaxLength)
            Else
                .Parameters.Add(New SqlParameter("@Nayong1", SqlDbType.NVarChar, WebNewsConstants.Nayong1MaxLength))
                .Parameters("@Nayong1").Value = Nayong
                .Parameters.Add(New SqlParameter("@Nayong2", SqlDbType.NText))
                .Parameters("@Nayong2").Value = ""
            End If
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.DateTime))
            .Parameters("@InputDateTime").Value = InputDateTime
            .Parameters.Add(New SqlParameter("@JunsongDateTime", SqlDbType.DateTime))
            .Parameters("@JunsongDateTime").Value = JunsongDateTime
            .Parameters.Add(New SqlParameter("@MediaID", SqlDbType.Int))
            .Parameters("@MediaID").Value = MediaID
            .Parameters.Add(New SqlParameter("@LocalID", SqlDbType.Int))
            .Parameters("@LocalID").Value = LocalID
            .Parameters.Add(New SqlParameter("@Importance", SqlDbType.Int))
            .Parameters("@Importance").Value = Importance
            .Parameters.Add(New SqlParameter("@LinkArticles", SqlDbType.NVarChar, 60))
            .Parameters("@LinkArticles").Value = LinkArticles
            .Parameters.Add(New SqlParameter("@chkPhoto", SqlDbType.NVarChar, 60))
            .Parameters("@chkPhoto").Value = chkPhoto
            Return .ExecuteScalar
        End With
        Connection1.Close()
    End Function
    Public Function SetJournalArticle(ByVal Command As String, ByVal JArticleID As Integer, ByVal Title As String, _
                    ByVal TitleEng As String, ByVal Writers As String, ByVal Nayong As String, _
                    ByVal JournalID As Integer, ByVal BalHengYear As Integer, ByVal GwonHo As Integer, _
                    ByVal Rugye As String, ByVal Page As String, ByVal JunSongCher As String, _
                    ByVal JunSongDate As String, ByVal FileName As String, ByVal FileSize As String, _
                    ByVal Status As String, ByVal InputDateTime As DateTime) As Integer


        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetJournalArticle"
            .Parameters.Add(New SqlParameter("@Command", SqlDbType.NVarChar, 10))
            .Parameters("@Command").Value = Command
            .Parameters.Add(New SqlParameter("@JArticleID", SqlDbType.Int))
            .Parameters("@JArticleID").Value = JArticleID
            .Parameters.Add(New SqlParameter("@Title", SqlDbType.NVarChar, 500))
            .Parameters("@Title").Value = Title
            .Parameters.Add(New SqlParameter("@TitleEng", SqlDbType.NVarChar, 500))
            .Parameters("@TitleEng").Value = TitleEng
            .Parameters.Add(New SqlParameter("@Writers", SqlDbType.NVarChar, 110))
            .Parameters("@Writers").Value = Writers
            If Len(Nayong) > WebNewsConstants.Nayong1MaxLength Then
                .Parameters.Add(New SqlParameter("@Nayong1", SqlDbType.NVarChar, WebNewsConstants.Nayong1MaxLength))
                .Parameters("@Nayong1").Value = Microsoft.VisualBasic.Left(Nayong, WebNewsConstants.Nayong1MaxLength)
                .Parameters.Add(New SqlParameter("@Nayong2", SqlDbType.NText))
                .Parameters("@Nayong2").Value = Microsoft.VisualBasic.Right(Nayong, Len(Nayong) - WebNewsConstants.Nayong1MaxLength)
            Else
                .Parameters.Add(New SqlParameter("@Nayong1", SqlDbType.NVarChar, WebNewsConstants.Nayong1MaxLength))
                .Parameters("@Nayong1").Value = Nayong
                .Parameters.Add(New SqlParameter("@Nayong2", SqlDbType.NText))
                .Parameters("@Nayong2").Value = ""
            End If
            .Parameters.Add(New SqlParameter("@JournalID", SqlDbType.Int))
            .Parameters("@JournalID").Value = JournalID
            .Parameters.Add(New SqlParameter("@BalHengYear", SqlDbType.Int))
            .Parameters("@BalHengYear").Value = BalHengYear
            .Parameters.Add(New SqlParameter("@GwonHo", SqlDbType.Int))
            .Parameters("@GwonHo").Value = GwonHo
            .Parameters.Add(New SqlParameter("@Rugye", SqlDbType.Int))
            .Parameters("@Rugye").Value = Rugye
            .Parameters.Add(New SqlParameter("@Page", SqlDbType.VarChar, 50))
            .Parameters("@Page").Value = Page
            .Parameters.Add(New SqlParameter("@JunSongCher", SqlDbType.NVarChar, 50))
            .Parameters("@JunSongCher").Value = JunSongCher
            .Parameters.Add(New SqlParameter("@JunSongDate", SqlDbType.NVarChar, 50))
            .Parameters("@JunSongDate").Value = JunSongDate
            .Parameters.Add(New SqlParameter("@FileName", SqlDbType.NVarChar, 50))
            .Parameters("@FileName").Value = FileName
            .Parameters.Add(New SqlParameter("@FileSize", SqlDbType.NVarChar, 50))
            .Parameters("@FileSize").Value = FileSize
            .Parameters.Add(New SqlParameter("@Status", SqlDbType.Char, 1))
            .Parameters("@Status").Value = Status
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.DateTime))
            .Parameters("@InputDateTime").Value = InputDateTime

            Return .ExecuteScalar
        End With
        Connection1.Close()
    End Function
    Public Function SetmmFile(ByVal Command As String, ByVal mmFileID As Integer, ByVal AuthID As Integer, ByVal SectionID As Integer, ByVal Title As String, ByVal TitleJpn As String, ByVal TitleEng As String, ByVal Caption As String, ByVal CaptionJpn As String, ByVal CaptionEng As String, _
                         ByVal Location_Thumb As String, ByVal Location_Small As String, ByVal Location_Large As String, ByVal FileSize As Integer, ByVal Status As String, ByVal inputDateTime As DateTime) As Integer
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetmmFile"
            .Parameters.Add(New SqlParameter("@Command", SqlDbType.NVarChar, 10))
            .Parameters("@Command").Value = Command
            .Parameters.Add(New SqlParameter("@mmFileID", SqlDbType.Int))
            .Parameters("@mmFileID").Value = mmFileID
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
            .Parameters.Add(New SqlParameter("@title", SqlDbType.NVarChar, 200))
            .Parameters("@title").Value = Title
            .Parameters.Add(New SqlParameter("@titleJpn", SqlDbType.NVarChar, 200))
            .Parameters("@titleJpn").Value = TitleJpn
            .Parameters.Add(New SqlParameter("@titleEng", SqlDbType.NVarChar, 200))
            .Parameters("@titleEng").Value = TitleEng
            .Parameters.Add(New SqlParameter("@Caption", SqlDbType.NVarChar, 2000))
            .Parameters("@Caption").Value = Caption
            .Parameters.Add(New SqlParameter("@CaptionJpn", SqlDbType.NVarChar, 2000))
            .Parameters("@CaptionJpn").Value = CaptionJpn
            .Parameters.Add(New SqlParameter("@CaptionEng", SqlDbType.NVarChar, 2000))
            .Parameters("@CaptionEng").Value = CaptionEng
            .Parameters.Add(New SqlParameter("@Location_Thumb", SqlDbType.NVarChar, 200))
            .Parameters("@Location_Thumb").Value = Location_Thumb
            .Parameters.Add(New SqlParameter("@Location_Small", SqlDbType.NVarChar, 200))
            .Parameters("@Location_Small").Value = Location_Small
            .Parameters.Add(New SqlParameter("@Location_Large", SqlDbType.NVarChar, 200))
            .Parameters("@Location_Large").Value = Location_Large
            .Parameters.Add(New SqlParameter("@FileSize", SqlDbType.Int))
            .Parameters("@FileSize").Value = FileSize
            .Parameters.Add(New SqlParameter("@Status", SqlDbType.Char, 1))
            .Parameters("@Status").Value = Status
            .Parameters.Add(New SqlParameter("@InputDateTime", SqlDbType.DateTime))
            .Parameters("@InputDateTime").Value = inputDateTime
            Return .ExecuteScalar
        End With
        Connection1.Close()
    End Function
    Public Function SetJournalName(ByVal Command As String, ByVal JournalID As Integer, ByVal JournalName As String, _
                         ByVal BalHengCher As String, ByVal BalHengJi As String, _
                         ByVal JungGiGanHengMulNo As String, ByVal ISSN As String) As Integer
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetJournalName"
            .Parameters.Add(New SqlParameter("@Command", SqlDbType.NVarChar, 10))
            .Parameters("@Command").Value = Command
            .Parameters.Add(New SqlParameter("@JournalID", SqlDbType.Int))
            .Parameters("@JournalID").Value = JournalID
            .Parameters.Add(New SqlParameter("@JournalName", SqlDbType.NVarChar, 50))
            .Parameters("@JournalName").Value = JournalName
            .Parameters.Add(New SqlParameter("@BalHengCher", SqlDbType.NVarChar, 50))
            .Parameters("@BalHengCher").Value = BalHengCher
            .Parameters.Add(New SqlParameter("@BalHengJi", SqlDbType.NVarChar, 50))
            .Parameters("@BalHengJi").Value = BalHengJi
            .Parameters.Add(New SqlParameter("@JungGiGanHengMulNo", SqlDbType.NVarChar, 50))
            .Parameters("@JungGiGanHengMulNo").Value = JungGiGanHengMulNo
            .Parameters.Add(New SqlParameter("@ISSN", SqlDbType.NVarChar, 50))
            .Parameters("@ISSN").Value = ISSN
            Return .ExecuteScalar
        End With
        Connection1.Close()
    End Function
    Public Sub SetLayout(ByVal ArticleID As Integer, ByVal LanguageID As Integer, ByVal LayoutID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetLayout"
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .Parameters.Add(New SqlParameter("@LayoutID", SqlDbType.Int))
            .Parameters("@LayoutID").Value = LayoutID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    Public Sub SetLayoutPreview2Layout(ByVal LanguageID As Integer)
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim comm As New SqlCommand
        With comm
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .CommandText = "spSetLayoutPreview2Layout"
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
            .ExecuteNonQuery()
        End With
        Connection1.Close()
    End Sub
    ' 이재욱 사용자 페이지용
    Public Function GetArticlesByMedia(ByVal MediaID As Integer, ByVal chk_lim As Integer, ByVal no_lim As Integer, ByVal LanguageID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByMediaID", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@MediaID", SqlDbType.Int))
            .Parameters("@MediaID").Value = MediaID
            .Parameters.Add(New SqlParameter("@chk_lim", SqlDbType.Int))
            .Parameters("@chk_lim").Value = chk_lim
            .Parameters.Add(New SqlParameter("@no_lim", SqlDbType.Int))
            .Parameters("@no_lim").Value = no_lim
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS
    End Function
    Public Function GetArticlesByETC(ByVal ID As Integer, ByVal chk_etc As String, ByVal lvl As String, ByVal no_lim As Integer, ByVal LanguageID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByETC", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))
            .Parameters("@ID").Value = ID
            .Parameters.Add(New SqlParameter("@chk_etc", SqlDbType.VarChar, 20))
            .Parameters("@chk_etc").Value = chk_etc
            .Parameters.Add(New SqlParameter("@lvl", SqlDbType.Char, 1))
            .Parameters("@lvl").Value = lvl
            .Parameters.Add(New SqlParameter("@no_lim", SqlDbType.Int))
            .Parameters("@no_lim").Value = no_lim
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS
    End Function
    Public Function GetArticleByArticleIDJoin(ByVal ArticleID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticleByArticleIDJoin", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@ArticleID", SqlDbType.Int))
            .Parameters("@ArticleID").Value = ArticleID
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS
    End Function
    Public Function FGetmmFilesByAuthID(ByVal AuthID As Integer, ByVal SectionID As Integer, ByVal Status As String) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("FspGetmmFilesByAuthID", Connection1)
        With DA.SelectCommand
            .Connection = Connection1
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@AuthID", SqlDbType.Int))
            .Parameters("@AuthID").Value = AuthID
            .Parameters.Add(New SqlParameter("@SectionID", SqlDbType.Int))
            .Parameters("@SectionID").Value = SectionID
            .Parameters.Add(New SqlParameter("@Status", SqlDbType.Char, 1))
            .Parameters("@Status").Value = Status
        End With
        DA.Fill(DS)
        Connection1.Close()
        Return DS
    End Function
    Public Function GetArticlesByJunsongDateTime(ByVal BeginDateTime As DateTime, ByVal EndDateTime As DateTime, ByVal MediaID As Integer, ByVal LanguageID As Integer) As DataSet
        If Not (Connection1.State = ConnectionState.Open) Then
            Connection1.Open()
        End If
        Dim DS As New DataSet
        Dim DA As New SqlDataAdapter("spGetArticlesByJunsongDateTime", Connection1)
        With DA.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@BeginDateTime", SqlDbType.SmallDateTime))
            .Parameters("@BeginDateTime").Value = BeginDateTime
            .Parameters.Add(New SqlParameter("@EndDateTime", SqlDbType.SmallDateTime))
            .Parameters("@EndDateTime").Value = EndDateTime
            .Parameters.Add(New SqlParameter("@MediaID", SqlDbType.Int))
            .Parameters("@MediaID").Value = MediaID
            .Parameters.Add(New SqlParameter("@LanguageID", SqlDbType.Int))
            .Parameters("@LanguageID").Value = LanguageID
        End With
        DA.Fill(DS)

        Connection1.Close()
        Return DS
    End Function
End Class
