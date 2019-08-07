Imports DotNetNuke
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.UI.Utilities
Imports Newtonsoft.Json

Public Class view1
    Inherits DotNetNuke.Entities.Modules.PortalModuleBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxScriptSupport()
            DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxAntiForgerySupport()
        Catch exc As Exception
            Exceptions.ProcessModuleLoadException(Me, exc)
        End Try

    End Sub

    Protected ReadOnly Property Users As String
        Get
            Dim usersval = UserController.GetUsers(PortalId).Cast(Of UserInfo)().[Select](Function(u) New With {Key .text = u.Username, Key .id = u.UserID})
            Return ClientAPI.GetSafeJSString(JsonConvert.SerializeObject(users))
        End Get
    End Property

End Class