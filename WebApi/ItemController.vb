Imports DotNetNuke.Security
Imports System
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Collections.Generic
Imports DotNetNuke.Web.Api
Imports frontdata.aj.AJ_angularModule2.Model
Imports frontdata.aj.AJ_angularModule2.Controller
' dnndev.me//desktopmodules/AJ_angularmodule2/api/item/AndreasTest
Namespace WebApi
    <SupportedModules("frontdata.aj.AJ_angularModule2")>
    Public Class ItemController
        Inherits DnnApiController

        <HttpGet>
        <AllowAnonymous>
        Public Function HelloWorld() As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.OK, "Hej Andreas module2 .net 4.6!")
        End Function

        <HttpGet>
        <AllowAnonymous>
        Public Function AndreasTest() As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.OK, "Hej AndreasTEST module2 .net 4.6!")
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        <ActionName("new")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit)>
        Public Function AddItem(ByVal item As ItemInfo) As HttpResponseMessage
            Try
                item.CreatedByUserId = UserInfo.UserID
                item.CreatedOnDate = DateTime.Now
                item.LastModifiedByUserId = UserInfo.UserID
                item.LastModifiedOnDate = DateTime.Now
                AppController.Instance.NewItem(item)
                Return Request.CreateResponse(HttpStatusCode.OK, item)
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        <ActionName("delete")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit)>
        Public Function DeleteItem(ByVal item As ItemInfo) As HttpResponseMessage
            Try
                AppController.Instance.DeleteItem(item.ItemId)
                Return Request.CreateResponse(HttpStatusCode.OK, True.ToString())
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message)
            End Try
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        <ActionName("edit")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit)>
        Public Function UpdateItem(ByVal item As ItemInfo) As HttpResponseMessage
            Try
                item.LastModifiedByUserId = UserInfo.UserID
                item.LastModifiedOnDate = DateTime.Now
                AppController.Instance.UpdateItem(item)
                Return Request.CreateResponse(HttpStatusCode.OK, True.ToString())
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpPost, HttpGet>
        <ValidateAntiForgeryToken>
        <ActionName("list")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.View)>
        Public Function GetModuleItems() As HttpResponseMessage
            Try
                Dim itemList = AppController.Instance.GetItems(ActiveModule.ModuleID)
                Return Request.CreateResponse(HttpStatusCode.OK, itemList.ToList())
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpGet>
        <ValidateAntiForgeryToken>
        <ActionName("byid")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.View)>
        Public Function GetItem(ByVal itemId As Integer) As HttpResponseMessage
            Try
                Dim item = AppController.Instance.GetItem(itemId)
                Return Request.CreateResponse(HttpStatusCode.OK, item)
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpPost, HttpGet>
        <ValidateAntiForgeryToken>
        <ActionName("reorder")>
        <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.Edit)>
        Public Function ReorderItems(ByVal sortedItems As List(Of ItemInfo)) As HttpResponseMessage
            Try

                For Each item In sortedItems
                    AppController.Instance.SetItemOrder(item.ItemId, item.Sort)
                Next

                Return Request.CreateResponse(HttpStatusCode.OK)
            Catch ex As Exception
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

    End Class
End Namespace
