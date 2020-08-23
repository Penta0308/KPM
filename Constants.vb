Public Class Constants
    'SqlServer database
    Public Const WebNewsDevServer As Int16 = 1
    Public Const WebNewsTestServer As Int16 = 2
    Public Const WebNewsGoLiveServer As Int16 = 3

    Public Const AdminEditModeWriterNew As Integer = 301
    'Public Const AdminEditModeArticleAuthentify As Integer = 151
    'Public Const AdminEditModeCorrection As Integer = 201
    'Public Const AdminEditModeEditorEdit As Integer = 301
    Public Const AdminEditModeJunsong As Integer = 303
    Public Const AdminEditModeEditAfterJunsong As Integer = 901
    Public Const AdminEditModePDFUpload As Integer = 903
    Public Const AdminEditModeLayoutChange As Integer = 910
    Public Const AdminEditModeSeeAllList As Integer = 920
    Public Const AdminEditModeAsksManagement As Integer = 925
    Public Const AdminEditModeSubscriptionManagement As Integer = 926
    Public Const AdminEditModeLinkManagement As Integer = 927
    Public Const AdminEditModeAdManagement As Integer = 928
    Public Const AdminEditModeArticleDelete As Integer = 930
    Public Const AdminEditModeMenuManagement As Integer = 931
    Public Const AdminEditModeAuthChange As Integer = 999

    Public Const LanguageKorean As Integer = 101
    Public Const LanguageJapanese As Integer = 201
    Public Const LanguageEnglish As Integer = 301

    Public Const AdTypeTop As Integer = 101
    Public Const AdTypeLeft As Integer = 201
    Public Const AdTypeRight As Integer = 301
    Public Const AdTypeCenter As Integer = 401

    Public Const LayoutTop1Article As Integer = 101
    Public Const LayoutTop2Article As Integer = 102
    Public Const LayoutTopRelatedArticle As Integer = 103
    Public Const LayoutNormalArticle As Integer = 110
    Public Const LayoutEventNotice As Integer = 112
    Public Const LayoutNotice As Integer = 113
    Public Const LayoutSasul As Integer = 108
    Public Const LayoutMeari As Integer = 109
    Public Const LayoutLocalArticle As Integer = 111

    Public Const FileUploadModeImageFile As Integer = 101
    Public Const FileUploadModeDocFile As Integer = 102
    Public Const FileUploadModeMovieFile As Integer = 103

    Public Const Nayong1MaxLength As Integer = 2000

    Public Const ConfirmCommandDeleteArticle As Integer = 1
    Public Const ConfirmCommandDeleteAd As Integer = 2
    Public Const ConfirmCommandDeleteMMFile As Integer = 3
    Public Const ConfirmCommandDeleteJournal As Integer = 4
    Public Const ConfirmCommandDeleteCustomer As Integer = 5


    Public Const ArticleInitByAsks As Integer = 101
    Public Const ActionDelete As Integer = 999


End Class
