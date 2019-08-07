Imports System.Data
Imports DotNetNuke.Data
Imports System.Collections.Generic
Imports frontdata.aj.AJ_angularModule2.Model

Namespace Controller
    Public Class AppController
        Private Shared _instance As AppController

        Public Shared ReadOnly Property Instance As AppController
            Get

                If _instance Is Nothing Then
                    _instance = New AppController()
                End If

                Return _instance
            End Get
        End Property

        Public Function GetItems(ByVal moduleId As Integer) As IEnumerable(Of ItemInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim sql As String = "SELECT items.*, users.username as AssignedUserName" & " FROM {databaseOwner}[{objectQualifier}Angularmodule_Items] items" & " LEFT OUTER JOIN [{objectQualifier}Users] users ON items.AssignedUserId = users.UserID" & " WHERE ModuleId = @0 ORDER BY Sort"
                Return ctx.ExecuteQuery(Of ItemInfo)(CommandType.Text, sql, moduleId)
            End Using
        End Function

        Public Function GetItem(ByVal itemId As Integer) As ItemInfo
            Dim item As ItemInfo

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep = ctx.GetRepository(Of ItemInfo)()
                item = rep.GetById(itemId)
            End Using

            Return item
        End Function

        Public Function NewItem(ByVal item As ItemInfo) As Integer
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep = ctx.GetRepository(Of ItemInfo)()
                rep.Insert(CType(item, ItemInfo))
                Return item.ItemId
            End Using
        End Function

        Public Sub UpdateItem(ByVal item As ItemInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep = ctx.GetRepository(Of ItemInfo)()
                rep.Update(CType(item, ItemInfo))
            End Using
        End Sub

        Public Sub DeleteItem(ByVal itemId As Integer)
            Using ctx As IDataContext = DataContext.Instance()
                Dim sql As String = "DELETE FROM {databaseOwner}[{objectQualifier}Angularmodule_Items] WHERE ItemId = @0"
                ctx.Execute(CommandType.Text, sql, itemId)
            End Using
        End Sub

        Public Sub SetItemOrder(ByVal itemId As Integer, ByVal sort As Integer)
            Using ctx As IDataContext = DataContext.Instance()
                Dim sql As String = "UPDATE {databaseOwner}[{objectQualifier}Angularmodule_Items] SET Sort = @1 WHERE ItemId = @0"
                ctx.Execute(CommandType.Text, sql, itemId, sort)
            End Using
        End Sub
    End Class
End Namespace
