Imports DotNetNuke.Web.Api

Namespace WebApi
    Public Class RouteMapper
        Implements IServiceRouteMapper

        Public Sub RegisterRoutes(ByVal routeManager As IMapRoute) Implements IServiceRouteMapper.RegisterRoutes
            routeManager.MapHttpRoute("Aj_Angularmodule2", "default", "{controller}/{action}", {"frontdata.aj.AJ_angularModule2.WebApi"})
        End Sub
    End Class
End Namespace
